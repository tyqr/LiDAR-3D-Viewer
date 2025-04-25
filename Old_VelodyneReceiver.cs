using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Concurrent;
using UnityEngine;

public class VelodyneReceiver : MonoBehaviour
{
    UdpClient udpClient;
    Thread receiveThread;
    int port = 2368;

    [Header("Point Pooling")]
    public const int maxPoints = 16384; // 16 KB (2^14)
    public GameObject pointPrefab;
    private GameObject[] pointPool;
    private int poolIndex = 0;

    [Header("LIDAR Settings")]
    public float scale = 10f;

    private readonly float[] verticalAngles = new float[16] {
        -15, 1, -13, 3, -11, 5, -9, 7,
        -7, 9, -5, 11, -3, 13, -1, 15
    };

    private ConcurrentQueue<(Vector3 pos, byte intensity)> pointQueue = new ConcurrentQueue<(Vector3, byte)>();

    void Start()
    {
        if (pointPrefab == null)
        {
            pointPrefab = Resources.Load<GameObject>("PointCube");
            if (pointPrefab == null)
            {
                Debug.LogError("Prefab not found in Resources folder.");
                return;
            }
        }

        InitPointPool();

        udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, 2368));
        receiveThread = new Thread(ReceiveData) { IsBackground = true };
        receiveThread.Start();
    }

    void InitPointPool()
    {
        pointPool = new GameObject[maxPoints];
        for (int i = 0; i < maxPoints; i++)
        {
            var p = Instantiate(pointPrefab);
            p.transform.SetParent(transform);
            p.SetActive(false);
            pointPool[i] = p;
        }
    }

    void ReceiveData()
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);

        while (true)
        {
            try
            {
                byte[] data = udpClient.Receive(ref remoteEndPoint);
                if (data.Length == 1206)
                    ParsePacket(data);
            }
            catch (SocketException) { break; }
            catch (Exception ex)
            {
                Debug.LogError("UDP Receive Error: " + ex.Message);
            }
        }
    }

    void ParsePacket(byte[] data)
    {
        const int blocks = 12;
        const int blockSize = 100;

        for (int block = 0; block < blocks; block++)
        {
            int offset = block * blockSize;
            // ushort flag = (ushort)(data[offset] | (data[offset + 1] << 8));
            ushort flag = BitConverter.ToUInt16(data, offset);
            if (flag != 0xEEFF) continue;

            // ushort azimuth = (ushort)(data[offset + 2] | (data[offset + 3] << 8));
            ushort azimuth = BitConverter.ToUInt16(data, offset + 2);
            float azimuthRad = (azimuth / 100f) * Mathf.Deg2Rad;

            for (int ch = 0; ch < 16; ch++)
            {
                int chOffset = offset + 4 + ch * 3;
                // ushort rawDist = (ushort)(data[chOffset] | (data[chOffset + 1] << 8));
                ushort rawDist = BitConverter.ToUInt16(data, chOffset);
                float dist = rawDist * 0.002f;
                byte intensity = data[chOffset + 2];
                float vAngleRad = verticalAngles[ch] * Mathf.Deg2Rad;

                Vector3 pos = new Vector3(
                    dist * Mathf.Cos(vAngleRad) * Mathf.Sin(azimuthRad) * scale,
                    dist * Mathf.Sin(vAngleRad) * scale,
                    dist * Mathf.Cos(vAngleRad) * Mathf.Cos(azimuthRad) * scale
                );

                pointQueue.Enqueue((pos, intensity));
            }
        }
    }

    void Update()
    {
        const int frameDrawLimit = 4096;
        int drawn = 0;

        while (pointQueue.TryDequeue(out var point) && drawn < frameDrawLimit)
        {
            DrawPoint(point.pos, point.intensity);
            drawn++;
        }
    }

    void DrawPoint(Vector3 position, byte intensity)
    {
        if (pointPool.Length == 0) return;

        GameObject p = pointPool[poolIndex];
        p.transform.position = position;
        if (!p.activeSelf) p.SetActive(true);

        float distance = position.magnitude / scale;

        float normalized = Mathf.Clamp01(Mathf.InverseLerp(0f, 5f, distance));
        float hue = Mathf.Lerp(0f, 0.66f, normalized); // red → blue
        Color color = Color.HSVToRGB(hue, 1f, 1f);

        Renderer renderer = p.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material mat = renderer.material;
            mat.color = color;

            // Opaque mode
            mat.SetFloat("_Mode", 0);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            mat.SetInt("_ZWrite", 1);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.DisableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = -1;
        }

        poolIndex = (poolIndex + 1) % pointPool.Length;
    }

    void OnApplicationQuit()
    {
        try { receiveThread?.Abort(); } catch { }
        udpClient?.Close();
    }
}

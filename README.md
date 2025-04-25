README - LiDAR 3D Real-Time Viewer

Program Name: My project.exe  
Platform: Windows  
Requires: LiDAR device (e.g., Velodyne VLP-16) connected via Ethernet

------------------------------------------------------------
ABOUT
------------------------------------------------------------
This program is a real-time 3D viewer for LiDAR data built using Unity.
It receives live data from a LiDAR sensor and displays each point
in 3D space with color based on distance.

You can move the camera using your keyboard and mouse 
to freely explore the point cloud environment.

------------------------------------------------------------
DOWNLOAD
------------------------------------------------------------
Unity Project (.zip):
https://drive.google.com/file/d/1wGSz5d9YD5wqmN5V_hdUeKta4_6g5oM1/view?usp=drive_link

Executable Project (.zip):
https://drive.google.com/file/d/1OZdVufH3vhUGqbv7pSywRoRcPUtp18Pt/view?usp=drive_link

------------------------------------------------------------
HOW TO USE
------------------------------------------------------------
1. Download the Executable Project
2. Connect the LiDAR sensor directly to your computer via a LAN cable.
3. Set your computer’s IP address to a valid one in the same network
   as the LiDAR (e.g., 192.168.1.100).
4. Make sure the LiDAR is configured to send UDP packets to:
   - Destination IP: your computer’s IP
   - Port: 2368
5. Run the program by opening:
   My project.exe

------------------------------------------------------------
CONTROLS
------------------------------------------------------------
W / A / S / D   - Move forward, left, back, right  
Q / E           - Move up, down  
Mouse           - Look around  
ESC             - Unlock the mouse cursor

------------------------------------------------------------
NOTES
------------------------------------------------------------
- The program handles approximately 32,000 points per second.
- It uses a circular queue to handle high-speed data efficiently and avoid lag.
- Built with Unity, optimized for real-time performance and stability.

------------------------------------------------------------
TROUBLESHOOTING
------------------------------------------------------------
- If nothing is visualized: make sure the LiDAR is sending data to your computer’s IP on port 2368.
- If the program crashes or lags: check your network setup and try restarting the program.
- If still no data: use Wireshark to confirm UDP packets are arriving on port 2368.

------------------------------------------------------------
CREDITS
------------------------------------------------------------
Project by: Moobaankonoha  
Built using Unity 2021.3.4f1 and C#

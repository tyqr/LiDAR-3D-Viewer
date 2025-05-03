README - LiDAR 3D Real-Time Viewer

Program Name: My project.exe  
Platform: Windows  
Requires: LiDAR device (e.g., Velodyne VLP-16) connected via Ethernet

------------------------------------------------------------

# ABOUT

------------------------------------------------------------
This program is a real-time 3D viewer for LiDAR data built using Unity.
It receives live data from a LiDAR sensor and displays each point
in 3D space, with color based on distance.

You can move the camera using your keyboard and mouse 
to freely explore the point cloud environment.

Sample data: lidar_raw.txt , lidar_raw.bin
You can open lidar_raw.txt in any text editor.
You can open lidar_raw.bin in Wireshark, MATLAB, Python, etc.

------------------------------------------------------------

# DOWNLOAD

------------------------------------------------------------

## Unity Project (.zip):

https://drive.google.com/file/d/1wGSz5d9YD5wqmN5V_hdUeKta4_6g5oM1/view?usp=drive_link

Two C# files provided in the GitHub repository are versions of `VelodyneReceiver.cs`  
that you can apply to the Unity project to compare before and after fixing the issues.

## Executable Project (.zip):

Before fixing problems:  
https://drive.google.com/file/d/11vKqZ1Vc_8oYaNHVz6Sw6BL8Yz6easHr/view?usp=drive_link

## After fixing problems:

https://drive.google.com/file/d/1OZdVufH3vhUGqbv7pSywRoRcPUtp18Pt/view?usp=drive_link

## Raw Data writer:

( the sample should be at %userprofile%\AppData\LocalLow\DefaultCompany\My project )
https://drive.google.com/file/d/12W3k_JYdVvyiMif05pVEFEgy694I_3Sj/view?usp=drive_link

------------------------------------------------------------

# HOW TO USE

------------------------------------------------------------
1. Download the executable project (.zip).
2. Unzip the downloaded file to any folder on your computer.
3. Connect the LiDAR sensor directly to your computer via a LAN cable.
4. Set your computer’s IP address to any valid value in the same network
   as the LiDAR (e.g., 192.168.1.100).
5. Make sure the LiDAR is configured to send UDP packets to:
   - Destination IP: your computer’s IP
   - Port: 2368
6. Run the program by opening:
   My project.exe


------------------------------------------------------------

# CONTROLS

------------------------------------------------------------
W / A / S / D   - Move forward, left, back, right  
Q / E           - Move up, down  
Mouse           - Look around  
ESC             - Unlock the mouse cursor

------------------------------------------------------------

# NOTES

------------------------------------------------------------
- The program handles approximately 32,000 points per second.
- It uses a circular queue to process high-speed data efficiently and avoid lag.
- Built with Unity and optimized for real-time performance and stability.

------------------------------------------------------------

# TROUBLESHOOTING

------------------------------------------------------------
- If nothing is visualized: make sure the LiDAR is sending data to your current IP on port 2368.
- If the program crashes or freezes: check your network setup and try restarting the program.
- Still no data? Use Wireshark to confirm UDP packets are arriving on port 2368.

------------------------------------------------------------

# CREDITS

------------------------------------------------------------
Project by: Moobaankonoha  
Built using Unity 2021.3.4f1 and C#

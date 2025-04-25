README - LiDAR 3D Real-Time Viewer

Program Name: My project.exe  
Platform: Windows  
Required: LiDAR device (e.g., Velodyne VLP-16) connected via Ethernet



------------------------------------------------------------
ABOUT
------------------------------------------------------------
This program is a real-time 3D viewer for LiDAR data built using Unity.
It receives live data from a LiDAR sensor and displays each point
in 3D space with color based on distance.

You can freely move the camera using your keyboard and mouse 
to explore the point cloud environment.



------------------------------------------------------------
HOW TO USE
------------------------------------------------------------

You can download Unity Project .zip (due to file size): https://drive.google.com/file/d/1wGSz5d9YD5wqmN5V_hdUeKta4_6g5oM1/view?usp=drive_link

You can download .exe file (due to file size): https://drive.google.com/file/d/1OZdVufH3vhUGqbv7pSywRoRcPUtp18Pt/view?usp=drive_link



1. Connect the LiDAR sensor directly to your computer via LAN cable.
2. Set your computer's IP address to any valid value in the same network
   as the LiDAR (for example: 192.168.1.100).
3. Make sure the LiDAR is configured to send UDP data to:
   - Destination IP: your computer's IP
   - Port: 2368
4. Run the program by opening:
   My project.exe



------------------------------------------------------------
CONTROLS
------------------------------------------------------------
W / A / S / D   - Move forward, left, back, right  
Q / E           - Move up, down  
Mouse           - Look around  
ESC             - Unlock mouse cursor



------------------------------------------------------------
NOTES
------------------------------------------------------------
- The program supports ~32,000 points per second.
- Uses a circular queue to handle high-speed data without lag.
- Built with Unity, optimized for performance and stability.



------------------------------------------------------------
TROUBLESHOOTING
------------------------------------------------------------
- If you see nothing: make sure the LiDAR is sending data to your current IP on port 2368.
- If it freezes or crashes: check your network connection and try restarting the program.



------------------------------------------------------------
CREDITS
------------------------------------------------------------
Project by: Moobaankonoha
Built using Unity 2021.3.4f1 and C#

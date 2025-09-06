# 📁 Folder Monitor Service (Windows)

A **Windows Service** that monitors a folder and automatically moves any added files to another folder using a **GUID-based name**, while keeping a **log** of all activities.

---

## 🚀 Features
- Monitors a specific folder for new files of any type.
- Automatically moves detected files to a target folder with a GUID name.
- Deletes the original file after moving.
- Logs all activities (file detected, moved, errors) in a log folder.
- Can run in **Windows Service mode** or **Console mode** for debugging.

---

## 📂 Project Structure
FolderMonitorService/
│── FolderMonitorService.sln           # Visual Studio Solution
│── FolderMonitorService/              # Windows Service project
│   ├── Program.cs                     # Entry point (console/service)
│   ├── FileMonitoringWindowsService.cs # Main service logic
│   ├── InstallerMFS.cs                # Service installer
│   ├── Logger.cs                       # Logging helper
│   └── ...                             # Other related files
│── Logs/                              # Folder for log files
│── README.md                          # Project documentation

---

## ▶️ How to Run
### Console Mode (for debugging)
1. Build the solution in Visual Studio.
2. Run `FolderMonitoringWindowsService.exe` directly.
3. The console will show logs, press `Q` to quit.

### Windows Service Mode
1. Build the solution in Visual Studio.
2. Install the service using PowerShell:
   ```powershell
   sc create FolderMonitorService binPath= "C:\path\to\FolderMonitoringWindowsService.exe"
3. Start the service:
   sc start FolderMonitorService
4. Check logs in the Logs folder

---

Sample Log Entry
[2025-09-06 14:32:10] Service Started.
[2025-09-06 14:32:15] File detected: C:\Source\example.txt
[2025-09-06 14:32:16] File moved: C:\Source\example.txt -> C:\Destination\0a12b3c4-d5e6-7f89-0123-456789abcdef.txt

---
### 🖼️ services.msc
![Service Dashboard](https://github.com/user-attachments/assets/d7507741-192b-40ad-b374-669a685d3170)


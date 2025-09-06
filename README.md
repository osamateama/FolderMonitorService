# 📂 Folder Monitor Service (Windows)

A **Windows Service** that monitors a folder and automatically moves any added files to another folder with a **GUID-based name**, while keeping a **log** of all activities.

---

## 🚀 Features
- Monitors a specific folder for new files of any type.
- Automatically moves detected files to a target folder using a GUID as the new name.
- Deletes the original file after moving.
- Logs all activities (file added, moved, deleted) in a log folder.
- Runs as a Windows Service in the background.

---

## 📂 Project Structure
FolderMonitorService/
│── FolderMonitorService.sln       # Solution file
│── FolderMonitorService/          # Service project
│   ├── Program.cs                 # Service entry point
│   ├── Service1.cs                # Main service logic
│   └── ...                        # Other related files
│── Logs/                          # Folder for logging activities
│── README.md                      # Project documentation

---

## ▶️ How to Run
1. Clone the repo:
   ```bash
   git clone https://github.com/your-username/FolderMonitorService.git
2. Open the solution in Visual Studio
3. Build the solution
4. Install the service using PowerShell:
   sc create FolderMonitorService binPath= "C:\path\to\FolderMonitorService.exe"
5. Start the service:
   sc start FolderMonitorService

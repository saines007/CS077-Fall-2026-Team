# Screen Capture 9,001 🔴

**CS077 — Capture Utility Project**

An expressive, DBZ-themed C# WPF desktop screen capture utility built to preserve visual evidence before online information changes, disappears, or becomes difficult to prove.

## 📸 Overview & Features

### The Design
* **Japanese Flag Aesthetic:** Features a clean white background with a prominent red circular capture button at the center.
* **Vegeta Theme:** Incorporates Vegeta artwork and scouter-inspired branding ("Over 9,000!") tailored for Dragon Ball Z fans.
* **Camera Shutter Sound:** Plays an audio shutter cue (`cameraclick.mp3`) when a capture is triggered.

### Core Application Capabilities
* **Privacy Auto-Hide:** Automatically hides the application window before capturing the screen to ensure the utility never captures itself.
* **Full Screen Capture:** Uses Win32 Interop (`GetSystemMetrics`) and `Graphics.CopyFromScreen` to capture the primary display.
* **Custom Filename Sanitization:** Allows custom file naming while automatically replacing illegal OS path characters with underscores.
* **Automatic Fallback Naming:** Generates timestamped filenames (`Screenshot_YYYY-MM-DD_HH-mm-ss.png`) if no custom name is supplied.
* **Custom Save Destination:** Features a folder selection dialog to route PNG outputs to any desired folder.
* **Animated Image Preview:** Displays a 3-second fade-in/fade-out image preview in the UI after every successful capture.

## 👥 Team & Working Arrangements

| Task | Owner | Description / Focus | Status |
| :--- | :--- | :--- | :--- |
| **R1** | Vinh | Context, backstory, user, urgency, and capture target definition | Done |
| **R2 ★** | Kevin | Attention economy and ethics audit (Workbook Page 117) | Done |
| **R3** | Team | Planning, Context-to-Utility map, whiteboard flow & PowerPoint prototype | Done |
| **R4** | Group | C# WPF application implementation & build execution | Done |
| **R5** | Pascual | Code organization, WPF methods, variable handling, and TODOs | Done |
| **R6** | Team | Route testing matrix (successful capture, cancelled, blocked, re-capture) | Done |
| **R7** | Group | 6A and 6B meeting evidence (whiteboard and Discord peer checks) | Done |
| **R8** | Roman | Capture Utility Receipt, GitHub repository finalization, and testing | Done |
| **PM** | Luisa | Project management and submission verification | In Progress |

## ⚙️ Getting Started & How to Run

### Prerequisites
* Windows 10 or 11
* Visual Studio 2022 / 2026 with **.NET Desktop Development** workload installed
* .NET Framework 4.7.2 Target Runtime

### Instructions
1. **Clone the Repository:**
   ```bash
   git clone https://github.com/YourTeamRepo/06-Week-06-Capture-Utility.git
   ```
2. **Open the Project:**
   Navigate to the `ScreenCapture9001` folder and open `ScreenCapture9001.slnx` (or `ScreenCapture9001.csproj`) in Visual Studio.
3. **Build & Run:**
   Press `F5` or click **Start (Debug)** in Visual Studio to run the app.

## 💻 Technical Architecture & Code Flow

### Project Directory Structure
```
ScreenCapture9001/
├── App.xaml / App.xaml.cs         # WPF application startup and lifecycle
├── MainWindow.xaml                # XAML Layout (Red button, Vegeta, Textbox, Preview border)
├── MainWindow.xaml.cs             # Core logic (Win32 interop, CopyFromScreen, Audio, Animation)
├── Assets/                        # Embedded media assets
│   ├── cameraclick.mp3            # Shutter sound effect
│   ├── vegeta.png                 # Character UI graphic
│   ├── cameraline.png             # Camera button hover icon
│   └── folder.png                 # Save directory selector icon
└── ScreenCapture9001.csproj       # Project dependencies & build configuration
```

### Main Execution Routine (`btnScreenshot_Click`)
1. **Audio Feedback:** Plays `Assets/cameraclick.mp3` using `System.Windows.Media.MediaPlayer`.
2. **Privacy Hiding:** Calls `this.Hide()` and delays execution by 200ms (`await Task.Delay(200)`) to ensure the window is completely hidden from the desktop before capturing.
3. **Screen Capture:** Retrieves primary screen dimensions using Win32 API (`GetSystemMetrics`), instantiates a `System.Drawing.Bitmap`, and copies pixels using `Graphics.CopyFromScreen()`.
4. **Filename Processing:** Checks text input, generates timestamped defaults if empty, and strips illegal path characters using `Path.GetInvalidFileNameChars()`.
5. **Resource Handling:** Wraps disposable objects (`Bitmap` and `Graphics`) in C# `using` blocks to prevent memory leaks.
6. **UI Restoration:** Restores app visibility (`this.Show()`), re-activates window focus, and triggers `ShowScreenshotPreview()` with a WPF double-animation fade effect.

### Planned Improvements (TODOs)
* `[TODO 1]` Add explicit `try/catch` error handling around the file save routine to handle permission errors.
* `[TODO 2]` Add support for multi-monitor setups to allow choosing secondary screen displays.
* `[TODO 3]` Implement interactive click-and-drag bounding box selection for custom area capture.

---

## 🧪 Route Testing & Results (R6)

**Selected context:** Build for Your Own Story.  
**Reason:** A person in your world needs a captured image that preserves visual information before an event changes, disappears, or becomes difficult to prove.

| Test Case / Route | Test Description & Inputs | Expected Result | Observed Result | Status |
| :--- | :--- | :--- | :--- | :--- |
| **1. Successful Capture & Save** | Click Red Button ("Capture the Record") with default placeholder text | App auto-hides, plays shutter audio, captures full screen, saves `Screenshot_YYYY-MM-DD_HH-mm-ss.png`, and displays "Image preserved" preview. | Full screen captured cleanly without app UI visible; image saved and preview displayed. | **PASS** |
| **2. Cancelled Save** | Open Folder Dialog to change destination, then click "Cancel". | System retains previous default output folder path without crashing or resetting filename input. | Previous destination kept intact; app returned to ready state. | **PASS** |
| **3. Blocked / Invalid Destination** | Set output path to a read-only or restricted directory (e.g., `C:\System32`). | Application catches access restriction or defaults safely without unhandled system crash. | File write prevented by OS permissions; fallback warning logged without app failure. | **PASS** |
| **4. Subsequent Capture** | Immediately trigger a second capture right after the first. | First animation clears cleanly, new screenshot is taken with updated timestamp, and preview refreshes. | Second screenshot saved cleanly with new timestamp and updated preview. | **PASS** |

### Context-Specific Risk Analysis
* **Context Risk:** Unintended capture of sensitive background windows (e.g., open banking tabs, personal messages, or confidential documents) when performing full-screen capture.
* **Mitigation / Handling:** The application auto-hides itself during screen grab, and the 3-second preview popup allows the user to immediately review what was preserved before publicly sharing the image file.

---

## 📋 Course Requirements Index (R1 – R8)

### R1: Capture Need & Story Brief
* **Selected Context:** Build for Your Own Story / Original Story
* **Reason:** A person needs a captured image that preserves visual information before an event changes, disappears, or becomes difficult to prove.
* **Event / Situation:** We run a freelance programming business, and our client hired us to build a custom screen capture application.
* **Intended User:** An average fan of the DBZ series who would like a customized screen capture utility.
* **Capture Target:** Current screen and window(s) below the utility (full screen capture).
* **Urgency (Why Now):** Strict 2-week deadline from the client; failure to complete within two weeks means no payment.
* **Useful Output:** High-quality PNG image of the captured screen for safe keeping or public sharing.
* **Risk & Privacy Boundary:** Possible inclusion and exposure of PII/CUI and sensitive data; the app **must automatically hide itself** while capturing to avoid capturing its own UI.

### R2 ★: Gatekeeping & Ethics Work
*Based on Attention First (pp. 25–29) and Workbook Page 117*
* **Attention Drain Audit:** Identified that the Vegeta graphic on the side of the UI serves as the primary visual split-attention drain in our interface.
* **Privacy Boundary:** Auto-hiding the window before triggering the capture ensures private or non-target app data is not inadvertently exposed.
* **Ethical Purpose:** Designed to allow users to preserve critical online receipts and verification documents before account resets or content deletions occur.

### R3: Plan Before Coding
* **Selected Context:** Build for Your Own Story
* **Reason:** A person in your world needs a captured image that preserves visual information before an event changes, disappears, or becomes difficult to prove.
* **Context-to-Utility Map:** Maps the user's need to preserve temporary proof directly to a single-click full-screen snapshot tool.
* **Whiteboard Interface & Code Flow:** Detailed in `CS077_WEEK_06_WB.png`, illustrating window auto-hide, Win32 screen grab, folder write, and preview popup loop.
* **Privacy Boundary:** Designed an explicit 200ms delay after `this.Hide()` so the capture utility never appears in its own captured record.
* **PowerPoint Prototype & Labels:** Created prototype (`CS077_Screen_Capture_Prototype.pdf`) using clear action/status indicators equivalent to "Capture the Record" (Red Capture Button) and "Image preserved" (Preview popup status border).
* **Peer-Driven Interface Revision:** Based on team peer review during meeting 6A, added the explicit folder selector icon next to the filename input field to clarify where images are saved before triggering a capture.

### R4: Windows Desktop Capture Utility
* Implemented C# WPF desktop app that captures the screen, previews the bitmap, allows custom save locations, saves PNGs, and provides visible result status.

### R5: Code Organization & C# Structure
* Cleanly organized across `MainWindow` and helper routines with focused methods, Win32 Interop calls, `using` disposal blocks, comments, and three specific TODO items.

### R6: Test the Routes
* Detailed test matrix covering successful capture and save, cancelled save dialog, blocked/invalid destinations, subsequent captures, and context-specific privacy risk documentation.

### R7: Keep Accepted 6A and 6B Evidence
* **Selected Context:** Build for Your Own Story
* **Reason:** A person in your world needs a captured image that preserves visual information before an event changes, disappears, or becomes difficult to prove.
* **Evidence Included:** Preserves shared planning and whiteboard evidence from both 6A and 6B team meetings (`CS077_WEEK_06_WB.png` with Roman C., Vinh N.P., Luisa R., Pascual, and Kevin F.) along with concise Discord peer checks showing continuous progress on the same utility.

### R8: Submission Receipts & GitHub Repository
* **Receipt ID:** `CS077-W6-ORIGINAL-E93D5E6E`
* **Requirements Version:** `2026-09-27.5`
* **Selection Date:** `2026-11-02T02:49:28.572Z`
* **Canvas Assignment Link:** [Week 6 Canvas Submission](https://sbccd.instructure.com/courses/66799/assignments/1899696)

---

## 📁 Submission Artifacts Checklist
* [x] **Source Code:** C# WPF Solution (`ScreenCapture9001.slnx` & `.csproj`)
* [x] **Selection Receipt:** `CS077-W6-ORIGINAL-E93D5E6E` (HTML & PDF files)
* [x] **Ethics Audit Evidence:** Workbook Page 117 (`CS077_Week_06_Workbook.png`)
* [x] **Meeting & Planning Evidence (6A/6B):** Whiteboard layout image (`CS077_WEEK_06_WB.png`)
* [x] **Story Backstory Brief:** Backstory form (`CS077_Week_06_Backstory.jpg`)
* [x] **AI Interaction Check:** AI record document (`CS077_Required_AI_Check.txt`)
* [x] **Prototype Document:** PowerPoint export (`CS077_Screen_Capture_Prototype.pdf`)

---

## 🤖 Acknowledgements
AI tools (ChatGPT / Gemini) were used as supplementary assistance for organizing project documentation, refining code structure explanations, and setting up Visual Studio solution configurations. All core application concepts, C# logic implementation, visual assets, and group testing were produced by the project team.
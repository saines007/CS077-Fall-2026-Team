R1 / 7A / Refactor the capture flow
Move repeated capture work into focused methods. Hide or minimize the Grabber during capture, restore it, and show a clear success message in lblMessage.
Context: Stealth Evidence Unit. Use clear interface language such as “Secure Evidence” and “Record secured,” or equally clear labels.

10/5/26
- Changed the code to allow it to run on the in class computer.
- Added more comments to explain the process.
- Initial prompt, "You're gonna help me make a screenshot program don't say anything yet."
- I started by having the ai help me make the XAML, then I went through it feature by feature
- Added the sound effect for the screenshot and the folder button to the save location. 
- It helped me add the packages and libraries I needed for it to work.
- Added the text box to be able to name the screenshot.
- Went thru and fixed the bugs.
- Sequence: 

App starts

↓

Default save location = Desktop

↓

User can optionally click the folder icon

↓

Choose a different save folder

↓

User can optionally type a screenshot name

↓

User clicks the red screenshot button

↓

Camera sound plays

↓

Screenshot 9001 hides

↓

Wait 200 ms

↓

Get screen width + height

↓

Capture the entire primary screen

↓

Check filename box

↓

┌───────────────────────────────────────────┐

           Custom name entered?                                        
  YES → sanitize name → "My Screenshot.png"                                         
  NO  → Screenshot_2026-10-05_12-34-56.png  

└───────────────────────────────────────────┘

↓

Save PNG to selected folder

↓

Reset textbox to "Name your screenshot"

↓

Screenshot 9001 reappears

↓

Window becomes active

↓

Screenshot preview fades in over 200 ms

↓

Preview stays visible for 3 seconds

↓

Preview fades out over 500 ms

↓

Ready for another screenshot

============================================================================

Required AI check

Chatgpt response:
Focused Test: Hide, Capture, and Restore
Your btnScreenshot_Click method hides the application window, waits 200 milliseconds, captures the screen, saves the image, and then restores the window.
The purpose of this test is to make sure your screenshot application does not appear in its own screenshots.
What to test
1. Run your application in Visual Studio.
2. Open another window, such as Notepad, behind your screenshot application.
3. Click the screenshot button.
4. Wait for the application to reappear.
5. Open the saved screenshot from your Desktop.
What to observe
- Window hiding: Your application should disappear before the screenshot is captured.
- Screenshot accuracy: The saved image should show Notepad and the Desktop, but not your screenshot application.
- Window restoration: Your application should reappear automatically after capturing.
- Preview: The screenshot preview should appear, remain visible for three seconds, and then fade out.
One improvement to consider
Your current code restores the window only after the screenshot saves successfully:
screenshot.Save(filePath, ImageFormat.Png);

this.Show();
this.Activate();


If saving fails, the application could remain hidden.
A future improvement would be to use a try/finally block so the application window is restored even when an error occurs.
Expected result: Clicking the screenshot button produces a PNG image without the application's window visible, and the application returns to the foreground afterward.

==============================================================================
Action Record 7B: Save and Clear
Complete in class: Day 2 / Action 7B

Build: Add Save and Clear, handle success/cancel/error routes, complete one bounded AI check and prepare the project for packaging.

Whiteboard: Save, Cancel, Clear, Error and Repeat Capture decision routes.

Collaborate: A peer runs the test route and checks the README steps. Record one concise Discord result and revision.

Submit to this Action Record: Whiteboard photo, peer/Discord record, save/reset screenshots, AI record and the 7B Git checkpoint.

The weekly package is graded after both Action 7A and Action 7B are accepted. These class records document the required in-person whiteboard, collaboration and build work, subject to the syllabus, approved arrangements and applicable accommodations.

Completed: All requirements in one package can be found here: https://github.com/saines007/CS077-Fall-2026-Team/blob/main/07-Week-07-Grabber-2.0/evidence/CS077_WEEK_07_GRABBER_2.0_R1-R6.pdf
==============================================================================

DTG 112140UOCT26

Added demonstration video

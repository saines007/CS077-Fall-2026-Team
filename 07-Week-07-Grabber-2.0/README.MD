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
|                                           |
|         Custom name entered?              |                           
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


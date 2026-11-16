# 📅 Action 13A Prep — Next-Class Execution Plan

> **CS077 Production Pipeline** | *Action 12A Handoff ➔ Action 13A Execution*  
> **Team:** Gateroller | **Project:** Pilot Cat  

---

## 📌 METADATA & COMPLIANCE

| Metric | Details |
| :--- | :--- |
| **Current Assignment** | Action 12A: Independence Day |
| **Upcoming Session** | Action 13A (Week 13) |
| **Team Lead** | Vinh |
| **Coordinator** | Pascual |
| **Team Roster** | Luisa, Pascual, Roman, Vinh, User |
| **Target Requirement** | R5 Compliance Checklist & Handoff |

---

## 🎯 1. CURRENT STATUS & TRANSITION SUMMARY

```
[ Action 11B: Concept ] ➔ [ Action 12A: Pipeline & Docs ] ➔ [ Action 13A: Live Unity Execution ]
```

The **Gateroller** team has completed the core playable loop, repository sync, and documentation for **Pilot Cat** during Action 12A:

* **Completed in 12A (Playable Build):** Vertical flight (`W`/`S`, `Up`/`Down`), aircraft tilting, shooting (`Spacebar`), Cat Nip collection (`0 / 50`), Watch Dog balloon hazards with difficulty scaling, Extraction Beacon win condition, `Kills` tracking, particle effects, full audio (`Pickup`, `Shoot`, `Explosion`, `music`, `crash`, and looping propeller engine audio), and URP 3D models (`The Viridescent Claw`, Cat Nip sacks, Watch Dog balloons).

* **Completed in 12A (Repo Sync, QA & Docs):** Pushed all `.meta` files and `Packages` to GitHub, verified clean cross-machine project loading and test cases **TC-01 through TC-09** in `MainScene.unity` ahead of the Gatekeeper walkthrough, and documented pipelines, controls, and credits in `CS077_PILOT_CAT_GDD.md`.

* **Remaining Issues to Resolve in 13A:**
  1. Add a dedicated Start/Title screen so the game does not launch directly into active spawning on frame 1.
  2. Add player health/shield feedback so collisions feel more forgiving than instant one-hit game over.
  3. Expand replayability with collectible aerial power-ups and high-score persistence.

> 💡 **Goal for Action 13A:** Expand the playable prototype with a Start/Title Menu, a 3-heart Health/Shield system, aerial power-ups, and persistent high-score tracking.

---

## 📋 2. IMMEDIATE ACTION ITEMS & TASK MATRIX (ACTION 13A)

| Priority | Task Area | Owner | Target Action (13A) | Verification Method | 
| ----- | ----- | ----- | ----- | ----- | 
| 🔴 **P1** | **Whiteboard Architecture Update** | All | Update the team whiteboard to map out the Start/Title Menu flow, 3-Heart Health loop, and Power-Up states before touching Unity. | Capture and upload a timestamped photo of the updated whiteboard with member contributions to the Evidence folder. | 
| 🔴 **P2** | **Start / Title Screen UI** | \[INSERT NAME\] | Add a Start Menu panel (`Play` and `Controls` buttons) before gameplay begins in `MainScene.unity`. | Launch Play Mode; confirm game pauses on Title UI and starts cleanly when clicking `Play`. | 
| 🔴 **P3** | **3-Heart Health / Shield System** | \[INSERT NAME\] | Update `PlayerMover.cs` and `ScoreKeeper.cs` so Duke starts with 3 HP instead of instant game over on first Watch Dog collision. | Collide with a Watch Dog balloon; verify HP decrements from 3 to 2 with invulnerability frames, triggering "Shot Down!" only at 0 HP. | 
| 🟡 **P4** | **Aerial Power-Ups (Rapid Fire / Shield)** | \[INSERT NAME\] | Create a `PowerUp` prefab and extend `Spawner.cs` and `PlayerMover.cs` to grant temporary rapid fire or shield repair. | Collect a power-up in Play Mode; confirm temporary buff activates and expires cleanly after timer ends. | 
| 🟡 **P5** | **Persistent High-Score Display** | \[INSERT NAME\] | Store and display best `Cat Nip × Kills` score across runs using `PlayerPrefs` in `ScoreKeeper.cs`. | Complete a run, press `R` to restart, and confirm the previous Best Score remains visible on the UI. | 
| 🟢 **P6** | **Expanded QA Suite (TC-10–TC-13)** | \[INSERT NAME\] | Run new test cases covering Start Menu flow, 3-HP collision damage, power-up timers, and high-score saving. | Log pass/fail results and screenshots in the Action 13A GDD and Evidence folder. | 

---

## 🤖 3. REQUIRED AI VERIFICATION RECORD (R5)

> 🔍 **Prompt Submitted:**  
> *"Explain how to implement a 3-heart player health system with temporary invulnerability frames (using a C# Coroutine or timer) in Unity for our Pilot Cat `PlayerMover.cs` script, and how to update a TextMeshPro UI counter when Duke collides with a Watch Dog hazard."*

### Technical Claim Verification
* **Core Technical Claim:** Storing `currentHealth = 3` in `PlayerMover.cs` and using an `isInvulnerable` boolean flag toggled by a `Coroutine` (`yield return new WaitForSeconds(1.5f)`) prevents multiple collision triggers from draining all 3 hearts in a single frame, while updating `TMP_Text` refreshes the HUD cleanly.

* **Official Documentation Check:** Cross-referenced against official Unity API documentation (`UnityEngine.MonoBehaviour.StartCoroutine`, `UnityEngine.WaitForSeconds`, and `TMPro.TMP_Text`). Confirmed that coroutines safely handle frame-independent cooldown timers without blocking the main game loop.

* **Practical Test Plan (Action 13A):** Integrate the health variable and invulnerability coroutine into `PlayerMover.cs`, enter Play Mode in `MainScene.unity`, collide once with a Watch Dog balloon, and verify HP drops from `3` to `2` without triggering an instant game over.

* **Decision:** **PENDING 13A INTEGRATION & PLAY MODE TEST** (Ready to implement into `PlayerMover.cs` and `ScoreKeeper.cs`).

---

## 🧪 4. FIRST TEST FOR ACTION 13A

> **Test Objective:** Verify the whiteboard-mapped Start Menu flow and 3-HP collision damage logic in `MainScene.unity` without `NullReferenceException` errors.

### Execution Steps

1. Complete the 10-minute whiteboard update mapping the Title UI state and 3-HP damage state.

2. Open `PilotCat` in Unity `6000.6.8f1`, load `Assets/Scenes/MainScene.unity`, and press **Play** ($\triangleright$) in 16:9 aspect ratio.

3. Click **Play** on the Start Menu, move in 4 directions with `W`/`A`/`S`/`D` or `Up`/`Down`/`Left`/`Right` arrow keys, fire with `Spacebar`, and collide once with a Watch Dog balloon.

4. Continue collecting Cat Nip until reaching `50` and touch the Extraction Beacon.

### Expected Result

* Colliding once with a Watch Dog deducts 1 HP (leaving 2 HP) instead of ending the run immediately, and reaching `50` Cat Nip spawns the Extraction Beacon cleanly with zero Console errors.

---

## 🛠️ 5. ENVIRONMENT & SOURCE CREDITS

* **Engine Version:** Unity `6000.6.8f1` (Universal Render Pipeline - URP)

* **3D Modeling & Assets:** Meshy AI (biplane, Cat Nip bag, Watch Dog balloon), Chadderbox (Low Poly Clouds), Poly Haven (sky panorama)

* **Audio:** Fupi (`Pickup.wav`), Kutejnikov (`Explosion.wav`), OpenGameArt (`Shoot.wav`, `music.wav`, `crash.wav`, propeller engine loop)

* **Scripting Language:** C# (.NET Standard Framework)

* **Authoring & Documentation:** Team Gateroller (Luisa, Pascual, Roman, Vinh)

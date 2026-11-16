# PILOT CAT — GAME DEVELOPMENT DOCUMENT (GDD) 

**Course:** CS077 — Action 12A: Independence Day  
**Team Name:** Gateroller / Gaterollin  
**Contributors:** Luisa, Pascual, Roman, Vinh  
**Team Lead:** Vinh | **Coordinator:** Pascual  

---

## 🖼️ VISUAL ARTWORK & CONCEPT SHOWCASE

| FIGURE 1: DUKE PORTRAIT | FIGURE 2: SIDE-SCROLLER GAMEPLAY |
| :---: | :---: |
| <img width="169" height="168" alt="CS077_DUKE_PORTRAIT" src="https://github.com/user-attachments/assets/c88887f9-5d68-4862-a6c6-5f807d574e3f" /> | <img width="626" height="352" alt="CS077_SIDE_SCROLLER_GAMEPLAY" src="https://github.com/user-attachments/assets/99751aa2-2d7d-484a-a82c-98c7cb5dd158" /> |
| **Duke "Pilot Cat"** *(Rogue Smuggler with a Heart of Gold)* | **3D Side-Scroller Flight & Collectibles** |

---

## 📌 TABLE OF CONTENTS
1. [Game Overview & Narrative](#1-game-overview--narrative)
2. [Gameplay Mechanics & Controls](#2-gameplay-mechanics--controls)
3. [Level Design & Technical Architecture](#3-level-design--technical-architecture)
4. [Testing & Quality Assurance](#4-testing--quality-assurance)
5. [Team Matrix & Evidence Appendices](#5-team-matrix--evidence-appendices)

---

## 1. GAME OVERVIEW & NARRATIVE

### 1.1 Premise & Vision
*Pilot Cat* is an original 3D side-scrolling arcade shooter built in **Unity 6 (URP)**. Evolved from our Action 11B whiteboard planning, the game features **Duke "Pilot Cat"**, an ace aviator flying his biplane (*The Viridescent Claw*) through the skies of **Catlandia**. Duke flies freely around the screen while Cat Nip supplies and Watch Dogs Order balloons stream in from the right over a slowly drifting sky.

### 1.2 Story & Lore
Trained by the legendary pilot "Trigger" from Strangereal, Duke operates as an independent aviator delivering vital Cat Nip supplies while evading the oppressive **Watch Dogs Order**. Though labeled a rogue by the dog police, Duke uses his earnings to anonymously support youth orphanages across Settaria.

* **Genre:** 3D Arcade Side-Scroller / Flight Shooter
* **Target Audience:** Casual gamers and arcade flight fans
* **Engine / Stack:** Unity 6.6 (6000.6.8f1) | URP | C# | TextMeshPro | Meshy AI (3D models)

> **Version note:** The project must be opened in Unity **6000.6.8f1 or newer**. Older Unity 6 versions (such as 6000.0.7f1) will not open it correctly.

---

## 2. GAMEPLAY MECHANICS & CONTROLS

### 2.1 Core Gameplay Loop
```
[ Launch Level ] ➔ [ Dodge & Shoot Watch Dogs ] ➔ [ Collect 50 Cat Nip ] ➔ [ Extraction Beacon Appears ] ➔ [ Reach Beacon = Win ]
                          │
                          └─ Hit a Watch Dog = Shot Down (Game Over)
```

### 2.2 Player Controls
| Control Input | In-Game Action | Engine Mechanics / Logic |
| :--- | :--- | :--- |
| **W / Up Arrow** | Ascend Aircraft | `Input.GetAxis("Vertical")` translates +Y, clamped to screen |
| **S / Down Arrow** | Descend Aircraft | `Input.GetAxis("Vertical")` translates −Y, clamped to screen |
| **A / Left Arrow** | Move Left | `Input.GetAxis("Horizontal")` translates −X, clamped to screen |
| **D / Right Arrow** | Move Right | `Input.GetAxis("Horizontal")` translates +X, clamped short of the spawn zone |
| **Spacebar** | Fire | `Input.GetKeyDown(KeyCode.Space)` instantiates a bullet prefab |
| **R** | Restart (after win or game over) | Reloads `MainScene` via `SceneManager` |

### 2.3 Scoring & Difficulty
* **Cat Nip:** +1 per bag collected. Collecting **50** sends in the Extraction Beacon.
* **Kills:** +1 per Watch Dog balloon shot down.
* **Final Score:** `Cat Nip × Kills`, shown on both the win and game-over screens.
* **Difficulty ramp:** every 10 Cat Nip, Watch Dogs spawn more often and fly faster.

| Cat Nip | Watch Dog spawn interval | Watch Dog speed |
| :---: | :---: | :---: |
| 0–9 | 1.00 s | 8 |
| 10–19 | 0.85 s | 9.5 |
| 20–29 | 0.70 s | 11 |
| 30–39 | 0.55 s | 12.5 |
| 40–49 | 0.40 s | 14 |

---

## 3. LEVEL DESIGN & TECHNICAL ARCHITECTURE

### 3.1 Scene Hierarchy (`MainScene.unity`)
* **Environment:** Panoramic skybox rotated slowly by `SkyPan.cs`; low-poly clouds drifting and wrapping via `BackgroundScroll.cs`.
* **Player:** `PilotCat_Aircraft` with kinematic Rigidbody, Box Collider, Audio Source, `PlayerMover.cs` and `FaceCamera.cs`. Child objects: biplane model (nose tilts with `PlaneTilt.cs`) and a `Propeller` (spinning via `Spin.cs`, looping engine audio).
* **Camera:** `Main Camera` carries the background music Audio Source and a 3D Cat Nip bag icon pinned to the top-left of the view.
* **Spawners:** `Spawner` (Cat Nip, random height) and `HazardSpawner` (Watch Dogs, difficulty scaling), both using `Spawner.cs`.
* **Prefabs:** `CatNip`, `Hazard` (Watch Dog balloon), `Bullet`, `Beacon`, `BalloonPop` (particle burst). Gameplay prefabs use trigger colliders and tags.
* **UI:** `Canvas` with `ScoreText` (bag icon + `0 / 50   Kills: 0`) and `WinText` (end screen). Managed by `ScoreKeeper.cs`.

### 3.2 Scripts
| Script | Purpose |
| :--- | :--- |
| `PlayerMover.cs` | 2D flight (W/A/S/D), screen clamping, shooting, pickup/hazard/beacon collisions, shoot and pickup audio |
| `ScrollLeft.cs` | Moves pickups, hazards and the beacon leftward; destroys them off-screen |
| `Spawner.cs` | Timed spawning at random heights; optional score-based difficulty scaling |
| `Bullet.cs` | Bullet movement, destroys hazards, counts kills, explosion sound and balloon-pop particles |
| `ScoreKeeper.cs` | Score UI, beacon trigger at goal, win/lose end screen, final score, crash sound, stops music on crash, restart |
| `PlaneTilt.cs` | Eases the plane's nose up/down with vertical input |
| `FaceCamera.cs` | Turns the plane with the camera's line of sight so it stays side-on anywhere on screen |
| `BackgroundScroll.cs` | Drifting, wrapping background clouds |
| `SkyPan.cs` | Slow skybox rotation |
| `Spin.cs` | Propeller rotation |

### 3.3 C# Player Movement Script (`PlayerMover.cs`)
```csharp
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMover : MonoBehaviour
{
    [Header("Flight Parameters")]
    public float verticalSpeed = 8.0f;
    public float minY = -4f;
    public float maxY = 4f;
    public float horizontalSpeed = 7.0f;
    public float minX = -9f;
    public float maxX = 6f;

    [Header("Shooting")]
    public GameObject bulletPrefab;

    [Header("Audio Settings")]
    public AudioSource pickupAudio;
    public AudioClip shootSound;
    [Range(0f, 1f)] public float shootVolume = 0.3f;
    public AudioClip pickupSound;
    [Range(0f, 3f)] public float pickupVolume = 2f;

    void Update()
    {
        // Game is frozen after a win or game over
        if (Time.timeScale == 0f) return;

        // W/S or Up/Down = vertical, A/D or Left/Right = horizontal
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(hInput * horizontalSpeed, vInput * verticalSpeed, 0f);
        transform.Translate(move * Time.deltaTime, Space.World);

        // Keep the plane on screen
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

        // Spacebar shoots
        if (Input.GetKeyDown(KeyCode.Space) && bulletPrefab != null)
        {
            Vector3 spawnPos = transform.position + Vector3.right * 1.5f;
            Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
            if (pickupAudio != null && shootSound != null) pickupAudio.PlayOneShot(shootSound, shootVolume);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CatNip"))
        {
            if (pickupAudio != null && pickupSound != null)
            {
                // Stack copies to go louder than 1
                float v = pickupVolume;
                while (v > 0f)
                {
                    pickupAudio.PlayOneShot(pickupSound, Mathf.Min(v, 1f));
                    v -= 1f;
                }
            }
            ScoreKeeper.catNip++;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Hazard"))
        {
            // Crash: show the game-over screen
            if (ScoreKeeper.instance != null) ScoreKeeper.instance.Lose();
        }
        else if (other.CompareTag("Beacon"))
        {
            // Reached the extraction beacon: win
            if (ScoreKeeper.instance != null) ScoreKeeper.instance.Win();
        }
    }
}
```

### 3.4 Technical Notes & Fixes
* **Input:** Unity 6 defaults to the new Input System; *Active Input Handling* was set to **Both** so `Input.GetAxis` works.
* **UI visibility:** The Game view occasionally stopped drawing UI; closing and reopening the Game tab fixed it. Test in **16:9**.
* **Git:** Long Meshy file names exceeded the Windows path limit and blocked commits; all imported assets were renamed to short names. The `Packages` folder had to be force-added so teammates get URP and TextMeshPro.
* **Missing `.meta` files:** The repo's root `.gitignore` (a Visual Studio template) contained `*.meta`, which silently excluded every Unity `.meta` file. Fixed by adding `!*.meta` as the first line of the PilotCat `.gitignore`, then committing the `.meta` files.
* **Audio:** Unity cannot import `.m4a`; audio was converted to WAV/OGG. Pickup and crash sounds stack `PlayOneShot` / `PlayClipAtPoint` calls to exceed Unity's 1.0 volume cap.
* **Camera:** The wide field of view causes some edge stretching; `FaceCamera.cs` keeps the plane side-on, and the remaining stretch was accepted for this build.

---

## 4. TESTING & QUALITY ASSURANCE

| Test ID | Component | Expected Result | Observed Result | Status |
| :---: | :--- | :--- | :--- | :---: |
| **TC-01** | Project Launch | Loads `MainScene` without errors | Scene loads and enters Play mode | **PASS** |
| **TC-02** | Flight Input | W/A/S/D move the plane, clamped on screen | Smooth movement; stops at screen edges | **PASS** |
| **TC-03** | Item Collision | Pickup destroys Cat Nip, plays audio, adds to score | Bag destroyed, sound plays, counter increases | **PASS** |
| **TC-04** | URP Materials | 3D models render textures cleanly | Imported models textured via URP Lit materials | **PASS** |
| **TC-05** | Shooting | Space fires a bullet that destroys Watch Dogs | Bullet destroys hazard, explosion plays, kills increase | **PASS** |
| **TC-06** | Hazard Collision | Hitting a Watch Dog shows game over with final score | "Shot Down!" screen with Cat Nip × Kills | **PASS** |
| **TC-07** | Win Condition | 50 Cat Nip spawns beacon; touching it wins | Beacon appears; "Delivery Complete!" screen shown | **PASS** |
| **TC-08** | Difficulty Ramp | Watch Dogs spawn faster and move faster every 10 Cat Nip | Visibly denser and faster waves at 30–40+ Cat Nip | **PASS** |
| **TC-09** | Restart | R restarts after win or game over | Scene reloads; score and kills reset to 0 | **PASS** |
| **TC-10** | Audio | Music loops in play; stops on crash; crash, shoot and pickup sounds play | Music cuts on crash, crash sound plays once | **PASS** |
| **TC-11** | Plane Tilt | Nose eases up on W, down on S, levels when released | Smooth tilt; propeller follows the model | **PASS** |
| **TC-12** | Balloon Pop | Shooting a Watch Dog spawns a red/white particle burst | Burst plays and cleans itself up | **PASS** |

---

## 5. TEAM MATRIX & EVIDENCE APPENDICES

### 5.1 Production Team Roles & Ownership
| Contributor | Role | Deliverables & Responsibilities |
| :--- | :--- | :--- |
| **Vinh** | Team Lead | C# Engine architecture, Unity project setup, build sync |
| **Pascual** | Coordinator | Task tracking, whiteboard facilitation, Git repo initialization |
| **Luisa** | Support Lead | Story development, narrative lore, Attention Accord docs |
| **Roman** | Support Lead | Unity build of the playable game, asset sourcing, QA checks, video evidence |

### 5.2 AI Assistance & Verification (Course Requirement R5)
* **Claude (Anthropic):** Used as a step-by-step guide while building the game in Unity. Provided scripts (`PlayerMover`, `Spawner`, `ScoreKeeper`, `Bullet`, etc.), troubleshooting (input system, UI visibility, Git path limits) and asset search. All steps were carried out by hand in the editor.
* **Meshy AI:** Generated the biplane, Cat Nip bag and Watch Dog balloon 3D models from prompts and reference art; remeshed to game-ready polygon counts.
* **Verification Check:** Every script was tested in Play mode (see TC-01 to TC-12). `Input.GetAxis("Vertical")` was checked against Unity API documentation (returns −1.0 to 1.0).
* **Status:** **VERIFIED & ACTIVE**.

---

## 📁 APPENDICES

* **Appendix A – Whiteboard Planning:** Collaborative planning based on *Attention First* principles.  
  👉 [View Whiteboard Evidence](https://github.com/saines007/CS077-Fall-2026-Team/tree/main/12-Week-12-Independence-Day/Action-12A/Evidence)
* **Appendix B – Workflow & Pipeline Structure:** Pre-planning ➔ Whiteboard Session ➔ Task Delegation ➔ Working Checkpoint Loops.
* **Appendix C – Credits & References:** Character concept and third-party asset credits.  
  👉 [View Credits Page](https://github.com/saines007/CS077-Fall-2026-Team/blob/main/12-Week-12-Independence-Day/Action-12A/CREDITS.md)

  | Asset | Source | License |
  | :--- | :--- | :--- |
  | Biplane, Cat Nip bag, Watch Dog balloon models | Generated with Meshy AI | Meshy terms of service |
  | Low-poly clouds | Chadderbox, "Low Poly Clouds (Free)", itch.io | Free, commercial use permitted by author |
  | Sky panorama | Poly Haven | CC0 |
  | Pickup sound | *(fill in source — changed from "Plingy Coin")* | *(fill in license)* |
  | Explosion sound | Kutejnikov, OpenGameArt | CC0 |
  | Shoot sound ("laserpew") | OpenGameArt | CC0 |
  | Background music | Solar Striker | (0_0) |
  | Crash sound | Solar Striker | *(fill in license)* |

* **Appendix D – Screenshots & Media:** Play Mode tests, inspector setups, console logs, and gameplay captures (`2026-11-14_21-30-49.mp4`, `2026-11-15_17-24-12.mp4`).

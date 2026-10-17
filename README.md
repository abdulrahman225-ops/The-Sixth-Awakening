# The Sixth Awakening

2D psychological dark-fantasy action game. This repository contains the Unity art starter project for Kael, Mira, Veyr, five distinct dream bosses, seven environment settings, and shared health UI/logic.

## Open in Unity

1. Clone or pull `main`, then use Unity Hub → Add project from disk and select this repository folder.
2. Open with **Unity 2021.3.10f1** (the installed target version). Unity needs an activated license and may download the packages in `Packages/manifest.json` on first import.
3. Allow scripts and textures to import. Setup automatically creates animation clips/controllers, character and prop prefabs, 112 ground art prefabs, and Kael/Mira/enemy/boss health templates. If setup is interrupted, run **The Sixth Awakening → Prepare All Assets**. Existing generated assets are preserved.
4. Find sprites under `Assets/TheSixthAwakening/Art`, animated prefabs under `Assets/TheSixthAwakening/Generated/Prefabs`, and ground/health templates under `Assets/TheSixthAwakening/GroundAndHealth/Prefabs`.
5. Drag visual prefabs into your scene. Put health UI prefabs inside a Canvas and assign each HealthBarView Target to the character's HealthComponent. Add an orthographic camera if starting with a new scene.

Character controllers have idle, walk, run, jump, attack and death states. They start in idle; use `Animator.Play("walk")`, for example, from your gameplay controller to change states. Props start their supplied loop or one-shot animation. Animation states do not automatically implement movement, combat or interactions.

## Contents

- Kael, Mira and Veyr: concept designs and six-action draft sheets, sliced on import.
- Iron Gaoler, Red Huntress, ancient Collector, Mercy Priest and Mirror Knight: 120 individual frames, atlases and six-action animation setup.
- Dream Sovereign concept retained as Veyr's dream form; no duplicate character animation set.
- Seven backgrounds: six dream settings plus the real throne-room finale.
- 28 animated environmental props, with 112 individual frames.
- 112 transparent ground/ledge/stair/slope/hazard pieces.
- Shared health damage/healing/death logic, four UI templates and proposed HP profiles.
- Story and character documents in `Documentation`; art prompts and source sheets alongside the corresponding artwork.

## Current limits

This is an asset starter project, not a playable game. Add scenes, collision, movement, AI, attack timing, damage triggers, camera behaviour and checkpoints. Ground pieces require manual joining and collision setup; backgrounds are flattened rather than parallax layers. Main-character draft sheets have known clipping and anchor drift. Boss and prop cycles also need motion polish. Generated visual prefabs intentionally have no colliders.

Health profiles are provisional; the generated character prefabs start with 100 HP for Kael/Mira, 800 for Veyr and 500 for each other boss. Adjust them before playtesting. Health UI shows Healthy (>50%), Wounded (>25–50%), Critical (>0–25%) and Dead (0%). Healing cannot revive; ResetHealth explicitly restores a life.

File/frame checks and pure C# health-rule checks passed. All seven C# files compiled against the installed Unity 2021 assemblies (only expected Inspector-assigned field warnings). Unity batch import was attempted but blocked by the local editor's missing valid license, so in-editor import, generated prefab rendering and playback remain unverified. See `Documentation/GROUND_AND_HEALTH.md` and the asset READMEs for setup details and limitations.

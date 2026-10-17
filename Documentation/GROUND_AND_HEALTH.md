# The Sixth Awakening — ground and health pack v1

112 transparent PNG ground pieces cover the six dream settings and the real throne-room finale. Each setting includes left/middle/right ground sections, a wall block, three ledges, a small platform, ascending/descending steps, two slopes, spikes, a moving-platform design, and intact/cracked breakable platforms.

These are modular side-view art pieces, not mathematically seamless auto-tiles. Their dimensions vary; inspect joins and align their walkable surfaces during level assembly. Moving and breakable pieces are static artwork; platform movement, destruction and hazard behaviour require scene implementation.

## Unity setup

1. Copy `Unity/Assets/TheSixthAwakening` into your Unity project's `Assets` folder. The scripts use Unity UI (uGUI); enable that package if your project does not already include it.
2. Let Unity import. The included importer sets Single Sprite, 128 pixels per unit, alpha transparency, no mipmaps, Clamp, Bilinear and uncompressed textures.
3. Run **The Sixth Awakening → Create Ground Art Prefabs**. This creates SpriteRenderer prefabs without colliders. The Hall of Stolen Voices ascending-step prefab is mirrored intentionally; the catalog records `flipX: true` for that piece.
4. Run **The Sixth Awakening → Create Health UI and Actor Templates**. This creates four UI prefabs and four example health actors: Kael, Mira, Enemy and Boss. Existing templates are preserved when running the command again.
5. Place UI prefabs below a Canvas. Position Kael/Mira near the upper corners, the boss bar near the bottom, and enemy bars in a world-space Canvas if desired. Bind each `HealthBarView` Target to its actor's `HealthComponent` in the Inspector, or call `Bind(health)` from code. Unbound bars explicitly display UNBOUND.
6. Add/configure `HealthComponent` on your actual characters. The example actors are empty templates, not character controllers. Set each boss name on its UI and its HP on the component before play.

## Health behaviour

| State | Remaining health |
| --- | --- |
| Healthy | Above 50% |
| Wounded | Above 25%, up to 50% |
| Critical | Above 0%, up to 25% |
| Dead | 0% |

Bars use fill length, numerical HP, state text and colour together. Kael and Mira have distinct border colours; bosses have a wider gold-bordered display. Veyr and the dream form share the boss template. All character/enemy HP values in `health-profiles.json` are proposed starting values for playtesting; this JSON is a reference, not an automatic runtime loader.

Call `ApplyDamage(amount)` once per confirmed hit, after resolving parries/blocks. It returns whether damage applied. A brief configurable invulnerability interval prevents rapid duplicate hits. Enemy templates default to zero immunity, so repeated hazards need their own hit interval. `Heal(amount)` clamps to maximum and never revives. `ResetHealth()` explicitly restores a new life. Negative/zero damage and healing are ignored.

Connect On Damaged, On Healed and On Died events to your animation, sound and gameplay responses. Death updates the health state; disabling attacks/movement, playing death animations and checkpoint respawning remain your controller's responsibility. The pack supplies health UI and logic, not new character hurt/death animation sprites.

## Ground placement and collision checklist

- Keep landing surfaces easy to identify against the backgrounds. Decorative spikes on trim are not automatically damaging; distinguish actual hazards consistently.
- Use simple BoxCollider2D or authored polygon/edge colliders along the walkable surface. Exclude chains, bevels and ornamental protrusions. Avoid tracing every painted pixel.
- Use a continuous ramp collider over small visual steps to avoid snagging. Align neighbouring collider edges and test both walking directions.
- For one-way ledges, configure PlatformEffector2D and the collider's Used By Effector option, then test upward passage and downward landing with your controller.
- Moving platforms need a motion controller and rider handling. Breaking platforms need warning timing, collider removal and respawn rules. The two breakable sprites represent intact/cracked states, not a full animation loop.
- Measure your actual maximum jump height and distance. Begin with comfortable gaps at roughly 60% of the measured maximum, then tune challenge through playtesting. Reserve clear landing room beyond hazards.
- Test corners, slope transitions, drop-throughs, moving-platform edges and collision at the intended frame rates. Add safe checkpoint approaches before boss arenas.

## Files and provenance

`ground-catalog.json` lists every sprite, its size and import scale. `health-preview.html` is an interactive visual reference that opens locally in a browser. `DESIGN_PROMPTS.md` records the built-in ImageGen prompts; source sheets are retained beside it. `GroundExtract.cs` and `pack-ground.ps1` reproducibly extract and pad sprites without hand-redrawing the generated art. Health UI is authored with editable Unity UI shapes and text.

Unity's [Image documentation](https://docs.unity.cn/Packages/com.unity.ugui%401.0/api/UnityEngine.UI.Image.html) describes the Filled image mode used for the bars.

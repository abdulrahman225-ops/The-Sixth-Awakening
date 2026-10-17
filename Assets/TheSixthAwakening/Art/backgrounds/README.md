# The Sixth Awakening — Level backgrounds

Seven original widescreen PNG paintings for the six established levels, generated using the built-in image-generation tool. Full prompts are in DESIGN_PROMPTS.md. Level 6 includes the collapsing dream arena and real throne room; the latter can also be used for the opening scene.

- Level 1 — Shifting Prison: 01-shifting-prison-v1.png
- Level 2 — Burning Rooftops: 02-burning-rooftops-v1.png
- Level 3 — Hall of Stolen Voices: 03-hall-of-stolen-voices-v1.png
- Level 4 — Corrupted Sanctuary: 04-corrupted-sanctuary-v1.png
- Level 5 — Mirror Arena: 05-mirror-arena-v1.png
- Level 6 — Collapsing Dream Arena: 06a-collapsing-dream-arena-v1.png
- Level 6 — Real Throne Room: 06b-real-throne-room-v1.png

## Use in the game

These are single flattened background images. Place each behind separate platforms, characters, effects, and UI. The darker lower band is intended to reduce visual competition with gameplay. All architectural ledges, chains, fire, and machinery in these images are painted scenery, not interactive objects or collision shapes. The dream engine will need a separate foreground asset if players must strike it.

The images are approximately 16:9: all are 1672 pixels wide; heights are 940 or 941 pixels. Exact dimensions are recorded in manifest.json. Preserve aspect ratio when scaling; use cover/crop or letterboxing for other screen shapes. They are opaque backgrounds, not transparent sprite assets.

Each is one representative environment scene, not a full level-length panorama. The edges are not verified to tile seamlessly. Separate parallax layers, animated fires/fog, tilesets, and in-engine integration are not included. Images were visually checked for scene identity, absence of characters/UI/text, and space for gameplay. Readability with actual combat sprites still needs in-engine testing.

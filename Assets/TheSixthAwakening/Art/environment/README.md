# The Sixth Awakening — Animated environmental objects

Initial environment art pack: 28 props, four frames each, 112 individual transparent PNG frames across seven settings covering all six levels and the real throne-room finale. Generated with the built-in image generation tool using the finished backgrounds as visual references. Exact prompts are in DESIGN_PROMPTS.md.

## Contents by setting

### Shifting Prison

- hanging-chain — loop, 6 fps; top anchor.
- wall-brazier — loop, 6 fps; center anchor.
- cell-gate — play once and hold, 8 fps; bottom anchor.
- chain-winch — loop, 6 fps; center anchor.

### Burning Rooftops

- torn-banner — loop, 6 fps; top anchor.
- smoking-chimney — loop, 6 fps; bottom anchor.
- burning-timber — loop, 6 fps; bottom anchor.
- roof-hatch — play once and hold, 8 fps; bottom anchor.

### Hall of Stolen Voices

- voice-vessel — loop, 6 fps; bottom anchor.
- resonance-bell — loop, 6 fps; top anchor.
- voice-cabinet — play once and hold, 8 fps; bottom anchor.
- resonator-wheel — loop, 6 fps; center anchor.

### Corrupted Sanctuary

- candle-stand — loop, 6 fps; bottom anchor.
- sanctuary-drape — loop, 6 fps; top anchor.
- false-reunion-shrine — loop, 6 fps; bottom anchor.
- sanctuary-door — play once and hold, 8 fps; bottom anchor.

### Mirror Arena

- cracked-mirror — loop, 6 fps; bottom anchor.
- orbiting-shards — loop, 6 fps; center anchor.
- guardian-standard — loop, 6 fps; top anchor.
- mirror-seal — play once and hold, 8 fps; center anchor.

### Collapsing Dream Arena

- floating-rubble — loop, 6 fps; center anchor.
- reality-rift — loop, 6 fps; center anchor.
- dream-chain — loop, 6 fps; top anchor.
- fracturing-pillar — play once and hold, 8 fps; bottom anchor.

### Real Throne Room

- dream-engine — loop, 6 fps; bottom anchor.
- throne-brazier — loop, 6 fps; bottom anchor.
- captive-shackle — play once and hold, 8 fps; top anchor.
- royal-banner — loop, 6 fps; top anchor.

## Import

- Individual frames: SET/PROP/PROP_00.png through PROP_03.png, each 512 x 512 RGBA.
- Individual animation strips: SET/PROP-strip.png, 2048 x 512, four horizontal 512 x 512 cells.
- Set atlases: SET-atlas.png, 2048 x 2048, four rows and four columns, 512 x 512 cells, no spacing or atlas margin. Each row is one prop in the order listed above.
- SET-atlas.json contains named rectangles, frame durations, animation tags, loop settings, and suggested normalized pivots. Coordinates are measured from the top-left. JSON is a generic metadata format, not an engine-specific controller.
- Pivots: x=0.5; y=0.125 for top-mounted props, y=0.875 for floor props, y=0.5 for centered props. Fine-tune placement for your scene.
- Source sheets are retained as SET-source.png. Use packed exports rather than slicing the source sheets.
- preview.html plays the four props for a selected setting, with pause, replay, step and background controls. Open it locally alongside its folders. Browser playback has not been visually verified in this environment.

## Art and gameplay notes

Animations are short prototype cycles. Generated frame-to-frame detail and anchor variation may require final polish. Loop frames may need extra in-between drawings for slower smooth motion. Gate, hatch, cabinet, door, seal, pillar and shackle animations are one-time actions; hold their last frame. Other props loop. The first prison chain is intended to meet a ceiling attachment and its source top link meets the image edge.

These are visual assets. Doors do not yet change collision, fire does not yet damage players, bells have no sound, and the dream engine does not yet have damage or destruction behavior. Add interaction code, collision shapes, separate lighting and sound in the game. Decorative frames can also be used as static props. No tilesets or level placement are included.

## Verification

All source sheets were visually reviewed. Props were extracted as connected artwork rather than cut through a rigid source grid; nearby detached parts were retained and edge alpha preserved locally. All packed frames must be populated and fit within padded cells. Source frames and selected strips were visually inspected, including an opening gate, moving cloth, orbiting shards, and the dream engine. Export dimensions, frame counts, alpha transparency, metadata and distinct animation-frame hashes are checked in the packing audit. In-engine behavior has not been tested.

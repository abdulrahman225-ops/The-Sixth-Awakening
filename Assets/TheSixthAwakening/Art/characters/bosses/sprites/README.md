# Five dream boss animation sets

Included: Iron Gaoler, Red Huntress, ancient Collector, Mercy Priest, Mirror Knight.
Dream Sovereign is intentionally excluded; use the existing Veyr assets as requested.
The rejected young Collector generation was deleted. The approved ancient Collector concept remains in the parent directory.

## Files
- NAME-atlas.png: transparent RGBA atlas, 1536 x 2304 pixels.
- NAME-atlas.json: frame rectangles, frame tags, provisional durations, and playback hints.
- NAME/: 24 individual transparent PNG frames, each 384 x 384.
- NAME-source.png: generated source sheet, retained for reference. Use the packed atlas or individual frames for import.
- preview.html: local browser animation player with pause, frame stepping, speed and facing controls.
- PROMPTS.md: exact prompts used with built-in image generation.
- pack-sprites.ps1 and SpriteExtract.cs: repeatable asset extraction/packing utilities.
- validation.json: packing validation results.

## Frame layout
Four columns, six rows; zero spacing, zero atlas margin.
Read each row left to right.
1. idle (frames 0–3), loop.
2. walk (frames 4–7), loop.
3. run (frames 8–11), loop.
4. jump (frames 12–15), play once.
5. attack (frames 16–19), play once.
6. death (frames 20–23), play once and hold final frame.

Start at 8 frames per second (125ms per frame), then tune for gameplay.
Frame origin suggested at x=192, y=336 measured from the top-left.
All characters face right. Horizontal flip can provide left-facing playback.
In an engine that supports image sequences, import the four PNGs for an action in filename order.
For grid slicing, use 384 x 384 cells on the packed atlas, not the source sheet.
Atlas metadata is generic JSON, not an engine-specific animation controller.
Movement, collision shapes, damage windows, AI, jump velocity, and spell projectiles are separate gameplay work.

## Validation and limitations
All five sources were checked for RGBA and transparent background samples. All 24 main poses per sheet were identified as separate connected shapes. Whole poses were extracted instead of cutting through a fixed source grid. Small nearby components and edge alpha were associated with each pose, and frames were packed without scaling into padded cells. Every frame passed the padded-boundary check. Exported atlas dimensions, individual-frame count, transparency, and JSON frame count were checked.

Visual spot checks covered the source sheets and selected exports, including the formerly clipped Gaoler chain weapon. Browser playback could not be visually verified in this environment: localhost access failed and the browser disallowed local file URLs. Open preview.html locally to review.

These are importable prototype animation assets, not a finished animation system. Four-frame cycles are deliberately short, some idle changes are subtle, and generated poses can have costume/weapon variation or anchor drift. Final motion polish and combat timing need in-engine review. Only one attack per boss is included; extra attack phases, hurt, parry, and special attacks are not supplied.

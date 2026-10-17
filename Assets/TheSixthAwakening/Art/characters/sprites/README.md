# Transparent animation drafts

Three RGBA PNG sheets, each 1254 x 1254 pixels. Alpha channel and fully transparent corner verified. Generated with the built-in image-generation tool from the concept art. See PROMPTS.md for full prompts.

Each sheet proposes six frames per animation, facing right. Rows top to bottom: idle, walk, run, jump, attack, death. Nominal equal-grid cell size is 209 x 209. Suggested review speeds: idle 6 fps, walk 8 fps, run 10 fps, jump 8 fps, attack 12 fps, death 8 fps. Loop only idle/walk/run; trigger other actions and hold or transition at completion. Engine movement and collision are separate from these images.

Open preview.html in a browser to play, pause, step through, and flip the draft animations against a checkerboard background. The checkerboard belongs to the preview, not the PNG files.

## Known limitations

These are review/prototype assets, not finished production animations. Generated spacing is imperfect: some attack blades cross adjacent cells, some Veyr poses reach row boundaries, and body anchors drift. Equal-grid slicing therefore clips some details or includes neighboring fragments. Walk cycles also need motion polish. A generation repair pass did not reliably resolve this. Cleanup and aligned per-frame export are still required before game integration. No engine import or runtime validation has been performed. Mira is uninjured; parry, hurt, and story-specific animation sets are not included.

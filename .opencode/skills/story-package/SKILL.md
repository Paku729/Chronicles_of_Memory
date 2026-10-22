---
name: Story Package SDK
description: Chronicles of Memory platform architecture — Story Package format, Twine importer pipeline, Story Runtime command set
---

## Platform layers (Novel Platform SDK)

1. **Story Package** — a `.zip` archive with everything a novel needs. Structure:

   ```text
   story.json          — the script
   metadata.json       — id, title, author, version, languages
   backgrounds/        — images
   characters/         — sprites
   music/              — audio
   localized/          — per-lang overrides
   ```

   Version the package (semver in `metadata.json`); newer packages must not break older ones.

2. **Story Importer** — Twine HTML export → `story.json`. Always validate passages: every jump target/choice destination must resolve to an existing passage.

3. **Story Runtime** — in-game engine that packages:
   - commands: `bg`, `show`, `text`, `choice`, `music`, `sfx`
   - variable/flag management
   - saves (local, serverless for MVP)
   - localization (ru default, then en, then others)

4. **Novel Client** — the Unity app (this repo). Home carousel → read screen → shop/profile later.
5. **Manager Panel** — future web panel; out of scope for game code.

## story.json conventions (draft)

```jsonc
{
  "passages": {
    "<id>": {
      "commands": [
        { "type": "bg",     "asset": "school_morning" },
        { "type": "show",   "character": "lisa", "sprite": "normal", "position": "center" },
        { "type": "text",   "speaker": "Лиза", "lines": ["Привет!"] },
        { "type": "choice", "options": [ { "label": "…", "goto": "<id>" } ] },
        { "type": "music",  "asset": "track_1", "loop": true },
        { "type": "sfx",    "asset": "door" }
      ],
      "next": "<id-or-null>"  // sequential flow when no player input
    }
  },
  "start": "<passage-id>"
}
```

- Asset fields are **logical names only** — the Runtime maps them to files inside the package.
- `choice.goto` must point to an existing passage (validated by the importer).
- `next` + trailing `choice`: only one should be set.

## Current code map

- `Assets/Scripts/Core/UIManager.cs` — screen stack + overlays + fades (`RegisterScreen`, `ShowScreen`, `RegisterOverlay`, `ShowOverlay`).
- `Assets/Scripts/Data/StoryData.cs` — current DTO for light carousel data (mock titles/genres/episodes).
- `Assets/Scripts/Data/StoryRepository.cs` — hardcoded mock source; to be replaced (Addressables or zip loader).
- `Assets/Scripts/UI/CarouselController.cs`, `StoryCard.cs`, `StoryPopupView.cs`, `NavButton.cs` — UI layer.

StoryData (carousel) is **not** the story.json script format — they feed different screens. Don't unify them blindly.

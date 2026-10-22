---
name: Unity Workflow
description: How to work with the NovelApp Unity editor via the unityMCP bridge — console checks, hierarchy paging, script compilation, play mode
---

## Bridge basics

- Unity instance: `NovelApp@8f85769216b495fa` (Unity 2022.3.62f3, transport: HTTP).
- Read editor state via resource `mcpforunity://editor/state`; list instances via `mcpforunity://instances`.
- Live tools advertise themselves: check resource `mcpforunity://custom-tools` first for project-specific tools.
- Salient tool groups: `read_console`, `manage_editor` (play mode), `manage_scene`, `manage_gameobject`, `manage_asset`, `manage_script`, `execute_code`, `batch_execute`, `manage_build`.

## Mandatory workflow

1. **Before changes:** read `mcpforunity://editor/state`. Skip issuing edits while `isCompiling` or `is_domain_reload_pending` is true.
2. **After creating/modifying any C# script:** poll the console (`read_console`, filter `Error`) until compilation finishes. Only then attach/use new component types.
3. **Large scenes/tools payloads:** page with `page_size` 50 max; follow `next_cursor` to the end.
4. **Console check before declaring done:** after any scene or code change, read console errors. "Done" means: compiled + no new errors + console looked at.
5. **Play mode:** run via `manage_editor` (enter play) rather than manual start, if needed; verify behavior via debug logs, then exit play mode.

## Scene specifics

- Active scene: `Assets/Scenes/SampleScene.unity`.
- Roots: `Main Camera`, `UIManager`, `Canvas` (screen children), `EventSystem`.
- Always keep `EventSystem` working after UI edits — StandaloneInputModule must remain.
- Prefabs live under `Assets/Prefabs` (create via `manage_prefabs` when reusing card/overlay layouts).

## Tools to prefer

- Prefer `manage_scene`/`manage_gameobject` reads over `execute_code` reflection when inspecting state.
- Use `batch_execute` for multi-step safe operations (many small commands) — caps at 25 commands per call.
- `execute_code` is a scalpel for anything not covered by dedicated tools; keep snippets short and side-effect-free where possible.

---
name: Git Flow
description: Commit/push conventions for NovelApp (GitHub origin, main branch)
---

## Repository

- Remote: `https://github.com/Paku729/Chronicles_of_Memory` (branch `main` tracks `origin/main`).
- Never commit `Library/`, `Temp/`, `obj/`, `.vs/`, `UserSettings/` — already covered by `.gitignore`; keep it that way.

## Commit style

- Short, lowercase-or-pascal, imperative, no prefixes like `feat:` — mirror of history:
  `init project`, `new UI`, `Story Popup`, `Update README.md`.
- One logical change per commit (e.g. "add mcp bridge", "fix script encoding").
- Before commit, `git status` and stage exactly the intended files; avoid `git add -A` when unrelated changes (like package manifest edits) are pending.

## Push

- User pushes via `git push` after commit. Confirm with the user before pushing if a commit is large or destructive; small incremental commits can be pushed right away.
- Never force-push. If remote diverged, `git pull --rebase` first.

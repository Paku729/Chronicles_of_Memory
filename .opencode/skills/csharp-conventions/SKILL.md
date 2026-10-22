---
name: C# Conventions
description: Unity/C# code style rules for NovelApp — naming, folders, encoding (UTF-8!), Unity version specifics
---

## Target

- Unity **2022.3 LTS** (APIs must exist in 2022.3 — don't use Unity 6-only APIs like `FindObjectsSortMode` differently, UI Toolkit runtime-heavy stuff, Awaitable).
- UI is uGUI + TextMeshPro. Keep it that way.

## Encoding — critical

- All `.cs` files MUST be **UTF-8 (with or without BOM)**. Files saved in Windows-1251 show mojibake for Russian strings in Unity/console.
- When writing Russian text into scripts, write/save as UTF-8. If a file shows `�` replacement chars or latin-case garbage where Cyrillic should be, treat it as an encoding bug and fix it.

## Style (mirrors existing code)

```csharp
public class CarouselController : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;   // private serialized fields in camelCase

    private void Awake() { LoadStories(); }           // one class per file, file name == class name
}
```

- PascalCase for classes, methods, properties; camelCase for fields; `_`-free prefixes.
- `[Header("...")]` to group inspector fields; `[SerializeField]` for private inspector-exposed data (no public fields unless it's a plain DTO like `StoryData`).
- Events: `public event Action<T>` named `OnSomething`, invoked in the owning class.
- Comments in Russian are fine (team language), but text must be stored in UTF-8.
- Keep folder structure: `Assets/Scripts/{Core,Data,UI}` — new layers follow the same one-folder-per-domain pattern.
- Managers run as scene singletons (`UIManager` uses `DontDestroyOnLoad`); don't add new singletons unless there's a cross-scene reason.

## Plain DTOs

- Data holder classes (`StoryData`) may use public serializable fields, `[Serializable]`, no logic.
- Repository-style static sources are placeholders; real data must come from Story Packages/Addressables later.

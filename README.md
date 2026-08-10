# 📖 Chronicles of Memory

**A platform for creating, publishing, and reading interactive visual novels.**

This is not just an app. It's an **ecosystem** that allows writers to create stories in Twine, managers to publish them through a web panel, and players to read them without updating the app.

---

## 🧬 Platform Architecture (Novel Platform SDK)



### 📦 1. Story Package — Standard Novel Archive
- A single `.zip` format containing everything: script, sprites, backgrounds, music, Live2D (for future versions).
- **Versioning:** package updates don't break older versions.
- **Structure:** `story.json`, `metadata.json`, `/backgrounds`, `/characters`, `/music`, `/localized`.

### ⚙️ 2. Story Importer — Twine → JSON Converter
- Parses HTML export from Twine.
- Generates `story.json` with dialogues, choices, and transitions.
- Automatic script validation (checks scene references).

### 📖 3. Story Runtime — Playback Engine
- Loads and unpacks `StoryPackage`.
- Handles commands: `bg`, `show`, `text`, `choice`, `music`, `sfx`.
- Manages variables, flags, and saves.
- Supports localization (Russian, English, and more).

### 📱 4. Novel Client — Mobile Application (Unity)
- Home screen (story carousel).
- Reading screen (dialogues, choices, animations).
- Shop, profile, Echo-chat (in development).
- Local saves (serverless — for MVP).

### 🌐 5. Manager Panel — Web Publishing Panel (planned)
- Upload HTML/packages.
- Manage story catalog.
- Completion statistics (in the future).



# 📖 Хроники Памяти (Chronicles of Memory)

**Платформа для создания, публикации и чтения интерактивных визуальных новелл.**

Это не просто приложение. Это **экосистема**, которая позволяет сценаристам писать истории в Twine, менеджерам — публиковать их через веб-панель, а игрокам — читать без обновлений приложения. 

---

## 🧬 Архитектура платформы (Novel Platform SDK)

Проект построен по принципам **модульности и расширяемости**. Мы разделили платформу на 5 независимых частей, которые могут развиваться отдельно:


### 📦 1. Story Package — стандарт архива новеллы
- Единый формат `.zip`, содержащий всё: сценарий, спрайты, фоны, музыку, Live2D (для будущих версий).
- **Версионность:** обновление пакета не ломает старые версии.
- **Структура:** `story.json`, `metadata.json`, папки `/backgrounds`, `/characters`, `/music`, `/localized`.

### ⚙️ 2. Story Importer — конвертер Twine → JSON
- Парсит HTML-экспорт из Twine.
- Генерирует `story.json` с диалогами, выборами и переходами.
- Автоматическая валидация сценария (проверка ссылок на сцены).

### 📖 3. Story Runtime — движок воспроизведения
- Загружает и распаковывает `StoryPackage`.
- Обрабатывает команды: `bg`, `show`, `text`, `choice`, `music`, `sfx`.
- Управляет переменными, флагами и сохранениями.
- Поддерживает локализацию (русский, английский и далее).

### 📱 4. Novel Client — мобильное приложение (Unity)
- Главный экран (карусель с историями).
- Экран чтения (диалоги, выборы, анимации).
- Магазин, профиль, Echo-чат (в разработке).
- Локальные сохранения (без сервера — для MVP).

### 🌐 5. Manager Panel — веб-панель публикации (планируется)
- Загрузка HTML/пакетов.
- Управление каталогом историй.
- Статистика по прохождениям (в будущем).

---
---

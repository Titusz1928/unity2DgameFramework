<p align="center">
  <img src="Samples~/FullFramework/UI/Sprites/logo1.png" width="300" alt="Framework Logo">
</p>

# TitusGames Framework

A reusable Unity framework for 2D Android games, providing ready-made systems for Scene Management, Windows/UI, Localization, and Audio.  
The framework is designed to help you rapidly build consistent projects without rewriting core systems every time.

---

## 🚀 Features

- **BootManager** – initializes all other managers automatically; no need to manually place managers in new scenes.  
- **SceneManager** – easily navigate between scenes and exit the game.  
- **WindowManager** – create UI windows and open them from buttons.  
- **LocalizationManager** – localize any TextMeshPro UI element with JSON files.  
- **AudioManager** – play SFX and music stored inside the framework’s audio folder.

---

## 📂 Project Structure

_Framework/<br>
│<br>
├── Managers/<br>
│ ├── Boot.cs<br>
│ ├── SceneManager.cs<br>
│ ├── WindowManager.cs<br>
│ ├── LocalizationManager.cs<br>
│ └── AudioManager.cs<br>
│<br>
├── UI/<br>
│ └── Windows/<br>
│ └── (Your Window Scripts + Prefabs)<br>
│<br>
├── Resources/<br>
│ ├── Audio/<br>
│ │ └── (Place your .wav / .mp3 files here)<br>
│ └── Languages/<br>
│ └── (JSON files for each language)<br>
│<br>
├── Scenes/<br>
│<br>
└── ThirdParty/<br>
└── Utils/<br>
└── MiniJSON (for localization)<br>



---

# 🎮 SceneManager

The `SceneManager` lets you load scenes by name and exit the game.

### ✔ Load a Scene
```csharp
SceneManager.Instance.LoadScene("GameScene");
```

### ✔ Exit the Game
```csharp
SceneManager.Instance.ExitGame();
```

### ✔ Add a Button That Loads a Scene

Add a Unity Button to the UI.

Add the UI_SceneButton script to it.

Enter the scene name into the Scene Name field.

Add an OnClick() event.

Drag the script into the event.

Select:

SceneManager → LoadScene()

# 🪟 WindowManager

Windows are UI panels (prefabs) that you can open and close dynamically.

### ✔ How to Create a New Window

Create a new UI panel.

You can start by duplicating or modifying the WindowBase prefab.

Customize it however you like.

### ✔ Add a Button That Opens a Window

Add a Button.

Add the UI_OpenWindow script.

Drag your window prefab into its field.

Add an OnClick() event.

Drag the UI_OpenWindow script into the event.

Select:

WindowManager → Open()

# 🌍 LocalizationManager

Add localization to any text using simple JSON files.


### Example JSON (eng.json)
```json
{
  "play_button": "Play",
  "settings_button": "Settings",
  "exit_button": "Exit"
}
```

### ✔ How to Localize a Text Element

Add the LocalizedText component to a TextMeshPro UI object.

Enter the Key from your JSON file (example: "play_button").

The text will automatically update based on the selected language.

### ✔ Add or Edit Localization

Open the JSON files in:

_Framework/Resources/Languages


Add or modify your keys.

Save — the manager automatically reloads them at runtime.

# 🔊 AudioManager

Plays audio clips by name using files stored inside:

_Framework/Resources/Audio/

### ✔ Add a New Sound

Place .wav or .mp3 files into _Framework/Resources/Audio.

Use the filename without extension when calling Play.

### ✔ Play a Sound
AudioManager.Instance.Play("click");

### ✔ Play Music
AudioManager.Instance.PlayMusic("background");

# 🧪 Example Workflow

To create a simple menu:

A Play button → loads the game scene using SceneManager.

A Settings button → opens a UI window through WindowManager.

All button text → localized via JSON keys.

Button click sounds → played using AudioManager.

# 📘 License

MIT License — free for personal and commercial use.


---

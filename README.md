# Fix: Swipe gesture to open Lunar Console is not working on Unity 6

This repository contains a small **C# workaround** for the  
> “Swipe gesture to open Lunar Console is not working on Unity 6”  
issue from the official [lunar-unity-console](https://github.com/SpaceMadness/lunar-unity-console) plugin.

On **Unity 6 (6000.x)** builds the native **two-finger swipe down** gesture no longer opens Lunar Console, even though:
- `LunarConsole.Show()` still works
- the debug overlay and logs appear as expected

This script re-implements the gesture on the C# side, without modifying the native Android/iOS plugins.

---

## Requirements

- **Unity 6 (6000.x)**
- **[Lunar Console](https://github.com/SpaceMadness/lunar-unity-console)** already installed in the project
- Target platform: **Android** or **iOS**

---

## Installation

### Option 1 – Unity package

1. Download the latest `.unitypackage` from the **Releases** page:  
   https://github.com/RimuruDev/Swipe-gesture-to-open-Lunar-Console-is-not-working-on-Unity-6-Solved/releases
2. Import it into your Unity project.

### Option 2 – Single script

1. Copy `LunarSwipeOpener.cs` from:  
   https://github.com/RimuruDev/Swipe-gesture-to-open-Lunar-Console-is-not-working-on-Unity-6-Solved/blob/main/LunarSwipeOpener.cs
2. Add it to your project (any folder under `Assets/`).

---

## Usage

1. Make sure Lunar Console is properly installed and enabled  
   (`Window → Lunar Mobile Console → Enable`).
2. Add the **LunarSwipeOpener** component to a GameObject in your **first scene**  
   (for example, near your `Lunar Console` prefab or bootstrap object).
3. (Optional) Enable `IsImmortalGameObject` if you want the object to survive `LoadScene` calls.
4. Build a **Development Build** for Android or iOS.
5. On the device, perform a **two-finger swipe down from the top area of the screen** – Lunar Console should open.

---

## Configuration

`LunarSwipeOpener` exposes a few simple settings:

- `MinSwipeDistance`  
  Minimum distance (in pixels) the center of the two touches has to travel downwards to trigger the gesture.

- `StartZoneHeight`  
  Normalized height of the top screen region where the gesture must start  
  (e.g. `0.25` = top 25% of the screen).

- `IsImmortalGameObject`  
  If `true`, the object is detached from its parent and marked with `DontDestroyOnLoad`, so the gesture works across all scenes.

---

## Limitations

- This script **does not fix** the native Lunar Console plugin.  
  It only emulates the gesture using C# and `LunarConsole.Show()`.
- If/when Lunar Console is officially updated for Unity 6 and the native swipe gesture works again, you can simply remove this script.

---

## Credits

- Original plugin: **[Lunar Console](https://github.com/SpaceMadness/lunar-unity-console)** by SpaceMadness  
- Workaround script: **RimuruDev / AbyssMoth**

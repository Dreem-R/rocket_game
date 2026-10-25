# Rocket Lander

A 2D physics-based lunar lander game made in **Unity 6**. Pilot a rocket through rocky caverns, collect coins and fuel, and touch down softly on a landing pad. The slower and straighter you land, and the riskier the pad, the higher your score.

<!-- TODO: add a gameplay GIF here, e.g. ![Gameplay](Docs/gameplay.gif) -->

## About this project

I started this project by following [Code Monkey's](https://unitycodemonkey.com/) Unity lander course. I'm now extending it on my own with extra polish ("game feel"), new mechanics and more levels, with the goal of releasing it on itch.io. The [roadmap](#roadmap) shows what's done and what's next.

## Gameplay

- **Thrust and rotate** to fight gravity and line up your descent.
- **Land softly and upright.** Coming in too fast or at too steep an angle makes you crash.
- **Pick your pad.** Each landing pad has a score multiplier (x1, x5, x10), and the high-value pads are harder to reach.
- **Manage your fuel.** Thrusting burns fuel; fuel canisters refill it.
- **Collect coins** for bonus points.
- **Carry cargo.** Hover inside a pickup zone until its ring fills, and a crate attaches to the rocket on a rope. Its swinging weight pulls you around, so every move needs more care. Delivering cargo is in progress.
- Your landing score is based on impact speed and landing angle, multiplied by the pad's multiplier.

## Controls

| Action | Keyboard | Gamepad | Mobile |
|---|---|---|---|
| Thrust | `W` / `↑` | D-pad up / left stick up | On-screen joystick |
| Rotate | `A` `D` / `←` `→` | D-pad / left stick | On-screen joystick |
| Pause | `Esc` | — | — |

## Technical highlights

- **Event-driven architecture.** The `Lander` exposes C# events (`OnLanded`, `OnCoinPickup`, `OnStateChange`, …). Visuals, audio, UI and the game manager subscribe to these events instead of referencing each other, so each system can be changed or removed on its own.
- **Data-driven levels.** Each level is a prefab with its own start position, camera framing and zoom level. The `GameManager` spawns the current level, and progress and total score persist across scene reloads.
- **Physics-based landing check.** A `Rigidbody2D` handles flight. When the rocket lands, it uses the collision's relative velocity and a dot product between the rocket's up vector and world up to decide between a soft landing, a speed crash and an angle crash.
- **New Input System.** One set of input actions supports keyboard (WASD and arrow keys), gamepad and a mobile touch joystick.
- **Cinemachine camera.** Before launch, the camera previews the whole level, then smoothly zooms in to follow the rocket.
- **Game feel.** Coins squash and stretch (Feel) and flash white on their own random timers, so they never animate in sync. Collecting a coin shrinks it away, releases a sparkle burst and shows a score popup that pops in with squash and stretch (a PrimeTween sequence). Coins and fuel float with a gentle idle wiggle.
- **Custom Shader Graph shaders.**
  - `SpriteFlash` blends any sprite toward a flash color. Each object gets its own material copy, which is cleaned up when the object is destroyed.
  - `RadialFill` draws a clockwise progress ring. It calculates each pixel's angle in the shader, so it needs no gradient texture.
- **Cargo system.**
  - One `CargoZone` component handles both pickup and drop zones, switched with an enum.
  - Each cargo type is a ScriptableObject asset holding its icon, prefab and delivery score, so adding a new type needs no code.
  - Zones update their color and icon in the editor through `OnValidate`, which makes building levels by hand quick.
  - The rope is a `DistanceJoint2D` in "max distance only" mode, so it can go slack, drawn with a `LineRenderer`. Each cargo positions and attaches itself.
- **Iteration tools.** Sandbox scenes and a popup tester script let effects be tuned live in Play mode. The Lander only depends on the input and sound managers, so it can be tested in any scene.
- **Audio.** Separate sound and music managers with adjustable volume. Music keeps playing smoothly between scenes.

## Roadmap

- [x] Core lander physics, fuel and scoring
- [x] Three levels with multiplier landing pads
- [x] Keyboard, gamepad and touch input
- [x] Main menu, pause menu, game over screen
- [x] Sound effects, music and volume settings
- [x] Coin idle animation (squash and stretch)
- [x] White hit-flash shader (Shader Graph)
- [x] Coin pickup feedback: shrink, sparkle particles, score popup
- [x] Idle wiggle on coins and fuel
- [x] Cargo pickup zone with a hover timer, radial progress ring and rope physics
- [ ] Cargo delivery zones and delivery score
- [ ] Cargo collects coins and fuel, and breaks on hard impact
- [ ] Key cargo that opens doors
- [ ] Low-fuel warning and crash/landing effects
- [ ] Level select with saved best scores
- [ ] Replace course art and audio with original assets
- [ ] WebGL build on itch.io

## Built with

- Unity 6 (6000.0.71f1), Universal Render Pipeline (2D Renderer), Shader Graph
- Cinemachine 3
- Unity Input System
- [Feel](https://feel.moremountains.com/) by More Mountains
- [PrimeTween](https://github.com/KyryloKuzyk/PrimeTween) by Kyrylo Kuzyk

## Running the project

1. Clone the repository and open it in **Unity 6000.0.71f1** or a later 6000.0 version.
2. Feel and PrimeTween are Asset Store packages, so they're **not included in this repository** for licensing reasons. Import both from the Unity Asset Store before opening the scenes.
3. Open `Assets/Scenes/MainMenuScene.unity` and press Play.

## Project structure

```
Assets/
├── Scripts/            # Gameplay, UI and audio scripts
│   ├── Lander.cs           # Flight physics, fuel, landing check, events
│   ├── GameManager.cs      # Level loading, score, timer, pause
│   ├── LanderVisuals.cs    # Thruster particles and explosion
│   ├── LanderAudio.cs      # Thruster sound
│   ├── CoinVisual.cs       # Coin idle animation, flash and pickup effects
│   ├── ScorePopup.cs       # Floating score popup (PrimeTween sequence)
│   ├── Cargo/              # Cargo, CargoZone, CargoTypeSO
│   ├── Test/               # Tools for tuning effects in sandbox scenes
│   └── ...
├── Data/
│   └── Cargo/          # Cargo type assets (ScriptableObjects)
├── Shaders/            # SpriteFlash and RadialFill shader graphs
├── Prefabs/
│   └── Levels/         # One prefab per level
└── Scenes/             # Main menu, game, game over and sandbox scenes
```

## Credits

- Course and starting project: [Code Monkey](https://unitycodemonkey.com/). The current art, sounds and music come from the course and will be replaced before release.
- Font: VT323 by Peter Hull, under the SIL Open Font License.
- Feel by More Mountains, PrimeTween by Kyrylo Kuzyk.

## Author

**Dreem-R**: [github.com/Dreem-R](https://github.com/Dreem-R)

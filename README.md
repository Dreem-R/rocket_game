# 2D Rocket Game

A dynamic and engaging rocket-themed game built with **Unity**, featuring advanced graphics programming with custom shaders.

## Overview

Rocket Game is a Unity-based game project that combines gameplay mechanics with sophisticated rendering techniques. The project leverages modern graphics programming with ShaderLab and HLSL for high-quality visual effects.

## 🎮 Features

- **Advanced Graphics**: Custom shader implementation using ShaderLab and HLSL for visually stunning effects
- **Unity Integration**: Built with the latest Unity engine capabilities
- **Optimized Performance**: Efficient rendering pipeline for smooth gameplay
- **Custom Shaders**: Hand-crafted shaders for unique visual aesthetics

## 🛠️ Technology Stack

| Technology | Usage | Percentage |
|-----------|-------|-----------|
| **ShaderLab** | Shader programming and graphics | 45.8% |
| **C#** | Game logic and mechanics | 44.7% |
| **HLSL** | Direct GPU programming | 9.5% |

## 📁 Project Structure

```
rocket_game/
├── Assets/                 # Game assets (sprites, models, animations)
├── Packages/              # Unity package dependencies
├── ProjectSettings/       # Unity project configuration
├── .gitattributes        # Git attributes configuration
├── .gitignore            # Git ignore rules
└── .vsconfig             # Visual Studio configuration
```

## Getting Started

### Prerequisites
- **Unity** 6.0
- **Visual Studio** (recommended for C# scripting)
- **Git** (for version control)

### Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/Dreem-R/rocket_game.git
   cd rocket_game
   ```

2. **Open the project in Unity**:
   - Launch Unity Hub
   - Click "Open Project"
   - Navigate to the cloned directory and select it
   - Unity will load the project

3. **Install dependencies**:
   - Any required packages will be automatically loaded from the `Packages` folder

## Usage

1. **Launching the Game**:
   - Open the project in Unity
   - Navigate to your main scene in the Assets folder
   - Press the Play button in the Unity Editor to test

2. **Building for Distribution**:
   - Go to `File → Build Settings`
   - Select your target platform
   - Click "Build and Run"

## 📚 Project Components

### Shaders
The project includes custom shaders written in ShaderLab and HLSL for specialized visual effects:
- Located primarily in the `Assets` folder
- Optimized for real-time rendering
- Contribute to the game's unique visual identity

### Scripts
C# scripts handle all game mechanics, including:
- Player controls and rocket behavior
- Physics and collision detection
- Game state management
- UI interactions

### Assets
Contains all game resources:
- 3D models and materials
- Textures and sprites
- Audio files
- Prefabs and scenes

## Development

### Building and Testing
- Use the Unity Editor Play mode for rapid iteration
- Test on target platforms using Build and Run feature
- Monitor performance in the Profiler window

### Contributing
To contribute to this project:
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/YourFeature`)
3. Commit your changes (`git commit -m 'Add YourFeature'`)
4. Push to the branch (`git push origin feature/YourFeature`)
5. Open a Pull Request

## 🔧 Configuration

### Visual Studio Integration
The `.vsconfig` file ensures proper Visual Studio integration with Unity. If you're using Visual Studio, ensure you have:
- Visual Studio Tools for Unity extension installed
- Proper IntelliSense setup for C#

### Git Configuration
The `.gitattributes` file is configured for proper handling of Unity-specific files to avoid merge conflicts.

## 📖 Documentation

For more information on:
- **Unity**: Visit [Unity Documentation](https://docs.unity3d.com/)
- **ShaderLab**: Check [Unity Shader Documentation](https://docs.unity3d.com/Manual/SL-Introduction.html)
- **HLSL**: See [Microsoft HLSL Documentation](https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl)

## Troubleshooting

### Common Issues

**Project won't load in Unity**:
- Ensure your Unity version matches the project requirements
- Check that all dependencies in the Packages folder are valid
- Verify the `.vsconfig` file matches your development environment

**Shader compilation errors**:
- Check that your GPU supports the required shader model
- Verify HLSL syntax in shader files
- Review the Console window in Unity for specific error messages

**Performance issues**:
- Profile using Unity's built-in Profiler
- Optimize shader complexity
- Reduce polygon count on models

## 📄 License

This project is currently unlicensed. To add a license, visit [choosealicense.com](https://choosealicense.com)

## 👤 Author

**Dreem-R** - [@Dreem-R](https://github.com/Dreem-R)

## 🙌 Support

If you encounter any issues or have questions about the project:
- Open an [Issue](https://github.com/Dreem-R/rocket_game/issues) on GitHub
- Check existing issues for solutions
- Provide detailed reproduction steps when reporting bugs

## 🔗 Links

- **Repository**: [Dreem-R/rocket_game](https://github.com/Dreem-R/rocket_game)
- **GitHub Profile**: [Dreem-R](https://github.com/Dreem-R)

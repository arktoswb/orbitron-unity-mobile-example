# Orbitron Unity Mobile Example

A Unity mobile application demonstrating the use of Orbitron fonts across different font weights, optimized for both Android and iOS platforms.

## Overview

This project showcases the Orbitron font family in a mobile-friendly Unity interface. The app displays different font weights (Regular, Medium, SemiBold, Bold, ExtraBold, and Black) with interactive controls for font size adjustment and background color changes.

## Features

- **Multi-weight Font Display**: Shows all available Orbitron font weights
- **Mobile-Optimized UI**: Touch-friendly interface with pinch-to-zoom functionality  
- **Cross-Platform**: Supports both Android and iOS builds
- **Interactive Controls**: Buttons for font size adjustment and background changes
- **Responsive Design**: Adapts to different screen orientations and sizes

## Prerequisites

- **Unity 2023.3.0f1 or later** (LTS recommended)
- **macOS** for iOS development
- **Xcode** (latest version) for iOS builds
- **Android Studio** or **Unity Android Build Support** for Android builds

## Font Assets

The project includes the complete Orbitron font family:
- `Orbitron-VariableFont_wght.ttf` (Variable font)
- `Orbitron-Regular.ttf`
- `Orbitron-Medium.ttf` 
- `Orbitron-SemiBold.ttf`
- `Orbitron-Bold.ttf`
- `Orbitron-ExtraBold.ttf`
- `Orbitron-Black.ttf`

All fonts are licensed under the SIL Open Font License 1.1.

## Project Setup

### 1. Clone and Open in Unity

```bash
git clone https://github.com/arktoswb/orbitron-unity-mobile-example.git
cd orbitron-unity-mobile-example
```

Open the project in Unity Hub:
- Click "Open" in Unity Hub
- Navigate to the cloned directory
- Select the project folder

### 2. Verify Project Structure

Ensure the following structure exists:
```
Assets/
├── Fonts/           # Orbitron font files (.ttf)
├── Scenes/          # MainScene.unity
├── Scripts/         # C# scripts (OrbitronFontDemo.cs, MobileUIController.cs)
└── UI/              # UI assets (if any)
```

### 3. Configure Build Settings

1. Open **File > Build Settings**
2. Add `Assets/Scenes/MainScene.unity` to **Scenes in Build**
3. Select your target platform (Android or iOS)

## Android Development Setup

### Prerequisites for Android

1. **Install Unity Android Build Support**:
   - Unity Hub > Installs > [Your Unity Version] > Add Modules
   - Check "Android Build Support"
   - Include "Android SDK & NDK Tools" and "OpenJDK"

2. **Configure Android Settings**:
   - **File > Build Settings > Android**
   - Click **Switch Platform**
   - **Player Settings > Android Settings**:
     - **Company Name**: `OrbitronExample`
     - **Product Name**: `Orbitron Unity Mobile Example`  
     - **Package Name**: `com.OrbitronExample.OrbitronUnityMobileExample`
     - **Minimum API Level**: 22 (Android 5.1)
     - **Target API Level**: Automatic (Highest Installed)

### Building for Android

1. **Connect Android Device** (recommended) or set up emulator
2. **Enable Developer Options** on device:
   - Go to **Settings > About phone**
   - Tap **Build number** 7 times
   - Go to **Settings > Developer options**
   - Enable **USB debugging**

3. **Build and Run**:
   ```bash
   # From Unity Editor
   File > Build Settings > Build and Run
   ```
   
   Or build APK:
   ```bash
   File > Build Settings > Build
   ```

### Running on Android Emulator

1. **Install Android Studio**: Download from [developer.android.com](https://developer.android.com/studio)

2. **Create AVD (Android Virtual Device)**:
   ```bash
   # Open Android Studio
   Tools > AVD Manager > Create Virtual Device
   # Choose device (e.g., Pixel 4)
   # Select system image (API 29+ recommended)
   # Finish setup
   ```

3. **Start Emulator**:
   ```bash
   # From Android Studio AVD Manager
   Click ▶️ next to your AVD
   
   # Or from command line (if Android tools in PATH)
   emulator -avd Pixel_4_API_29
   ```

4. **Deploy to Emulator**:
   - In Unity: **File > Build Settings > Build and Run**
   - Unity will automatically detect running emulator

## iOS Development Setup

### Prerequisites for iOS

1. **macOS with Xcode**: 
   - Install latest Xcode from Mac App Store
   - Accept Xcode license: `sudo xcodebuild -license accept`

2. **Unity iOS Build Support**:
   - Unity Hub > Installs > [Your Unity Version] > Add Modules  
   - Check "iOS Build Support"

### Building for iOS

1. **Configure iOS Settings**:
   - **File > Build Settings > iOS**
   - Click **Switch Platform**
   - **Player Settings > iOS Settings**:
     - **Bundle Identifier**: `com.OrbitronExample.OrbitronUnityMobileExample`
     - **Target minimum iOS Version**: 12.0
     - **Target Device**: iPhone + iPad
     - **Architecture**: ARM64

2. **Build Xcode Project**:
   ```bash
   File > Build Settings > Build
   # Choose output folder (e.g., "iOS-Build")
   ```

3. **Open in Xcode**:
   ```bash
   # Navigate to build folder
   open iOS-Build/Unity-iPhone.xcodeproj
   ```

### Running on iOS Simulator

1. **In Xcode**:
   - Select simulator device from device menu (e.g., "iPhone 14 Pro")
   - **Product > Run** (⌘R)

2. **Alternative - Command Line**:
   ```bash
   # List available simulators
   xcrun simctl list devices
   
   # Boot a simulator (example)
   xcrun simctl boot "iPhone 14 Pro"
   
   # Install and run (from Xcode project directory)
   xcodebuild -project Unity-iPhone.xcodeproj -scheme Unity-iPhone -destination 'platform=iOS Simulator,name=iPhone 14 Pro' -configuration Release
   ```

### Running on iOS Device

1. **Apple Developer Account**: Required for device deployment
2. **In Xcode**:
   - Connect iOS device via USB
   - **Window > Devices and Simulators**
   - Trust device if prompted
   - Select your device from device menu
   - **Product > Run** (⌘R)

## Usage Instructions

### App Controls

- **Font + / Font - buttons**: Adjust font size
- **Background button**: Cycle through background colors
- **Pinch gesture** (mobile): Zoom font size in/out
- **Scroll**: Navigate through font samples

### Font Samples

The app displays each Orbitron weight with:
- Font weight name (Regular, Medium, etc.)
- Sample text showing character variety
- Consistent sizing for easy comparison

## Troubleshooting

### Common Unity Issues

**"Fonts not displaying"**:
- Verify fonts are in `Assets/Fonts/` directory
- Check font import settings in Inspector
- Ensure font names match script references

**"Build fails"**:
- Clear Unity cache: `Unity > Preferences > External Tools > Clear Cache`
- Reimport all assets: `Assets > Reimport All`
- Check Console for specific error messages

### Android Issues

**"Unable to install APK"**:
- Enable "Install from Unknown Sources" in Android settings
- Check USB debugging is enabled
- Try `adb devices` to verify device connection

**"App crashes on startup"**:
- Check Android Logcat: `Window > Analysis > Android Logcat`
- Verify minimum API level compatibility
- Clear app data and reinstall

### iOS Issues

**"Code signing error"**:
- Set up Apple Developer account in Xcode
- Configure automatic signing in project settings
- Verify bundle identifier is unique

**"Simulator won't boot"**:
```bash
# Reset simulator
xcrun simctl erase all
xcrun simctl boot "iPhone 14 Pro"
```

## Project Structure

```
orbitron-unity-mobile-example/
├── Assets/
│   ├── Fonts/                  # Orbitron TTF files
│   │   ├── Orbitron-Regular.ttf
│   │   ├── Orbitron-Medium.ttf
│   │   └── ... (other weights)
│   ├── Scenes/
│   │   └── MainScene.unity     # Main app scene
│   ├── Scripts/
│   │   ├── OrbitronFontDemo.cs # Font display logic
│   │   └── MobileUIController.cs # Mobile UI controls
│   └── UI/                     # UI prefabs/assets
├── ProjectSettings/            # Unity project configuration
├── Packages/                   # Package dependencies
└── README.md                   # This file
```

## License

- **Code**: MIT License
- **Orbitron Fonts**: SIL Open Font License 1.1 (see `fonts/Orbitron/OFL.txt`)

## Contributing

1. Fork the repository
2. Create feature branch: `git checkout -b feature-name`
3. Commit changes: `git commit -am 'Add feature'`
4. Push branch: `git push origin feature-name`
5. Submit pull request

## Support

For issues and questions:
- Check Unity Console for error messages
- Review device logs (Android Logcat / Xcode Console)
- Ensure all prerequisites are properly installed
- Verify font files are correctly imported in Unity

---

**Happy mobile font development! 🎨📱**
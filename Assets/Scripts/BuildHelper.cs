using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;

/// <summary>
/// Helper class for building the Orbitron font demo for different platforms
/// </summary>
public class BuildHelper
{
#if UNITY_EDITOR
    [MenuItem("Orbitron Demo/Build Android")]
    public static void BuildAndroid()
    {
        string[] scenes = { "Assets/Scenes/MainScene.unity" };
        
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenes;
        buildPlayerOptions.locationPathName = "Builds/Android/OrbitronDemo.apk";
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;
        
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;
        
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }
        
        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }
    
    [MenuItem("Orbitron Demo/Build iOS")]
    public static void BuildIOS()
    {
        string[] scenes = { "Assets/Scenes/MainScene.unity" };
        
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenes;
        buildPlayerOptions.locationPathName = "Builds/iOS";
        buildPlayerOptions.target = BuildTarget.iOS;
        buildPlayerOptions.options = BuildOptions.None;
        
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;
        
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("iOS Xcode project generated successfully");
            Debug.Log("Open the generated Xcode project in: Builds/iOS/Unity-iPhone.xcodeproj");
        }
        
        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("iOS build failed");
        }
    }
    
    [MenuItem("Orbitron Demo/Configure Android Settings")]
    public static void ConfigureAndroidSettings()
    {
        PlayerSettings.companyName = "OrbitronExample";
        PlayerSettings.productName = "Orbitron Unity Mobile Example";
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.OrbitronExample.OrbitronUnityMobileExample");
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel22;
        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;
        
        Debug.Log("Android settings configured successfully");
    }
    
    [MenuItem("Orbitron Demo/Configure iOS Settings")]
    public static void ConfigureIOSSettings()
    {
        PlayerSettings.companyName = "OrbitronExample";
        PlayerSettings.productName = "Orbitron Unity Mobile Example";
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, "com.OrbitronExample.OrbitronUnityMobileExample");
        PlayerSettings.iOS.targetOSVersionString = "12.0";
        PlayerSettings.iOS.targetDevice = iOSTargetDevice.iPhoneAndiPad;
        
        Debug.Log("iOS settings configured successfully");
    }
#endif
}
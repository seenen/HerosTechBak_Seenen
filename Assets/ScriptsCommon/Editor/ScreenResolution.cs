using UnityEngine;
using System.Collections;
using UnityEditor;

public class ScreenResolution : EditorWindow
{
    [@MenuItem("CUSTOM/Screen Resolution")]
    private static void Init()
    {
        ScreenResolution window = (ScreenResolution)GetWindow(typeof

        (ScreenResolution), true, "ScreenResolution");
        window.Show();
    }

    // 显示窗体里面的内容 
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Screen Resolution");
        GUILayout.EndHorizontal();

        GUILayout.Label(" ");
        if (GUILayout.Button("1024 x 768 iPad 1,iPad2,iPad mini(4:3)"))
        {
            PlayerSettings.defaultScreenWidth = 1024;
            PlayerSettings.defaultScreenHeight = 768;
        }
        if (GUILayout.Button("960 x 640 iPhone 4/4S，iPod Touch 4(3:2)"))
        {
            PlayerSettings.defaultScreenWidth = 960;
            PlayerSettings.defaultScreenHeight = 640;
        }
        if (GUILayout.Button("1136 x 640  iPhone 5，iPod Touch 5(16:9)"))
        {
            PlayerSettings.defaultScreenWidth = 1136;
            PlayerSettings.defaultScreenHeight = 640;
        }
        if (GUILayout.Button("1920 x 1080  Samgsun 5s(16:9)"))
        {
            PlayerSettings.defaultScreenWidth = 1920;
            PlayerSettings.defaultScreenHeight = 1080;
        }
        if (GUILayout.Button("2048 x 1536  New iPad，iPad 4(4:3)"))
        {
            PlayerSettings.defaultScreenWidth = 2048;
            PlayerSettings.defaultScreenHeight = 1536;
        }
        if (GUILayout.Button("1260 x 840  Custom(3:2)"))
        {
            PlayerSettings.defaultScreenWidth = 1260;
            PlayerSettings.defaultScreenHeight = 840;
        }
        if (GUILayout.Button("1120 x 840  Custom(4:3)"))
        {
            PlayerSettings.defaultScreenWidth = 1120;
            PlayerSettings.defaultScreenHeight = 840;
        }

        if (GUILayout.Button("800 x 480  Custom(5:3)"))
        {
            PlayerSettings.defaultScreenWidth = 800;
            PlayerSettings.defaultScreenHeight = 480;
        }

        if (GUILayout.Button("Clear PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }


}

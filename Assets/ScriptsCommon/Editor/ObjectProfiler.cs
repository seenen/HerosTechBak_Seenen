using UnityEngine;
using System.Collections;
using UnityEditor;

public class ObjectProfiler : EditorWindow
{
    [@MenuItem("CUSTOM/Object Profiler")]
    private static void Init()
    {
        ObjectProfiler window = (ObjectProfiler)GetWindow(typeof

        (ObjectProfiler), true, "ObjectProfiler");
        window.Show();
    }

    // 显示窗体里面的内容 
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("ObjectProfiler");
        GUILayout.EndHorizontal();

        GUILayout.Label(" ");
        if (GUILayout.Button("Objects"))
        {
            int countactiveInHierarchy = 0;

            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
                if (go.activeInHierarchy)
                    countactiveInHierarchy++;

            Debuger.Log("Count = " + allObjects.Length.ToString() + " activeInHierarchy Count = " + countactiveInHierarchy.ToString());
        }
        if (GUILayout.Button("960 x 640 iPhone 4/4S，iPod Touch 4(3:2)"))
        {
        }
        if (GUILayout.Button("1136 x 640  iPhone 5，iPod Touch 5(16:9)"))
        {
        }
        if (GUILayout.Button("1920 x 1080  Samgsun 5s(16:9)"))
        {
        }
        if (GUILayout.Button("2048 x 1536  New iPad，iPad 4(4:3)"))
        {
        }
        if (GUILayout.Button("1260 x 840  Custom(3:2)"))
        {
        }
        if (GUILayout.Button("1120 x 840  Custom(4:3)"))
        {
        }

        if (GUILayout.Button("800 x 480  Custom(5:3)"))
        {
        }

        if (GUILayout.Button("Clear PlayerPrefs"))
        {
        }
    }


}

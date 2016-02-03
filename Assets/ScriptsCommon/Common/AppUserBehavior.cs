using UnityEngine;
using System.Collections;

public class AppUserBehavior : MonoBehaviour
{
    public bool paused;

    void OnApplicationPause(bool pauseStatus)
    {
        paused = pauseStatus;

        Debuger.Log("AppUserBehavior.OnApplicationPause ");

        ABFileLog.Write("AppUserBehavior", LogType.Log, "AppUserBehavior.OnApplicationPause " + pauseStatus.ToString());
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.Save();

        Debuger.Log("AppUserBehavior.OnApplicationQuit ");

        ABFileLog.Write("AppUserBehavior", LogType.Log, "AppUserBehavior.OnApplicationQuit");
    }

    void OnApplicationFocus(bool focusStatus)
    {
        paused = focusStatus;

        Debuger.Log("AppUserBehavior.OnApplicationFocus ");

        ABFileLog.Write("AppUserBehavior", LogType.Log, "AppUserBehavior.OnApplicationFocus " + focusStatus.ToString());
    }

    static string sProfiler = string.Empty;
    public static void GameBehavior(string log)
    {
        Debuger.Log("AppUserBehavior.OnApplicationFocus ");

        UpdateProfiler();

        ABFileLog.Write("AppUserBehavior", LogType.Log, sProfiler + log);

    }

    Rect windowRect = new Rect(0.02f * Screen.width, 0.02f * Screen.height, Screen.width - (2 * 0.02f * Screen.width), Screen.height - (2 * 0.02f * Screen.height));

    void OnGUI()
    {
        return;

        windowRect = GUILayout.Window(456, windowRect, AppSceneCountWindow, "AppSceneCount");
    }

    float delayTime = 1;

    void AppSceneCountWindow(int windowID)
    {
        if (GUILayout.Button(sProfiler, GUILayout.ExpandWidth(true)))
        {
            UpdateProfiler();
        }

        // Set the window to be draggable by the top title bar
        GUI.DragWindow(new Rect(0, 0, 10000, 20));

        if (delayTime > 0)
        {
            delayTime -= Time.deltaTime;

            return;

        }

        delayTime = 1;

        UpdateProfiler();

    }

    static private void UpdateProfiler()
    {
        sProfiler = string.Empty;
#if ENABLE_PROFILER
        sProfiler += "\t";
        sProfiler += Profiler.usedHeapSize / 1024 / 1024;
        sProfiler += "\t";
        sProfiler += Profiler.GetMonoHeapSize() / 1024 / 1024;
        //sProfiler += "[Allocated Mono heap size]" + Profiler.GetMonoHeapSize() / 1024 / 1024 + "MBytes";
        sProfiler += "\t";
        sProfiler += Profiler.GetMonoUsedSize() / 1024 / 1024;
        //sProfiler += "[Mono used size]" + Profiler.GetMonoUsedSize() / 1024 / 1024 + "MBytes";
        sProfiler += "\t";
#endif
    }
}

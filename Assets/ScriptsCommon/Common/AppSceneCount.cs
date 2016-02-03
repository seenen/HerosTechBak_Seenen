using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AppSceneCount : MonoBehaviour {

	// Use this for initialization
	void Start () {
#if ENABLE_PROFILER
        Profiler.logFile = "I:/mylog.log";
        Profiler.enabled = true;
#endif

    }

    // Update is called once per frame
    void Update () {
	
	}

    Rect windowRect = new Rect(0.02f * Screen.width, 0.02f * Screen.height, Screen.width - (2 * 0.02f * Screen.width), Screen.height - (2 * 0.02f * Screen.height));

    void OnGUI()
    {
        return;

        windowRect = GUILayout.Window(123, windowRect, AppSceneCountWindow, "AppSceneCount");
    }

    void AppSceneCountWindow(int windowID)
    {
#if ENABLE_PROFILER
        if (GUILayout.Button("Sample", GUILayout.ExpandWidth(true)))
        {
            Profiler.BeginSample("MyPieceOfCode");
            List<string> list = new List<string>();
            Profiler.EndSample();
        }

        if (GUILayout.Button("AddFramesFromFile", GUILayout.ExpandWidth(true)))
        {
            Profiler.AddFramesFromFile("I:/mylogframe.log");
        }
#endif

        if (GUILayout.Button("Objects", GUILayout.ExpandWidth(true)))
        {
            List<string> list = new List<string>();

            {
                list.Clear();

                GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
                foreach (GameObject go in allObjects)
                    list.Add(go.name);

                list.Sort();

                Debuger.Log("GameObject Count = " + allObjects.Length.ToString() );

                list.Insert(0, "******************** GameObject Count = " + allObjects.Length.ToString());

                WriteFile(list);

            }

            {
                list.Clear();

                Texture[] allObjects = UnityEngine.Object.FindObjectsOfType<Texture>();
                foreach (Texture go in allObjects)
                    list.Add("Size is " + go.width + " by " + go.height.ToString() + "\t" + go.name);

                list.Sort();

                Debuger.Log("Objects Texture Count = " + allObjects.Length.ToString());

                list.Insert(0, "******************** Objects Texture Count = " + allObjects.Length.ToString());

                WriteFile(list);

            }

            ////{
            ////    list.Clear();

            ////    Texture[] allObjects = UnityEngine.Resources.FindObjectsOfTypeAll<Texture>();
            ////    foreach (Texture go in allObjects)
            ////        list.Add("Size is " + go.width + " by " + go.height.ToString() + "\t" + go.name);

            ////    list.Sort();

            ////    Debuger.Log("Resources Texture Count = " + allObjects.Length.ToString());

            ////    list.Insert(0, "******************** Resources Texture Count = " + allObjects.Length.ToString());

            ////    WriteFile(list);

            ////}

            {
                list.Clear();

                Texture2D[] allObjects = UnityEngine.Object.FindObjectsOfType<Texture2D>();
                foreach (Texture2D go in allObjects)
                    list.Add("Size is " + go.width + " by " + go.height.ToString() + "\t" + go.name);

                list.Sort();

                Debuger.Log("Texture2D Count = " + allObjects.Length.ToString());

                list.Insert(0, "******************** Texture2D Count = " + allObjects.Length.ToString());

                WriteFile(list);

            }

            {
                list.Clear();

                Animation[] allObjects = UnityEngine.Object.FindObjectsOfType<Animation>();
                foreach (Animation go in allObjects)
                    list.Add(go.name);

                list.Sort();

                Debuger.Log("Animation Count = " + allObjects.Length.ToString());

                list.Insert(0, "******************** Animation Count = " + allObjects.Length.ToString());

                WriteFile(list);

            }

            {
                list.Clear();

                AudioSource[] allObjects = UnityEngine.Object.FindObjectsOfType<AudioSource>();
                foreach (AudioSource go in allObjects)
                    list.Add(go.name);

                list.Sort();

                Debuger.Log("AudioSource Count = " + allObjects.Length.ToString());

                list.Insert(0, "******************** AudioSource Count = " + allObjects.Length.ToString());

                WriteFile(list);

            }

            {
                list.Clear();

                SkinnedMeshRenderer[] allObjects = UnityEngine.Object.FindObjectsOfType<SkinnedMeshRenderer>();
                foreach (SkinnedMeshRenderer go in allObjects)
                    list.Add(go.name);

                list.Sort();

                Debuger.Log("SkinnedMeshRenderer Count = " + allObjects.Length.ToString());

                list.Insert(0, "******************** SkinnedMeshRenderer Count = " + allObjects.Length.ToString());

                WriteFile(list);

            }

            {
                list.Clear();

                MeshRenderer[] allObjects = UnityEngine.Object.FindObjectsOfType<MeshRenderer>();
                foreach (MeshRenderer go in allObjects)
                    list.Add(go.name);

                list.Sort();

                Debuger.Log("MeshRenderer Count = " + allObjects.Length.ToString());

                list.Insert(0, "******************** MeshRenderer Count = " + allObjects.Length.ToString());

                WriteFile(list);

            }

            {
                list.Clear();

                TextAsset[] allObjects = UnityEngine.Object.FindObjectsOfType<TextAsset>();
                foreach (TextAsset go in allObjects)
                    list.Add(go.name);

                list.Sort();

                Debuger.Log("TextAsset Count = " + allObjects.Length.ToString());

                list.Insert(0, "******************** TextAsset Count = " + allObjects.Length.ToString());

                WriteFile(list);

            }

            {
                list.Clear();

                MonoBehaviour[] allObjects = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>();
                foreach (MonoBehaviour go in allObjects)
                    list.Add(go.name + " <-> " + go.ToString() + " [by] " + (GetTopParent(go.gameObject)).name);

                list.Sort();

                Debuger.Log("MonoBehaviour Count = " + allObjects.Length.ToString());

                list.Insert(0, "******************** MonoBehaviour Count = " + allObjects.Length.ToString());

                WriteFile(list);

            }
        }

        // Set the window to be draggable by the top title bar
        GUI.DragWindow(new Rect(0, 0, 10000, 20));

    }

    private GameObject GetTopParent(GameObject obj)
    {
        GameObject top = obj;
        while (top.transform.parent != null)
        {
            top = top.transform.parent.gameObject;
        }

        return top;
    }

    void WriteFile(List<string> list)
    {
        foreach (string e in list)
        {
            ABFileLog.Write("AppSceneCount", LogType.Log, (string)e);
        }
    }
}

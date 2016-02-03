using UnityEngine;
using System.Collections;
using UnityEditor;

public class DisableShadow : EditorWindow
{
    [@MenuItem("CUSTOM/DisableShadow")]
    public static void Do()
    {
        foreach (Transform t in Selection.transforms)
        {
            Debuger.Log(t.GetComponents(typeof(Component)).Length);
            foreach (MonoBehaviour c in t.GetComponentsInChildren(typeof(MonoBehaviour)))
            {
                foreach (SkinnedMeshRenderer e in c.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true))
                {
                    e.castShadows = false;
                    e.receiveShadows = false;
                }

                foreach (MeshRenderer e in c.gameObject.GetComponentsInChildren<MeshRenderer>(true))
                {
                    e.castShadows = false;
                    e.receiveShadows = false;
                }

            }
        }
    }


}

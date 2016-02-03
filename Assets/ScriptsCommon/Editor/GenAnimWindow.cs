using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class GenAnimWindow : EditorWindow
{
    [MenuItem("Tool/GenAnimWindow", false)]
    public static void Main()
    {
        thisWindow = (GenAnimWindow)EditorWindow.GetWindow(typeof(GenAnimWindow));
        thisWindow.title = "GenAnimFile";
        thisWindow.Show();
    }
    static GenAnimWindow thisWindow;

    GameObject model;
    GameObject targetNode;
    Vector2 scrollPos = Vector2.zero;

    List<string> animStateNames = new List<string>();

    void OnGUI()
    {
        GameObject go = EditorGUILayout.ObjectField("Model", model, typeof(GameObject), true, GUILayout.ExpandWidth(true)) as GameObject;
        targetNode = EditorGUILayout.ObjectField("TargetNode", targetNode, typeof(GameObject), true, GUILayout.ExpandWidth(true)) as GameObject;

        if (go != model)
        {
            model = go;
            animStateNames.Clear();
            if(model != null)
            {
                Animation anim = model.animation;
                if (anim != null)
                {
                    foreach (AnimationState state in anim)
                    {
                        animStateNames.Add(state.name);
                    }
                }
            }
        }

        EditorGUILayout.LabelField("Animations", GUILayout.ExpandWidth(true));
        scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.ExpandWidth(true));
        foreach (string name in animStateNames)
        {
            if (GUILayout.Button(name, GUILayout.ExpandWidth(true)))
            {
                if (targetNode != null)
                    OnBtnAnimClick(name);
            }
        }

        GUILayout.EndScrollView();

    }

    AnimationCurve posXCurve;
    AnimationCurve posYCurve;
    AnimationCurve posZCurve;

    AnimationCurve rotXCurve;
    AnimationCurve rotYCurve;
    AnimationCurve rotZCurve;
    AnimationCurve rotWCurve;


    void OnBtnAnimClick(string animName)
    {
        Debuger.Log("OnBtnAnimClick " + animName);

        if (!Application.isPlaying)
        {
            EditorUtility.DisplayDialog("GenAnimWindown", "Must be Running", "Close");
            return;
        }

        if (model == null || targetNode == null || model.animation == null)
            return;

        Animation anim = model.animation;
        AnimationState state = anim[animName];
        if (state == null)
            return;
		
        state.time = 0f;
        anim.Play(animName, PlayMode.StopAll);

        AnimationClip genClip = new AnimationClip();
        genClip.name = animName + "_clip";

        posXCurve = new AnimationCurve();
        posYCurve = new AnimationCurve();
        posZCurve = new AnimationCurve();

        rotXCurve = new AnimationCurve();
        rotYCurve = new AnimationCurve();
        rotZCurve = new AnimationCurve();
        rotWCurve = new AnimationCurve();

        int nCount = (int)(state.length / (1 / 30.0f) + 0.5f);
        for (int i = 0; i <= nCount; i++ )
        {
            state.time = i * 1 / 30.0f;
            state.enabled = true;
            anim.Sample();
            state.enabled = false;
            AddOneKey(state.time);
        }


        genClip.SetCurve("", typeof(Transform), "localPosition.x", posXCurve);
        genClip.SetCurve("", typeof(Transform), "localPosition.y", posYCurve);
        genClip.SetCurve("", typeof(Transform), "localPosition.z", posZCurve);

        genClip.SetCurve("", typeof(Transform), "localRotation.x", rotXCurve);
        genClip.SetCurve("", typeof(Transform), "localRotation.y", rotYCurve);
        genClip.SetCurve("", typeof(Transform), "localRotation.z", rotZCurve);
        genClip.SetCurve("", typeof(Transform), "localRotation.w", rotWCurve);

        DuplicateAnimationClip(genClip);
        

		//bGenerating = true;

//         state.time = state.length / 2.0f;
//         state.enabled = true;
//         anim.Sample();
//         state.enabled = false;
    }

    void AddOneKey(float t)
    {
        //Vector3 pos = model.transform.worldToLocalMatrix.MultiplyPoint(targetNode.transform.position);

        Vector3 pos = targetNode.transform.position;

        posXCurve.AddKey(new Keyframe(t, pos.x));
        posYCurve.AddKey(new Keyframe(t, pos.y));
        posZCurve.AddKey(new Keyframe(t, pos.z));

        
        Quaternion q = targetNode.transform.rotation;

        rotXCurve.AddKey(new Keyframe(t, q.x));
        rotYCurve.AddKey(new Keyframe(t, q.y));
        rotZCurve.AddKey(new Keyframe(t, q.z));
        rotWCurve.AddKey(new Keyframe(t, q.w));
    }

    void DuplicateAnimationClip(AnimationClip sourceClip)
    {
        if (sourceClip != null)
        {
            string path = "Assets/Models/Effect/" + sourceClip.name + ".anim";

            AssetDatabase.CreateAsset(sourceClip, path);

            EditorUtility.DisplayDialog("GenAnimWindown", "Generate Anim " + sourceClip.name + " success!", "Close");
        }
    }
}

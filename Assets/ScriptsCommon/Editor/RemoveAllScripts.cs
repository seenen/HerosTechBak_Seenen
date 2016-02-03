using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public class RemoveAllScripts : EditorWindow
{
 
	[MenuItem("CUSTOM/Remove All Script Entries")]
	public static void Do() 
    {
	    foreach (Transform t in Selection.transforms) 
        {
	        Debuger.Log(t.GetComponents(typeof(Component)).Length);
            foreach (MonoBehaviour c in t.GetComponentsInChildren(typeof(MonoBehaviour)))
            {
	            if (c != null){
	                Debuger.Log("NULL");
	                // throw caution to the wind and destroy anyway!!! AHAHHAHAHAH!!!
	                GameObject.DestroyImmediate(c);
	                // awwww nothing happened.  still there.
	            }
	            else
	                Debuger.Log(c.GetType());
	        }
	    }
	}
}

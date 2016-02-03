using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public class RemoveMissingScripts : EditorWindow {
 
	[MenuItem("CUSTOM/Remove Missing Script Entries")]
	public static void Do() {
	    foreach (Transform t in Selection.transforms) {
	        Debuger.Log(t.GetComponents(typeof(Component)).Length);
	        foreach (Component c in t.GetComponents(typeof(Component))) {
	            if (c == null){
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

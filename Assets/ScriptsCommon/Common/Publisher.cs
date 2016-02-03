using UnityEngine;
using System.Collections;

public sealed class Publisher : MonoBehaviour 
{
    public string IP;
    public string Port;

    public string ID = "201307291150";

    static public Publisher instance;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    void OnGUI ()
    {
        GUI.Label(new Rect(0, Screen.height - 120, Screen.width, 40), "[µØÖ·:] " +IP + " [¶Ë¿Ú:] " + Port + "[°æ±¾ºÅ:] " + ID);
    }
}

using UnityEngine;

public class DetectLeaks : MonoBehaviour
{
    void OnGUI()
    {
        GUILayout.Space(300);

        if (GUILayout.Button("Unload Unused Assets"))
        {
            Resources.UnloadUnusedAssets();
        }

        if (GUILayout.Button("Mono Garbage Collect"))
        {
            System.GC.Collect();
        }

        if (GUILayout.Button("List Loaded Textures"))
        {
            ListLoadedTextures();
        }

        if (GUILayout.Button("List Loaded Skins"))
        {
            ListLoadedSkins();
        }

        if (GUILayout.Button("List Loaded Styles"))
        {
            ListLoadedStyles();
        }

        if (GUILayout.Button("List Loaded Sounds"))
        {
            ListLoadedAudio();
        }

        if (GUILayout.Button("List Loaded GameObjects"))
        {
            ListLoadedGameObjects();
        }

        if (GUILayout.Button("List Loaded Other"))
        {
            ListOther();
        }
    }

    public void Init()
    {

    }

    private void ListOther()
    {
        Object[] others = Resources.FindObjectsOfTypeAll(typeof(Object));
								
		if (others == null)
			return;

        string list = string.Empty;

        for (int i = 0; i < others.Length; i++)
        {
            if (others[i].name == string.Empty)
            {
                continue;
            }

            if (Profiler.GetRuntimeMemorySize(others[i]) > 100000)
                Debuger.Log(others[i].name + " " + Profiler.GetRuntimeMemorySize(others[i]));

            //if (!(others[i] is Texture))
            //    continue;
            //if (!(others[i] is GUISkin))
            //    continue;
            //if (!(others[i] is AudioClip))
            //    continue;
            //if (!(others[i] is GameObject))
            //    continue;

            //list += (i.ToString() + ". " + others[i].name + "\n");

            //if (i == 500)
            //{
            //    Debuger.Log(list);
            //    list = string.Empty;
            //}
        }

        Debuger.Log(list);
    }

    private void ListLoadedTextures()
    {
        Object[] textures = Resources.FindObjectsOfTypeAll(typeof(Texture));
						
		if (textures == null)
			return;

        string list = string.Empty;

        for (int i = 0; i < textures.Length; i++)
        {
            if (textures[i].name == string.Empty)
            {
                continue;
            }

            list += (i.ToString() + ". " + textures[i].name  + " size: " + Profiler.GetRuntimeMemorySize(textures[i]) + "\n");

            if (i == 500)
            {
                Debuger.Log(list);
                list = string.Empty;
            }
        }

        Debuger.Log(list);
    }

    private void ListLoadedSkins()
    {
        Object[] skins = Resources.FindObjectsOfTypeAll(typeof(GUISkin));
						
		if (skins == null)
			return;

        string list = string.Empty;

        for (int i = 0; i < skins.Length; i++)
        {
            if (skins[i].name == string.Empty)
            {
                continue;
            }

            list += (i.ToString() + ". " + skins[i].name + "\n");

            if (i == 500)
            {
                Debuger.Log(list);
                list = string.Empty;
            }
        }

        Debuger.Log(list);
    }

    private void ListLoadedStyles()
    {
        Object[] styles = Resources.FindObjectsOfTypeAll(typeof(GUIStyle));
		
		if (styles == null)
			return;

        string list = string.Empty;

        for (int i = 0; i < styles.Length; i++)
        {
            if (styles[i].name == string.Empty)
            {
                continue;
            }

            list += (i.ToString() + ". " + styles[i].name + "\n");

            if (i == 500)
            {
                Debuger.Log(list);
                list = string.Empty;
            }
        }

        Debuger.Log(list);
    }

    private void ListLoadedAudio()
    {
        Object[] sounds = Resources.FindObjectsOfTypeAll(typeof(AudioClip));
		
		if (sounds == null)
			return;
		
        string list = string.Empty;

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == string.Empty)
            {
                continue;
            }
            list += (i.ToString() + ". " + sounds[i].name + " size: " + Profiler.GetRuntimeMemorySize(sounds[i])  + "\n");
        }

        Debuger.Log(list);
    }

    private void ListLoadedGameObjects()
    {
        Object[] gos = Resources.FindObjectsOfTypeAll(typeof(GameObject));
				
		if (gos == null)
			return;

        string list = string.Empty;

        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].name == string.Empty)
            {
                continue;
            }
            list += (i.ToString() + ". " + gos[i].name + " size: " + Profiler.GetRuntimeMemorySize(gos[i]) +  "b\n");
			
        }

        Debuger.Log(list);
    }
}

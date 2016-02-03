using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundUtility : MFMonoBehaviour
{
    public static SoundUtility Uti;
    public AudioListener mListen;
    private bool soundEffect = false;

    protected override void MFStart()
    {
        base.MFStart();

        Uti = this;

        DontDestroyOnLoad(gameObject);
    }

    protected override void MFOnDestroy()
    {
        base.MFOnDestroy();

        Clean();

        Uti = null;
    }

    public static IEnumerator Clear()
    {
        AudioSource[] srcs = (AudioSource[])GameObject.FindObjectsOfType(typeof(AudioSource));

        foreach (AudioSource e in srcs)
        {
            AudioSource s = (AudioSource)e;
            s.Stop();
            s.clip = null;
        }

        yield return 1;
    }

    public static void Reload()
    {
        foreach(GameObject e in listAudioSource)
        {
            GameObject obj = (GameObject)e;

            if (obj == null)
                continue;

            AudioSource source = obj.GetComponent<AudioSource>();
            if (source != null)
                source.clip = null;
            source = null;

            GameObject.DestroyImmediate(obj);
            obj = null;
        }

        listAudioSource.Clear();

        AudioListener[] listen = (AudioListener[])GameObject.FindObjectsOfType(typeof(AudioListener));

        if (listen != null)
        {
            foreach (AudioListener e in listen)
            {
                AudioListener al = (AudioListener)e;

                if (al == null)
                    continue;

                GameObject.DestroyImmediate(al);
                al = null;
            }
        }

        if (Uti != null)
            Uti.mListen = Uti.gameObject.AddComponent<AudioListener>();

    }


    static GameObject attachObj;

    public static void AttachTo(GameObject root)
    {
        attachObj = root;
    }

    static List<GameObject> listAudioSource = new List<GameObject>();

    public static AudioSource Get2DAudioSource(string name)
    {
        GameObject obj = new GameObject(name);

        AudioSource source = obj.AddComponent<AudioSource>();
        source.mute = !Uti.soundEffect;
        source.playOnAwake = false;

        obj.transform.parent = Uti.mListen.gameObject.transform;
        obj.transform.position = Uti.mListen.transform.position;

        listAudioSource.Add(obj);

        return source;
    }

    /// <summary>
    /// 音效开关.（有些audioSource不受此控制，而是由EffectSoundController.cs控制）
    /// </summary>
    /// <param name="sound">true开启，fales关闭</param>
    public static void EnableSelfManageSoundEffect(bool sound)
    {
        Uti.soundEffect = sound;

        AudioSource[] sources = Uti.GetComponentsInChildren<AudioSource>(true);

        foreach (var source in sources)
        {
            // 战中背景音是个例外
            if (source.gameObject.name != "BattleBg")
                source.mute = !Uti.soundEffect;
        }
    }

    void Clean()
    {
        attachObj = null;
    }
}

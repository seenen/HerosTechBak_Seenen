using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResData
{
#region Count
    public static int mRef = 0;
    private static List<ResData> mReferenceList = new List<ResData>();
    public static string DumpAllResDataList()
    {
        string sout = string.Empty;

        for (int i = mReferenceList.Count - 1; i >= 0; --i)
        {
            sout += ((ResData)mReferenceList[i]).Log();
            sout += "\r\n";
        }

        return sout;
    }
#endregion

    string mResname;
    System.Type mType;

    public ResData(string resname, System.Type type)
    {
        mResname = resname;
        mType = type;

        Create();

        mRef++;
        mReferenceList.Add(this);

        mIsDispose = false;

    }

    private bool mIsDispose = false;

    public void Dispose()
    {
        Destory();

        if (mIsDispose)
            return;

        mIsDispose = true;

        mRef--;
        mReferenceList.Remove(this);

    }

    void Create()
    {
        if (mType == null)
        {
            Debuger.LogError(mType);

            return;
        }

        if (mType == typeof(GameObject))
        {
            _gameobject = (GameObject)GameObject.Instantiate(Resources.Load(mResname, typeof(GameObject)));
            _gameobject.name = mResname + "_gameobject";

        }
        else if (mType == typeof(TextAsset))
        {
            _textasset = (TextAsset)GameObject.Instantiate(Resources.Load(mResname, typeof(TextAsset)));
            _textasset.name = mResname + "_textasset";

        }

    }

    void Destory()
    {
        if (_gameobject != null)
        {
            Object.Destroy(_gameobject);
            _gameobject = null;

        }

        if (_textasset != null)
        {
            Object.Destroy(_textasset);
            _textasset = null;
        }
    }

    GameObject _gameobject;

    public GameObject gameobject
    {
        get
        {
            return _gameobject;
        }
    }

    TextAsset _textasset;

    public TextAsset textasset
    {
        get
        {
            return _textasset;
        }
    }

    public string Log()
    {
        return this.ToString() + " [resname:]" + mResname + "[type:]" + mType;
    }
}

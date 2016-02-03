using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class MFResources
{
    private static List<ResData> text = new List<ResData>();
    private static List<ResData> go = new List<ResData>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static ResData LoadTextAsset(string path)
    {
        Debuger.Log("MFResources.LoadTextAsset " + path);
        ResData data = null;
        try 
        {
           data = new ResData(path, typeof(TextAsset));
        }
        catch (System.Exception e)
        {
            string msg = "[InnerException:]" + e.InnerException +
                            "[Exception:]" + e.Message +
                            "[Source:]" + e.Source +
                            "[StackTrace:]" + e.StackTrace;

            
            //MFUIAlertManager.Instance.ShowNotifyView(path + "File Not Find", 2.0f);
            
        }
        
        text.Add(data);

        return data;

    }
    public static ResData LoadGameObject(string path)
    {
        Debuger.Log("MFResources.LoadGameObject " + path);

        ResData data = new ResData(path, typeof(GameObject));
        go.Add(data);

        return data;
    }

    public static void Clean()
    {
        foreach (ResData e in text)
        {
            ResData ta = (ResData)e;
            ta.Dispose();
            ta = null;
        }
        text.Clear();

        foreach (ResData e in go)
        {
            ResData ta = (ResData)e;
            ta.Dispose();
            ta = null;
        }
        go.Clear();
    }
}

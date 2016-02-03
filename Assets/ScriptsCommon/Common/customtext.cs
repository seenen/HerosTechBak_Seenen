using UnityEngine;
using System.Collections;

public class customtext
{
    void Start()
    {
        stringFormat("abc{0}def{1}ghi", 1, "4");
    }

    public static string stringFormat(string src, params object[] values)
    {
        ArrayList paramList = new ArrayList();

        //  解析带参数的字符串
        int len = src.Length;
        for (int i = 0; i < len; i++)
        {
            int bi = src.IndexOf("{", i);
            int ei = src.IndexOf("}", i + 1);
            if (bi >= 0 && ei > 1)
            {
                string param = src.Substring(bi, ei - bi + 1);
                paramList.Add(param);

                i = ei;
            }
        }

        //  判断
        if (paramList.Count != values.Length)
        {
            Debuger.LogError("Unvalid Param");

            return src;
        }

        //foreach (string e in paramList)
        //{
        //    Debuger.Log(e);
        //}

        //  重组字符串
        string ret = src;
        for(int j = 0 ; j < paramList.Count; ++j)
        {
            string e = (string)paramList[j];
            ret = ret.Replace(e, values[j].ToString());

        }

        return ret;

    }
}

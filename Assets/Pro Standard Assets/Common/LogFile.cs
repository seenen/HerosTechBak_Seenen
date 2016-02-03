using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;

public sealed class LogFile
{
    static private Dictionary<string, bool> LogFileDic = new Dictionary<string, bool>();

    //  写文件 
    static string WriteFile(string flt)
    {
#if UNITY_ANDROID
    #if UNITY_EDITOR
            string filePathName = "C:/" + flt.ToString() + ".txt";
    #else
        	string filePathName = "jar:file://" + Application.dataPath + "!/assets/"+ flt.ToString() + ".txt";
	#endif
#elif UNITY_STANDALONE_WIN
	#if UNITY_EDITOR
			string filePathName = "C:/" + flt.ToString() + ".txt";
	#else
			string filePathName = Application.dataPath + "/" + flt.ToString() + ".txt";
	#endif
#elif UNITY_IPHONE
	string filePathName = Application.dataPath + "/" + flt.ToString() + ".txt";
#else
	string filePathName = Application.dataPath + "/" + flt.ToString() + ".txt";
#endif
        if (LogFileDic.ContainsKey(flt))
        return filePathName;

        FileInfo TheFile = new FileInfo(filePathName);   
        if (TheFile.Exists)   
            TheFile.Delete();

        StreamWriter fileWriter = File.CreateText(filePathName); 
        fileWriter.Close();

        LogFileDic.Add(flt, true);

        return filePathName;
    }

    public static void Write(string filename, LogType lf, string log)
    {
#if UNITY_ANDROID
        return;
#endif
        string filePathName = WriteFile(filename);

        FileStream fs = new FileStream(filePathName, FileMode.Append);
        StreamWriter sw = new StreamWriter(fs);
        //开始写入 
        sw.WriteLine("");
        //
        string str = "[";
        str += System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ffff");//默认当天时间。
        str += "]";
        str += "\t";
        str += lf.ToString();
        str += "\t\t";
        str += log;

        sw.Write(str);
        //清空缓冲区 
        sw.Flush();
        //关闭流 
        sw.Close();
        fs.Close();
    }
 }

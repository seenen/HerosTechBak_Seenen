using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public enum PlayerEditorType
{
    Fighter = 0,
    MainCity = 1,
    Pure = 2,
    Net = 3,
    Selected = 4,
}

public sealed class GameMenuCommon 
{
    public static List<string> ReadTimeConfig(string path)  
    {  
//       	string path = "Assets/Scripts/Editor/Game/GameMenu_CreateMonstersCofnig.txt";
        List<string> lines = new List<string>();  
        if (File.Exists(path))  
        {  
            FileStream fs = new FileStream(path, FileMode.Open);  
            StreamReader sr = new StreamReader(fs);  
            while (!sr.EndOfStream)  
            {  
                string line = sr.ReadLine();  
                if (!string.IsNullOrEmpty(line) && !line.StartsWith("//"))  
				{
					Debuger.Log(line);
                    lines.Add(line);  
				}
            }  
            sr.Close();  
            fs.Close();  
            return lines;  
        }  
		
        return null;  
    }  

	public static string FindFile(string sPathName, string name)
	{
		Debuger.Log(sPathName + " " + name);
					
        //创建一个队列用于保存子目录//   
        Queue<string> pathQueue = new Queue<string>();
        //首先把根目录排入队中//
        pathQueue.Enqueue(sPathName);
        //开始循环查找文件，直到队列中无任何子目录//   
        while (pathQueue.Count > 0)
        {
            //从队列中取出一个目录，把该目录下的所有子目录排入队中//   
            DirectoryInfo diParent = new DirectoryInfo(pathQueue.Dequeue());
            foreach (DirectoryInfo diChild in diParent.GetDirectories())
                pathQueue.Enqueue(diChild.FullName);
            //查找该目录下的所有文件，依次处理//   
            foreach (FileInfo fi in diParent.GetFiles())
			{
                //Console.WriteLine(fi.FullName);
				if (fi.FullName.ToLower().EndsWith(name.ToLower()))
				{
					string reallyfullname = fi.FullName.ToLower().Replace("\\", "/");

					int index = reallyfullname.ToLower().IndexOf(sPathName.ToLower());
					if (index == -1)	Debuger.LogError(" Index Failed: " + reallyfullname + "   " + sPathName.ToLower());
					
					string ret = fi.FullName.ToLower().Substring(index);
					
					return ret;
				}
			}
        }

		return "";
	}

    static public GameObject FindAttach(GameObject root, string attachname)
    {
        if (root == null)
        {
            Debuger.LogWarning("root is null");

            return null;
        }

        Transform[] hingeJoints = root.GetComponentsInChildren<Transform>();
        /*foreach (Transform joint in hingeJoints)
        {
            if (joint.gameObject.name.ToLower() == attachname.ToLower())
                return joint.gameObject;
        }*/

        for (int i = 0; i < hingeJoints.Length; i++)
        {
            if (hingeJoints[i].gameObject.name == attachname)
                return hingeJoints[i].gameObject;
        }

        return null;
    }

    
}

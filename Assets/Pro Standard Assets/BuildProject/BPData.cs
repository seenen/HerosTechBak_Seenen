using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

namespace BuildProject
{

    public class BPData
    {
        #region Value
        Dictionary<string, string> m_DirBPData = new Dictionary<string, string>();

        string m_FilePath = Application.streamingAssetsPath + "/versionConfig.txt";
        #endregion

        #region Attr
        public string BP_PublishCoder
        {

            get
            {
				//return "版本 1.0";
                return GetValueString(PUPLISH_CODER_NAME);
            }
            set
            {
                string strinfo = string.Format("{0}={1}\n", PUPLISH_CODER_NAME, value);
                CreateFile(strinfo);

            }
        }
        public string BP_Time
        {
            get
            {
                return GetValueString(PUBLISH_TIME);
            }
            set
            {
                string strinfo = string.Format("{0}={1}\n", PUBLISH_TIME, value);
                CreateFile(strinfo);
            }
        }

        public string BP_PubilshVersion
        {
            get
            {
				//return "Ver:1.2";
                return GetValueString(PUBLISH_VERSION);

            }
            set
            {
                string strinfo = string.Format("{0}={1}\n", PUBLISH_VERSION, value);
                CreateFile(strinfo);

            }

        }

        //public bool BP_InterNet
        //{
        //    get
        //    {
        //        return GetValue(PUBLISH_INTERNET);
        //    }
        //    set
        //    {
        //        string strinfo = string.Format("{0}={1}\n", PUBLISH_INTERNET, value ? "1" : "0");
        //        CreateFile(strinfo);

        //    }
        //}

        public bool BP_Debug
        {
            get
            {
				return GetValue(PUBLISH_DEBUG);
            }
            set
            {
				string strinfo = string.Format("{0}={1}\n", PUBLISH_DEBUG, value ? "1" : "0");
                CreateFile(strinfo);
            }
        }

		
		public bool BP_Profiler
		{
			get
			{
				return GetValue(PUBLISH_PROFILER);
			}
			set
			{
				string strinfo = string.Format("{0}={1}\n", PUBLISH_PROFILER, value ? "1" : "0");
				CreateFile(strinfo);
			}
		}
		public bool BP_Fps
        {
            get
            {
                return GetValue(PUBLISH_FPS);
            }
            set
            {
                string strinfo = string.Format("{0}={1}\n", PUBLISH_FPS, value ? "1" : "0");
                CreateFile(strinfo);
            }
        }

        public bool BP_Log
        {
            get
            {
                return GetValue(PUBLISH_LOG);
            }
            set
            {
                string strinfo = string.Format("{0}={1}\n", PUBLISH_LOG, value ? "1" : "0");
                CreateFile(strinfo);
            }
        }

        public bool BP_Bin
        {
            get
            {
                return GetValue(PUBLISH_BIN);
            }
            set
            {
                string strinfo = string.Format("{0}={1}\n", PUBLISH_BIN, value ? "1" : "0");
                CreateFile(strinfo);
            }
        }

        public bool BP_ChatGM
        {
            get 
            {
                return GetValue(PUBLISH_CHATGM);
            }
            set
            {
                string strinfo = string.Format("{0}={1}\n", PUBLISH_CHATGM, value ? "1" : "0");
                CreateFile(strinfo);
            }
        }
        public string BP_Language
        {

            get
            {
                return GetValueString(PUBLISH_LANGUAGE);
            }
            set
            {
                string strinfo = string.Format("{0}={1}\n", PUBLISH_LANGUAGE, value);
                CreateFile(strinfo);

            }
        }
        #endregion

        #region Singleton
		public string GetUrl()
		{
			return m_FilePath;
		}

        private static BPData m_Instance = null;
        public static BPData GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new BPData();
            }
            return m_Instance;
        }
        #endregion

        #region File Handle
        void CreateFile(string info)
        {
            Debug.Log(m_FilePath);
            StreamWriter sw;
            FileInfo t = new FileInfo(m_FilePath);
            if (!t.Exists)
            {
                sw = t.CreateText();
            }
            else
            {

                sw = t.AppendText();
            }
            sw.WriteLine(info);
            sw.Close();
            sw.Dispose();
        }

		void LoadFile(StreamReader sr)
        {
			m_DirBPData.Clear();

            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Equals("") || line.Equals(string.Empty))
                {
                    continue;
                }
                string[] spritArray = line.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                if (spritArray.Length == 2)
                {
                    m_DirBPData.Add(spritArray[0], spritArray[1]);
                }
            }

            sr.Close();

            sr.Dispose();
        }

        IniParser mIni = null;

        public void GetBPData(string content)
		{
			mIni = new IniParser(content, true);
		}

        public void DeleteFile()
        {
            FileInfo t = new FileInfo(m_FilePath);
            if (t.Exists)
            {
                File.Delete(m_FilePath);
            }

        }

        //key
        const string PUPLISH_CODER_NAME = "BP_PublishCoder";
        const string PUBLISH_TIME       = "BP_PublishTime";
        const string PUBLISH_VERSION    = "BP_PublishVersion";
        const string PUBLISH_INTERNET   = "BP_PublishInterNet";
        const string PUBLISH_PROFILER   = "BP_Profiler";
		const string PUBLISH_DEBUG      = "BP_Debug";
		const string PUBLISH_FPS        = "BP_Fps";
        const string PUBLISH_LOG        = "BP_Log";
        const string PUBLISH_BIN        = "BP_Bin";
        const string PUBLISH_CHATGM     = "BP_GM";
        const string PUBLISH_LANGUAGE   = "BP_LANGUAGE";
        const string PUBLISH_IN         = "BP_IN";
        const string BP_OUT             = "BP_OUT";

        bool GetValue(string name)
        {
            string value = GetValueString(name);

            if (string.IsNullOrEmpty(value))
                return false;

            if (value.Equals("1")) return true;

            return false;
        }

        string GetValueString(string name)
        {
            if (mIni == null)
                return string.Empty;

            string value = mIni.GetSetting(name);

            return value;
        }
        #endregion

    }
}

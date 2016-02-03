using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

namespace BuildProject
{
    public class BPWindow : EditorWindow
    {
        [MenuItem("File/Build Settings... %B", false, 0)]
        public static void Main()
        {
            thisWindow = (BPWindow)EditorWindow.GetWindow(typeof(BPWindow));
            thisWindow.title = "BPWindow";
			thisWindow.GetPaltform(thisWindow);
            thisWindow.ShowPopup();
            thisWindow.Focus();
        }

        static IBPPlatform mFlatform = null;

		public void Head()
		{
            UpdateScene();
		}

        public void Load(IBPPlatform platform)
		{
			GetList();

            UpdateScene();

            mFlatform = platform;
            mFlatform.Head();
        }

        static BPWindow thisWindow;

        public GUISkin skin;

        Vector2 scrollPos = Vector2.zero;

		private void GetPaltform(BPWindow window)
		{
			#if UNITY_IPHONE
			mFlatform = new BPIOSPlatform();
			#elif UNITY_ANDROID
			mFlatform = new BPANDROIDPlatform();
			#else
			mFlatform = new BPWinPlatform();
			#endif
			if (window != null)
				window.Load(mFlatform);
		}

		private void GetList()
		{
			taScene = AssetDatabase.LoadAssetAtPath(BPConfig.cfgPath, typeof(TextAsset)) as TextAsset;
			
			List<string> lsData = BPTool.GetContent(new MemoryStream(taScene.bytes));
			
			listSceneDatas.Clear();
			foreach (string e in lsData)
			{
				SceneData data = new SceneData();
				data.scnenname = e;
				data.flag = true;
				
				listSceneDatas.Add(data);
			}
		}

        void OnGUI()
        {
            if (skin == null)
                skin = new GUISkin();

            GUILayout.Box("Build Project Tool By Zhang Shining Publish at 2014-1-11", skin.FindStyle("Box"), GUILayout.ExpandWidth(true));

            PackageInfo();

            if (mFlatform == null)
			{
				GetPaltform(thisWindow);
                return;
			}
            
            mFlatform.Setting();

//			GUILayout.Space(10);

			if (mFlatform != null)
				mFlatform.Draw();
			
			if (GUILayout.Button("Build", GUILayout.ExpandWidth(true)))
            {
                BuildCommon();
            }

            PackageScene();

        }

        #region Coder
        string coder = System.Environment.MachineName;
        string version = "0.0.0.0";
        string data = System.DateTime.Now.ToString();

        #region Net
        public enum NetSelect
        {
            In_Net,
            Out_Net,
        }

        //void NetLoad()
        //{
        //    netsdatas.Clear();
        //    netsdatas.Add(NetSelect.In_Net.ToString());
        //    netsdatas.Add(NetSelect.Out_Net.ToString());
        //}
        //List<string> netsdatas = new List<string>();
        //string nets = "";

        #endregion
        bool bProfiler = false;
		bool bDebug = true;
		bool bFps = true;
		bool bLog = true;
        bool bLoadBin = true;

        void PackageInfo()
        {
            GUILayout.Label("\t", GUILayout.ExpandWidth(true));

            EditorGUILayout.LabelField("Build Platform", Application.platform.ToString(), GUILayout.ExpandWidth(true));

            coder = EditorGUILayout.TextField("Build Coder", coder, GUILayout.ExpandWidth(true));

            version = EditorGUILayout.TextField("Build Version", version, GUILayout.ExpandWidth(true));

            EditorGUILayout.LabelField("Build Time", data, GUILayout.ExpandWidth(true));

            bProfiler = EditorGUILayout.Toggle("Profiler", bProfiler);

			bDebug = EditorGUILayout.Toggle("Debug", bDebug);

            bFps = EditorGUILayout.Toggle("Fps", bFps);

            bLog = EditorGUILayout.Toggle("Log", bLog);

            bLoadBin = EditorGUILayout.Toggle("Bin", bLoadBin);

        }
        #endregion

        #region Scene

        class SceneData
        {
            public bool flag;

            public string scnenname;

        }
        List<SceneData> listSceneDatas = new List<SceneData>();

        TextAsset taScene = null;

        void PackageScene()
        {
            GUILayout.Label("\t", GUILayout.ExpandWidth(true));

            if (listSceneDatas != null && listSceneDatas.Count > 0)
            {
                scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.ExpandWidth(true));

                foreach (SceneData e in listSceneDatas)
                {
                    e.flag = EditorGUILayout.Toggle(e.scnenname, e.flag);
                }

                GUILayout.EndScrollView();

            }

            UpdateScene();
        }
        #endregion

        static List<SceneData> listDatas = null;

        public void UpdateScene()
        {
			if (listSceneDatas.Count == 0)
			{
				GetList();

				return;
			}

			if (listSceneDatas == listDatas)
                return;

            listDatas = listSceneDatas;

            List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>();

            for (int i = 0; i < listSceneDatas.Count; ++i)
            {
                SceneData data = (SceneData)listSceneDatas[i];

                if (data.flag)
                {
                    EditorBuildSettingsScene sceneToAdd = new EditorBuildSettingsScene(data.scnenname, true);

                    scenes.Add(sceneToAdd);
                }
            }

            EditorBuildSettings.scenes = scenes.ToArray();

            AssetDatabase.Refresh();
        }

        void BuildCommon()
        {

            if (string.IsNullOrEmpty(coder))
            {
                EditorUtility.DisplayDialog("BuildProject", "No Name", "Close");

                return;
            }
			if (string.IsNullOrEmpty(version))
            {
                EditorUtility.DisplayDialog("BuildProject", "No version", "Close");

                return;
            }
			
			if(taScene == null)
			{
				EditorUtility.DisplayDialog("BuildProject", "No Scenes Import", "Close");

                return;
			}

            //bool InterNet = (nets == NetSelect.Out_Net.ToString()) ? false : true;
            //string sNet = InterNet ? "LocalLan" : "WLan";

            if (EditorUtility.DisplayDialog("", " Is Select Package", "OK", "Cancel"))
            {
                BPData.GetInstance().DeleteFile();
                BPData.GetInstance().BP_PublishCoder   = "[Name:] " + coder + " [From Address:] " + BPTool.GetMacAddress();
                BPData.GetInstance().BP_Time           = "[Publish time:] " + data;
                BPData.GetInstance().BP_PubilshVersion = "Ver:" + version;
				BPData.GetInstance().BP_Profiler       = bProfiler;
                BPData.GetInstance().BP_Fps            = bFps;
                BPData.GetInstance().BP_Log            = bLog;
				BPData.GetInstance().BP_Debug          = bDebug;
                BPData.GetInstance().BP_Bin            = bLoadBin;

                string path = EditorUtility.OpenFolderPanel("Select Saved Path", "", "Heros");

                if (!string.IsNullOrEmpty(path))
                {
                    List<string> listDatas = new List<string>();

                    foreach (SceneData e in listSceneDatas)
                    {
                        if (e.flag)
                            listDatas.Add(e.scnenname);
                    }

                    string outputfolder = mFlatform.Build(path, bProfiler, listDatas);

            		AssetDatabase.Refresh();

                    mFlatform.PostBuild(outputfolder);
                }

			}
        }
    }
}
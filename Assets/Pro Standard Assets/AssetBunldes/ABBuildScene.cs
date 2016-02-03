using UnityEngine;
using System.Collections;
using AssetBundleEditor;

public class ABBuildScene : MonoBehaviour 
{
    string url = null;

    IEnumerator Start()
    {
        yield return StartCoroutine(ReloadVersionInfo());

        yield return new WaitForEndOfFrame();

        string resname = "map_haiwan_001";

        string filePath = "/Region/" + resname + ".assetBundles";

        //  加载.
        AssetBundleUti.OnSceneFinishLoading += AssetBundleUtiCallBack;

        //string url = ProjectSystem.StreamingAssets + filePath;
#if UNITY_IOS
		url = ProjectSystem.PrefixPlatform + Application.streamingAssetsPath + "/IOS/scene/map_haiwan_001.assetBundles";
#else
		url = "file:///C:/map_haiwan_001.assetBundles";
#endif

		Debug.Log(url);

        if (!AssetBundleUti.GetABScene(url, resname, null, AssetBundleEditor.ABDataType.Normal))
        {

        }
    }

    #region 1、版本信息.
    public bool bDebug = false;
    public bool bFps = true;
    public bool bLog = false;
    public bool bLoadFromBin = true;
    public bool bChatGM = true;
    public string Version = string.Empty;

    IEnumerator ReloadVersionInfo()
    {
        Debuger.EnableLog = true;

        yield return 1;

        Debuger.Log("==================== VersionInfo Begin ====================");

        do
        {
            string versiontxt = string.Empty;

            string filePath = Application.streamingAssetsPath + "/versionConfig.txt";

            Debuger.Log("filePath " + filePath);

            bool success = false;

            if (filePath.Contains("://"))
            {
                WWW www = new WWW(filePath);

                yield return www;

                Debuger.Log("www" + www.error);

                if (string.IsNullOrEmpty(www.error))
                {
                    success = true;

                    versiontxt = www.text;
                }

                www.Dispose();
                www = null;
            }
            else
            {
                if (System.IO.File.Exists(filePath))
                {
                    versiontxt = System.IO.File.ReadAllText(filePath);
                    success = true;
                }
            }


            if (success)
            {
                BuildProject.BPData.GetInstance().GetBPData(versiontxt);

                Version += BuildProject.BPData.GetInstance().BP_PublishCoder + "\n";
                Version += BuildProject.BPData.GetInstance().BP_Time + "\n";
                Version += BuildProject.BPData.GetInstance().BP_PubilshVersion + "\n";
                Version += BuildProject.BPData.GetInstance().BP_Debug + "\n";

                Debuger.Log("[VersionInfo:]" + Version);

                bDebug = BuildProject.BPData.GetInstance().BP_Debug;

                bLog = BuildProject.BPData.GetInstance().BP_Log;

                bFps = BuildProject.BPData.GetInstance().BP_Fps;

                bLoadFromBin = BuildProject.BPData.GetInstance().BP_Bin;
                bChatGM = BuildProject.BPData.GetInstance().BP_ChatGM;

                //mConsole.SetEnable(bLog);

                Debuger.EnableLog = bLog;
                //mFpsCounter.enabled = bFps;
            }
            else
            {
                Debuger.LogError("[Get] " + versiontxt + " [Error] " + versiontxt);


                bDebug = false;

                bLog = false;

                //mConsole.SetEnable(bLog);

                //mFpsCounter.enabled = bFps;
            }

        }
        while (false);

        Debuger.Log("==================== VersionInfo End ====================");
    }

    #endregion


    ABDataScene data = null;

    private void AssetBundleUtiCallBack(string url, string resname, bool success, string errorcode = "")
    {
        Debuger.Log("AssetBundleUtiCallBack:" + url + " " + resname + " " + success);

        data = AssetBundleUti.GetObjectScene(url);

    }

    void OnGUI()
    {
        if (data == null)
            return;

        if (GUI.Button(new Rect(0, 400, 300, 300), "Release "))
            StartCoroutine(Release());

    }

    public IEnumerator Release()
    {
        yield return null;

        AssetBundleUti.CleanByScene();

    }

}

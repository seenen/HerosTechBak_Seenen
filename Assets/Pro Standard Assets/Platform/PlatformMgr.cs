using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlatformMgr //: MFMonoBehaviour
{
    ////public GameObject mIOSPlatform;

    ////public GameObject mAndroidPlugin;

    ////public GameObject mPcPlatform;

    ////IPlatform mIPlatform;

    //gu:网络状态数值意义  
    private enum NetworkStatus
    {
      NotConnect = 0,
      WiFi       = 1,
      Network2G  = 2,
      Network3G  = 3,
      Network4G  = 4,
   }

    private const string WIFI = "Wi-Fi";

    private const string NETWORK_2G = "2G";

    private const string NETWORK_3G = "3G";

    private const string NETWORK_4G = "4G";

////    public GameObject mPlatform = null;

////    // Use this for initialization
////    protected override void MFStart()
////    {
////        GameObject.DontDestroyOnLoad(gameObject);

////#if UNITY_IPHONE
////#if UNITY_EDITOR
////        mIPlatform = null;
////#else
////        mPlatform = (GameObject)GameObject.Instantiate(mIOSPlatform);
////        mIPlatform = (IPlatform)mPlatform.GetComponent<IosPlatform>();
////#endif
////#endif

////#if UNITY_ANDROID
////#if UNITY_EDITOR
////        mIPlatform = null;
////#else
////        mPlatform = (GameObject)GameObject.Instantiate(mAndroidPlugin);
////        mIPlatform = (IPlatform)mPlatform.GetComponent<AndroidPlugin>();
////#endif
////#endif

////#if UNITY_STANDALONE_WIN
////        mPlatform = (GameObject)GameObject.Instantiate(mPcPlatform);
////        mIPlatform = (IPlatform)mPlatform.GetComponent<PcPlatform>();
////#endif

////        Debuger.Log("mIPlatform =" + mIPlatform);
////    }

    public static IPlatform mIPlatform;

    ////protected override void MFAwake()
    ////{
    ////    mPlatformMgr.mIPlatform = null;

    ////    mPlatformMgr = this;
    ////}

    ////protected override void MFOnDestroy()
    ////{
    ////    if (mPlatform != null)
    ////        GameObject.Destroy(mPlatform);

    ////    mPlatformMgr = null;
    ////}

    public static string Cardname()
    {
		if (mIPlatform == null)
			return "";

        return mIPlatform.GetCardName();
    }

    public static double PluginGetUsedMemory()
    {
        if (mIPlatform == null)
            return 0;

        return mIPlatform.GetUsedMemory();
    }

    public static string PluginGetDeviceType()
    {
		if (mIPlatform == null)
			return "";
		
		return mIPlatform.GetDeviceType();
    }

	public static string PluginGetUUID()
	{
		if (mIPlatform == null)
			return "";

        return mIPlatform.GetMacAddress();

	}

    public static string PluginGetNetworkStatus()
    {
        if (mIPlatform == null)
            return "";
        

		string sPlatform = "";

        switch(mIPlatform.GetNetworkStatus())
        {
            case (int)NetworkStatus.NotConnect:
                sPlatform = "No Network";
                break;
            case (int)NetworkStatus.WiFi:
                sPlatform = WIFI;
                break;
            case (int)NetworkStatus.Network2G:
                sPlatform = NETWORK_2G;
                break;
            case (int)NetworkStatus.Network3G:
                sPlatform = NETWORK_3G;
                break;
            case (int)NetworkStatus.Network4G:
                sPlatform = NETWORK_4G;
                break;
            default:
                sPlatform = "Unknown Network";
                break;
        }

        return sPlatform;
    }

    public static string PluginGetSignalStrength()
    {
        if (mIPlatform == null)
            return "";


        string sPlatform = "";


        return sPlatform + mIPlatform.GetSignalStrength();
    }

    public static long GetFreeSpace(string driveDirectoryName)
    {
        if (mIPlatform == null)
            return long.MaxValue;

        //driveDirectoryName = System.IO.Path.GetPathRoot(driveDirectoryName);

        return mIPlatform.GetFreeSpace(driveDirectoryName);
    }

    public static long GetHardDiskSpace(string driveDirectoryName)
    {
        if (mIPlatform == null)
			return long.MaxValue;
		
		//driveDirectoryName = System.IO.Path.GetPathRoot(driveDirectoryName);

        return mIPlatform.GetHardDiskSpace(driveDirectoryName);
    }

}

using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class AndroidPlugin : MonoBehaviour, IPlatform
{
	static string _sdcardname = string.Empty;

    static string _macaddress = string.Empty;

	static string _devicetype = string.Empty;

    static long _freeMemory = long.MaxValue;

    static long _totalMemory = long.MaxValue;

    //0 没有网络 1 wifi 2 2G 3 3G 4 4G 5 WAP
    static int _networktype = 0;
	
    public string GetMacAddress()
    {
        //Debuger.Log("AndroidPlugin.GetMacAddress " + _macaddress);

        return _macaddress;
    }

    public double GetUsedMemory()
    {
        //Debuger.Log("AndroidPlugin.GetUsedMemory ");

        return -1;
    }

    public string GetDeviceType()
    {
        //Debuger.Log("AndroidPlugin.GetDeviceType " + _devicetype);

        return _devicetype;
    }

    //gu:添加网络状态和信号强度 

    public int GetNetworkStatus()
    {
        return _networktype;
    }

    public int GetSignalStrength()
    {
        return 0;
    }

#if UNITY_ANDROID
    private AndroidJavaClass jc = null;
	private AndroidJavaObject jo = null;
	
	private static AndroidPlugin instance;
	
	void Awake()
	{
		instance = this;
	}
	
    // Use this for initialization
	void Start () 
    {
		GameObject.DontDestroyOnLoad(gameObject);

		Init();

        crateNetWorkBroadcast();

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Debuger.Log(GetCardName());

		Debuger.Log(_getMacAddress());

        Debuger.Log(_getDeviceType());

        Debuger.Log(_getDirFreeMemory());

        Debuger.Log(_getDirTotalMemory());

        Debuger.Log(_getNetWorkType());
	}
	
	// Update is called once per frame
	void Update () 
    {
	}
	
	private void Init()
	{
		        try
        {
            if (jc == null)
            {
                jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            }
            if (jo == null)
            {
                jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }
        catch (System.Exception)
        {

            Debuger.LogError("Init JavaClass Object == false");
        }
	}
	
	private bool IsInit()
	{
		if(jc != null && jo != null)
		{
			return true;
		}
		return false;
	}

    public void crateNetWorkBroadcast()
    {
        using (AndroidJavaClass obj = new AndroidJavaClass("com.morefun.NetWorkUtil"))
        {
            obj.CallStatic("crateNetWorkBroadcast", jo);
        }
    }

    public void destoryNetWorkBroadcast()
    {
        using (AndroidJavaClass obj = new AndroidJavaClass("com.morefun.NetWorkUtil"))
        {
            obj.CallStatic("destoryNetWorkBroadcast", jo);
        }
    }

    public string GetCardName()
    {
		Init();

        using (AndroidJavaObject obj = new AndroidJavaObject("com.morefun.sdcard.MFAssetsHelp"))
		{
			_sdcardname = obj.CallStatic<string>("getSDCardName",jo) as string;
			
			return _sdcardname;
		}
    }

    /// <summary>
    /// 获取网络状态，wifi,wap,2g,3g,4g
    /// </summary>
    /// <returns></returns>
    public int _getNetWorkType()
    {
        Init();

        using (AndroidJavaObject obj = new AndroidJavaObject("com.morefun.NetWorkUtil"))
        {
            _networktype = obj.CallStatic<int>("getNetWorkType",jo);

            return _networktype;
        }
    }
	

	public string _getMacAddress()
    {
        Init();

        using (AndroidJavaObject obj = new AndroidJavaObject("com.morefun.NetWorkUtil"))
        {
            _macaddress = obj.CallStatic<string>("getLocalMacAddress", jo);

            return _macaddress;
        }
    }

	public string _getDeviceType()
    {
		Init();

        using (AndroidJavaObject obj = new AndroidJavaObject("com.morefun.sdcard.MFAssetsHelp"))
		{
			_devicetype = obj.CallStatic<string>("getProductModel") as string;
			
			return _devicetype;
		}
    }

    /// <summary>
    /// 获取指定路径所在磁盘可用空间
    /// </summary>
    /// <param name="path">Example: /mnt/sdcard</param>
    /// <returns></returns>
    public long _getDirFreeMemory()
    {
        using (AndroidJavaObject obj = new AndroidJavaObject("com.morefun.sdcard.MFAssetsHelp"))
        {
            _freeMemory = obj.CallStatic<long>("getDirFreeMemory", GetCardName());

            return _freeMemory;
        }
    }

    /// <summary>
    /// 获取指定路径所在磁盘总空间
    /// </summary>
    /// <param name="path">Example: /mnt/sdcard</param>
    /// <returns></returns>
    public long _getDirTotalMemory()
    {
        using (AndroidJavaObject obj = new AndroidJavaObject("com.morefun.sdcard.MFAssetsHelp"))
        {
            _totalMemory = obj.CallStatic<long>("getDirTotalMemory", GetCardName());

            return _totalMemory;
        }
    }

    public void AndroidReceive(string content)
    {
        _networktype = int.Parse(content);
        Debuger.Log("current network type ==" + content);
    }

#else
    public string GetCardName()
    {
        throw new System.NotImplementedException();
    }

#endif

    public long GetFreeSpace(string driveDirectoryName)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return _freeMemory;
#else
        return long.MaxValue;
#endif
    }

    public long GetHardDiskSpace(string driveDirectoryName)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return _totalMemory;
#else
        return long.MaxValue;
#endif
    }

}

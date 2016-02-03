using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class IosPlatform : MonoBehaviour, IPlatform
{
    [DllImport("__Internal")]
	private static extern double _getUsedMemory();

    [DllImport("__Internal")]
	private static extern string  _getDeviceType();	
	
	[DllImport("__Internal")]
	private static extern string  _getDeviceUUID();	


	[DllImport("__Internal")]
	private static extern int  _getNetworkStatus();	

	[DllImport("__Internal")]
	private static extern int  _getSignalStrength();	

	[DllImport("__Internal")]
	private static extern double  _getFreeDiskSpace();	

    /// <summary>
    /// 获取可用内存.
    /// </summary>
    /// <returns></returns>
    public double GetUsedMemory()
    {
#if UNITY_IPHONE 
#if UNITY_EDITOR
        return 0;
#else
		return _getUsedMemory() / 1024 / 1024;
#endif
#else
        return 0;
#endif
    }

	string _device = string.Empty;

    /// <summary>
    /// 获取可用内存.
    /// </summary>
    /// <returns></returns>
	public string GetDeviceType()
    {
#if UNITY_IPHONE 
#if UNITY_EDITOR
        return "";
#else
		if (_device == string.Empty)
			_device = _getDeviceType();
		
		return _device;
#endif
#endif
        return "";

    }

	string _macaddress = string.Empty;

	/// <summary>
	/// 获取mac地址
	/// </summary>
	/// <returns>The mac address.</returns>
    public string GetMacAddress()
    {
		#if UNITY_IPHONE 
#if UNITY_EDITOR
		return "";
#else
		if (_macaddress == string.Empty)
			_macaddress = _getDeviceUUID();

		return _macaddress;
#endif
#endif

		return "";
    }

    //gu:添加网络状态和信号强度  

    int _networkStatus = 0;

    /// <summary>
    /// 获取网络状态
    /// </summary>
    /// <returns>The network status.</returns>
    public int GetNetworkStatus()
    {
#if UNITY_IPHONE
#if UNITY_EDITOR
		return 0;
#else
		if (_networkStatus == 0)
			_networkStatus = _getNetworkStatus();

		return _networkStatus;
#endif
#endif

        return 0;
    }

    int _signalStrength = 0;

    /// <summary>
    /// 获取信号强度
    /// </summary>
    /// <returns>The signal strength.</returns>
    public int GetSignalStrength()
    {
#if UNITY_IPHONE
#if UNITY_EDITOR
		return 0;
#else
		_signalStrength = _getSignalStrength();

		return _signalStrength;
#endif
#endif

        return 0;
    }

    // Use this for initialization
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);

#if UNITY_IPHONE

        iPhoneSettings.screenCanDarken = false;
#endif

    }

    public long GetFreeSpace(string driveDirectoryName)
    {
		#if UNITY_IPHONE 
		#if UNITY_EDITOR
		return 0;
		#else
		return (long)_getFreeDiskSpace() ;
		#endif
		#else
		return 0;
		#endif
    }

    public long GetHardDiskSpace(string driveDirectoryName)
    {
        return long.MaxValue;
    }

    public string GetCardName()
    {
        throw new NotImplementedException();
    }
}

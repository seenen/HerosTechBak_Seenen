using UnityEngine;
using System.Collections;

public interface IPlatform
{
    /// <summary>
    /// 获取mac地址
    /// </summary>
    string GetMacAddress();

    /// <summary>
    /// 获取占用内存.
    /// </summary>
    double GetUsedMemory();

    /// <summary>
    /// 获取设备类型.
    /// </summary>
    string GetDeviceType();


    //gu:添加网络状态和信号强度    
    /// <summary>
    /// 获取网络状态.
    /// </summary>
    int GetNetworkStatus();

    /// <summary>
    /// 获取信号强度.
    /// </summary>
    int GetSignalStrength();

    long GetFreeSpace(string driveDirectoryName);

    long GetHardDiskSpace(string driveDirectoryName);

    string GetCardName();
}

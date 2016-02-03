using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IDevice 
{
    /// <summary>
    /// 发现设备
    /// </summary>
    void OnDevicesFound(string[] controllers);

    /// <summary>
    /// 发现设备
    /// </summary>
    void OnDevicesMissing();

    /// <summary>
    /// 链接上
    /// </summary>
    /// <param name="controllernames"></param>
    void OnConnect(string controllernames);

    /// <summary>
    /// 失去链接 
    /// </summary>
    void OnDisconnect();

}

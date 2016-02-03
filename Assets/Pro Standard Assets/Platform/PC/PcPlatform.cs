using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System;

public class PcPlatform : MonoBehaviour, IPlatform
{
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);

    }

    string macAdress = string.Empty;

    public string GetMacAddress()
    {
        if (!string.IsNullOrEmpty(macAdress))
            return macAdress;

        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

        foreach (NetworkInterface adapter in nics)
        {

            PhysicalAddress address = adapter.GetPhysicalAddress();

            if (address.ToString() != "")
            {
                macAdress = address.ToString();

                break;
            }

        }

        return macAdress;
    }

#if UNITY_STANDALONE_WIN
    #region Win32内存

    public struct MEMORY_INFO
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public uint dwTotalPhys;
        public uint dwAvailPhys;
        public uint dwTotalPageFile;
        public uint dwAvailPageFile;
        public uint dwTotalVirtual;
        public uint dwAvailVirtual;
    }

    [DllImport("kernel32")]
    public static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);


    #endregion
#endif

    Process proc = null;

    public double GetUsedMemory()
    {
#if UNITY_STANDALONE_WIN
        //MEMORY_INFO MemInfo;
        //MemInfo = new MEMORY_INFO();
        //GlobalMemoryStatus(ref MemInfo);
        //long totalMb = System.Convert.ToInt64(MemInfo.dwTotalPhys.ToString()) / 1024 / 1024;
        //long avaliableMb = System.Convert.ToInt64(MemInfo.dwAvailPhys.ToString()) / 1024 / 1024;

        //return totalMb;

        if (proc == null)
            proc = Process.GetCurrentProcess();

        return proc.PrivateMemorySize64;

#else
        return 0;
#endif
    }

    string CPUName = string.Empty;

    public string GetDeviceType()
    {
#if UNITY_STANDALONE_WIN
        if (string.IsNullOrEmpty(CPUName))
        {
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0");
            object obj = rk.GetValue("ProcessorNameString");
            CPUName = (string)obj;
        }

        return CPUName.TrimStart();

#else
        return "PC";
#endif
    }

    //gu:添加网络状态和信号强度  
    public int GetNetworkStatus()
    {
        return -1;
    }

    public int GetSignalStrength()
    {
        return 0;
    }


    public long GetFreeSpace(string driveDirectoryName)
    {
        return long.MaxValue;

        long freefreeBytesAvailable = 0;

        try
        {
            string[] drives = Directory.GetLogicalDrives();
            foreach (string drive in drives)
                Debuger.Log("PcPlatform.GetFreeSpace is " + drive);

            Debuger.Log("PcPlatform.GetFreeSpace driveDirectoryName is " + driveDirectoryName);

            System.IO.DriveInfo driver = new DriveInfo(driveDirectoryName);

            freefreeBytesAvailable = driver.TotalFreeSpace;

            return freefreeBytesAvailable;



            //System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            //foreach (System.IO.DriveInfo drive in drives)
            //{
            //    if (drive.Name.ToLower() == driveDirectoryName.ToLower())
            //    {
            //        freefreeBytesAvailable = drive.TotalFreeSpace;
            //    }
            //}
        }
        catch (System.Exception error)
        {
            string msg = "[InnerException:]" + error.InnerException +
            "[Exception:]" + error.Message +
            "[Source:]" + error.Source +
            "[StackTrace:]" + error.StackTrace;

            UnityEngine.Debug.LogException(error, this);
        }
            
        Debuger.Log("PcPlatform.GetFreeSpace" + freefreeBytesAvailable);

        return freefreeBytesAvailable;

    }

    /// <summary>
    /// 获取指定驱动器的空间总大小(单位为B) 
    /// </summary>
    /// <param name="driveDirectoryName">驱动器名 </param> 
    /// <returns>驱动器的空间总大小(单位为B) </returns>
    public long GetHardDiskSpace(string driveDirectoryName)
    {
        return long.MaxValue;

        long totalSize = new long();
        string[] drives = Directory.GetLogicalDrives();
        foreach (string drive in drives)
            Debuger.Log("PcPlatform.GetHardDiskSpace is " + drive);

        Debuger.Log("PcPlatform.GetHardDiskSpace" + totalSize + " " + driveDirectoryName);


        System.IO.DriveInfo driver = new DriveInfo(driveDirectoryName);

        totalSize = driver.TotalFreeSpace;

        return totalSize;
        //long totalSize = new long();
        //System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
        //foreach (System.IO.DriveInfo drive in drives)
        //{
        //    if (drive.Name.ToLower() == driveDirectoryName.ToLower())
        //    {
        //        totalSize = drive.TotalSize;
        //    }
        //}
        return totalSize;
    }

    public string GetCardName()
    {
        throw new NotImplementedException();
    }

}

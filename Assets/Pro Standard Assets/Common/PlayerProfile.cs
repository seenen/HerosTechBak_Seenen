using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public class LocalUserInfo
{
    public string username;
    public string password;
}


public class PlayerProfile
{
	#region system
	public static int m_ProfileVersion = 1;
	
	private const string m_PrivateKey = "com.morefungames.heros";
	private static string[] m_Encryptedkeys = 
	{
		"com.morefungames.heros.1",
		"com.morefungames.heros.2",
		"com.morefungames.heros.3",
		"com.morefungames.heros.4",
		"com.morefungames.heros.5",
	};
	
	public const string KEY_SYS_PROFILE_VERSION 	= "KEY_PROFILE_VERSION";
	#endregion
	
	#region System Mothed
	public static void Initialize()
	{
		if( !PlayerPrefs.HasKey(KEY_SYS_PROFILE_VERSION) )
		{
			SaveProfileVersion(m_ProfileVersion);
		}
		else
		{
			m_ProfileVersion = PlayerPrefs.GetInt(KEY_SYS_PROFILE_VERSION);
		}
	}
	
	
	public static void Clear()
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
	}
	
	public static bool HasKey(string key)
	{
		return PlayerPrefs.HasKey(key);
	}
	
	public static void SaveProfileVersion(int version)
	{
		PlayerPrefs.SetInt(KEY_SYS_PROFILE_VERSION, version);
		PlayerPrefs.Save();
	}
	
	public static string Md5(string strToEncrypt) 
	{
        UTF8Encoding ue = new UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

        byte[] hashBytes = md5.ComputeHash(bytes);

        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++) 
		{
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }
        return hashString.PadLeft(32, '0');
    }
	
	public static void SaveEncryption(string key, string type, string value) 
	{
        int keyIndex = (int)Mathf.Floor(UnityEngine.Random.value * m_Encryptedkeys.Length);
        string secretKey = m_Encryptedkeys[keyIndex];
        string check = Md5(type + "_" + m_PrivateKey + "_" + secretKey + "_" + value);
        PlayerPrefs.SetString(key + "_encryption_check", check);
        PlayerPrefs.SetInt(key + "_used_key", keyIndex);
    }

    public static bool CheckEncryption(string key, string type, string value) 
	{
        int keyIndex = PlayerPrefs.GetInt(key + "_used_key");
        string secretKey = m_Encryptedkeys[keyIndex];
        string check = Md5(type + "_" + m_PrivateKey + "_" + secretKey + "_" + value);
        if(!PlayerPrefs.HasKey(key + "_encryption_check")) return false;
        string storedCheck = PlayerPrefs.GetString(key + "_encryption_check");
        return storedCheck == check;
    }

    public static void SetInt(string key, int value) 
	{
        PlayerPrefs.SetInt(key, value);
        SaveEncryption(key, "int", value.ToString());
        PlayerPrefs.Save();
    }

    public static void SetFloat(string key, float value) 
	{
        PlayerPrefs.SetFloat(key, value);
        SaveEncryption(key, "float", Mathf.Floor(value*1000).ToString());
        PlayerPrefs.Save();
    }

    public static void SetString(string key, string value) 
	{
        PlayerPrefs.SetString(key, value);
        SaveEncryption(key, "string", value);
        PlayerPrefs.Save();
    }

    public static int GetInt(string key) 
	{
        return GetInt(key, 0);
    }

    public static float GetFloat(string key) 
	{
        return GetFloat(key, 0.0f);
    }

    public static string GetString(string key) 
	{
        return GetString(key, "");
    }

    public static int GetInt(string key,int defaultValue) 
	{
        int value = PlayerPrefs.GetInt(key);
        if(!CheckEncryption(key, "int", value.ToString())) return defaultValue;
        return value;
    }

    public static float GetFloat(string key, float defaultValue) 
	{
        float value = PlayerPrefs.GetFloat(key);
        if(!CheckEncryption(key, "float", Mathf.Floor(value*1000).ToString())) return defaultValue;
        return value;
    }

    public static string GetString(string key, string defaultValue) 
	{
        string value = PlayerPrefs.GetString(key);
        if(!CheckEncryption(key, "string", value)) return defaultValue;
        return value;
    }

    public static void DeleteKey(string key) 
	{
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.DeleteKey(key + "_encryption_check");
        PlayerPrefs.DeleteKey(key + "_used_key");
    }
	#endregion
	
	// ===============================================================
	
	// keys
	
	public const string KEY_FIRST_TIME_START_GAME	= "KEY_FIRST_TIME_START_GAME";
	public const string KEY_FIRST_TIME_LOGIN		= "KEY_FIRST_TIME_LOGIN";
	
	public const string KEY_PLAYER_INFO_USERNAME 	= "KEY_PLAYER_INFO_USERNAME";
	public const string KEY_PLAYER_INFO_PASSWORD 	= "KEY_PLAYER_INFO_PASSWORD";
    public const string KEY_PLAYER_INFO_USERNAME_LIST = "KEY_PLAYER_INFO_USERNAME_LIST";
	public const string KEY_PLAYER_INFO_CREATEROLE 	= "KEY_PLAYER_INFO_CREATEROLE";
	
	public const string KEY_PLAYER_FBSD_SDINFO 	    = "KEY_PLAYER_FBSD_SDINFO";

    public const string KEY_GEM_SYNTHESIS           = "KEY_GEM_SYNTHESIS";
	// save load
    public const string KEY_RES_NAME = "KEY_RES_NAME";

	public static void SaveFirstTimeStartGame()
	{
		SetInt(KEY_FIRST_TIME_START_GAME, 1);
	}
	
	public static int LoadFirstTimeStartGame()
	{
		return GetInt(KEY_FIRST_TIME_START_GAME);
	}
	
	public static void SaveFirstTimeLogin()
	{
		SetInt(KEY_FIRST_TIME_LOGIN, 1);
	}
	
	public static int LoadFirstTimeLogin()
	{
		return GetInt(KEY_FIRST_TIME_LOGIN);
	}
	
	public static void SaveUsername(string username)
	{
		SetString(KEY_PLAYER_INFO_USERNAME, username);
	}

    public static void SaveGemSynthesis(string visble) 
    {
        SetString(KEY_GEM_SYNTHESIS,visble);
    }

    public static string LoadGemSynthesis() 
    {
        string visble = GetString(KEY_GEM_SYNTHESIS);

        return visble;
    }

	public static string LoadUsername()
	{
		string username = GetString(KEY_PLAYER_INFO_USERNAME);
		return username;
	}
	
	public static void SavePassWord(string password)
	{
		SetString(KEY_PLAYER_INFO_PASSWORD, password);
	}
	
	public static string LoadPassWord()
	{
		string password = GetString(KEY_PLAYER_INFO_PASSWORD);
		return password;
	}

    public static void SaveResName(string resname)
    {
        SetString(KEY_RES_NAME, resname);
    }

    public static string GetResName()
    {
        string resname = GetString(KEY_RES_NAME);
        return resname;
    }

    #region UserInfo
    private static Queue<LocalUserInfo> UserInfos = new Queue<LocalUserInfo>();
    private static string mContent = string.Empty;

    public static void SaveUserInfo(string username, string password)
    {
        bool saved = false;

        foreach(LocalUserInfo e in UserInfos)
        {
            if (e.username == username)
            {
                e.password = password;
                saved = true;
            }
        }

        if (saved)
            return;

        LocalUserInfo removed = null;
        removed = new LocalUserInfo();

        removed.username = username;
        removed.password = password;

        UserInfos.Enqueue(removed);

        ToString();

        SaveProfiler();
    }

    public static LocalUserInfo[] LoadUserInfo()
    {
        LoadProfiler();

        ToLocalUserInfo();

        LocalUserInfo[] infos = new LocalUserInfo[UserInfos.Count];

        int i = 0;

        foreach (LocalUserInfo e in UserInfos)
        {
            infos[i] = new LocalUserInfo();
            infos[i].username = e.username;
            infos[i].password = e.password;

            i++;
        }

        return infos;
    }

    private static void SaveProfiler()
    {
        SetString(KEY_PLAYER_INFO_USERNAME_LIST, mContent);
    }

    private static void LoadProfiler()
    {
        mContent = GetString(KEY_PLAYER_INFO_USERNAME_LIST, "");
    }

    private static void ToString()
    {
        string content = string.Empty;

        foreach (LocalUserInfo e in UserInfos)
        {
            content += e.username;
            content += ",";
            content += e.password;
            content += ";";
        }

        mContent = content;
    }

    private static void ToLocalUserInfo()
    {
        if (string.IsNullOrEmpty(mContent))
            return;

        UserInfos.Clear();

        string[] userpass = mContent.Split(new char[1] { ';' });

        if (userpass == null)
            return;

        for (int i = 0; i < userpass.Length; ++i)
        {
            string[] up = userpass[i].Split(new char[1] { ',' });

            if (up == null || up.Length != 2)
                continue;

            LocalUserInfo info = new LocalUserInfo();
            info.username = up[0];
            info.password = up[1];

            UserInfos.Enqueue(info);
        }
    }
#endregion
}

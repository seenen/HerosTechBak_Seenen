using System;
using System.Security;
using System.Security.Cryptography;

class CheckUtility
{
    [Obsolete("Can not match with JAVA")]
    //SHA1,用于数据包完整性校验
    public static string GetSHA1(byte[] pwBytes, string maskString)
    {
        System.Text.Encoding encoding = System.Text.Encoding.UTF8;
        string pwString = encoding.GetString(pwBytes);
        byte[] bytes = encoding.GetBytes(pwString + maskString);
        SHA1 sha1 = SHA1.Create();
        byte[] hashBytes = sha1.ComputeHash(bytes);
        return BitConverter.ToString(hashBytes).Replace("-", "");
    }

    //线性同余数
    public static int GetLinearCongruence(int seed)
    {
        return ((seed + 13) * 17 + 23) % 1000000;
    }


    //获取字节流校验值
    public static int GetDataValidation(byte[] dataBytes, int maskNum = 0)
    {
        int ret = 0;
        for (int i = 0; i < dataBytes.Length; i++)
        {
            int index = (maskNum + i * 17) % (i + 11);
            ret += (dataBytes[i] + i) * 13 + GetFibonacci(index) + 107;
        }
        return ret; 
    }

    //取斐波拉切数列第n项
    //n>45时取 n%45项
    public static int GetFibonacci(int n)
    {
        if (n <= 0)
        {
            n = 1;
        }
        // when n>45 data overflow
        if (n > 45)
        {
            n = n % 45;
        }
        int a = 1;
        int b = 1;
        for (int i = 3; i <= n; i++)
        {
            b = a + b;
            a = b - a;
        }
        return b;
    }

}


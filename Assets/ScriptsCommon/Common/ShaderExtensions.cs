using UnityEngine;
using System.Collections;
using System.IO;

public static class ShaderExtensions
{
    static public Material GetMaterial(string shadername)
    {
        string path = Application.dataPath + "/Shaders/IphoneShader/" + shadername + ".shader";

        string shader = File.ReadAllText(path);

        Material m = new Material(shader);

        return m;
    }
}

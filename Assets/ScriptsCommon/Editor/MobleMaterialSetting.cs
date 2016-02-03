using UnityEngine;
using System.Collections;
using UnityEditor;

public class MobleMaterialSetting : EditorWindow
{
    Shader shader_mo_1 = Shader.Find("Mobile/Diffuse");
    Shader shader_mo_2 = Shader.Find("Mobile/Particles/Additive");
    Shader shader_mo_3 = Shader.Find("Mobile/Particles/Alpha Blended");
    Shader shader_mo_4 = Shader.Find("iPhone/Simple");

    Shader shader_pc_1 = Shader.Find("Diffuse");
    Shader shader_pc_2 = Shader.Find("Particles/Additive");
    Shader shader_pc_3 = Shader.Find("Particles/Alpha Blended");

    [@MenuItem("CUSTOM/Mobile Material Setting")]
    private static void Init()
    {
        MobleMaterialSetting window = (MobleMaterialSetting)GetWindow(typeof

        (MobleMaterialSetting), true, "MobleMaterialSetting");
        window.Show();
    }

    // 显示窗体里面的内容 
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Mobile Material Setting");
        GUILayout.EndHorizontal();

        GUILayout.Label(" ");
        if (GUILayout.Button("Diffuse -> Mobile/Diffuse"))
            LoopSetMaterials(shader_pc_1, shader_mo_1);

        GUILayout.Label(" ");
        if (GUILayout.Button("Particles/Additive -> Mobile/Particles/Additive"))
            LoopSetMaterials(shader_pc_2, shader_mo_2);

        GUILayout.Label(" ");
        if (GUILayout.Button("Particles/Alpha Blended -> Mobile/Particles/Alpha Blended"))
            LoopSetMaterials(shader_pc_3, shader_mo_3);

        GUILayout.Label(" ");
        if (GUILayout.Button(" -> iPhone/Simple"))
            LoopSetMaterials(shader_mo_4);
    }

    private void LoopSetMaterials(Shader old_shader, Shader new_shader)
    {
        Object[] materials = GetSelectedMaterials();
        Selection.objects = new Object[0];

        foreach (Material m in materials)
        {
            if (m.shader == old_shader)
            {
                m.shader = new_shader;
            }
        }
    }
	
    private void LoopSetMaterials(Shader new_shader)
    {
        Object[] materials = GetSelectedMaterials();
        Selection.objects = new Object[0];

        foreach (Material m in materials)
        {
			m.shader = new_shader;
       }
    }
	
	
	
    private Object[] GetSelectedMaterials()
    {
        return Selection.GetFiltered(typeof(Material), SelectionMode.DeepAssets);
    }
}
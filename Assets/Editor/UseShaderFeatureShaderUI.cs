using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 自定义某个shader的面板是需要继承实现自UnityEditor.ShaderGUI
public class UseShaderFeatureShaderUI : ShaderGUI
{
    // 对应于shader中的
    MaterialProperty m_IsPurple = null;

    /// <summary>
    /// 找到指定的shader properties
    /// </summary>
    /// <param name="props">由编辑器传递进来的</param>
    public void FindProperties(MaterialProperty[] props)
    {
        // 在编辑器传递进来的MaterialProperty数组中找到名为_IsSemiTransparent
        // 的那个shader property缓存下来
        m_IsPurple = FindProperty("_IsPurple", props);
    }

    /// <summary>
    /// 继承实现基类的OnGUI方法
    /// </summary>
    /// <param name="materialEditor">由编辑器传递进来的</param>
    /// <param name="props"></param>
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
    {
        FindProperties(props);

        // 检查属性是否发生了变化
        EditorGUI.BeginChangeCheck();
        {
            bool semi = m_IsPurple.floatValue == 1;
            m_IsPurple.floatValue = EditorGUILayout.Toggle("Is Purple", semi) ? 1 : 0;
            base.OnGUI(materialEditor, props);
        }

        // 根据所选择的shader propety，决定当前处理的材质球的keyword哪些开启，哪些关闭
        if (EditorGUI.EndChangeCheck())
        {
            foreach (var obj in materialEditor.targets)
                MaterialChanged((Material)obj);
        }
    }

    public static void MaterialChanged(Material material)
    {
        // 根据shader中的_IsSemiTransparent属性的取值，去决定keyword _IS_SEMITRANSPARENT 是否开启
        // _IsSemiTransparent属性为1的时候1，开启_IS_SEMITRANSPARENT，为0的时候，关闭_IS_SEMITRANSPARENT
        float value = material.GetFloat("_IsPurple");
        SetKeyword(material, "_IS_PURPLE", value == 1);
    }

    private static void SetKeyword(Material mat, string keyword, bool enable)
    {
        if (enable)
            mat.EnableKeyword(keyword);
        else
            mat.DisableKeyword(keyword);
    }
}

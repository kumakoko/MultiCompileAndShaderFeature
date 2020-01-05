using UnityEngine;

public class UseShaderFeature : MonoBehaviour
{
    public Renderer m_Renderer;
    public string m_Info = string.Empty;
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 60, 150, 50), "Use Purple"))
        {
            var mat = m_Renderer.material;
            mat.EnableKeyword("_IS_PURPLE");
            m_Renderer.material = mat;
#if UNITY_EDITOR
            m_Info = "在编辑器模式中，只要有一个启用了_IS_PURPLE关键字的材质球，无论只该材质球是否被使用上，那么使用Marterial.EnableKeyword方法也是可以启用这个key word的";
#else
            m_Info = "在非编辑器模式中，就算有一个启用了_IS_PURPLE关键字的材质球，只要该材质球未被使用上，就不会被打包进游戏包中，这时候如果一个材质球使用了无_IS_PURPLE关键字的材质，那么使用Marterial.EnableKeyword方法也是无法启用这个key word的";
#endif
        }

        if (GUI.Button(new Rect(170, 60, 150, 50), "Quit"))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        GUI.TextArea(new Rect(0, 120, 700, 80), m_Info);
    }
}
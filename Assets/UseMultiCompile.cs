using UnityEngine;

public class UseMultiCompile : MonoBehaviour
{
    public Renderer m_Renderer;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,150,50),"Red"))
        {
            var mat = m_Renderer.material;
            mat.EnableKeyword("_MULTI_RED");
            mat.DisableKeyword("_MULTI_GREEN");
            mat.DisableKeyword("_MULTI_BLUE");
            m_Renderer.material = mat;
        }

        if (GUI.Button(new Rect(170, 0, 150, 50), "Green"))
        {
            var mat = m_Renderer.material;
            mat.DisableKeyword("_MULTI_RED");
            mat.EnableKeyword("_MULTI_GREEN");
            mat.DisableKeyword("_MULTI_BLUE");
            m_Renderer.material = mat;
        }

        if (GUI.Button(new Rect(340, 0, 150, 50), "Blue"))
        {
            var mat = m_Renderer.material;
            mat.DisableKeyword("_MULTI_RED");
            mat.DisableKeyword("_MULTI_GREEN");
            mat.EnableKeyword("_MULTI_BLUE");
            m_Renderer.material = mat;
        }

        if (GUI.Button(new Rect(510, 0, 150, 50), "SemiTransparent"))
        {
            var mat = m_Renderer.material;
            mat.EnableKeyword("_USE_SEMITRANSPARENT");
            mat.DisableKeyword("_USE_OPAQUE");
            m_Renderer.material = mat;
        }

        if (GUI.Button(new Rect(680, 0, 150, 50), "Opaque"))
        {
            var mat = m_Renderer.material;
            mat.DisableKeyword("_USE_SEMITRANSPARENT");
            mat.EnableKeyword("_USE_OPAQUE");
            m_Renderer.material = mat;
        }
    }
}

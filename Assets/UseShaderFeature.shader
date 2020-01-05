Shader "Unlit/UseShaderFeature"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        // [HideInInspector]指令告诉编辑器在界面面板中隐藏此属性的默认显示，
        // 在自定义的shader界面面板中用自定义的C#脚本代码对应显示
        [HideInInspector] _IsPurple("", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            // 在界面选项中可以决定此keyword是否开启或者关闭，如果开启的话
            // 会将此keyword的开启记录在使用了该shader的材质球文件中。如果
            // 关闭的话则不会编译这段shader代码进去
            #pragma shader_feature _IS_PURPLE
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;

#if _IS_PURPLE
            uniform fixed _IsPurple;
#endif
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
#if _IS_PURPLE
                col = col * fixed4(0.5,0,0.5,1);
#endif
                return col;
            }
            ENDCG
        }
    }

	CustomEditor "UseShaderFeatureShaderUI"
}

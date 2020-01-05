Shader "Unlit/UseMultiCompile"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile  _USE_SEMITRANSPARENT _USE_OPAQUE
            #pragma multi_compile  _MULTI_RED _MULTI_GREEN _MULTI_BLUE
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                #if _MULTI_RED
                    col = col * fixed4(1,0,0,1);
                #elif _MULTI_GREEN
                    col = col * fixed4(0,1,0,1);
                #elif  _MULTI_BLUE
                    col = col * fixed4(0,0,1,1);
                #endif

                #if _USE_SEMITRANSPARENT
                    col.a = 0.5;
                #elif _USE_OPAQUE
                    col.a = 1.0;
                #endif

                return col;
            }
            ENDCG
        }
    }
}

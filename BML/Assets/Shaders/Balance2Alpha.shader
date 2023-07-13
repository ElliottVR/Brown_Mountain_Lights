Shader "Custom/Balance2Alpha" {
    Properties{
        _MainTex1("Texture 1", 2D) = "white" {}
        _Tiling1("Tiling 1", Vector) = (1, 1, 0, 0)
        _MainTex2("Texture 2", 2D) = "white" {}
        _Tiling2("Tiling 2", Vector) = (1, 1, 0, 0)
        _Emission("Emission", Range(0.0, 1.0)) = 0.0
    }
        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Opaque"}
            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex1;
                sampler2D _MainTex2;
                float4 _Tiling1;
                float4 _Tiling2;
                float _Emission;

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv1 : TEXCOORD0;
                    float2 uv2 : TEXCOORD1;
                };

                struct v2f {
                    float2 uv1 : TEXCOORD0;
                    float2 uv2 : TEXCOORD1;
                    float4 vertex : SV_POSITION;
                };

                v2f vert(appdata v) {
                    v2f o;
                    o.uv1 = v.uv1 * _Tiling1.xy + _Tiling1.zw;
                    o.uv2 = v.uv2 * _Tiling2.xy + _Tiling2.zw;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    float4 col1 = tex2D(_MainTex1, i.uv1);
                    float4 col2 = tex2D(_MainTex2, i.uv2);
                    float alpha = col1.a;
                    fixed4 col = fixed4(col1.rgb * alpha + col2.rgb * (1.0 - alpha), 1.0);
                    col.rgb *= (1.0 - _Emission);
                    return col;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}










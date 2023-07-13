Shader "Custom/Balance2Textures"{
    Properties{
        _MainTex1("Texture 1", 2D) = "white" {}
        _MainTex2("Texture 2", 2D) = "white" {}
        _Balance("Balance", Range(0,1)) = 0.5
        _Tile1("Tiling 1", Range(1,10)) = 1
        _Tile2("Tiling 2", Range(1,10)) = 1
        _Offset1("Offset 1", Range(-1,1)) = 0
        _Offset2("Offset 2", Range(-1,1)) = 0
        _Emission1("Emission 1", Range(0,1)) = 0
        _Emission2("Emission 2", Range(0,1)) = 0
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 100

            CGPROGRAM
            #pragma surface surf Standard

            sampler2D _MainTex1;
            sampler2D _MainTex2;
            float _Balance;
            float _Tile1;
            float _Tile2;
            float _Offset1;
            float _Offset2;
            float _Emission1;
            float _Emission2;

            struct Input {
                float2 uv_MainTex1;
                float2 uv_MainTex2;
            };

            void surf(Input IN, inout SurfaceOutputStandard o) {
                float2 uv1 = IN.uv_MainTex1 * _Tile1 + _Offset1;
                float2 uv2 = IN.uv_MainTex2 * _Tile2 + _Offset2;
                fixed4 color1 = tex2D(_MainTex1, uv1);
                fixed4 color2 = tex2D(_MainTex2, uv2);
                o.Albedo = lerp(color1.rgb, color2.rgb, _Balance);
                o.Metallic = 0;
                o.Smoothness = 0.5;
                o.Normal = float3(0, 0, 1);
                o.Emission = lerp(_Emission1 * color1.rgb, _Emission2 * color2.rgb, _Balance);
            }
            ENDCG
        }
            FallBack "Diffuse"
}
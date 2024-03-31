Shader "Hidden/3MaskTintCompressed"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _MaskTex ("Mask (RGB channels are mask ABC)", 2D) = "white" {}
        _MaskColA ("Mask A Tint", Color) = (1,1,1,1)
        _MaskColB ("Mask B Tint", Color) = (1,1,1,1)
        _MaskColC ("Mask C Tint", Color) = (1,1,1,1)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _MaskTex;
            float4 _MaskColA, _MaskColB, _MaskColC;

            float4 blendColor(float4 fore, float4 back)
            {
                return float4(lerp(back.xyz, fore.xyz, fore.w), saturate(fore.w + back.w));
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                float4 maskRaw = tex2D(_MaskTex, i.uv);
                float4 colFromMaskA = float4(1, 1, 1, maskRaw.r) * _MaskColA;
                float4 colFromMaskB = float4(1, 1, 1, maskRaw.g) * _MaskColB;
                float4 colFromMaskC = float4(1, 1, 1, maskRaw.b) * _MaskColC;

                col = blendColor(colFromMaskA, col);
                col = blendColor(colFromMaskB, col);
                col = blendColor(colFromMaskC, col);

                return col;
            }
            ENDCG
        }
    }
}

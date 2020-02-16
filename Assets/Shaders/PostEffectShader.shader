Shader "Custom/PostEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        // Color property for material inspector, default to white
        _Color ("Main Color", Color) = (1,1,1,1)
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
            fixed4 _Color;
           
            fixed4 frag (v2f i) : SV_Target
            {
                
                
                //fixed4 col = tex2D(_MainTex, i.uv + float2(0, 0.01));
                // just invert the colors
                
                //col.r = 1;
                //col.g = 0;
                //col.rgb = 1 - col.rgb;
                //return col;
                fixed4 col = tex2D(_MainTex, i.uv);
                return fixed4(_Color.r * 0.567 + _Color.g * 0.433, _Color.r * 0.558 + _Color.g * 0.442, _Color.g * 0.242 + _Color.b * 0.758, _Color.a);
                //return fixed4(col.r * 0.567 + col.g * 0.433, col.r * 0.558 + col.g * 0.442, col.g * 0.242 + col.b * 0.758, col.a);
            }
            ENDCG
        }
    }
}
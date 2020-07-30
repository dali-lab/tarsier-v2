Shader "Anivision/UV"
{
    Properties
    {
		[HideInInspector] _MainTex ("Texture", 2D) = "white" {}
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

			float3 ApplyHue(float3 col, float hueAdjust) // From https://gist.github.com/mairod/a75e7b44f68110e1576d77419d608786
			{
				const float3 k = float3(0.57735, 0.57735, 0.57735);
				half cosAngle = cos(hueAdjust);
				return col * cosAngle + cross(k, col) * sin(hueAdjust) + k * dot(k, col) * (1.0 - cosAngle);
			}

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
				float3 colShift = ApplyHue(col, -2);
				col.rgb = colShift.rgb;
				return col;
            }
            ENDCG
        }
    }
}

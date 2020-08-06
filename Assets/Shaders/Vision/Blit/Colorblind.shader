Shader "Anivision/Colorblind"
{
	Properties
	{
		[HideInInspector] _MainTex("Texture", 2D) = "white" {}
		// Parameter for colorblindness type
		[KeywordEnum(Protanopia, Protanomaly, Deuteranopia, Deuteranomaly, Tritanopia, Tritanomaly, Achromatopsia, Achromatomaly)] _Type("Colorblindness Type", Float) = 0
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

				// Create different versions of the shader for every colorblind ty[e
				#pragma multi_compile _TYPE_PROTANOPIA _TYPE_PROTANOMALY _TYPE_DEUTERANOPIA _TYPE_DEUTERANOMALY _TYPE_TRITANOPIA _TYPE_TRITANOMALY _TYPE_ACHROMATOPSIA _TYPE_ACHROMATOMALY

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

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				sampler2D _MainTex;

				fixed4 frag(v2f i) : SV_Target
				{
					// Get the proper colorblind matrix based on the colorblind type
					#ifdef _TYPE_PROTANOPIA
					const float4x4 m = {
						0.567,	0.433,	0.0,	0.0,
						0.558,	0.442,	0.0,	0.0,
						0.0,	0.242,	0.758,	0.0,
						0.0,	0.0,	0.0,	1.0,
					};
					#elif _TYPE_PROTANOMALY
					const float4x4 m = {
						0.817,	0.183,	0.0,	0.0,
						0.333,	0.667,	0.0,	0.0,
						0.0,	0.125,	0.875,	0.0,
						0.0,	0.0,	0.0,	1.0,
					};
					#elif _TYPE_DEUTERANOPIA
					const float4x4 m = {
						0.625,	0.375,	0.0,	0.0,
						0.7,	0.3,	0.0,	0.0,
						0.0,	0.3,	0.7,	0.0,
						0.0,	0.0,	0.0,	1.0,
					};
					#elif _TYPE_DEUTERANOMALY
					const float4x4 m = {
						0.8,	0.2,	0.0,	0.0,
						0.258,	0.742,	0.0,	0.0,
						0.0,	0.142,	0.858,	0.0,
						0.0,	0.0,	0.0,	1.0,
					};
					#elif _TYPE_TRITANOPIA
					const float4x4 m = {
						0.95,	0.05,	0.0,	0.0,
						0.0,	0.433,	0.567,	0.0,
						0.0,	0.475,	0.525,	0.0,
						0.0,	0.0,	0.0,	1.0,
					};
					#elif _TYPE_TRITANOMALY
					const float4x4 m = {
						0.967,	0.033,	0.0,	0.0,
						0.0,	0.733,	0.267,	0.0,
						0.0,	0.183,	0.817,	0.0,
						0.0,	0.0,	0.0,	1.0,
					};
					#elif _TYPE_ACHROMATOPSIA
					const float4x4 m = {
						0.299,	0.587,	0.114,	0.0,
						0.299,	0.587,	0.114,	0.0,
						0.299,	0.587,	0.114,	0.0,
						0.0,	0.0,	0.0,	1.0,
					};
					#elif _TYPE_ACHROMATOMALY
					const float4x4 m = {
						0.618f,	0.320,	0.062,	0.0,
						0.163,	0.775,	0.062,	0.0,
						0.163,	0.320,	0.516,	0.0,
						0.0,	0.0,	0.0,	1.0,
					};
					#endif

					fixed4 col = tex2D(_MainTex, i.uv);
					return mul(m, col);
				}
				ENDCG
			}
		}
}

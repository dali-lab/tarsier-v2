// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "IOSPostEffects/DOFandBloom" {
Properties {
	_MainTex  ("Dont use this, Used by code", 2D) = "white" {}
	_BlurTexA ("Dont use this, Used by code", 2D) = "white" {}
	_BlurTexB ("Dont use this, Used by code", 2D) = "white" {}
}

SubShader {
	
	
Pass { // pass 0
		name "composit"
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
		Blend Off
				
		CGPROGRAM
		
			#pragma vertex vert_img
			#pragma fragment fragLQ
			#pragma fragmentoption ARB_precision_hint_fastest 
			//#pragma debug
			//#pragma only_renderers gles
			
			#include "UnityCG.cginc"
			
			uniform sampler2D _BlurTexA;
			uniform sampler2D _BlurTexB;
			uniform fixed _BloomCutoff;
			uniform fixed _BloomStrength;
			
			
			// optimized Version
			fixed4 fragLQ (v2f_img i) : COLOR 
			{
				fixed4 blurA = tex2D(_BlurTexA, i.uv);
				fixed4 blurB = tex2D(_BlurTexB, i.uv);
				
				fixed4 blurSample = lerp(blurA,blurB,min(blurB.a,blurA.a));
				fixed3 bloomSample = max(blurA,blurB).rgb;
				
				fixed bloomLuminance = Luminance(bloomSample);
				fixed bloomrange = saturate(bloomLuminance - _BloomCutoff) * _BloomStrength;
				
				blurSample.a += bloomrange;
				
				blurSample.rgb += bloomSample * bloomrange;
				return blurSample;
				

			}

		ENDCG

	}
	
	pass { // pass 1
	    name "SimpleBlur"
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
		Blend Off
	
		CGPROGRAM
		
			#pragma vertex vertMax
			#pragma fragment fragMax
			#pragma fragmentoption ARB_precision_hint_fastest 
			//#pragma debug
			//#pragma only_renderers gles
			
			#include "UnityCG.cginc"
			
			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			
			struct v2f_withMaxCoords {
				half4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
				half2 uv2[4] : TEXCOORD1;
			};	
			
			v2f_withMaxCoords vertMax (appdata_img v)
			{
				v2f_withMaxCoords o;
				o.pos = UnityObjectToClipPos (v.vertex);
	        	o.uv = v.texcoord;
	        	
	        	o.uv2[0] = v.texcoord + _MainTex_TexelSize.xy * half2(1.5,1.5)  ;					
				o.uv2[1] = v.texcoord + _MainTex_TexelSize.xy * half2(-1.5,-1.5) ;
				o.uv2[2] = v.texcoord + _MainTex_TexelSize.xy * half2(1.5,-1.5) ;
				o.uv2[3] = v.texcoord + _MainTex_TexelSize.xy * half2(-1.5,1.5) ;
				return o;
			}	
			

			fixed4 fragMax ( v2f_withMaxCoords i ) : COLOR
			{				
				fixed4 color = 0;
				color += tex2D (_MainTex, i.uv ) * 0.4;	
				color += tex2D (_MainTex, i.uv2[0]) * 0.15;	
				color += tex2D (_MainTex, i.uv2[1]) * 0.15;	
				color += tex2D (_MainTex, i.uv2[2]) * 0.15;		
				color += tex2D (_MainTex, i.uv2[3]) * 0.15;	
				return color;
			}
			 			
		ENDCG
		
	}
	
		pass { // pass 2 SmoothBlur, too slow
		name "SmoothBlur"
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
		Blend Off
	
		CGPROGRAM
		
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			//#pragma debug
			//#pragma only_renderers gles 
			
			#include "UnityCG.cginc"
			
			struct v2f {
				half4 pos : POSITION;
				half2 uv : TEXCOORD0;
				half4 uv01 : TEXCOORD1;
				half4 uv23 : TEXCOORD2;
				half4 uv45 : TEXCOORD3;
				half4 uv67 : TEXCOORD4;
			};
			
			uniform sampler2D _MainTex;
		    uniform half4 _MotionBlurOffset;
		    uniform half4 _MainTex_TexelSize;
				
			v2f vert (appdata_img v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				
				o.uv.xy = v.texcoord.xy;
		
				o.uv01 =  v.texcoord.xyxy +  _MainTex_TexelSize.xyxy * half4(1.5,1.5, -1.5,-1.5) ;
				o.uv23 =  v.texcoord.xyxy +  _MainTex_TexelSize.xyxy * half4(1.5,-1.5, -1.5,1.5) ; 
				o.uv45 =  v.texcoord.xyxy +  _MainTex_TexelSize.xyxy * half4(2.5,2.5, -2.5,-2.5) ;
				o.uv67 =  v.texcoord.xyxy +  _MainTex_TexelSize.xyxy * half4(2.5,-2.5, -2.5,2.5) ;

		
				return o;  
			}
				
			half4 frag (v2f i) : COLOR {
				fixed4 color = fixed4 (0,0,0,0);

				color += 0.35 * tex2D (_MainTex, i.uv);
				color += 0.12 * tex2D (_MainTex, i.uv01.xy);
				color += 0.12 * tex2D (_MainTex, i.uv01.zw);
				color += 0.12 * tex2D (_MainTex, i.uv23.xy);
				color += 0.12 * tex2D (_MainTex, i.uv23.zw);
				color += 0.0675 * tex2D (_MainTex, i.uv45.xy);
				color += 0.0675 * tex2D (_MainTex, i.uv45.zw);	
				color += 0.0675 * tex2D (_MainTex, i.uv67.xy);
				color += 0.0675 * tex2D (_MainTex, i.uv67.zw);

				
				return color;
			} 
			 			
		ENDCG
		
	}
	
		pass { // pass 3
		name "FinalBlend"
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
		Blend Off
	
		CGPROGRAM
		
			#pragma vertex vert_img
			#pragma fragment SimpleBlit
			#pragma fragmentoption ARB_precision_hint_fastest 
			
			//#pragma debug
			//#pragma only_renderers gles
			
			#include "UnityCG.cginc"
			
			uniform sampler2D _MainTex;
			uniform sampler2D _BlurTexD;
			
			fixed4 SimpleBlit (v2f_img i) : COLOR
			{				
				fixed4 colorA = tex2D(_BlurTexD, i.uv);
				fixed4 colorB = tex2D(_MainTex, i.uv);
				colorA = lerp(colorB,colorA,colorA.a);
				return colorA;
			}
			 			
		ENDCG
		
	}
	
	Pass { // pass 4
		name "SimpleBlendandComposit"
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
		Blend Off
				
		CGPROGRAM
		
			#pragma vertex vertMax
			#pragma fragment fragMax
			#pragma fragmentoption ARB_precision_hint_fastest 
			
			#include "UnityCG.cginc"
			
			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform fixed _BloomCutoff;
			uniform fixed _BloomStrength;
			
			struct v2f_withMaxCoords {
				half4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
				half2 uv2[4] : TEXCOORD1;
			};	
			
			v2f_withMaxCoords vertMax (appdata_img v)
			{
				v2f_withMaxCoords o;
				o.pos = UnityObjectToClipPos (v.vertex);
	        	
	        	o.uv = v.texcoord;
	        	o.uv2[0] = v.texcoord + _MainTex_TexelSize.xy * half2(2.5,2.5)  ;					
				o.uv2[1] = v.texcoord + _MainTex_TexelSize.xy * half2(-2.5,-2.5);
				o.uv2[2] = v.texcoord + _MainTex_TexelSize.xy * half2(2.5,-2.5) ;
				o.uv2[3] = v.texcoord + _MainTex_TexelSize.xy * half2(-2.5,2.5) ;
				
				return o;

			}	
			

			fixed4 fragMax ( v2f_withMaxCoords i ) : COLOR
			{				
				fixed4 sample = 0;
				sample += tex2D (_MainTex, i.uv ) * 0.3;	
				sample += tex2D (_MainTex, i.uv2[0]) * 0.175;	
				sample += tex2D (_MainTex, i.uv2[1]) * 0.175;	
				sample += tex2D (_MainTex, i.uv2[2]) * 0.175;		
				sample += tex2D (_MainTex, i.uv2[3]) * 0.175;
					
				fixed bloomLuminance = Luminance(sample);
				fixed bloomrange = saturate(bloomLuminance - _BloomCutoff) * _BloomStrength;
				
				sample.a += bloomrange;
				
				sample.rgb += sample.rgb * bloomrange;
				return sample;
			}
			 			
		ENDCG

	}
}

Fallback off

}

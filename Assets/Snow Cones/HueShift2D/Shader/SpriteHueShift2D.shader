// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

/**
 * HueShift.shader
 * Author: Thomas Hummes
 * compatible to Shader Model 2 (<= 64 instructions)
 * **/
Shader "Tastenhacker/HueShiftSprite2D" 
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_Alpha ("Alpha factor", float) = 1
		[PerRendererData]_Shift ("Shift", float) = 0
        [PerRendererData]_LimitL ("Lower Limit", float) = 0
        [PerRendererData]_LimitU ("Upper Limit", float) = 0
		[PerRendererData]_Inverted ("Inverted", float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend One OneMinusSrcAlpha
		
Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
			};

			half3 Hue(float H)
			{
				H *= 6;
				half R = abs(H - 3) - 1;
				half G = 2 - abs(H - 2);
				half B = 2 - abs(H - 4);
				return clamp(half3(R,G,B), 0.0, 1.0);
			}
		
			half3 HSVtoRGB(in half3 HSV)
			{
				return ((Hue(HSV.x) - 1) * HSV.y + 1) * HSV.z;
			}

			half RGBCVtoHUE(in half3 RGB, in half C, in half V)
			{
				half3 Delta = (V - RGB) / C;
				Delta.rgb = (Delta.rgb - Delta.brg) + half3(2,4,6);
				Delta.brg = step(V, RGB) * Delta.brg;
				return frac(max(Delta.r, max(Delta.g, Delta.b)) / 6);
			}
		
			half3 RGBtoHSV(in half3 RGB)
			{
				half3 HSV = 0;
				HSV.z = max(RGB.r, max(RGB.g, RGB.b));
				half C = HSV.z - min(RGB.r, min(RGB.g, RGB.b));
				if (C != 0)
				{
					HSV.x = RGBCVtoHUE(RGB, C, HSV.z);
					HSV.y = C / HSV.z;
				}
				return HSV;
			}
			
			fixed4 _Color;
			half _Shift;
			half _LimitL;
			half _LimitU;
			int _Inverted;
			half _Alpha;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
				half3 cl = RGBtoHSV(c);
				float d = (_LimitU - cl.x) * (cl.x - _LimitL) * _Inverted;
				if(d < 0)
				{
					cl.x = clamp(cl.x + _Shift, 0.0, 1.0);
				}
				c.rgb = HSVtoRGB(cl);
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
	Fallback "Transparent/VertexLit"
	CustomEditor "HueShift2DMaterialInspector"

}
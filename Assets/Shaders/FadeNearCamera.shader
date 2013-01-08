Shader "Custom/FadeNearCamera"
{
	Properties
	{
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}

	SubShader
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		AlphaTest Greater .01
		Lighting Off
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 200

		Pass
		{
			CGPROGRAM
				#pragma exclude_renderers xbox360
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest

				#include "UnityCG.cginc"

				sampler2D _MainTex;

				struct v2f
				{
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
					float dist;
				};

				float4 _MainTex_ST;

				v2f vert(appdata_img v)
				{
					v2f o;
					o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					o.dist = o.pos.z;
					return o;
				}

				half4 frag(v2f i) : COLOR
				{
					half4 texcol = tex2D(_MainTex, i.uv);
					// texcol.a = min(clamp(39.0 - i.dist, 0, 1.0), texcol.a);
					texcol.a = min(texcol.a, i.dist);
					return texcol;
				}
			ENDCG
		}
	}
	// Fallback "VertexLit"
} 
Shader "Unlit/Additive"
{
	Properties
	{
		_Color ("Color", color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}

	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		Blend One One

		Pass
		{
//		SetTexture[_MainTex]

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
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

			half4 _Color;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return _Color;
			}
			ENDCG
		}
	}
}

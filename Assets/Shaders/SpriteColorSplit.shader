Shader "Custom/SpriteColorSplit"
{


	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
	//        _Color ("Tint", Color) = (1,1,1,1)
	[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		[Toggle] _RenderRed("Render Red", Float) = 0
		[Toggle] _RenderBlue("Render Blue", Float) = 0
		[Toggle] _RenderGreen("Render Green", Float) = 0

		// Will set "_INVERT_ON" shader keyword when set
		//_RenderRed("RenderRed", Boolean) = false;
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
	}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog{ Mode Off }

		Pass
	{
		Blend SrcAlpha One

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile DUMMY PIXELSNAP_ON
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
		fixed4 color : COLOR;
		half2 texcoord  : TEXCOORD0;
	};

	bool _RenderRed;
	bool _RenderGreen;
	bool _RenderBlue;

	v2f vert(appdata_t IN)
	{
		v2f OUT;
		OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
		OUT.texcoord = IN.texcoord;
		OUT.color = IN.color * float4(
			(_RenderRed ? 1 : 0),
			(_RenderGreen ? 1 : 0),
			(_RenderBlue ? 1 : 0),
			1);
#ifdef PIXELSNAP_ON
		OUT.vertex = UnityPixelSnap(OUT.vertex);
#endif

		return OUT;
	}

	sampler2D _MainTex;

	fixed4 frag(v2f IN) : COLOR
	{
		return tex2D(_MainTex, IN.texcoord) *IN.color;
	}
		ENDCG
	}
	}
}
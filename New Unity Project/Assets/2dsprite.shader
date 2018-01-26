Shader "Custom/SpriteDiffuse"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	_Color("Color Tint",Color) = (1,1,1,1)
		_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
	}

		SubShader
	{
		Tags{ 
		"lightType" = "SahdowCaster"
		"RenderType" = "TransparentCutOut" 
		"Queue" = "AlphaTest"
		"IgnoreProjector" = "True"
		}
		LOD 200
		CGPROGRAM
#pragma surface surf Lambert addshadow  alphatest:_Cutoff

		sampler2D _MainTex;
	fixed4 _Color;



	struct Input
	{
		float2 uv_MainTex;
		fixed4 color : COLOR;
	};

	void surf(Input IN, inout SurfaceOutput o)
	{
		half4 c = tex2D(_MainTex, IN.uv_MainTex)*_Color;

		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG
	}
}
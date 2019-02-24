Shader "Custom/Twist"
{
	Properties
	{
		_MainTex("主要 Texture", 2D) = "white" {}
	EffectAmount("程度", Float) = 0
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

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		return o;
	}

	sampler2D _MainTex;
	float EffectAmount;

	fixed4 frag(v2f i) : SV_Target
	{

		i.uv = frac(i.uv + float2(0., (sin(i.uv*12. * EffectAmount + _Time.y * EffectAmount)*.1 * EffectAmount*.1).x + EffectAmount * _Time.y));

	fixed4 col = tex2D(_MainTex, i.uv);
	// just invert the colors
	col.rgb = col.rgb;




	return col;
	}
		ENDCG
	}
	}
}
Shader "Toon/LightedHeightBlend" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_Tex1 ("Base (RGB)", 2D) = "white" {}
		_Tex2 ("Tex2 (RGB)", 2D) = "white" {}
		_BlendTex ("Blend (RGB)",2D) = "white"{}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
		_MaxHeight("Max height (f)",float) = 0
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
CGPROGRAM
#pragma surface surf ToonRamp

sampler2D _Ramp;

// custom lighting function that uses a texture ramp based
// on angle between light direction and normal
#pragma lighting ToonRamp exclude_path:prepass
inline half4 LightingToonRamp (SurfaceOutput s, half3 lightDir, half atten)
{
	#ifndef USING_DIRECTIONAL_LIGHT
	lightDir = normalize(lightDir);
	#endif
	
	half d = dot (s.Normal, lightDir)*0.5 + 0.5;
	half3 ramp = tex2D (_Ramp, float2(d,d)).rgb;
	
	half4 c;
	c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
	c.a = 0;
	return c;
}


sampler2D _Tex1;
sampler2D _Tex2;
sampler2D _BlendTex;
float4 _Color;
float _MaxHeight;

struct Input {
	float4 WorldPos: TEXCOORD4;
	float2 uv_Tex1: TEXCOORD2;
	float2 uv_Tex2: TEXCOORD3;
};

void surf (Input IN, inout SurfaceOutput o) {
	float interp = clamp(IN.WorldPos.y / _MaxHeight,0,1);
	
	half4 c = tex2D(_Tex1, IN.uv_Tex1)*interp + tex2D(_Tex2,IN.uv_Tex2)*(1.0-interp) * _Color;
//	half4 c = tex2D(_BlendTex,IN.uv_BlendTex);
	o.Albedo = c.rgb;
	o.Alpha = c.a;
}
ENDCG

	} 

	Fallback "Diffuse"
}

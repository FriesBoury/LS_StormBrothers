Shader "_OwlShader"
{
	Properties 
	{
_Huxleydiffuse("_Huxleydiffuse", 2D) = "black" {}
_Rimlightintensity("_Rimlightintensity", Range(0,2) ) = 1.865
_Rimpower("_Rimpower", Range(0.1,10) ) = 4.43
_Rimcolor("_Rimcolor", Color) = (0.2100691,0.3927378,0.6119403,1)
_DiffuseMainColor("_DiffuseMainColor", Color) = (0.1176208,0.1437102,0.2462686,1)
_ExtraIllumination("_ExtraIllumination", Range(0,1) ) = 0.649
_EyesAlpha("_EyesAlpha", 2D) = "black" {}

	}
	
	SubShader 
	{
		Tags
		{
"Queue"="Geometry"
"IgnoreProjector"="False"
"RenderType"="Opaque"

		}

		
Cull Back
ZWrite On
ZTest LEqual
ColorMask RGBA
Fog{
}


		CGPROGRAM
#pragma surface surf BlinnPhongEditor  vertex:vert
#pragma target 2.0


sampler2D _Huxleydiffuse;
float _Rimlightintensity;
float _Rimpower;
float4 _Rimcolor;
float4 _DiffuseMainColor;
float _ExtraIllumination;
sampler2D _EyesAlpha;

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
				half4 Custom;
			};
			
			inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
			{
half3 spec = light.a * s.Gloss;
half4 c;
c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
c.a = s.Alpha;
return c;

			}

			inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 h = normalize (lightDir + viewDir);
				
				half diff = max (0, dot ( lightDir, s.Normal ));
				
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0);
				
				half4 res;
				res.rgb = _LightColor0.rgb * diff;
				res.w = spec * Luminance (_LightColor0.rgb);
				res *= atten * 2.0;

				return LightingBlinnPhongEditor_PrePass( s, res );
			}
			
			struct Input {
				float2 uv_Huxleydiffuse;
float3 viewDir;
float2 uv_EyesAlpha;

			};

			void vert (inout appdata_full v, out Input o) {
float4 VertexOutputMaster0_0_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_1_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_2_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_3_NoInput = float4(0,0,0,0);


			}
			

			void surf (Input IN, inout EditorSurfaceOutput o) {
				o.Normal = float3(0.0,0.0,1.0);
				o.Alpha = 1.0;
				o.Albedo = 0.0;
				o.Emission = 0.0;
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Custom = 0.0;
				
float4 Tex2D0=tex2D(_Huxleydiffuse,(IN.uv_Huxleydiffuse.xyxy).xy);
float4 Add1=_DiffuseMainColor + Tex2D0;
float4 Fresnel0_1_NoInput = float4(0,0,1,1);
float4 Fresnel0=(1.0 - dot( normalize( float4( IN.viewDir.x, IN.viewDir.y,IN.viewDir.z,1.0 ).xyz), normalize( Fresnel0_1_NoInput.xyz ) )).xxxx;
float4 Pow0=pow(Fresnel0,_Rimpower.xxxx);
float4 Multiply0=_Rimcolor * Pow0;
float4 Multiply1=Multiply0 * _Rimlightintensity.xxxx;
float4 Tex2D1=tex2D(_EyesAlpha,(IN.uv_EyesAlpha.xyxy).xy);
float4 Multiply3=Tex2D0 * Tex2D1.aaaa;
float4 Add0=Multiply1 + Multiply3;
float4 Master0_1_NoInput = float4(0,0,1,1);
float4 Master0_3_NoInput = float4(0,0,0,0);
float4 Master0_4_NoInput = float4(0,0,0,0);
float4 Master0_5_NoInput = float4(1,1,1,1);
float4 Master0_7_NoInput = float4(0,0,0,0);
float4 Master0_6_NoInput = float4(1,1,1,1);
o.Albedo = Add1;
o.Emission = Add0;

				o.Normal = normalize(o.Normal);
			}
		ENDCG
	}
	Fallback "Diffuse"
}
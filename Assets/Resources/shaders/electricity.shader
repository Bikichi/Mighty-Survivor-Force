Shader "Hovl/Particles/Electricity" {
	Properties {
		_MainTexture ("Main Texture", 2D) = "white" {}
		_Dissolveamount ("Dissolve amount", Range(0, 1)) = 0.332
		_Mask ("Mask", 2D) = "white" {}
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
		_Emission ("Emission", Float) = 6
		_RemapXYFresnelZW ("Remap XY/Fresnel ZW", Vector) = (-10,10,2,2)
		_Speed ("Speed", Vector) = (0.189,0.225,-0.2,-0.05)
		_Opacity ("Opacity", Range(0, 1)) = 1
		[MaterialToggle] _Usedepth ("Use depth?", Float) = 0
		_Depth ("Depth", Float) = 0.15
		[Enum(Cull Off,0, Cull Front,1, Cull Back,2)] _CullMode ("Culling", Float) = 2
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
}
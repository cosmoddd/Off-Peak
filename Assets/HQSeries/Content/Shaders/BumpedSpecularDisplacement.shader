Shader "Ship/Tessellation/Bumped Specular (displacement)" 
{
	Properties 
	{
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess ("Shininess", Range (0.03, 1)) = 0.078125
		_ParallaxOffset ("Height Offset", float) = 0.0
		_Parallax ("Height", float) = 0.0
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_ParallaxMap ("Heightmap (A)", 2D) = "black" {}
		_RimColor ("Rim Color", Color) = (0.5,0.5,0.5,1)
		_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
		_EdgeLength ("Edge length", Range(3,50)) = 10
	}
SubShader 
{ 
	Tags { "RenderType"="Opaque" }
	LOD 800
	
	CGPROGRAM
	#pragma surface surf BlinnPhong addshadow vertex:disp tessellate:tessEdge
	#include "Tessellation.cginc"


struct appdata 
{
	fixed4 vertex : POSITION;
	fixed4 tangent : TANGENT;
	fixed3 normal : NORMAL;
	fixed2 texcoord : TEXCOORD0;
	fixed2 texcoord1 : TEXCOORD1;
};

fixed _Parallax, _ParallaxOffset, _EdgeLength;

fixed4 tessEdge (appdata v0, appdata v1, appdata v2)
{
	return UnityEdgeLengthBasedTessCull (v0.vertex, v1.vertex, v2.vertex, _EdgeLength, _Parallax * 1.5f ) ;
}

sampler2D _ParallaxMap;

void disp (inout appdata v)
{
	fixed d = (tex2Dlod(_ParallaxMap, float4(v.texcoord.xy,0,0)).a + _ParallaxOffset) * _Parallax ;
	d -= _Parallax * .5;
	v.vertex.xyz += v.normal * d;
}

sampler2D _MainTex;
sampler2D _BumpMap;
fixed _Shininess, _RimPower;
fixed4 _RimColor;

struct Input 
{
	fixed2 uv_MainTex;
	fixed2 uv_BumpMap;
	fixed3 viewDir;
};

	void surf (Input IN, inout SurfaceOutput o) 
	{
		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = tex.rgb;
		o.Gloss = tex.a;
		o.Alpha = tex.a;
		o.Specular = _Shininess;
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		
		fixed rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
		o.Albedo += tex.rgb * (pow (rim, _RimPower) * (_RimColor - .5) * 4);
	}
	ENDCG
}

FallBack "Bumped Specular"
}

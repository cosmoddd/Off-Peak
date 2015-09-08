Shader "Rim Bumped Specular" 
{
Properties 
{	
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess ("Shininess", Range (0.03, 1)) = 0.078125
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_RimColor ("Rim Color", Color) = (0.5,0.5,0.5,1)
	_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
}
SubShader 
{ 
	Tags { "RenderType"="Opaque" }
	LOD 400
	
	CGPROGRAM
	#pragma surface surf BlinnPhong
	#pragma target 3.0

	
	sampler2D _MainTex;
	sampler2D _BumpMap;
	fixed4 _Color,_RimColor;
	fixed _Shininess , _RimPower;
	
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
		o.Alpha = tex.a * _Color.a;
		o.Specular = _Shininess;
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		
		fixed rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
		o.Albedo += tex.rgb * (pow (rim, _RimPower) * (_RimColor - .5) * 4);
	}
	ENDCG
}

FallBack "Specular"
}

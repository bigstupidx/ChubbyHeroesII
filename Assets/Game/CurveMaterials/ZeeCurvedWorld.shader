﻿Shader "Custom/ZeeCurvedWorld" {
	Properties{
		// Diffuse texture
		_MainTex("Base (RGB)", 2D) = "white" {}
		// Degree of curvature
		_Curvature("Curvature", Float) = 0.001
		_CurvatureSide("CurvatureSide", Float) = 0.001
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Surface shader function is called surf, and vertex preprocessor function is called vert
		// addshadow used to add shadow collector and caster passes following vertex modification
#pragma surface surf Lambert vertex:vert addshadow

		// Access the shaderlab properties
		uniform sampler2D _MainTex;
	uniform float _Curvature;
	uniform float _CurvatureSide;

	// Basic input structure to the shader function
	// requires only a single set of UV texture mapping coordinates
	struct Input {
		float2 uv_MainTex;
	};

	// This is where the curvature is applied by ZEE
	//void vert(inout appdata_full v)
	//{
	//	// Transform the vertex coordinates from model space into world space
	//	float4 vv = mul(_Object2World, v.vertex);
	//	float4 vx = mul(_Object2World, v.vertex);

	//	// Now adjust the coordinates to be relative to the camera position
	//	vv.xyz -= _WorldSpaceCameraPos.xyz;
	//	vx.xyz -= _WorldSpaceCameraPos.xyz;
	//	// Reduce the y coordinate (i.e. lower the "height") of each vertex based
	//	// on the square of the distance from the camera in the z axis, multiplied
	//	// by the chosen curvature factor
	//	//  vv = float4( 0.0f, (vv.z * vv.z) * - _Curvature, 0.0f, 0.0f );
	//	vx = float4(((vv.z * vv.z)) * -_CurvatureSide, ((vv.z * vv.z)) * -_Curvature, 0.0f, 0.0f);
	//	//vv = float4( 0.0f, ((vv.z * vv.z) + (vv.x * vv.x)) * - _Curvature, 0.0f, 0.0f )

	//	// Now apply the offset back to the vertices in model space
	//	//  v.vertex += mul(_World2Object, vv);
	//	v.vertex += mul(_World2Object, vx);
	//}

	//Rares
	void vert(inout appdata_full v)
	{
		float4 vv = mul(_Object2World, v.vertex);
		vv.xyz -= _WorldSpaceCameraPos.xyz;

		vv = float4(0.0f, ((vv.x * vv.x)) * -_Curvature * 0.01, 0.0f, 0.0f);
		//vv = float4(0.0f, ((vv.z * vv.z) + (vv.x * vv.x)) * -_Curvature, 0.0f, 0.0f);
		v.vertex += mul(_World2Object, vv);
	}

	// This is just a default surface shader
	void surf(Input IN, inout SurfaceOutput o) {
		half4 c = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG
	}
		// FallBack "Diffuse"
}
Shader "GIT/CurvedDifuse" {
	Properties {
  	_MainTex ("Base (RGB)", 2D) = "white" {}
  	_QOffset ("Offset", Vector) = (0,0,0,0)
  	_Dist ("Distance", Float) = 100.0
	_Color("Main Color", Color) = (0,0,0,1)
  }
	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass
		{
			Lighting Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"

			fixed4 _Color;
			sampler2D _MainTex;
			float4 _QOffset;
			float _Dist;
			uniform float4 _MainTex_ST;
			
			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
			};

			v2f vert (appdata_base v)
			{
				v2f o;
				float4 vPos = mul (UNITY_MATRIX_MV, v.vertex);
				float zOff = vPos.z/_Dist*0.0001;
				vPos += _QOffset*zOff*zOff;
				o.pos = mul (UNITY_MATRIX_P, vPos);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}

			half4 frag (v2f i) : COLOR
			{
				// sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
				UNITY_APPLY_FOG(i.fogCoord, col);
				//return tex2D(_MainTex, i.uv);
				return col * _Color;
			}
			ENDCG
		}
	}
	FallBack "Mobile/Unlit"
}
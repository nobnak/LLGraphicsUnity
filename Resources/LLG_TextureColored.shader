// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Simple "just colors" shader that's used for built-in debug visualizations,
// in the editor etc. Just outputs _Color * vertex color; and blend/Z/cull/bias
// controlled by material parameters.

Shader "LLG/TextureColored" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}

		_SrcBlend("SrcBlend", Int) = 5.0 // SrcAlpha
		_DstBlend("DstBlend", Int) = 10.0 // OneMinusSrcAlpha
		_ZWrite("ZWrite", Int) = 1.0 // On
		_ZTest("ZTest", Int) = 4.0 // LEqual
		_Cull("Cull", Int) = 0.0 // Off
		_ZBias("ZBias", Float) = 0.0
		_ColorMask("ColorMask", Int) = 0.0 // Off
	}

		SubShader{
			Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
			Pass {
				Blend[_SrcBlend][_DstBlend]
				ZWrite[_ZWrite]
				ZTest[_ZTest]
				Offset[_ZBias],[_ZBias]
				Cull[_Cull]
				ColorMask[_ColorMask]

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				#pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
				#include "UnityCG.cginc"

				struct appdata_t {
					float4 vertex : POSITION;
					float4 color : COLOR;
					float2 uv : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};
				struct v2f {
					float4 color : COLOR;
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					UNITY_VERTEX_OUTPUT_STEREO
				};

				float4 _Color;
				sampler2D _MainTex;

				v2f vert(appdata_t v) {
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color = v.color * _Color;
					o.uv = v.uv;
					return o;
				}
				fixed4 frag(v2f i) : SV_Target{
					float4 cmain = tex2D(_MainTex, i.uv);
					return cmain * i.color;
				}
				ENDCG
			}
		}
}

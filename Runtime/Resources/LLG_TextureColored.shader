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
			ENDCG
		}
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
			ENDCG
		}
	}

		
			CGINCLUDE
			#pragma target 2.0
			#pragma multi_compile ___ NO_VERTEX_COLOR
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			struct v2g {
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
				float2 px : TEXCOORD1;
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
			float4 _MainTex_TexelSize;

			v2f vert(appdata_t IN) {
				float4 c = _Color;

				#ifndef NO_VERTEX_COLOR
				c *= IN.color;
				#endif

				float2 uv = IN.uv;
				if (_MainTex_TexelSize.y < 0) uv.y = 1 - uv.y;

				v2f o;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.vertex = UnityObjectToClipPos(IN.vertex);
				o.color = c;
				o.uv = IN.uv;
				return o;
			}
			v2g vert_px(appdata_t IN) {
				float4 c = _Color;

				#ifndef NO_VERTEX_COLOR
				c *= IN.color;
				#endif

				float2 uv = IN.uv;
				if (_MainTex_TexelSize.y < 0) uv.y = 1 - uv.y;

				float4 pos = UnityObjectToClipPos(IN.vertex);
				float2 px = 0.5 * (pos.xy / pos.w + 1) * _ScreenParams.xy;

				v2g o;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.vertex = pos;
				o.color = c;
				o.uv = IN.uv;
				o.px = px;
				return o;
			}

			[maxvertexcount(6)]
			void geom_thick_line(line v2g IN, inout TriangleStream<v2f> OUT) {

			}

			fixed4 frag(v2f IN) : SV_Target{
				float4 cmain = tex2D(_MainTex, IN.uv);
				return cmain * IN.color;
			}
			ENDCG
}

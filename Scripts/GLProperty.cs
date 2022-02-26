using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace LLGraphicsUnity {

	public class GLProperty {

		public enum ZTestEnum {
			NEVER = 1, LESS = 2, EQUAL = 3, LESSEQUAL = 4,
			GREATER = 5, NOTEQUAL = 6, GREATEREQUAL = 7, ALWAYS = 8
		};
		public enum CullEnum {
			None = 0, Front = 1, Back = 2
		};
		[System.Flags]
		public enum ColorMaskEnum {
			None = 0,
			A = 1,
			B = 1 << 1,
			G = 1 << 2,
			R = 1 << 3,
			RGB = R | G | B,
			ALL = RGB | A
		}

		public enum KW_VERTEX_COLOR { ___ = 0, NO_VERTEX_COLOR }

		public static readonly int P_COLOR = Shader.PropertyToID("_Color");
		public static readonly int P_MainTex = Shader.PropertyToID("_MainTex");


		public static readonly int P_SRC_BLEND = Shader.PropertyToID("_SrcBlend");
		public static readonly int P_DST_BLEND = Shader.PropertyToID("_DstBlend");
		public static readonly int P_ZWRITE = Shader.PropertyToID("_ZWrite");
		public static readonly int P_ZTEST = Shader.PropertyToID("_ZTest");
		public static readonly int P_ZBIAS = Shader.PropertyToID("_ZBias");
		public static readonly int P_CULL = Shader.PropertyToID("_Cull");
		public static readonly int P_ColorMask = Shader.PropertyToID("_ColorMask");

		public Color Color = Color.white;
		public Texture MainTex = null;

		public BlendMode SrcBlend = BlendMode.SrcAlpha;
		public BlendMode DstBlend = BlendMode.OneMinusSrcAlpha;

		public bool ZWriteMode = true;
		public ZTestEnum ZTestMode = ZTestEnum.LESSEQUAL;
		public float ZBias = 0f;

		public CullEnum Cull = CullEnum.None;
		public ColorMaskEnum ColorMask = ColorMaskEnum.ALL;

		public KW_VERTEX_COLOR VertexColor = default;

		public GLProperty() { }
		public GLProperty(GLProperty src) {
			Copy(src, this);
		}
		public GLProperty(Material src) {
			Copy(src, this);
		}

		#region interface
		public GLProperty Set(Material mat) {
			Copy(this, mat);
			return this;
		}
		#endregion

		#region static
		public static void Copy(Material src, GLProperty dst) {
			dst.Color = src.color;
			dst.MainTex = src.mainTexture;

			dst.SrcBlend = (BlendMode)src.GetInt(P_SRC_BLEND);
			dst.DstBlend = (BlendMode)src.GetInt(P_DST_BLEND);

			dst.ZWriteMode = src.GetInt(P_ZWRITE) == 1;
			dst.ZTestMode = (GLProperty.ZTestEnum)src.GetInt(P_ZTEST);
			dst.ZBias = src.GetFloat(P_ZBIAS);

			dst.Cull = (CullEnum)src.GetInt(P_CULL);
			dst.ColorMask = (ColorMaskEnum)src.GetInt(P_ColorMask);

			dst.VertexColor = src.IsKeywordEnabled(KW_VERTEX_COLOR.NO_VERTEX_COLOR.ToString()) ? 
				KW_VERTEX_COLOR.NO_VERTEX_COLOR : default;
		}
		public static void Copy(GLProperty src, GLProperty dst) {
			dst.Color = src.Color;
			dst.MainTex = src.MainTex;

			dst.SrcBlend = src.SrcBlend;
			dst.DstBlend = src.DstBlend;

			dst.ZWriteMode = src.ZWriteMode;
			dst.ZTestMode = src.ZTestMode;
			dst.ZBias = src.ZBias;

			dst.Cull = src.Cull;
			dst.ColorMask = src.ColorMask;

			dst.VertexColor = src.VertexColor;
		}
		public static void Copy(GLProperty src, Material dst) {
			dst.color = src.Color;
			dst.mainTexture = src.MainTex;

			dst.SetInt(P_SRC_BLEND, (int)src.SrcBlend);
			dst.SetInt(P_DST_BLEND, (int)src.DstBlend);

			dst.SetInt(P_ZWRITE, src.ZWriteMode ? 1 : 0);
			dst.SetInt(P_ZTEST, (int)src.ZTestMode);
			dst.SetFloat(P_ZBIAS, src.ZBias);

			dst.SetInt(P_CULL, (int)src.Cull);
			dst.SetInt(P_ColorMask, (int)src.ColorMask);

			dst.shaderKeywords = null;
			if (src.VertexColor != default) dst.EnableKeyword(src.VertexColor.ToString());
		}
		#endregion
	}
}

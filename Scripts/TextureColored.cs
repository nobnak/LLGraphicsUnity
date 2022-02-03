using Gist2.Extensions.ComponentExt;
using UnityEngine;
using UnityEngine.Rendering;

namespace LLGraphicsUnity {

	public class TextureColored : System.IDisposable {

        public enum ZTestEnum {
            NEVER = 1, LESS = 2, EQUAL = 3, LESSEQUAL = 4,
            GREATER = 5, NOTEQUAL = 6, GREATEREQUAL = 7, ALWAYS = 8
        };

        public const string LINE_SHADER = "LLG_TextureColored";

        public static readonly int P_COLOR =  Shader.PropertyToID("_Color");
		public static readonly int P_MainTex = Shader.PropertyToID("_MainTex");


		public static readonly int P_SRC_BLEND = Shader.PropertyToID("_SrcBlend");
        public static readonly int P_DST_BLEND = Shader.PropertyToID("_DstBlend");
        public static readonly int P_ZWRITE = Shader.PropertyToID("_ZWrite");
        public static readonly int P_ZTEST = Shader.PropertyToID("_ZTest");
        public static readonly int P_CULL = Shader.PropertyToID("_Cull");
        public static readonly int P_ZBIAS = Shader.PropertyToID("_ZBias");

		protected Material mat;

		public TextureColored() {
			var lineShader = Shader.Find (LINE_SHADER);
			mat = new Material (lineShader);
		}

		#region static
		public static implicit operator Material(TextureColored v) {
			return v.mat;
		}
		#endregion

		#region interface

		#region IDisposable implementation
		public void Dispose() {
			mat.Destroy();
			mat = null;
		}
		#endregion

		public TextureColored Set(Data d) {
			mat.color = d.Color;
			mat.mainTexture = d.MainTex;

			mat.SetInt(P_SRC_BLEND, (int)d.SrcBlend);
			mat.SetInt(P_DST_BLEND, (int)d.DstBlend);

			mat.SetInt(P_ZWRITE, d.ZWriteMode ? 1 : 0);
			mat.SetInt(P_ZTEST, (int)d.ZTestMode);
			mat.SetFloat(P_ZBIAS, d.ZBias);

			return this;
		}
		public Data Get() {
			return new Data() {
				Color = mat.color,
				MainTex = mat.mainTexture,

				SrcBlend = (BlendMode)mat.GetInt(P_SRC_BLEND),
				DstBlend = (BlendMode)mat.GetInt(P_DST_BLEND),

				ZWriteMode = mat.GetInt(P_ZWRITE) == 1,
				ZTestMode = (ZTestEnum)mat.GetInt(P_ZTEST),
				ZBias = mat.GetFloat(P_ZBIAS)
			};
		}
		#endregion

		#region classes
		public class Data {
			public Color Color = Color.white;
			public Texture MainTex = null;

			public BlendMode SrcBlend = BlendMode.SrcAlpha;
			public BlendMode DstBlend = BlendMode.OneMinusSrcAlpha;

			public bool ZWriteMode = true;
			public ZTestEnum ZTestMode = ZTestEnum.LESSEQUAL;
			public float ZBias = 0f;
		}
		#endregion
	}
}
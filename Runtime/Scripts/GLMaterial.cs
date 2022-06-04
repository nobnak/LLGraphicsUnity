using UnityEngine;
using Gist2.Extensions.ComponentExt;

namespace LLGraphicsUnity {

    public class GLMaterial : System.IDisposable {

        public const string LINE_SHADER = "LLG_TextureColored";

		protected Material mat;

		public GLMaterial() {
			var lineShader = Resources.Load<Shader>(LINE_SHADER);
			mat = new Material (lineShader);
		}

		#region static
		public static implicit operator Material(GLMaterial v) {
			return v.mat;
		}
		#endregion

		#region interface

		#region IDisposable
		public void Dispose() {
			mat.Destroy();
			mat = null;
		}
		#endregion

		public GLMaterial LoadProperty(GLProperty d) {
			d.Set(mat);
			return this;
		}
		public GLProperty GetProperty() => new GLProperty(mat);
		public GLPropertyScope GetScope(GLProperty next, int pass = 0) {
			var scope = new GLPropertyScope(this);
			if (next != null) LoadProperty(next);
			SetPass(pass);
			return scope;
		}
		public GLPropertyScope GetScope(GLProperty next, ShaderPass pass)
			=> GetScope(next, (int)pass);
		public GLPropertyScope GetScope(int pass = 0) => GetScope(null, pass);

		public bool SetPass(int pass = 0) => mat.SetPass(pass);
		#endregion

		#region classes
		#endregion
	}
}
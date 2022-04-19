using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LLGraphicsUnity {

	public class GLMatrixScope : System.IDisposable {

		public GLMatrixScope() {
			GL.PushMatrix();
		}

		public void Dispose() {
			GL.PopMatrix();
		}
	}

	public class GLModelViewScope : System.IDisposable {

		protected Matrix4x4 modelview;

		public GLModelViewScope() {
			this.modelview = GL.modelview;
		}
		public GLModelViewScope(Matrix4x4 mult) : this() {
			GL.MultMatrix(mult);
		}

		public void Dispose() {
			GL.modelview = modelview;
		}
	}
}
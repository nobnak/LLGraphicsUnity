using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LLGraphicsUnity {

	public class GLPropertyScope : System.IDisposable {

		protected GLMaterial mat;
		protected GLProperty prev;

		public GLPropertyScope(GLMaterial mat) {
			this.mat = mat;
			this.prev = mat.GetProperty();
		}

		public void Dispose() {
			mat.LoadProperty(prev);
		}
	}
}

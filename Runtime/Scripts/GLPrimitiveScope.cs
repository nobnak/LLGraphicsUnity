using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LLGraphicsUnity {

	public class GLPrimitiveScope : System.IDisposable {

		public GLPrimitiveScope(int mode) {
			GL.Begin(mode);
		}
		public void Dispose() {
			GL.End();
		}
	}
}

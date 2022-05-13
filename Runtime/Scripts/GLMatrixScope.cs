using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
			GL.modelview *= mult;
		}
		public GLModelViewScope(float3 translate, quaternion rotation, float3 scale)
			: this(float4x4.TRS(translate, rotation, scale)) { }

		public void Dispose() {
			GL.modelview = modelview;
		}
	}
}
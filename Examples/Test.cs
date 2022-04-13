using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LLGraphicsUnity {

	[ExecuteAlways]
	public class Test : MonoBehaviour {

		public Texture mainTex;

		protected GLMaterial mat;

		#region unity
		protected void OnEnable() {
			mat = new GLMaterial();
		}
		protected void OnDisable() {
			mat.Dispose();
		}
		protected void OnRenderObject() {
			var c = Camera.current;
			if (c != Camera.main || mat == null || !isActiveAndEnabled) return;

			var data = new GLProperty() {
				Color = Color.white,
				MainTex = null,
				ZWriteMode = false,
				ZTestMode = GLProperty.ZTestEnum.ALWAYS,
			};
			var aspect = c.aspect;
			var size = 0.5f;
			var rot = Quaternion.identity;
			var scale = new Vector3(size / aspect, size, 1f);

			using (mat.GetScope(data))
			using (new GLMatrixScope()) {
				GL.LoadOrtho();
				GL.LoadIdentity();

				using (mat.GetScope(new GLProperty(data) { Color = Color.red }))
				using (new GLModelViewScope(Matrix4x4.TRS(new Vector3(scale.x * .5f, scale.y * .5f, -1), rot, scale))) {
					GLTool.DrawQuad();
				}

				using (mat.GetScope(new GLProperty(data) { MainTex = mainTex }))
				using (new GLModelViewScope(Matrix4x4.TRS(new Vector3(scale.x * .5f, scale.y * 1.5f, -1), rot, scale))) {
					GLTool.DrawQuad();
				}

				using (mat.GetScope(new GLProperty(data) { Color = Color.magenta }))
				using (new GLModelViewScope(Matrix4x4.TRS(new Vector3(scale.x * 1.5f, scale.y * .5f, -1), rot, .8f * scale))) {
					GLTool.QuadOutlineVertices().DrawLineStrip();
				}
				using (mat.GetScope(new GLProperty(data) { Color = Color.cyan }))
				using (new GLModelViewScope(Matrix4x4.TRS(new Vector3(scale.x * 1.5f, scale.y * 1.5f, -1), rot, .8f * scale))) {
					GLTool.CircleOutlineVertices(0.5f, 50).DrawLineStrip();
				}
			}
		}
		#endregion
	}
}

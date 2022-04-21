using LLGraphicsUnity.Shapes;
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
			if (mat == null || !isActiveAndEnabled) return;

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

			if (c == Camera.main) {

				using (mat.GetScope(data))
				using (new GLMatrixScope()) {
					GL.LoadIdentity();
					GL.LoadOrtho();

					using (mat.GetScope(new GLProperty(data) { Color = Color.red }))
					using (new GLModelViewScope(Matrix4x4.TRS(new Vector3(scale.x * .5f, scale.y * .5f, -1), rot, scale))) {
						Quad.TriangleStrip();
					}

					using (mat.GetScope(new GLProperty(data) { MainTex = mainTex }))
					using (new GLModelViewScope(Matrix4x4.TRS(new Vector3(scale.x * .5f, scale.y * 1.5f, -1), rot, scale))) {
						Quad.TriangleStrip();
					}

					using (mat.GetScope(new GLProperty(data) { Color = Color.magenta }))
					using (new GLModelViewScope(Matrix4x4.TRS(new Vector3(scale.x * 1.5f, scale.y * .5f, -1), rot, .8f * scale))) {
						Quad.LineStrip();
					}
					using (mat.GetScope(new GLProperty(data) { Color = Color.cyan }))
					using (new GLModelViewScope(Matrix4x4.TRS(new Vector3(scale.x * 1.5f, scale.y * 1.5f, -1), rot, .8f * scale))) {
						Circle.LineStrip(0.5f, 50);
					}
				}
			}

			rot = Quaternion.Euler(new Vector3(-5f, 15f, 0f));
			using (new GLMatrixScope()) {
				GL.LoadIdentity();
				GL.LoadProjectionMatrix(c.projectionMatrix);
				GL.modelview = c.worldToCameraMatrix;

				using (mat.GetScope(new GLProperty(data) { Color = Color.yellow }))
				using (new GLModelViewScope(Matrix4x4.TRS(new Vector3(.5f, .5f, 0), rot, .8f * Vector3.one))) {
					Box.Lines();
				}
				using (mat.GetScope(new GLProperty(data) { Color = Color.green }))
				using (new GLModelViewScope(Matrix4x4.TRS(new Vector3(.5f, -.5f, 0), rot, .8f * Vector3.one))) {
					Box.Triangles();
				}
			}
		}
		#endregion
	}
}

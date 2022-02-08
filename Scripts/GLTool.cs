using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LLGraphicsUnity {

	public static class GLTool {

		public static void DrawQuad() {
			var v = 0.5f;
			var u = 0;
			GL.Begin(GL.TRIANGLE_STRIP);

			GL.MultiTexCoord2(u, 0f, 0f);
			GL.Vertex3(-v, -v, 0f);

			GL.MultiTexCoord2(u, 0f, 1f);
			GL.Vertex3(-v,  v, 0f);

			GL.MultiTexCoord2(u, 1f, 0f);
			GL.Vertex3(v, -v, 0f);

			GL.MultiTexCoord2(u, 1f, 1f);
			GL.Vertex3( v,  v, 0f);

			GL.End();
		}

		public static void DrawLineStrip(this IEnumerable<Vector3> vertices) {
			GL.Begin(GL.LINE_STRIP);
			foreach (var v in vertices)
				GL.Vertex(v);
			GL.End();
		}
		public static void DrawLineStrip(this IEnumerable<Vector2> vertices, float z = 0f)
			=> vertices.Select(v => new Vector3(v.x, v.y, z)).DrawLineStrip();

		public static IEnumerable<Vector3> QuadOutlineVertices() {
			var v = .5f;
			yield return new Vector3(-v, -v, 0);
			yield return new Vector3(-v, v, 0);
			yield return new Vector3(v, v, 0);
			yield return new Vector3(v, -v, 0);
			yield return new Vector3(-v, -v, 0);
		}

		public static Matrix4x4 SquareOrtho(float aspect, float height = 1f)
			=> Matrix4x4.Ortho(0f, aspect * height, 0f, height, 1f, -100f);

		public static void LoadSquareOrtho(float aspect, float height = 1f)
			=> GL.LoadProjectionMatrix(SquareOrtho(aspect, height));
	}

}
using LLGraphicsUnity.Shapes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LLGraphicsUnity {

	public static class GLTool {


		public static void DrawLineStrip(this IEnumerable<Vector3> vertices) {
			GL.Begin(GL.LINE_STRIP);
			foreach (var v in vertices)
				GL.Vertex(v);
			GL.End();
		}
		public static void DrawLineStrip(this IEnumerable<Vector2> vertices, float z = 0f)
			=> vertices.Select(v => new Vector3(v.x, v.y, z)).DrawLineStrip();

		public static Matrix4x4 SquareOrtho(float aspect, float height = 1f)
			=> Matrix4x4.Ortho(0f, aspect * height, 0f, height, 1f, -100f);

		public static void LoadSquareOrtho(float aspect, float height = 1f)
			=> GL.LoadProjectionMatrix(SquareOrtho(aspect, height));
	}

}
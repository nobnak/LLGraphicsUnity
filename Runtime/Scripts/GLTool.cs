using LLGraphicsUnity.Shapes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace LLGraphicsUnity {

	public static class GLTool {


		public static void LineStrip(this IEnumerable<float3> vertices) {
			GL.Begin(GL.LINE_STRIP);
			foreach (var v in vertices)
				GL.Vertex(v);
			GL.End();
		}
		public static void LineStrip(this IEnumerable<float2> vertices, float z = 0f)
			=> vertices.Select(v => new float3(v, z)).LineStrip();
		public static void LineStrip(this IEnumerable<Vector3> vertices)
			=> vertices.Cast<float3>().LineStrip();
		public static void LineStrip(this IEnumerable<Vector2> vertices, float z = 0f)
			=> vertices.Select(v => new float3(v, z)).LineStrip();

		public static Matrix4x4 SquareOrtho(float aspect, float height = 1f)
			=> Matrix4x4.Ortho(0f, aspect * height, 0f, height, 1f, -100f);

		public static void LoadSquareOrtho(float aspect, float height = 1f)
			=> GL.LoadProjectionMatrix(SquareOrtho(aspect, height));
	}

}
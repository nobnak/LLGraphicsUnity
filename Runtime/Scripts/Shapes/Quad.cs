using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LLGraphicsUnity.Shapes {

    public static class Quad {

		public static readonly Vector3[] UVs = new Vector3[] {
			new Vector3(0f, 0f, 0f),
			new Vector3(0f, 1f, 0f),
			new Vector3(1f, 0f, 0f),
			new Vector3(1f, 1f, 0f),
		};
		public static readonly Vector3[] Vertices = new Vector3[] {
			new Vector3(-.5f, -.5f, 0f),
			new Vector3(-.5f,  .5f, 0f),
			new Vector3( .5f, -.5f, 0f),
			new Vector3( .5f,  .5f, 0f),
		};

		public static readonly int[] Indices_TriangleStrip = new int[] { 0, 1, 2, 3 };
		public static readonly int[] Indices_LineStrip = new int[] { 0, 1, 3, 2, 0 };

		public static void TriangleStrip() {
			GL.Begin(GL.TRIANGLE_STRIP);
			foreach (var i in Indices_TriangleStrip) {
				GL.TexCoord(UVs[i]);
				GL.Vertex(Vertices[i]);
			}
			GL.End();
		}
		public static void LineStrip() {
			GL.Begin(GL.LINE_STRIP);
			foreach (var i in Indices_LineStrip) {
				GL.TexCoord(UVs[i]);
				GL.Vertex(Vertices[i]);
			}
			GL.End();
		}
	}
}

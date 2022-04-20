using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LLGraphicsUnity.Shapes {

	public static class Circle {

		public static void LineStrip(float radius, int steps) {
			GL.Begin(GL.LINE_STRIP);
			var rad = 2f * Mathf.PI / steps;
			for (var i = 0; i <= steps; i++)
				GL.Vertex3(radius * Mathf.Cos(i * rad), radius * Mathf.Sin(i * rad), 0f);
			GL.End();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LLGraphicsUnity.Shapes {

    public static class Box {

        public static readonly Vector3[] Vertices = new Vector3[] {
            new Vector3(-.5f, -.5f, -.5f),
            new Vector3( .5f, -.5f, -.5f),
            new Vector3(-.5f,  .5f, -.5f),
            new Vector3( .5f,  .5f, -.5f),

            new Vector3(-.5f, -.5f,  .5f),
            new Vector3( .5f, -.5f,  .5f),
            new Vector3(-.5f,  .5f,  .5f),
            new Vector3( .5f,  .5f,  .5f),
        };

        public static readonly int[] Indices_Triangles = new int[] {
            0, 2, 3, 0, 3, 1,
            1, 3, 7, 1, 7, 5,
            5, 7, 6, 5, 6, 4,
            4, 6, 2, 4, 2, 0,
            2, 6, 7, 2, 7, 3,
            4, 0, 1, 4, 1, 5,
        };
        public static readonly int[] Indices_Lines = new int[] {
            0, 2, 2, 3, 3, 1, 1, 0,
            5, 7, 7, 6, 6, 4, 4, 5,
            1, 5, 3, 7, 0, 4, 2, 6,
        };

        public static void Triangles() {
            GL.Begin(GL.TRIANGLES);
            foreach (var i in Indices_Triangles)
                GL.Vertex(Vertices[i]);
            GL.End();
        }
        public static void Lines() {
            GL.Begin(GL.LINES);
            foreach (var i in Indices_Lines)
                GL.Vertex(Vertices[i]);
            GL.End();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Marching;

namespace Marching {
	
	public static class MeshGenerator {

		const int A = 8;
		const int B = 4;
		const int C = 2;
		const int D = 1;
	
		public static Mesh SquareGenerate(Square square) {
			Vector3[] vertices = new Vector3[0];
			int[] indices = new int[0];

			switch(square.State) {
				// active point is one.
				case 8:
					vertices = new Vector3[] { square.PosA, square.up, square.right };
					indices = new int[] { 0, 1, 2 };
					break;
				case 4:
					vertices = new Vector3[] { square.PosB, square.right, square.up };
					indices = new int[] { 0, 1, 2 };
					break;
				case 2:
					vertices = new Vector3[] { square.PosC, square.down, square.right };
					indices = new int[] { 0, 1, 2 };
					break;
				case 1:
					vertices = new Vector3[] { square.PosD, square.left, square.down };
					indices = new int[] { 0, 1, 2 };
					break;
				// active points are two.
				case 12:
					vertices = new Vector3[] { square.PosA, square.PosB, square.right, square.left };
					indices = new int[] { 0, 1, 2, 2, 3, 0 };
					break;
				case 6:
					vertices = new Vector3[] { square.PosB, square.PosC, square.down, square.up };
					indices = new int[] { 0, 1, 2, 2, 3, 0 };
					break;
				case 3:
					vertices = new Vector3[] { square.PosC, square.PosD, square.left, square.right };
					indices = new int[] { 0, 1, 2, 2, 3, 0 };
					break;
				case 9:
					vertices = new Vector3[] { square.PosD, square.PosA, square.up, square.down };
					indices = new int[] { 0, 1, 2, 2, 3, 0 };
					break;
				// active points are three.
				case 14:
					vertices = new Vector3[] { square.PosA, square.PosB, square.PosC, square.down, square.left };
					indices = new int[] { 0, 1, 2, 2, 3, 4, 4, 0, 2 };
					break;
				case 7:
					vertices = new Vector3[] { square.PosB, square.PosC, square.PosD, square.left, square.up };
					indices = new int[] { 0, 1, 2, 2, 3, 4, 4, 0, 2 };
					break;
				case 11:
					vertices = new Vector3[] { square.PosC, square.PosD, square.PosA, square.up, square.right };
					indices = new int[] { 0, 1, 2, 2, 3, 4, 4, 0, 2 };
					break;
				case 13:
					vertices = new Vector3[] { square.PosD, square.PosA, square.PosB, square.right, square.down };
					indices = new int[] { 0, 1, 2, 2, 3, 4, 4, 0, 2 };
					break;
				// active point are two; special case
				case 10:
					vertices = new Vector3[] { square.PosA, square.up, square.right, square.PosC, square.down, square.left };
					indices = new int[] { 0, 1, 2, 2, 3, 0, 0, 3, 4, 4, 5, 0 };
					break;
				
				case 5:
					vertices = new Vector3[] { square.PosB, square.right, square.down, square.PosD, square.left, square.up };
					indices = new int[] { 0, 1, 2, 2, 3, 0, 0, 3, 4, 4, 5, 0 };
					break;
				// active point are fore
				case 15:
					vertices = new Vector3[] { square.PosA, square.PosB, square.PosC, square.PosD };
					indices = new int[] { 0, 1, 2, 2, 3, 0 };
					break;
			}

			return GenerateHelper(vertices, indices);
		}

		public static Mesh GenerateMesh(this Tetrahedron tetra) {
			Vector3[] vertices = new Vector3[0];
			int[] indices = new int[0];

			switch(tetra.State) {
				case 0:
				case A | B | C | D:
					return null;
				case A:
					vertices = new [] { tetra.AD, tetra.AB, tetra.AC };
					indices = new int[] { 0, 1, 2 };
					break;
				case B:
					vertices = new [] { tetra.BD, tetra.BC, tetra.AB };
					indices = new int[] { 0, 1, 2 };
					break;				
				case C:
					vertices = new [] { tetra.BC, tetra.CD, tetra.AC };
					indices = new int[] { 0, 1, 2 };
					break;
				case D:
					vertices = new [] { tetra.CD, tetra.BD, tetra.AD };
					indices = new int[] { 0, 1, 2 };
					break;
				case B | C | D:
					vertices = new [] { tetra.AC, tetra.AB, tetra.AD };
					indices = new int[] { 0, 1, 2 };
					break; 
				case A | C | D:
					vertices = new [] { tetra.AB, tetra.BC, tetra.BD };
					indices = new int[] { 0, 1, 2 };
					break; 
				case A | B | D:
					vertices = new [] { tetra.AC, tetra.CD, tetra.BC };
					indices = new int[] { 0, 1, 2 };
					break; 
				case A | B | C:
					vertices = new [] { tetra.AD, tetra.BD, tetra.CD };
					indices = new int[] { 0, 1, 2 };
					break;
				case A | B:
					vertices = new [] { tetra.AC, tetra.AD, tetra.BD, tetra.BC };
					indices = new int[] { 0, 1, 2, 2, 3, 0 };
					break;
				case B | C:
					vertices = new [] { tetra.AB, tetra.BD, tetra.CD, tetra.AC };
					indices = new int[] { 0, 1, 2, 2, 3, 0 };
					break;
				case C | D:
					vertices = new [] { tetra.AC, tetra.BC, tetra.BD, tetra.AD };
					indices = new int[] { 0, 1, 2, 2, 3, 0 };
					break;
				case D | A:
					vertices = new [] { tetra.AB, tetra.AC, tetra.CD, tetra.BD };
					indices = new int[] { 0, 1, 2, 2, 3, 0 };
					break;
				case A | C:
					vertices = new [] { tetra.AB, tetra.BC, tetra.CD, tetra.AD };
					indices = new int[] { 0, 1, 2, 2, 3, 0 };
					break;
				case B | D:
					vertices = new [] { tetra.AB, tetra.AD, tetra.CD, tetra.BC };
					indices = new int[] { 0, 1, 2, 2, 3, 0 };
					break;
			}

			return GenerateHelper(vertices, indices);
		}

		static Mesh GenerateHelper(Vector3[] vertex, int[] indices) {
			var mesh = new Mesh();
			mesh.SetVertices(vertex.Cast<Vector3>().ToList());
			mesh.SetIndices(indices, MeshTopology.Triangles, 0);
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();

			return mesh;
		}

		public static Mesh ConcatMesh(this Mesh a, Mesh b) {
			if (b == null) return a;
			var vert = new List<Vector3>(a.vertices);
			var indices = new List<int>(a.GetIndices(0));

			vert.AddRange(b.vertices);
			indices.AddRange(b.GetIndices(0).Select(x => x + a.vertexCount));

			var mesh = new Mesh();
			mesh.SetVertices(vert);
			mesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);

			return mesh;
		}

	}

}
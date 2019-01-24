using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Marching;

namespace Marching {
	
	public static class MeshGenerator {
	
		public static Mesh Generate(Square square) {
			Vector3[] vertices = new Vector3[0];
			int[] indices = new int[0];

			switch(square.State) {
				// active point is one.
				case 8:
					vertices = new Vector3[] {
						square.PosA,
						square.vertexies[0],
						square.vertexies[1]
					};
					indices = new int[] {
						0, 1, 2
					};
					break;
				case 4:
					vertices = new Vector3[] {
						square.PosB,
						square.vertexies[1],
						square.vertexies[0]
					};
					indices = new int[] {
						0, 1, 2
					};
					break;
				case 2:
					vertices = new Vector3[] {
						square.PosC,
						square.vertexies[1],
						square.vertexies[0]
					};
					indices = new int[] {
						0, 1, 2
					};
					break;
				case 1:
					vertices = new Vector3[] {
						square.PosD,
						square.vertexies[1],
						square.vertexies[0]
					};
					indices = new int[] {
						0, 1, 2
					};
					break;
				// active points are two.
				case 12:
					vertices = new Vector3[] {
						square.PosA,
						square.PosB,
						square.vertexies[0],
						square.vertexies[1]
					};
					indices = new int[] {
						0, 1, 2,
						2, 3, 0
					};
					break;
				case 6:
					vertices = new Vector3[] {
						square.PosB,
						square.PosC,
						square.vertexies[1],
						square.vertexies[0]
					};
					indices = new int[] {
						0, 1, 2,
						2, 3, 0
					};
					break;
				case 3:
					vertices = new Vector3[] {
						square.PosC,
						square.PosD,
						square.vertexies[1],
						square.vertexies[0]
					};
					indices = new int[] {
						0, 1, 2,
						2, 3, 0
					};
					break;
				case 9:
					vertices = new Vector3[] {
						square.PosD,
						square.PosA,
						square.vertexies[0],
						square.vertexies[1]
					};
					indices = new int[] {
						0, 1, 2,
						2, 3, 0
					};
					break;
				// active points are three.
				case 14:
					vertices = new Vector3[] {
						square.PosA,
						square.PosB,
						square.PosC,
						square.vertexies[0],
						square.vertexies[1]
					};
					indices = new int[] {
						0, 1, 2,
						2, 3, 4,
						4, 0, 2
					};
					break;
				case 7:
					vertices = new Vector3[] {
						square.PosB,
						square.PosC,
						square.PosD,
						square.vertexies[1],
						square.vertexies[0]
					};
					indices = new int[] {
						0, 1, 2,
						2, 3, 4,
						4, 0, 2
					};
					break;
				case 11:
					vertices = new Vector3[] {
						square.PosC,
						square.PosD,
						square.PosA,
						square.vertexies[0],
						square.vertexies[1]
					};
					indices = new int[] {
						0, 1, 2,
						2, 3, 4,
						4, 0, 2
					};
					break;
				case 13:
					vertices = new Vector3[] {
						square.PosD,
						square.PosA,
						square.PosB,
						square.vertexies[0],
						square.vertexies[1]
					};
					indices = new int[] {
						0, 1, 2,
						2, 3, 4,
						4, 0, 2
					};
					break;
				// active point are two; special case
				case 10:
					vertices = new Vector3[] {
						square.PosA,
						square.vertexies[0],
						square.vertexies[1],
						square.PosC,
						square.vertexies[2],
						square.vertexies[3]
					};
					indices = new int[] {
						0, 1, 2,
						2, 3, 0,
						0, 3, 4,
						4, 5, 0
					};
					break;
				
				case 5:
					vertices = new Vector3[] {
						square.PosB,
						square.vertexies[1],
						square.vertexies[2],
						square.PosD,
						square.vertexies[3],
						square.vertexies[0]	
					};
					indices = new int[] {
						0, 1, 2,
						2, 3, 0,
						0, 3, 4,
						4, 5, 0
					};
					break;
				// active point are fore
				case 15:
					vertices = new Vector3[] {
						square.PosA,
						square.PosB,
						square.PosC,
						square.PosD
					};
					indices = new int[] {
						0, 1, 2,
						2, 3, 0
					};
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

	}

}
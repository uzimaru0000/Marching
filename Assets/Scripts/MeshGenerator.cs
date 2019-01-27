using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Marching;

namespace Marching {

	public class MeshData {
		public List<Vector3> vertices;
		public List<int> indices;

		public MeshData(List<Vector3> vertices, List<int> indices) {
			this.vertices = vertices;
			this.indices = indices;
		}
	}
	
	public static class MeshGenerator {

		const int A = 8;
		const int B = 4;
		const int C = 2;
		const int D = 1;

		public static MeshData GenerateMeshData(this Tetrahedron tetra) {
			List<Vector3> vertices = new List<Vector3>();
			List<int> indices = new List<int>();

			switch(tetra.State) {
				case 0:
				case A | B | C | D:
					return null;
				case A:
					vertices = new List<Vector3> { tetra.AD, tetra.AB, tetra.AC };
					indices = new List<int> { 0, 1, 2 };
					break;
				case B:
					vertices = new List<Vector3> { tetra.BD, tetra.BC, tetra.AB };
					indices = new List<int> { 0, 1, 2 };
					break;				
				case C:
					vertices = new List<Vector3> { tetra.BC, tetra.CD, tetra.AC };
					indices = new List<int> { 0, 1, 2 };
					break;
				case D:
					vertices = new List<Vector3> { tetra.CD, tetra.BD, tetra.AD };
					indices = new List<int> { 0, 1, 2 };
					break;
				case B | C | D:
					vertices = new List<Vector3> { tetra.AC, tetra.AB, tetra.AD };
					indices = new List<int> { 0, 1, 2 };
					break; 
				case A | C | D:
					vertices = new List<Vector3> { tetra.AB, tetra.BC, tetra.BD };
					indices = new List<int> { 0, 1, 2 };
					break; 
				case A | B | D:
					vertices = new List<Vector3> { tetra.AC, tetra.CD, tetra.BC };
					indices = new List<int> { 0, 1, 2 };
					break; 
				case A | B | C:
					vertices = new List<Vector3> { tetra.AD, tetra.BD, tetra.CD };
					indices = new List<int> { 0, 1, 2 };
					break;
				case A | B:
					vertices = new List<Vector3> { tetra.AC, tetra.AD, tetra.BD, tetra.BC };
					indices = new List<int> { 0, 1, 2, 2, 3, 0 };
					break;
				case B | C:
					vertices = new List<Vector3> { tetra.AB, tetra.BD, tetra.CD, tetra.AC };
					indices = new List<int> { 0, 1, 2, 2, 3, 0 };
					break;
				case C | D:
					vertices = new List<Vector3> { tetra.AC, tetra.BC, tetra.BD, tetra.AD };
					indices = new List<int> { 0, 1, 2, 2, 3, 0 };
					break;
				case D | A:
					vertices = new List<Vector3> { tetra.AB, tetra.AC, tetra.CD, tetra.BD };
					indices = new List<int> { 0, 1, 2, 2, 3, 0 };
					break;
				case A | C:
					vertices = new List<Vector3> { tetra.AB, tetra.BC, tetra.CD, tetra.AD };
					indices = new List<int> { 0, 1, 2, 2, 3, 0 };
					break;
				case B | D:
					vertices = new List<Vector3> { tetra.AB, tetra.AD, tetra.CD, tetra.BC };
					indices = new List<int> { 0, 1, 2, 2, 3, 0 };
					break;
			}

			return new MeshData(vertices, indices);
		}

		public static Mesh Generate(MeshData data) {
			var mesh = new Mesh();
			mesh.SetVertices(data.vertices);
			mesh.SetIndices(data.indices.ToArray(), MeshTopology.Triangles, 0);
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();

			return mesh;
		}

		public static MeshData ConcatMeshData(this MeshData a, MeshData b) {
			if (b == null) return a;
			
			// 結合後の頂点の番号
			var mappedIndices = Enumerable.Repeat(0, b.vertices.Count).ToList();

			// bの頂点からaの頂点と同じものを削除
			var verts = new List<Vector3>();
			var count = 0;
			for (var i = 0; i < b.vertices.Count; i++) {
				var flag = true;
				for (var j = 0; j < a.vertices.Count; j++) {
					if ((a.vertices[j] - b.vertices[i]).sqrMagnitude <= Mathf.Pow(Mathf.Epsilon, 2)) {
						mappedIndices[i] = j;
						flag = false;
						break;
					}
				}
				if (flag) {
					verts.Add(b.vertices[i]);
					mappedIndices[i] = a.vertices.Count + count;
					count++;
				}
			}

			a.indices.AddRange(b.indices.Select(x => mappedIndices[x]));
			a.vertices.AddRange(verts);

			return a;
		}

	}

}
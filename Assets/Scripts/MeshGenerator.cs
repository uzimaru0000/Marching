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

	public struct Tuple<T1, T2> {
		public T1 first;
		public T2 second;

		public Tuple(T1 f, T2 s) {
			first = f;
			second = s;
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

			var allVerts = new List<Vector3>(a.vertices)
							.Select((x, i) => new Tuple<int, Vector3>(i, x))
							.Concat(b.vertices.Select((x, i) => new Tuple<int, Vector3>(i, x)))
							.ToList();

			var u_min = CalcUnitSize(allVerts.Select(x => x.second).ToList(), 3);
			var octree = CreateOctree(allVerts, u_min);
			
			// 結合後の頂点の番号
			var mappedIndices = Enumerable.Repeat(0, b.vertices.Count).ToList();

			// bの頂点からaの頂点と同じものを削除
			var verts = new List<Vector3>();
			var count = 0;
			for (var i = 0; i < b.vertices.Count; i++) {
				var flag = true;
				var index = CalcIndex(u_min.first, b.vertices[i] - u_min.second);

				for (var j = 0; j < octree[index].Count; j++) {
					if ((octree[index][j].second - b.vertices[i]).sqrMagnitude <= Mathf.Pow(Mathf.Epsilon, 2)) {
						mappedIndices[i] = octree[index][j].first;
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

			Debug.Log(verts.Count);

			a.vertices.AddRange(verts);
			a.indices.AddRange(b.indices.Select(x => mappedIndices[x]));

			return a;
		}

		static Tuple<Vector3, Vector3> CalcUnitSize(List<Vector3> pos, int level) {
			var xs = pos.OrderBy(x => x.x);
			var ys = pos.OrderBy(x => x.y);
			var zs = pos.OrderBy(x => x.z);

			var min = new Vector3(xs.First().x, ys.First().y, zs.First().z);
			var max = new Vector3(xs.Last().x, ys.Last().y, zs.Last().z);
			var box = max - min;

			return new Tuple<Vector3, Vector3>(box / Mathf.Pow(2, level), min);
		}

		static Dictionary<int, List<Tuple<int, Vector3>>> CreateOctree(List<Tuple<int, Vector3>> pos, Tuple<Vector3, Vector3> u_min) {
			var tree = new Dictionary<int, List<Tuple<int, Vector3>>>();

			for (var i = 0; i < pos.Count; i++) {
				var v = pos[i].second - u_min.second;
				var index = CalcIndex(u_min.first, v);
				if (!tree.ContainsKey(index)) tree.Add(index, new List<Tuple<int, Vector3>> { pos[i] });
				else tree[index].Add(pos[i]);
			}
			
			return tree;
		}

		static int CalcIndex(Vector3 u, Vector3 p) {
			System.Func<int, int> shiftNum = n => {
				var r = (n | n << 8) & 0x00ff00ff;
				r = (r | r << 4) & 0x0f0f0f0f;
				return (r | r << 2) & 0x33333333;
			};

			return shiftNum((int) (p.x / u.x)) | shiftNum((int) (p.y / u.y)) << 1 | shiftNum((int) (p.z / u.z)) << 2;
		}

	}

}
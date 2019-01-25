using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Marching {
	
	public class Cube {
		Vector3 center;
		Vector3 size;

		public Tetrahedron a, b, c, d, e;

		public Dictionary<string, Vector3> Verts {
			get {
				return new Dictionary<string, Vector3>() {
					{ "A", center + Vector3.Scale(size, new Vector3(0.5f, 0.5f, 0.5f)) },
					{ "B", center + Vector3.Scale(size, new Vector3(-0.5f, 0.5f, 0.5f)) },
					{ "C", center + Vector3.Scale(size, new Vector3(-0.5f, -0.5f, 0.5f)) },
					{ "D", center + Vector3.Scale(size, new Vector3(0.5f, -0.5f, 0.5f)) },
					{ "E", center + Vector3.Scale(size, new Vector3(0.5f, -0.5f, -0.5f)) },
					{ "F", center + Vector3.Scale(size, new Vector3(-0.5f, -0.5f, -0.5f)) },
					{ "G", center + Vector3.Scale(size, new Vector3(-0.5f, 0.5f, -0.5f)) },
					{ "H", center + Vector3.Scale(size, new Vector3(0.5f, 0.5f, -0.5f)) }
				};
			}
		}

		public Mesh Mesh {
			get {
				var mesh = ConcatMesh(new Mesh(), MeshGenerator.TetrahedronGenerator(a));
				mesh = ConcatMesh(mesh, MeshGenerator.TetrahedronGenerator(b));
				mesh = ConcatMesh(mesh, MeshGenerator.TetrahedronGenerator(c));
				mesh = ConcatMesh(mesh, MeshGenerator.TetrahedronGenerator(d));
				mesh = ConcatMesh(mesh, MeshGenerator.TetrahedronGenerator(e));

				return mesh;
			}
		}

		public Cube(Vector3 center, Vector3 size,
			float a, float b, float c, float d,
			float e, float f, float g, float h,
			bool odd
		) {
			this.center = center;
			this.size = size;
			
			if (odd) {
				this.a = new Tetrahedron(Verts["A"], Verts["H"], Verts["G"], Verts["E"], a, h, g, e);
				this.b = new Tetrahedron(Verts["B"], Verts["A"], Verts["G"], Verts["C"], b, a, g, c);
				this.c = new Tetrahedron(Verts["C"], Verts["E"], Verts["G"], Verts["F"], c, e, g, f);
				this.d = new Tetrahedron(Verts["D"], Verts["A"], Verts["C"], Verts["E"], d, a, c, e);
				this.e = new Tetrahedron(Verts["E"], Verts["C"], Verts["G"], Verts["A"], e, c, g, a);
			} else {
				this.a = new Tetrahedron(Verts["A"], Verts["D"], Verts["H"], Verts["B"], a, d, h, b);
				this.b = new Tetrahedron(Verts["B"], Verts["H"], Verts["G"], Verts["F"], b, h, g, f);
				this.c = new Tetrahedron(Verts["C"], Verts["D"], Verts["B"], Verts["F"], c, d, b, f);
				this.d = new Tetrahedron(Verts["D"], Verts["H"], Verts["B"], Verts["F"], d, h, b, f);
				this.e = new Tetrahedron(Verts["E"], Verts["D"], Verts["F"], Verts["H"], e, d, f, h);
			}
		}

		public Mesh ConcatMesh(Mesh a, Mesh b) {
			if (b == null) return a;
			var vert = new List<Vector3>(a.vertices);
			var indices = new List<int>(a.GetIndices(0));

			vert.AddRange(b.vertices);
			indices.AddRange(b.GetIndices(0).Select(x => x + a.vertexCount));

			var mesh = new Mesh();
			mesh.SetVertices(vert);
			mesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();

			return mesh;
		}

	}

}
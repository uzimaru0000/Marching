using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Marching {
	
	public class Cube {
		Vector3 center;
		Vector3 size;

		public Mesh mesh;

		Dictionary<string, Vector3> verts;
		public Dictionary<string, Vector3> Verts {
			get {
				if (verts == null) {
					verts = new Dictionary<string, Vector3>() {
						{ "A", center + Vector3.Scale(size, new Vector3(-0.5f, 0.5f, -0.5f)) },
						{ "B", center + Vector3.Scale(size, new Vector3(0.5f, 0.5f, -0.5f)) },
						{ "C", center + Vector3.Scale(size, new Vector3(0.5f, -0.5f, -0.5f)) },
						{ "D", center + Vector3.Scale(size, new Vector3(-0.5f, -0.5f, -0.5f)) },
						{ "E", center + Vector3.Scale(size, new Vector3(-0.5f, -0.5f, 0.5f)) },
						{ "F", center + Vector3.Scale(size, new Vector3(0.5f, -0.5f, 0.5f)) },
						{ "G", center + Vector3.Scale(size, new Vector3(0.5f, 0.5f, 0.5f)) },
						{ "H", center + Vector3.Scale(size, new Vector3(-0.5f, 0.5f, 0.5f)) }
					};
				}
				return verts;
			}
		}

		public Cube(Vector3 center, Vector3 size,
			float a, float b, float c, float d,
			float e, float f, float g, float h
		) {
			this.center = center;
			this.size = size;

			mesh = new [] {
				new Tetrahedron(Verts["A"], Verts["D"], Verts["C"], Verts["F"], a, d, c, f).GenerateMesh(),
				new Tetrahedron(Verts["A"], Verts["C"], Verts["B"], Verts["F"], a, c, b, f).GenerateMesh(),
				new Tetrahedron(Verts["A"], Verts["B"], Verts["G"], Verts["F"], a, b, g, f).GenerateMesh(),
				new Tetrahedron(Verts["A"], Verts["G"], Verts["H"], Verts["F"], a, g, h, f).GenerateMesh(),
				new Tetrahedron(Verts["A"], Verts["H"], Verts["E"], Verts["F"], a, h, e, f).GenerateMesh(),
				new Tetrahedron(Verts["A"], Verts["E"], Verts["D"], Verts["F"], a, e, d, f).GenerateMesh()
			}.Aggregate(new Mesh(), (acc, x) => acc.ConcatMesh(x));
		}

	}

}
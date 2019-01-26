using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Marching {
	
	public class Cube {
		Vector3 center;
		Vector3 size;

		public Tetrahedron[] tetrahedrons;

		public Dictionary<string, Vector3> Verts {
			get {
				return new Dictionary<string, Vector3>() {
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
		}

		public Mesh Mesh {
			get {
				return tetrahedrons.Select(x => MeshGenerator.TetrahedronGenerator(x))
								   .Aggregate(new Mesh(), (acc, x) => MeshGenerator.ConcatMesh(acc, x));
			}
		}

		public Cube(Vector3 center, Vector3 size,
			float a, float b, float c, float d,
			float e, float f, float g, float h,
			bool odd
		) {
			this.center = center;
			this.size = size;

			tetrahedrons = new [] {
				new Tetrahedron(Verts["A"], Verts["D"], Verts["C"], Verts["F"], a, d, c, f),
				new Tetrahedron(Verts["A"], Verts["C"], Verts["B"], Verts["F"], a, c, b, f),
				new Tetrahedron(Verts["A"], Verts["B"], Verts["G"], Verts["F"], a, b, g, f),
				new Tetrahedron(Verts["A"], Verts["G"], Verts["H"], Verts["F"], a, g, h, f),
				new Tetrahedron(Verts["A"], Verts["H"], Verts["E"], Verts["F"], a, h, e, f),
				new Tetrahedron(Verts["A"], Verts["E"], Verts["D"], Verts["F"], a, e, d, f)
			};
			
			// if (odd) {
			// 	tetrahedrons = new [] {
			// 		new Tetrahedron(Verts["A"], Verts["C"], Verts["D"], Verts["F"], a, f, c, d),
			// 		new Tetrahedron(Verts["A"], Verts["B"], Verts["C"], Verts["F"], a, b, c, f),
			// 		new Tetrahedron(Verts["A"], Verts["G"], Verts["B"], Verts["F"], a, g, b, f),
			// 		new Tetrahedron(Verts["A"], Verts["H"], Verts["G"], Verts["F"], a, h, g, f),
			// 		new Tetrahedron(Verts["A"], Verts["D"], Verts["E"], Verts["F"], a, d, e, f)
			// 	};
			// } else {
			// 	tetrahedrons = new [] {
			// 		new Tetrahedron(Verts["A"], Verts["B"], Verts["H"], Verts["D"], a, b, h, d),
			// 		new Tetrahedron(Verts["B"], Verts["F"], Verts["G"], Verts["H"], b, f, g, h),
			// 		new Tetrahedron(Verts["C"], Verts["F"], Verts["B"], Verts["D"], c, f, b, d),
			// 		new Tetrahedron(Verts["D"], Verts["F"], Verts["B"], Verts["H"], d, f, b, h),
			// 		new Tetrahedron(Verts["E"], Verts["H"], Verts["F"], Verts["D"], e, h, f, d)
			// 	};
			// }
		}

	}

}
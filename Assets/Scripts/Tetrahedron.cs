using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marching {
	
	public class Tetrahedron {
		
		Vector3 A, B, C, D;
		float a, b, c, d;
		
		public Vector3 AB, BC, CD, AC, AD, BD;

		public float threshold = 0.5f;

		public Vector3[] Verticis {
			get {
				return new [] {A, B, C, D};
			}
		}

		public int State {
			get {
				return (IsActive(a) ? 8 : 0) + (IsActive(b) ? 4 : 0) + (IsActive(c) ? 2 : 0) + (IsActive(d) ? 1 : 0);
			}
		}

		public Tetrahedron(Vector3 A, Vector3 B, Vector3 C, Vector3 D, float a, float b, float c, float d) {
			this.A = A;
			this.B = B;
			this.C = C;
			this.D = D;
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;

			AB = new Edge(A, B, a, b).GetVertex(threshold);
			AC = new Edge(A, C, a, c).GetVertex(threshold);
			AD = new Edge(A, D, a, d).GetVertex(threshold);
			BC = new Edge(B, C, b, c).GetVertex(threshold);
			BD = new Edge(B, D, b, d).GetVertex(threshold);
			CD = new Edge(C, D, c, d).GetVertex(threshold);
		}

		bool IsActive(float n) {
			return n >= threshold;
		}

	}

}
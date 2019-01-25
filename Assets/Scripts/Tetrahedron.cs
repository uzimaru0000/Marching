using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marching {
	
	public class Tetrahedron {
		
		Vector3 A, B, C, D;
		float a, b, c, d;
		
		public Edge ab, bc, cd, ac, ad, bd;

		public float threshold = 0.5f;

		public Vector3 AB {
			get {
				return ab.GetVertex(threshold);
			}
		}
		public Vector3 AC {
			get {
				return ac.GetVertex(threshold);
			}
		}
		public Vector3 AD {
			get {
				return ad.GetVertex(threshold);
			}
		}
		public Vector3 BC {
			get {
				return bc.GetVertex(threshold);
			}
		}
		public Vector3 BD {
			get {
				return bd.GetVertex(threshold);
			}
		}
		public Vector3 CD {
			get {
				return cd.GetVertex(threshold);
			}
		}

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

			ab = new Edge(A, B, a, b);
			ac = new Edge(A, C, a, c);
			ad = new Edge(A, D, a, d);
			bc = new Edge(B, C, b, c);
			bd = new Edge(B, D, b, d);
			cd = new Edge(C, D, c, d);
		}

		bool IsActive(float n) {
			return n >= threshold;
		}

	}

}
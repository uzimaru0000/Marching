using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Marching {

    public class Square {

        float A, B, C, D;
        Vector2 center;
        Vector2 size;

        public float threshold = 0.5f;
        
        public Vector2[] vertexies;

        public bool StateA {
            get {
                return IsActive(A);
            }
        }
        public bool StateB {
            get {
                return IsActive(B);
            }
        }
        public bool StateC {
            get {
                return IsActive(C);
            }
        }
        public bool StateD {
            get {
                return IsActive(D);
            }
        }

        public bool StateX {
            get {
                return !StateA && StateB && !StateC && StateD;
            }
        }

        public int State {
            get {
                return (StateA ? 8 : 0) + (StateB ? 4 : 0) + (StateC ? 2 : 0) + (StateD ? 1 : 0);
            }
        }

        public Vector2 PosA {
            get {
                return center + Vector2.Scale(size, new Vector2(-0.5f, 0.5f));
            }
        }
        public Vector2 PosB {
            get {
                return center + Vector2.Scale(size, new Vector2(0.5f, 0.5f));
            }
        }
        public Vector2 PosC {
            get {
                return center + Vector2.Scale(size, new Vector2(0.5f, -0.5f));
            }
        }
        public Vector2 PosD {
            get {
                return center + Vector2.Scale(size, new Vector2(-0.5f, -0.5f));
            }
        }
        
        public Square(Vector2 center, Vector2 size, float a, float b, float c, float d) {
            this.center = center;
            this.size = size;
            A = a;
            B = b;
            C = c;
            D = d;

            var e1 = StateA ^ StateB;
            var e2 = StateB ^ StateC;
            var e3 = StateC ^ StateD;
            var e4 = StateA ^ StateD;

            var edge1 = e1 ? new Edge(PosA, PosB, A, B) : null;
            var edge2 = e2 ? new Edge(PosB, PosC, B, C) : null;
            var edge3 = e3 ? new Edge(PosC, PosD, C, D) : null;
            var edge4 = e4 ? new Edge(PosD, PosA, D, A) : null;

            vertexies = new Edge[] {edge1, edge2, edge3, edge4}
                            .Where(x => x != null)
                            .Select(x => x.GetVertex(threshold))
                            .ToArray();
        }
        
        bool IsActive(float n) {
            return n >= threshold;
        }
    }

    public class Edge {
        
        Vector2 vert1, vert2;
        float weightA;
        float weightB;

        public Edge(Vector2 a, Vector2 b, float weightA, float weightB) {
            vert1 = a;
            vert2 = b;
            this.weightA = weightA;
            this.weightB = weightB;
        }

        public Vector2 GetVertex(float threshold) {
            if (weightA > weightB) {
                return Vector2.Lerp(vert2, vert1, weightA - weightB);
            } else {
                return Vector2.Lerp(vert1, vert2, weightB - weightA);
            }
        } 

    }

}
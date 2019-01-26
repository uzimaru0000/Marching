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

        public Vector2 up, down, right, left;

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
                return center + Vector2.Scale(size, new Vector2(0.5f, 0.5f));
            }
        }
        public Vector2 PosB {
            get {
                return center + Vector2.Scale(size, new Vector2(-0.5f, 0.5f));
            }
        }
        public Vector2 PosC {
            get {
                return center + Vector2.Scale(size, new Vector2(-0.5f, -0.5f));
            }
        }
        public Vector2 PosD {
            get {
                return center + Vector2.Scale(size, new Vector2(0.5f, -0.5f));
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

            up = edge1 != null ? (Vector2) edge1.GetVertex(threshold) : Vector2.Lerp(PosA, PosB, 0.5f);
            right = edge2 != null ? (Vector2) edge2.GetVertex(threshold) : Vector2.Lerp(PosB, PosC, 0.5f);
            down = edge3 != null ? (Vector2) edge3.GetVertex(threshold) : Vector2.Lerp(PosC, PosD, 0.5f);
            left = edge4 != null ? (Vector2) edge4.GetVertex(threshold) : Vector2.Lerp(PosD, PosA, 0.5f);
        }
        
        bool IsActive(float n) {
            return n >= threshold;
        }
    }


}
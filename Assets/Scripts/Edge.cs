using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marching {
    public class Edge {
        
        Vector3 vert1, vert2;
        float weightA;
        float weightB;

        public Edge(Vector3 a, Vector3 b, float weightA, float weightB) {
            vert1 = a;
            vert2 = b;
            this.weightA = weightA;
            this.weightB = weightB;
        }

        public Vector3 GetVertex(float threshold) {
            if (weightA > weightB) {
                return Vector3.Lerp(vert2, vert1, (weightA - weightB) * 0.9f);
            } else {
                return Vector3.Lerp(vert1, vert2, (weightB - weightA) * 0.9f);
            }
        }
    }
}
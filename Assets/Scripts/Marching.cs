using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Marching {
    
    static int[][] points = new int [][] {
        new int[]{},
        new int[]{ 0, 1, 5 },
        new int[]{ 0, 4, 3 },
        new int[]{ 1, 4, 3, 5, 1 },
        new int[]{ 2, 5, 3 },
        new int[]{ 1, 2, 3, 0, 1 },
        new int[]{ 5, 2, 4, 0, 5 },
        new int[]{ 4, 2, 1 },
        new int[]{ 1, 2, 4 }
    };

    public static int[] GetEdgeIndex(bool[] vertex) {
        var flag = BoolsToInt(vertex);
        var invert = IsInvert(flag);
        var index = invert ? ~flag : flag;

        return invert ? points[index].Reverse().ToArray() : points[index];
    }

    public static bool[][] SplitCube(bool[] vertex) {
        if (vertex.Length != 8) throw new System.Exception("vertex must be 8 points");

        // TODO: 6分割にする
        return new bool[][] {
            // new bool[] { vertex[0], vertex[1], vertex[4], vertex[3] },
            // new bool[] { vertex[2], vertex[3], vertex[6], vertex[1] },
            // new bool[] { vertex[5], vertex[4], vertex[1], vertex[6] },
            // new bool[] { vertex[5], vertex[4], vertex[1], vertex[6] },
            // new bool[] { vertex[5], vertex[4], vertex[1], vertex[6] },
            // new bool[] { vertex[7], vertex[6], vertex[3], vertex[4] }
        };
    }

    static int BoolsToInt(bool[] vertex) {
        var result = vertex.Select((x, i) => x ? Mathf.Pow(2, i) : 0).Sum();
        return Mathf.RoundToInt(result);
    }

    static bool IsInvert(int flag) {
        return !(0 <= flag && flag <= 8);
    }
}
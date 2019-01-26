using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Marching {
	
	public class MapGenerator {

		float[,,] scalerMap;
		Vector3 size;

		public MapGenerator(float[,,] scalerMap, Vector3 size) {
			this.scalerMap = scalerMap;
			this.size = size;
		}

		public Mesh GenerateMesh() {
			var width = scalerMap.GetLength(0);
			var height = scalerMap.GetLength(1);
			var depth = scalerMap.GetLength(2);
			Cube[] cubes = new Cube[(width + 1) * (height + 1) * (depth + 1)];

			for (var x = 0; x < width + 1; x++) {
				for (var y = 0; y < height + 1; y++) {
					for (var z = 0; z < depth + 1; z++) {
						var index = x + (width + 1) * y + (width + 1) * (height + 1) * z;
						var pos = new Vector3(x, y, z) + size / 2;
						cubes[index] = new Cube(
							pos, size,
							GetValue(x-1, y, z-1),
							GetValue(x, y, z-1),
							GetValue(x, y-1, z-1),
							GetValue(x-1, y-1, z-1),
							GetValue(x-1, y-1, z),
							GetValue(x, y-1, z),
							GetValue(x, y, z),
							GetValue(x-1, y, z),
							Mathf.Abs(x + y + z) % 2 == 0
						);
					}
				}
			}

			return cubes.Select(x => x.Mesh).Aggregate((acc, x) => MeshGenerator.ConcatMesh(acc, x));
		}

		float GetValue(int x, int y, int z) {
			var width = scalerMap.GetLength(0);
			var height = scalerMap.GetLength(1);
			var depth = scalerMap.GetLength(2);

			if ((0 <= x && x < width) && (0 <= y && y < height) && (0 <= z && z < depth)) {
				return scalerMap[x, y, z];
			} else {
				return 0;
			}
		}

	}

}
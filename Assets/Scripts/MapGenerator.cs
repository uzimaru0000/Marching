using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Marching {
	
	public class MapGenerator {

		float[] scalerMap;
		Vector3 size;
		Vector3Int dataSize;

		public MapGenerator(float[] scalerMap, Vector3Int dataSize, Vector3 size) {
			this.scalerMap = scalerMap;
			this.size = size;
			this.dataSize = dataSize;
		}

		public Mesh GenerateMesh() {
			var width = dataSize.x;
			var height = dataSize.y;
			var depth = dataSize.z;
			Mesh mesh = new Mesh();

			for (var x = 0; x < width + 1; x++) {
				for (var y = 0; y < height + 1; y++) {
					for (var z = 0; z < depth + 1; z++) {
						var pos = new Vector3(x, y, z) + size / 2;
						var cube = new Cube(
							pos, size,
							GetValue(x-1, y, z-1),
							GetValue(x, y, z-1),
							GetValue(x, y-1, z-1),
							GetValue(x-1, y-1, z-1),
							GetValue(x-1, y-1, z),
							GetValue(x, y-1, z),
							GetValue(x, y, z),
							GetValue(x-1, y, z)
						);

						mesh = mesh.ConcatMesh(cube.mesh);
					}
				}
			}

			mesh.RecalculateBounds();
			mesh.RecalculateNormals();

			return mesh;
		}

		float GetValue(int x, int y, int z) {
			var width = dataSize.x;
			var height = dataSize.y;
			var depth = dataSize.z;

			if ((0 <= x && x < width) && (0 <= y && y < height) && (0 <= z && z < depth)) {
				return scalerMap[x + y * width + z * width * height];
			} else {
				return 0;
			}
		}

	}

}
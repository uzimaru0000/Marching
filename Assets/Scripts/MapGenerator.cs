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
			MeshData meshData = null;

			for (var x = 0.0f; x < width + 1; x += size.x) {
				for (var y = 0.0f; y < height + 1; y += size.y) {
					for (var z = 0.0f; z < depth + 1; z += size.z) {
						var pos = new Vector3(x, y, z) + size / 2;
						var cube = new Cube(
							pos, size,
							GetValue(x-size.x, y, z-size.z),
							GetValue(x, y, z-size.z),
							GetValue(x, y-size.y, z-size.z),
							GetValue(x-size.x, y-size.y, z-size.z),
							GetValue(x-size.x, y-size.y, z),
							GetValue(x, y-size.y, z),
							GetValue(x, y, z),
							GetValue(x-size.x, y, z)
						);

						if (meshData == null) meshData = cube.meshData;
						else meshData = meshData.ConcatMeshData(cube.meshData);
					}
				}
			}

			return MeshGenerator.Generate(meshData);
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

		float GetValue(float x, float y, float z) {
			var width = dataSize.x;
			var height = dataSize.y;
			var depth = dataSize.z;
			
			var small = GetValue(Mathf.FloorToInt(x), Mathf.FloorToInt(y), Mathf.FloorToInt(z));
			var big = GetValue(Mathf.RoundToInt(x), Mathf.RoundToInt(y), Mathf.RoundToInt(z));

			return Mathf.Lerp(small, big, 0.5f);
		}

	}

}
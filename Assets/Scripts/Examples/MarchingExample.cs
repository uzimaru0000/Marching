using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Marching;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MarchingExample : MonoBehaviour {

	[SerializeField]
	Vector3Int size;

	[SerializeField]
	float scale;

	MeshFilter meshFilter;

	float[,,] map;

	void Start () {
		meshFilter = GetComponent<MeshFilter>();
		map = new float[size.x, size.y, size.z];
		for (var x = 0; x < size.x; x++) {
			for (var y = 0; y < size.y; y++) {
				for (var z = 0; z < size.z; z++) {
					map[x, y, z] = PerlineNoise3D(new Vector3(x, y, z) / scale);
				}
			}
		}

		var generator = new Marching.MapGenerator(map, Vector3.one);
		meshFilter.mesh = generator.GenerateMesh();
	} 

	float PerlineNoise3D(Vector3 seed) {
		float AB = Mathf.PerlinNoise(seed.x, seed.y);
		float BC = Mathf.PerlinNoise(seed.y, seed.z);
		float AC = Mathf.PerlinNoise(seed.x, seed.z);

		float BA = Mathf.PerlinNoise(seed.y, seed.x);
		float CB = Mathf.PerlinNoise(seed.z, seed.y);
		float CA = Mathf.PerlinNoise(seed.z, seed.x);

		return (AB + BC + AC + BA + CB + CA) / 6.0f;
	}

	void OnDrawGizmos() {
		// for (var x = 0; x < size.x; x++) {
		// 	for (var y = 0; y < size.y; y++) {
		// 		for (var z = 0; z < size.z; z++) {
					
		// 		}
		// 	}
		// }
	}
}

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
	[SerializeField]
	ComputeShader computeShader;

	MeshFilter meshFilter;

	float[] map;

	void Start () {
		meshFilter = GetComponent<MeshFilter>();
		var buffer = new ComputeBuffer(size.x * size.y * size.z, sizeof(float), ComputeBufferType.Default);
		computeShader.SetBuffer(0, "map", buffer);
		computeShader.SetInt("width", size.x);
		computeShader.SetInt("height", size.y);
		computeShader.SetInt("depth", size.z);
		computeShader.SetFloat("scale", scale);
		computeShader.Dispatch(0, size.x / 4, size.y / 4, size.z / 4);

		map = new float[size.x * size.y * size.z];
		buffer.GetData(map);
		
		var time = System.DateTime.Now;

		var generator = new Marching.MapGenerator(map, size, Vector3.one);
		meshFilter.mesh = generator.GenerateMesh();

		print((System.DateTime.Now - time).TotalMilliseconds);

		buffer.Release();
	}

	void OnDrawGizmos() {
		
	}
}

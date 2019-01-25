using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Marching;

public class CubeExample : MonoBehaviour {

	[SerializeField, Range(0, 1)]
	float a;
	[SerializeField, Range(0, 1)]
	float b;
	[SerializeField, Range(0, 1)]
	float c;
	[SerializeField, Range(0, 1)]
	float d;
	[SerializeField, Range(0, 1)]
	float e;
	[SerializeField, Range(0, 1)]
	float f;
	[SerializeField, Range(0, 1)]
	float g;
	[SerializeField, Range(0, 1)]
	float h;

	[SerializeField]
	bool odd;

	MeshFilter mf;
	Cube cube;

	MeshFilter MeshFilter {
		get {
			if (!mf) mf = GetComponent<MeshFilter>();
			return mf;
		}
	}

	void Start () {
		
	}
	
	void Update () {
		
	}

	void OnDrawGizmos() {
		if (cube == null) return;
		cube.Verts.Values.ToList().ForEach(x => Gizmos.DrawCube(x, Vector3.one * 0.1f));

		Gizmos.color = Color.magenta;
		Gizmos.DrawCube(cube.Verts["A"], Vector3.one * a * 0.1f);
		Gizmos.color = Color.green;
		Gizmos.DrawCube(cube.Verts["B"], Vector3.one * b * 0.1f);
		Gizmos.color = Color.blue;
		Gizmos.DrawCube(cube.Verts["C"], Vector3.one * c * 0.1f);
		Gizmos.color = Color.red;
		Gizmos.DrawCube(cube.Verts["D"], Vector3.one * d * 0.1f);
		Gizmos.color = Color.magenta;
		Gizmos.DrawCube(cube.Verts["E"], Vector3.one * e * 0.1f);
		Gizmos.color = Color.green;
		Gizmos.DrawCube(cube.Verts["F"], Vector3.one * f * 0.1f);
		Gizmos.color = Color.blue;
		Gizmos.DrawCube(cube.Verts["G"], Vector3.one * g * 0.1f);
		Gizmos.color = Color.red;
		Gizmos.DrawCube(cube.Verts["H"], Vector3.one * h * 0.1f);
	}

	void OnDrawGizmosSelected() {
		cube = new Cube(transform.position, Vector3.one, a, b, c, d, e, f, g, h, odd);
		mf.mesh = cube.Mesh;
	}
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Marching;

public class SquareExample : MonoBehaviour {

	[SerializeField, Range(0, 1)]
	float A;
	[SerializeField, Range(0, 1)]
	float B;
	[SerializeField, Range(0, 1)]
	float C;
	[SerializeField, Range(0, 1)]
	float D;

	Square square;

	void Start () {
	}
	
	void Update () {
		
	}

	void OnDrawGizmos() {
		square = new Square(transform.position, Vector2.one, A, B, C, D);
		Gizmos.color = Color.gray;
		Gizmos.DrawCube(square.PosA, Vector3.one * 0.1f);
		Gizmos.DrawCube(square.PosB, Vector3.one * 0.1f);
		Gizmos.DrawCube(square.PosC, Vector3.one * 0.1f);
		Gizmos.DrawCube(square.PosD, Vector3.one * 0.1f);

		Gizmos.color = Color.magenta;
		Gizmos.DrawMesh(MeshGenerator.SquareGenerate(square));
		Gizmos.DrawWireMesh(MeshGenerator.SquareGenerate(square));
	}
}

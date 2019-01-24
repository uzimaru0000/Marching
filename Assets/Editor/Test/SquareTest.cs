using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;
using Marching;

public class SquareTest {

	Square square = new Square(new Vector2(0, 0), new Vector2(1, 1), 0.5f, 0.5f, 0, 0);

	[Test]
	public void StateTest() {
		Assert.IsTrue(square.StateA);
		Assert.IsTrue(square.StateB);
		Assert.IsFalse(square.StateC);
		Assert.IsFalse(square.StateD);
	}

	[Test]
	public void VertexiesTest() {
		
	}

}
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;
using Marching;

public class EdgeTest {

    [Test]
    public void GetVertexTest() {
        Edge edge = new Edge(new Vector2(-0.5f, 0.5f), new Vector2(0.5f, 0.5f), 0.5f, 0);
        Assert.AreEqual(edge.GetVertex(0.5f), new Vector3(-0.2f, 0.5f));

        edge = new Edge(new Vector2(0.5f, 0.5f), new Vector2(0.5f, -0.5f), 0, 1.0f);
        Assert.AreEqual(edge.GetVertex(0.5f), new Vector3(0.5f, -0.4f));

        edge = new Edge(new Vector2(0.5f, -0.5f), new Vector2(-0.5f, -0.5f), 1.0f, 2.0f);
        Assert.AreEqual(edge.GetVertex(0.5f), new Vector3(0.3f, -0.5f));

        edge = new Edge(new Vector2(-0.5f, -0.5f), new Vector2(-0.5f, 0.5f), 0.5f, 0.2f);
        Assert.AreEqual(edge.GetVertex(0.5f), new Vector3(-0.5f, 0.1f));
    }
}

using UnityEngine;
using System.Collections;

public class HexTrianglePolygon : TessellationPolygon {
	
	private float distance = 52f;
	
	void Start() {	
		
		AdjacentObject[] objects = {new AdjacentObject("TriangleHex",30,distance,30),
										new AdjacentObject("TriangleHex",90,distance,90),
										new AdjacentObject("TriangleHex",150,distance,150),
										new AdjacentObject("TriangleHex",210,distance,210),
										new AdjacentObject("TriangleHex",270,distance,270),
										new AdjacentObject("TriangleHex",330,distance,330)};
		
		adjacentObjects = objects;

		Initialize ();
		
	}
	
	void Update()
	{		
		updateMe();
	}
}




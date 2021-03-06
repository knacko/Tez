
using UnityEngine;
using System.Collections;

public class TriangleHexPolygon : TessellationPolygon {
	
	private float distance = 52f;	
	
	void Start() {	
		
		
		AdjacentObject[] objects = {new AdjacentObject("HexTriangle",60,distance,30),
										new AdjacentObject("HexTriangle",180,distance,150),
										new AdjacentObject("HexTriangle",300,distance,270)};
		
		adjacentObjects = objects;

		Initialize ();
		
	}
	
	void Update()
	{		
		updateMe();
	}
}




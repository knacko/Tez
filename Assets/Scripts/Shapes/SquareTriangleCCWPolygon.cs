using UnityEngine;
using System.Collections;

public class SquareTriangleCCWPolygon : TessellationPolygon {
	
	
	private float distance = 46f;
		
	void Start() {	
		
		AdjacentObject[] objects = {new AdjacentObject("TriangleSquare",0,distance*0.79f,120),
										new AdjacentObject("TriangleSquare",90,distance*0.79f,210),
										new AdjacentObject("TriangleSquare",180,distance*0.79f,300),
										new AdjacentObject("TriangleSquare",270,distance*0.79f,30)};
		
		adjacentObjects = objects;
		
		Initialize ();
		
	}
	
	void Update()
	{	
		updateMe();			
	}
		
}




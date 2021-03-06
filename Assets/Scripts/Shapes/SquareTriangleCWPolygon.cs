using UnityEngine;
using System.Collections;

public class SquareTriangleCWPolygon : TessellationPolygon {
	
	
	private float distance = 46f;
		
	void Start() {	
		
		AdjacentObject[] objects = {new AdjacentObject("TriangleSquare",0,distance*0.79f,240),
										new AdjacentObject("TriangleSquare",90,distance*0.79f,330),
										new AdjacentObject("TriangleSquare",180,distance*0.79f,60),
										new AdjacentObject("TriangleSquare",270,distance*0.79f,150)};
		
		adjacentObjects = objects;
		
		Initialize ();
		
	}
	
	void Update()
	{	
		updateMe();			
	}
		
}




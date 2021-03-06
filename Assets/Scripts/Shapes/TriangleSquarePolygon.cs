using UnityEngine;
using System.Collections;

public class TriangleSquarePolygon : TessellationPolygon {
	
	
	private float distance = 46f;
		
	void Start() {	
		
		AdjacentObject[] objects = {new AdjacentObject("SquareTriangleCCW",60,distance*0.79f,60),
										new AdjacentObject("TriangleSquare",180,distance*0.58f,180),
										new AdjacentObject("SquareTriangleCW",300,distance*0.79f,300)};
		
		adjacentObjects = objects;
		
		Initialize ();
		
	}
	
	void Update()
	{	
		updateMe();			
	}
		
}




using UnityEngine;
using System.Collections;

public class SquarePolygon : TessellationPolygon {
	
	
	private float distance = 45f;
	
	void Start() {	
		
		AdjacentObject[] objects = {new AdjacentObject(0,distance,0),
										new AdjacentObject(90,distance,0),
										new AdjacentObject(180,distance,0),
										new AdjacentObject(270,distance,0)};
		
		adjacentObjects = objects;
		
		Initialize ();
		
	}
	
	void Update()
	{	
		updateMe();			
	}
		
}




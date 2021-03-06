using UnityEngine;
using System.Collections;

public class ExtendedSquarePolygon : TessellationPolygon {
	
	private float distance = 45f;
	
	void Start() {	
		
		AdjacentObject[] objects = {new AdjacentObject(0,distance,0),
										new AdjacentObject(45,distance*1.41f,0),
										new AdjacentObject(90,distance,0),
										new AdjacentObject(135,distance*1.41f,0),
										new AdjacentObject(180,distance,0),
										new AdjacentObject(225,distance*1.41f,0),
										new AdjacentObject(270,distance,0),
										new AdjacentObject(315,distance*1.41f,0)};
		
		adjacentObjects = objects;

		Initialize ();
		
	}
	
	void Update()
	{	
		updateMe();			
	}
		
}




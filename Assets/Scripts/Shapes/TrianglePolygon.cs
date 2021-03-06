using UnityEngine;
using System.Collections;

public class TrianglePolygon : TessellationPolygon {
	
	private float distance = 27f;
		
	void Start() {	
		
		AdjacentObject[] objects = {new AdjacentObject(60,distance,180),
										new AdjacentObject(180,distance,180),
										new AdjacentObject(300,distance,180)};
		
		adjacentObjects = objects;
		
		Initialize ();
		
	}
	
	void Update()
	{		
		updateMe();
	}
}




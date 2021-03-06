
using UnityEngine;
using System.Collections;

public class HexagonPolygon : TessellationPolygon {
	
	private float distance = 44.5f;
	
	void Start() {	
		
		AdjacentObject[] objects = {new AdjacentObject(30,distance*1.738f,0),
										new AdjacentObject(90,distance*1.738f,0),
										new AdjacentObject(150,distance*1.738f,0),
										new AdjacentObject(210,distance*1.738f,0),
										new AdjacentObject(270,distance*1.738f,0),
										new AdjacentObject(330,distance*1.738f,0)};
		
		adjacentObjects = objects;

		Initialize ();
		
	}
	
	void Update()
	{		
		updateMe ();//drawRays();
	}
}




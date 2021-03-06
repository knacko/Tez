using UnityEngine;
using System.Collections;

public class ExtendedTrianglePolygon : TessellationPolygon {
	
	private float distance = 47f;

	void Start() {	
		
		AdjacentObject[] objects = {new AdjacentObject(0,		distance*1.1547f,	180),
										new AdjacentObject(30,	distance*1,	0),
										new AdjacentObject(60,	distance*0.5776f,	180),
										new AdjacentObject(90,	distance*1,	0),
										new AdjacentObject(120,	distance*1.1547f,	180),
										new AdjacentObject(150,	distance*1,	0),
										new AdjacentObject(180,	distance*0.5776f,	180),
										new AdjacentObject(210,	distance*1,	0),
										new AdjacentObject(240,	distance*1.1547f,	180),
										new AdjacentObject(270,	distance*1,	0),
										new AdjacentObject(300,	distance*0.5776f,	180),
										new AdjacentObject(330,	distance*1,	0)};
		
		adjacentObjects = objects;

		Initialize ();
		
	}
	
	void Update()
	{		
		updateMe();
		
	}
}




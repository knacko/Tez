using UnityEngine;
using System.Collections;

public class ExtendedRhombusPolygon : TessellationPolygon {
	
	private float distance = 45.5f;

	void Start() {	
		
		AdjacentObject[] objects = {new AdjacentObject(30,	distance*0.866f,	60),
										new AdjacentObject(60,	distance*1.5f,		-60),
										new AdjacentObject(90,	distance*1.7321f,	0),
										new AdjacentObject(120,	distance*1.5f,		240),
										new AdjacentObject(150,	distance*0.866f,	120),
										new AdjacentObject(210,	distance*0.866f,	240),
										new AdjacentObject(240,	distance*1.5f,		120),
										new AdjacentObject(270,	distance*1.7321f,	180),
										new AdjacentObject(300,	distance*1.5f,		60),
										new AdjacentObject(330,	distance*0.866f,	-60)};
		
		adjacentObjects = objects;
		
		Initialize ();
		
	}
	
	void Update()
	{	
		updateMe();			
	}
		
}




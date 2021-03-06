using UnityEngine;
using System.Collections;

public class HexRhombPolygon : TessellationPolygon {
	
	private float distance = 45f;

	void Start() {	
		
		AdjacentObject[] objects = {	new AdjacentObject("RhombHex", 40.9f,	distance*1.32f,		0),
										new AdjacentObject("HexRhomb", 90f,		distance*1.73f,		0),
										new AdjacentObject("RhombHex", 139.1f,	distance*1.32f,		0),
										new AdjacentObject("RhombHex", 220.9f,	distance*1.32f,		0),
										new AdjacentObject("HexRhomb", 270f,		distance*1.73f,		0),
										new AdjacentObject("RhombHex", 319.1f,	distance*1.32f,		0)};
		
		adjacentObjects = objects;

		Initialize ();
		
	}
	
	void Update()
	{	
		updateMe();			
	}
		
}




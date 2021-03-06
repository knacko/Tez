using UnityEngine;
using System.Collections;

public class RhombHexPolygon : TessellationPolygon {
	
	private float distance = 45f;
		
	void Start() {	
			
		AdjacentObject[] objects = {	new AdjacentObject("HexRhomb", 40.9f,	distance*1.32f,		0),
										new AdjacentObject("HexRhomb", 139.1f,	distance*1.32f,		0),
										new AdjacentObject("HexRhomb", 220.9f,	distance*1.32f,		0),
										new AdjacentObject("HexRhomb", 319.1f,	distance*1.32f,		0)};
		
		adjacentObjects = objects;
		
		Initialize ();
		
	}
	
	void Update()
	{	
		updateMe();			
	}
		
}




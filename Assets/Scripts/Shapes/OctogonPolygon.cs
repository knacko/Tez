using UnityEngine;
using System.Collections;

public class OctogonPolygon : TessellationPolygon {

	private float distance = 46f;
	
	void Start() {	

		AdjacentObject[] objects = {new AdjacentObject("Square",0,distance,0),
										new AdjacentObject("Square",90,distance,0),
										new AdjacentObject("Square",180,distance,0),
										new AdjacentObject("Square",270,distance,0)};
		
		adjacentObjects = objects;
		
		Initialize ();
		
	}
	
	void Update()
	{	
		updateMe();			
	}
		
}




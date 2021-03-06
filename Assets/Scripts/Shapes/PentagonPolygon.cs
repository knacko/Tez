
using UnityEngine;
using System.Collections;

public class PentagonPolygon : TessellationPolygon {
	
	private float distance = 45f;
	
	void Start() {	
		
		AdjacentObject[] objects = {	new AdjacentObject( 29,distance*1.274f,90),
										new AdjacentObject(119,distance*1.274f,270),
										new AdjacentObject(180,distance*1.24f,180),
										new AdjacentObject(241,distance*1.274f,90),
										new AdjacentObject(331,distance*1.274f,270)};
		
		adjacentObjects = objects;
				
		Initialize ();
		
	}
	
	void Update()
	{	
		updateMe();			
	}
		
}




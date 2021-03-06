using UnityEngine;
using System.Collections;

public class SquareOctoPolygon : TessellationPolygon {
	
	
	private float distance = 44.5f;
		
	
	void Start() {	
		
		AdjacentObject[] objects = {new AdjacentObject("OctoSquare",0,distance*1.71f,0),
										new AdjacentObject("OctoSquare",90,distance*1.71f,0),
										new AdjacentObject("OctoSquare",180,distance*1.71f,0),
										new AdjacentObject("OctoSquare",270,distance*1.71f,0)};
		
		adjacentObjects = objects;
		
		Initialize ();
		
	}
	
	void Update()
	{	
		updateMe();			
	}
		
}




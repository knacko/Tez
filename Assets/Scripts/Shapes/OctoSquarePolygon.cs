using UnityEngine;
using System.Collections;

public class OctoSquarePolygon : TessellationPolygon {

	void Start() {	
		
		float distance = 44.5f;
		AdjacentObject[] objects = {new AdjacentObject("SquareOcto",0,distance*1.71f,0),
										new AdjacentObject("OctoSquare",45,distance*2.41f,0),
										new AdjacentObject("SquareOcto",90,distance*1.71f,0),
										new AdjacentObject("OctoSquare",135,distance*2.41f,0),
										new AdjacentObject("SquareOcto",180,distance*1.71f,0),
										new AdjacentObject("OctoSquare",225,distance*2.41f,0),
										new AdjacentObject("SquareOcto",270,distance*1.71f,0),
										new AdjacentObject("OctoSquare",315,distance*2.41f,0)};
		
		
		adjacentObjects = objects;
		
		Initialize ();
		
	}
	
	void Update()
	{	
		updateMe();			
	}
		
}




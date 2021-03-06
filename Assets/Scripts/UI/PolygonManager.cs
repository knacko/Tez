using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Polygon manager - contains references to all of the polygons, materials and containers for use in the game
/// </summary>
public class PolygonManager : MonoBehaviour {
	
	public List<Material> lightUnlit;
	public List<Material> darkUnlit;
	public List<Material> lightShaded;
	public List<Material> darkShaded;
	
	public Dictionary<string, GameObject> polygonList = new Dictionary<string, GameObject>();
	
	// Use this for initialization
	void Start () {
		
		//Get the materials
		Material light, dark;
		int index = 0;
		
		while (true) {
		
			light =  Resources.Load("Materials/Tile" + index + " - Light - Unlit") as Material;
			dark =  Resources.Load("Materials/Tile" + index + " - Dark - Unlit") as Material;
			
			if (light == null || dark == null) break;
			
			lightUnlit.Add(light);
			darkUnlit.Add(dark);
			
			light =  Resources.Load("Materials/Tile" + index + " - Light - Shaded") as Material;
			dark =  Resources.Load("Materials/Tile" + index + " - Dark - Shaded") as Material;
			
			if (light == null || dark == null) break;
			
			lightShaded.Add(light);
			darkShaded.Add(dark);
			
			index++;
		}			
		
		//Get the shapes
		
		Object[] polygons = Resources.LoadAll("Prefabs/Shapes");
		
		//Debug.Log (polygons.Length + " polygons found.");		
		
		foreach (Object p in polygons) {
		
			polygonList.Add(p.name, p as GameObject);	
		//	Debug.Log ("Added polygon: " + p.name);
		}	
	}
	
	public void getMaterial(string quality, out Material lightMaterial, out Material darkMaterial) {
		
		lightMaterial = darkMaterial = null;
		
		lightMaterial = lightShaded[0];
		darkMaterial = darkShaded[0];
		
		if (true) return;
		
		
		int random;
		
		switch (quality) {
			
		case "Low":
			
			random = Random.Range(0,lightUnlit.Count);
			
			lightMaterial = lightUnlit[random];
			darkMaterial = darkUnlit[random];
			
			break;
			
		case "Medium":
			
		case "High":
			
			random = Random.Range(0,lightShaded.Count);
			
			lightMaterial = lightShaded[random];
			darkMaterial = darkShaded[random];
			
			break;
		}
		
	}
	
	public GameObject getPolygon(string name) {
		
		//Debug.Log ("Getting " + name);
		
		return polygonList[name];
	}
	
	public GameObject getContainer(string name, string difficulty) {
		
		string containerDir = "Prefabs/Containers/" + name + " - " + difficulty;
		
		//Debug.Log ("Loading: >" + containerDir + "<");
		return Resources.Load(containerDir) as GameObject;
			
	}
}
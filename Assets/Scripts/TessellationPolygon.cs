using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent (typeof (MeshCollider))]
[RequireComponent (typeof (MeshRenderer))]

public class TessellationPolygon : MonoBehaviour {

	public List<TessellationPolygon> adjacentPolygons;
	public AdjacentObject[] adjacentObjects;
	
	public bool state = false; //true is dark, false is light
	private bool actualState = false;
	
	private bool winState = false; //in this state when the tiles are gold
	private bool startState = true; //in this state when initialized, flips after first call to animation
	
	private int id;
	
	private Color onColor = new Color (0,0,0,1);
	private Color offColor = new Color (1,1,1,1);
	
	/*
	[HideInInspector]
	public string On2OffAnim;
	[HideInInspector]
	public string Off2OnAnim;
	[HideInInspector]
	public string On2WinAnim;	
	[HideInInspector]
	public string Off2WinAnim;
	*/
	private static string On2OffAnim = "On2Off";
	private static string Off2OnAnim = "Off2On";
	private static string On2WinAnim = "On2Win";
	private static string Off2WinAnim = "Off2Win";
	private static string Win2OnAnim = "Win2On";
	private static string Win2OffAnim = "Win2Off";
	private static string On2OnAnim = "On2On";
	private static string Off2OffAnim = "Off2Off";
	
	private GameScript script;
	private PolygonManager polygonManager;
	private SoundManager soundManager;
	
	private bool disabled = true;

	private bool calledThisFrame = false;

	public bool toggleSpawnAdjacent = false; //for debug
	public bool flipMe = false; //for debug	
	
	void Update () {
		updateMe ();
	}
	
	// Use this for initialization
	public void Initialize ()
	{	
		//Debug.Log ("Spawned " + gameObject.transform.parent.name + " at " + gameObject.transform.position.ToString() + ".");
		
		script = GameObject.FindWithTag("GameController").GetComponent<GameScript>();
		soundManager = script.getSoundManager();
		polygonManager = script.getPolygonManager();
		gameObject.tag="Polygon";
				
		foreach (AnimationState state in animation) {
			state.speed = 0.5f/0.3f; // animation length / desire length
        }		
		
		foreach (AdjacentObject obj in adjacentObjects)
		{
			//Add parent rotation to the adjacent objs
			obj.incAngle(transform.parent.transform.eulerAngles.z);
		}		
			
		if (countAdjacentPolygons() <= 1) {
			deletePolygon();
			return;
		}
		
		//if (GameObject.Find("container") != null)
		
		if (script.countPolygons() < 100)
			spawnAdjacentObjects();	

		script.SendMessage("AddPolygon",this);		

	}
	
	public void ToggleState()
	{
		ToggleState(true, true, 0);
	}
	
	public void ToggleState(bool animated, bool propogate, float delay)
	{	
		state = !state;
				
		if (animated ) StartCoroutine(animate(state, delay)); //only animate when actually playing the game, setup uses this to compute initial start pos
			
		if (propogate) 
		{
			foreach(TessellationPolygon p in adjacentPolygons) {
				p.ToggleState(animated, false, delay);// + propogationDelay);	
			}				
		}
	}	
	
	public IEnumerator animate(bool toState, float delay) {
		
		if (toState == actualState && startState) {
		startState = false;
			yield break;
		}
		
		yield return new WaitForSeconds(delay);
		
		if (toState == actualState && !winState) {
		
			animation.Play(state ? On2OnAnim : Off2OffAnim);	
			
		} else {
		
			animation.Play(state ? (winState ? Win2OnAnim : Off2OnAnim) : (winState ? Win2OffAnim :On2OffAnim));	
		
		}
				
		if (!startState) soundManager.playTileFlip();
	
		actualState = toState;
		
		startState = false;
		winState = false;
		
	}	
	
	public void win(bool state) {
		if (state) animation.Play(state ? Off2WinAnim : On2WinAnim);
		winState = true;
	}

	//Searches for polygons when initialized, signals objects it finds to add this new polygon to their own list
	private int countAdjacentPolygons() {
		
		int initialized = 0;
		int uninitialized = 0;
				
		foreach(AdjacentObject obj in adjacentObjects) {
			Vector3 targetPos = obj.getRange () * MathS.angleToUnitVector(obj.getAngle());
			GameObject g = findObjAtPos(transform.position + targetPos);
			
			if (g != null) {
				
				if (g.tag != "Polygon") continue; //if border, ignore
				
				initialized++;		//if it already exists, increment the initialized counter
				
			} else {
				uninitialized++;	//if it doesn't exist, increment the uninitalized counter
			}
		}	
		
		script.SendMessage("AddAdjacents", uninitialized);
		
		return initialized + uninitialized;
		
	}
		
	public void spawnAdjacentObjects()
	{				
		adjacentPolygons.Clear();
			
		foreach (AdjacentObject obj in adjacentObjects) {
			Vector3 targetPos = obj.getRange () * MathS.angleToUnitVector(obj.getAngle ());
			GameObject g = findObjAtPos(transform.position + targetPos);
			
			if (g == null) {
				g = Instantiate(getPolygon(obj.getShape()), transform.position + targetPos,  Quaternion.Euler(0, 0, obj.getRotation()+transform.parent.transform.eulerAngles.z)) as GameObject;
				g.name = this.transform.parent.name;
				//g.name = "Polygon";
			} else {
				
				if (g.tag != "Polygon") continue; //if border, ignore
				
				adjacentPolygons.Add (g.GetComponent<TessellationPolygon>());
				g.SendMessage("AddPolygon",this);
			}
		}				
	}

	//Checks to see if an object is at target position, return null if not
	private GameObject findObjAtPos(Vector3 pos) {
				
		Collider[] hitColliders = Physics.OverlapSphere(pos, 1);
		if (hitColliders.Length == 0) {	
			return null;
		}
		//Debug.Log ("Object found at: " + pos.ToString());	
		return hitColliders[0].gameObject;
	}
	
	public List<TessellationPolygon> getAdjacentPolygons() {
		return adjacentPolygons;	
	}
	
	/// <summary>
	/// Sends a message to all of the adjacent polygons for them to delete this polygon from their lists
	/// </summary>
	private void deletePolygon() {
		
		script.SendMessage("DeletePolygon", this);
		
		//foreach (TessellationPolygon p in adjacentPolygons) p.SendMessage("DeletePolygon",this);
		
		Destroy(this.transform.parent.gameObject);
	}
	/// <summary>
	/// Sends a message to all of the adjacent polygons for them to delete this polygon from their lists
	/// </summary>
	private void deletePolygon(TessellationPolygon t) {
		adjacentPolygons.Remove(t);
	}
	
	//Message from another object to add the sent object to its list
	public void AddPolygon(TessellationPolygon t) {
		if (!adjacentPolygons.Contains(t)) adjacentPolygons.Add (t);
	}
	
	public void updateMe() {
		
		drawRays();
		
		if (toggleSpawnAdjacent) {
			Debug.Log ("DEBUG: toggle spawn hit");
			toggleSpawnAdjacent = false;
			spawnAdjacentObjects();	
		}
		
		if (flipMe) {
			Debug.Log ("DEBUG: toggle state hit");
			ToggleState(true, false, 0);
			flipMe = false;
		}
	}
	
	public bool getState() {
		return state;		
	}
	
	/// <summary>
	/// Gets the polygon prefab.
	/// </summary>
	/// <returns>
	/// The polygon prefab.
	/// </returns>
	/// <param name='name'>
	/// The name of the polygon. If null, means the same object as what is calling
	/// </param>
	public GameObject getPolygon(string name) {
		
		if (name == null) name = this.transform.parent.name;
		
		return polygonManager.getPolygon (name);
		
	}
		
	public void EnableMe(bool toAnimate) {
		//Debug.Log ("Polygon enabled");
		disabled = false;
		collider.enabled = true;
		if (toAnimate) StartCoroutine(animate(state,0f));
	}

	public void DisableMe() {
		disabled = true;	
		collider.enabled = false;
	}	

	public void OnMouseDown() {

		if (disabled) return;
		ToggleState();
		script.SendMessage("PolygonFlipped",this);
		Debug.Log ("Game object: " + transform.parent.gameObject.name + " has been touched at " + Time.time);
	}
	
	public void drawRays() {
		
		foreach(AdjacentObject obj in adjacentObjects) 
		{		
			Debug.DrawRay(transform.position, obj.getRange () * MathS.angleToUnitVector(obj.getAngle()), Color.green);
		}	
	}
	
	public void setID(int id) {
		
		this.id = id;
		
	}
	
	public int getID() {
		
		return id;
	}

	public bool isEnabled() {
		return !disabled;
	}

}


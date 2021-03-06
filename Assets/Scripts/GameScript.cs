using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameScript : MonoBehaviour {
	public List<TessellationPolygon> polygons;
	private List<bool> targetPolygons;
	private List<bool> originalPolygons;
	public bool solveMe = false;
	private int numOfPolygons = 1;
	private Camera gameCam;
	private enum GameType {Normal, Puzzle};
	private enum GameShape {Triangle, Square, Pentagon, Hexagon, ExtendedPentagon, ExtendedSquare, Rhombus, ExtendedTriangle, OctoSquare, TriangleSquare, HexTriangle, HexRhomb};
	private enum GameDiff {Easy, Medium, Hard};
	[HideInInspector]
	public enum GameGraphics {Low, Medium, High};
	public enum GameAnimations{Off, Slow, Fast};
	private GameType type = GameType.Normal;
	private GameShape shape;
	private GameDiff diff;
	private GameGraphics graphics = GameGraphics.Low;
	private GameAnimations animations = GameAnimations.Slow;
	private float orthoCamMax = 0;
	private string containerName = "container";
	public GameObject MenuUI;
	//public GameObject gameplayHolder;
	public GameObject gameOptions;
	public GameObject playScreenMenu;
	public UILabel scoreText;
	private PolygonManager polygonManager;
	private SoundManager soundManager;
	private float spawnStartTime = 0;
	public GameObject background;
	private bool menuAnimating = false;
	
	private float playTime = 0;
	private int numOfFlips = 0;
	private int numOfMoves = 0;
		
	private bool winState = false;
		
	public GameObject loadingPanel;
	
		// Use this for initialization
	void Start () {
		
		gameCam = Camera.main;
		//scoreText = GameObject.Find("Score").GetComponent<UILabel>();
		polygonManager = GameObject.Find ("PolygonManager").GetComponent<PolygonManager>();
		soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager>();
		//GameObject.Find("OptionsMenu").SetActiveRecursively(false);
		setupOptionsMenu();
		//background.active = true;
	}
	
	// Update is called once per frame
	void Update () {
		playTime += Time.deltaTime;
	}	
	
	/// <summary>
	/// Sets the type of the game.
	/// </summary>
	/// <param name='obj'>
	/// The UIButton with a name of GameType enum
	/// </param>
	void SetGameType(GameObject obj) {
		type = (GameType)System.Enum.Parse(typeof(GameType), obj.name);
		Debug.Log ("Type: " + type.ToString() + " called by " + obj.name);
	}
	
	/// <summary>
	/// Sets the game difficult.
	/// </summary>
	/// <param name='obj'>
	/// the UIButton with a name of GameDiff enum
	/// </param>
	void SetGameDiff(GameObject obj) {
		diff = (GameDiff)System.Enum.Parse(typeof(GameDiff), obj.name);	
		Debug.Log ("Diff: " + diff.ToString() + " called by " + obj.name);
	}	
	
	/// <summary>
	/// Sets the game shape.
	/// </summary>
	/// <param name='obj'>
	/// the UIButton with the name of a GameShape enum
	/// </param>
	void SetGameShape(GameObject obj) {
		shape = (GameShape)System.Enum.Parse(typeof(GameShape), obj.name);	
		Debug.Log ("Shape: " + shape.ToString() + " called by " + obj.name);
	}
	
	void startSpawn(Object obj) {
		
		StartCoroutine(startSpawnHelper(obj));	
		
	}	
	
	/// <summary>
	/// Starts the spawn. Creates an object and container based on the enum for game shape and diff
	/// </summary>
	/// <param name='obj'>
	/// Only for debugging purposes, indicated which object threw the command to start the spawn
	/// </param>
	IEnumerator startSpawnHelper(Object obj) {
				
		yield return new WaitForSeconds(0.5f);
		
		Debug.Log ("Spawning polygon from " + obj.name + " at " + Time.realtimeSinceStartup);
		
		numOfPolygons = 1;
	
		GameObject polygonPrefab = getPolygon(shape.ToString());
		GameObject containerPrefab = getContainer(shape.ToString(), diff.ToString());
		
		spawnStartTime = Time.realtimeSinceStartup ;
		
		GameObject g = Instantiate(polygonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		g.name = shape.ToString ();
		Instantiate(containerPrefab, Vector3.zero, Quaternion.identity).name = containerName; //

	}
	
	/// <summary>
	/// Adds a polygon to the polygons list. Called by a message when the polygon is instantiated
	/// </summary>
	/// <param name='p'>
	/// The polygon being instantiated
	/// </param>
	void AddPolygon(TessellationPolygon p) {
		
		if (polygons.Contains(p)) return;
		
		//Debug.Log ("Adding polygon: " + p.transform.parent.name);
		
		p.transform.parent.name = "Polygon " + polygons.Count;	
		p.setID(polygons.Count);
		
		polygons.Add(p);
		
		Debug.Log(polygons.Count + " of " + numOfPolygons + " registered.");
		checkIfAllPolygonsSpawned();
	}	
		
	/// <summary>
	/// Increments the adjacent polygon count. Used for determining if all of the polygons have spawned.
	/// Always called by message from the instantiated polygon before the polygon calls to add it to the polygons list.
	/// </summary>
	/// <param name='numOfAdjacents'>
	/// Number of adjacents.
	/// </param>
	void AddAdjacents(int numOfAdjacents) {
		numOfPolygons += numOfAdjacents;		
	}
	
	/// <summary>
	/// Removes a polygon from the list.
	/// </summary>
	/// <param name='p'>
	/// P.
	/// </param>
	void DeletePolygon(TessellationPolygon p ) {
		
		Debug.Log ("deleting polygon");
		
		if (polygons.Contains(p)) polygons.Remove(p);
		
		numOfPolygons--;
		
		checkIfAllPolygonsSpawned();
	}
	
	void checkIfAllPolygonsSpawned() {
		if (polygons.Count >= numOfPolygons) {
			//Debug.Log ("Flipping polygons");
			
			//fitCamera();
			Debug.Log ("Spawn time took " + (Time.realtimeSinceStartup  - spawnStartTime) + " seconds, finshed at " + Time.realtimeSinceStartup);
			
			setupGameMode();
			
			startGame(this);
			
			StartCoroutine(moveCamToGame(true));
					
		}
	}
	
	public int countPolygons() {
		return polygons.Count;	
	}	
	
	void setupGameMode() {
		
		numOfMoves = 0;
		
		updateMovesDisplay();		
		
		playTime = 0;
		numOfFlips = 0;
		
					
		for (int i = 0; i < polygons.Count; i++) {
				
				polygons[i].ToggleState(false, false, 0);
		}
		
		
		if (type == GameType.Normal) {
			
			for (int i = 0; i < polygons.Count; i++) {
				
				int randomNumber = Random.Range(0, polygons.Count);	
				//Debug.Log (i + ": Polygon " + randomNumber + " flipped");
				polygons[randomNumber].ToggleState(false, true, 0);
			}
			
		}
		
		if (type == GameType.Puzzle) {
			
			targetPolygons = new List<bool>(polygons.Count);
						
			for (int i = 0 ; i < polygons.Count; i++) {
				targetPolygons.Add (Random.value >= 0.5);					
			}
			
			//targetPolygons = createRandomFlippedList(polygons);	
			
		}
		
		originalPolygons = new List<bool>(polygons.Count);
		for (int i = 0 ; i < polygons.Count; i++) {
			originalPolygons.Add(polygons[i].getState());
		}
		
		Destroy(GameObject.Find(containerName));
	}
		
	/// <summary>
	/// Starts the game. Creates the random state of the polygons, deletes the container, then enabled the polygons.
	/// </summary>
	void startGame(Object obj) {
		
		winState = false;
		
		Debug.Log ("startGame called by " + obj.name);
		enablePolygons(true);
		
		AdjacencyMatrix adj = new AdjacencyMatrix(polygons);
		Debug.Log (adj.ToString());
		
	}
	
	void pauseGame() {
		
		disablePolygons();	
		
	}
	
	/// <summary>
	/// Checks for the win condition for the respective game type.
	/// </summary>
	/// <returns>
	/// Whether the game has been won
	/// </returns>
	private bool checkForWin() {
		
		if (type == GameType.Normal) {
		
			foreach(TessellationPolygon p in polygons) {
				if(p.getState() == true) return false;
			}
						
			return true;
			
		}
		
		if (type == GameType.Puzzle) {
			
			bool win = true;
			
			for (int i = 0; i < polygons.Count; i++) {
				
				//win = win & (polygons[i].getState() == targetPolygons[i].getState());
			}
			
			return win;
		}
		
		return false;
				
	}

	
	
	/// <summary>
	/// Creates the random flipped list.
	/// </summary>
	/// <returns>
	/// The random flipped list.
	/// </returns>
	/// <param name='polygons'>
	/// The list of polygons
	/// </param>
	List<TessellationPolygon> createRandomFlippedList(List<TessellationPolygon> polygons) {
		
		for (int i = 0; i < polygons.Count*2; i++) {
			
			int randomNumber = Random.Range(0, polygons.Count);	
			//Debug.Log (i + ": Polygon " + randomNumber + " flipped");
			polygons[randomNumber].ToggleState(false, true, 0);

		}

		return polygons;
	}
	
	/// <summary>
	/// Enables the polygons. Can now touch and interact with them.
	/// </summary>
	/// <param name='animate'>
	/// Whether the polygons should be animated for the state switch.
	/// Will be false when creating the random list, true otherwise.
	/// </param>
	void enablePolygons(bool animate) {
		foreach (TessellationPolygon t in polygons) {
			t.EnableMe(animate);
		}			
	}
	
	void enablePolygons(Object obj) {
		enablePolygons (false);	
	}
	
	/// <summary>
	/// Disables the polygons. No longer able to touch or interact.
	/// </summary>
	void disablePolygons() {
		foreach (TessellationPolygon t in polygons) {
			t.DisableMe();
		}		
	}

	void togglePolygons() {

		if(!polygons[0]) return;

		if (polygons[0].isEnabled()) disablePolygons();
		else enablePolygons(false);
	}
	
	/// <summary>
	/// Called by message when a polygon is flipped. Checks for win condition.
	/// </summary>
	void PolygonFlipped(TessellationPolygon p) {	
		
		//StartCoroutine(shakeCamera());
		numOfFlips += 1+p.getAdjacentPolygons().Count;
		numOfMoves++;
		updateMovesDisplay();
		if (checkForWin()) {
			Debug.Log ("*****WINNER*****");
			disablePolygons();
			StartCoroutine(win());
		
		}
		//AdjacencyMatrix adj = new AdjacencyMatrix(polygons);
		//Debug.Log (adj.ToString());
	}
	
	IEnumerator win() {
		
		winState = true;
				
		yield return new WaitForSeconds(0.5f);
		setPolygonsToWin(true);		
		soundManager.playLevelWin();
		yield return new WaitForSeconds(1f);
		gameOptions.SendMessage("On");
		
	}

	/// <summary>
	/// Indicates the options menu is toggled
	/// 
	/// If in winstate, do nothing
	/// 
	/// If the polygons are disable, enable them
	/// </summary>
	void toggledOptionsMenu() {

		if (winState) return;

		togglePolygons();

	}

	
	void setPolygonsToWin(bool state) {
				
		foreach (TessellationPolygon p in polygons)
		{
			p.win (true);
		}
		
	}
	
	
	/// <summary>
	/// Gets the middle point from the list of polygons
	/// </summary>
	/// <returns>
	/// The middle point of the lsit.
	/// </returns>

	private Vector3 getMiddlePoint() {
		
		Vector3 center = Vector3.zero;
		
		foreach (TessellationPolygon p in polygons)
		{
			center += p.transform.position;
		}
		
		center /= polygons.Count;
		
		return center;
	}
	
	/// <summary>
	/// Gets the bounds for a list of polygons.
	/// </summary>
	/// <returns>
	/// The bounds of the list.
	/// </returns>
	/// <param name='center'>
	/// The middle point of the list
	/// </param>
	private Bounds getBounds(Vector3 center){
		
		Bounds b = new Bounds(center, Vector3.zero);
		
		foreach (TessellationPolygon p in polygons)
		{
			b.Encapsulate(p.renderer.bounds);
		}
		
		return b;		
	}
	
	private IEnumerator shakeCamera() {
		
		
	float elapsed = 0.0f;
	float duration = 0.2f;
		float magnitude = 0.5f;
		
	Vector3 originalCamPos = gameCam.transform.position;
	while (elapsed < duration) {
		elapsed += Time.deltaTime;
		float percentComplete = elapsed / duration;
		float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
		// map noise to [-1, 1]
		float x = Random.value * 2.0f - 1.0f;
		float y = Random.value * 2.0f - 1.0f;
		x *= magnitude * damper;
		y *= magnitude * damper;
		gameCam.transform.position = new Vector3(x, y, originalCamPos.z);
		yield return null;
	}
	gameCam.transform.position = originalCamPos;
		
		
		
		
		/*
		Vector3 camStartPos = gameCam.transform.position;
		Vector3 camEndPos = gameCam.transform.position + new Vector3(0,1,0);;
		
		for (float t = 0.0f; t < 1.0f; t += 2 * Time.deltaTime / (getAnimationLength()))
		{		
			gameCam.transform.position = camStartPos + new Vector3(0, 0.1f*-((6.4f*t-3.2f)*(6.4f*t-3.2f))/10f + 1f,0); //approximation of sin curve for range of 0,1 and domain of 0,1 with max at 0.5,1
				//MathS.ULerp(camStartPos, camEndPos, t < 0.5f ? t / 0.5f : 1 - ((t-0.5f) / 0.5f));
			
				
				//gameCam.transform.position = Vector3.Lerp(camStartPos, camEndPos, t);
						
			yield return null;
		}
		
		gameCam.transform.position = camStartPos;
		*/
	}
	
	/// <summary>
	/// Moves the cam to/from game. Initially above, will slide down to the center point and properly frame the polygons.
	/// </summary>
	/// <returns>
	/// The coroutine stuff
	/// </returns>
	/// <param name='to'>
	/// True for going to the game, false for away.
	/// </param>
	private IEnumerator moveCamToGame(bool to) {
		
		if (to == true) {	
			
			yield return new WaitForSeconds(1f);

			loadingPanel.SendMessage("fadeOutPanel");
		}
		
		//disablePolygons();
		//Debug.Log ("Moving cam called: " + to.ToString());
		
		Bounds b = getBounds(getMiddlePoint());
		Vector3 camStartPos = b.center + new Vector3(0,b.size.y*3,100);
		Vector3 camEndPos = b.center + new Vector3(0,0,100);
		
		if (to == false) { //if false, increment end point
			
			Vector3 temp = camStartPos;
			camStartPos = camEndPos;
			camEndPos = temp;	
			camEndPos.y *= -1;
			//yield return new WaitForSeconds(getScreenSlideLength()); 
		}
			
		orthoCamMax = 1.15f * Mathf.Max (b.extents.x/gameCam.aspect, b.extents.y);
		//if (b.extents.y > b.extents.x) orthoCamMax = 1.15f * b.extents.y;	//fix for the triangle easy
		//Debug.Log ("size: " + gameCam.orthographicSize + " to " + orthoCamMax);
		gameCam.orthographicSize = orthoCamMax != 0 ? orthoCamMax : 1;
		
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 0.5f)
		{
			gameCam.transform.position = MathS.ULerp(camStartPos, camEndPos, MathS.bounceInOutLogic(t));
			//gameCam.transform.position = Vector3.Lerp(camStartPos, camEndPos, t);
						
			yield return null;
		}
		
		gameCam.transform.position = camEndPos;
		
		
		if (to == true) {	
			Debug.Log ("Going to game");
			//startGame(this);
			playScreenMenu.SetActive(true);
			gameOptions.SendMessage("Off");
			//playScreenMenu.SendMessage("fadeInPanel");
		}
		
		if (to == false) {
			Debug.Log ("Going to menu");
			gameCam.transform.position = new Vector3(-100,250,-100);
			foreach(TessellationPolygon p in polygons) {
				Destroy(p.gameObject.transform.parent.gameObject);	
			}
			polygons.Clear();
			
		}
				
	}
	
	/// <summary>
	/// Shows the main menu.
	/// </summary>
	private void showMainMenu() {
		
		//playScreenMenu.SetActiveRecursively(false);
		StartCoroutine(moveCamToGame(false));
		
	}

	/// <summary>
	/// THe message send from the UIButton to indicate another game with the same settings
	/// </summary>
	private void playAgain() {
				
		disablePolygons();
		
		setupGameMode();
		
		startGame(this);
	}
		
	/// <summary>
	/// Resets the polygons to the initial configuration.
	/// </summary>
	private void retry() {
		
		numOfMoves = 0;
		
		updateMovesDisplay();
		
		disablePolygons();
		
		//yield return new WaitForSeconds(getScreenSlideLength());
		
		if (type == GameType.Normal) {
			
			for (int i = 0; i < polygons.Count; i++) {
				
				if (polygons[i].state != originalPolygons[i]) polygons[i].ToggleState(false, false, 0);	
				
			}	
			
			enablePolygons(true);
		}
		
		originalPolygons = new List<bool>(polygons.Count);
		for (int i = 0 ; i < polygons.Count; i++) {
			originalPolygons.Add(polygons[i].getState());
		}
			
	}

	
	/// <summary>
	/// Gets the polygon prefab.
	/// </summary>
	/// <returns>
	/// The polygon prefab.
	/// </returns>
	/// <param name='name'>
	/// The name of the polygon
	/// </param>
	public GameObject getPolygon(string name) {
		
		return polygonManager.getPolygon(name);

	}
	
	
	/// <summary>
	/// Gets the container prefab.
	/// </summary>
	/// <returns>
	/// The container prefab.
	/// </returns>
	/// <param name='name'>
	/// Name.
	/// </param>
	/// <param name='difficulty'>
	/// Difficulty.
	/// </param>
	public GameObject getContainer(string name, string difficulty) {
		
		return polygonManager.getContainer(name, difficulty);
		
	}

	private void setupOptionsMenu() {
		
		//Set the initial state of the graphics checkbox		
		//GameObject.Find ("Graphics").transform.Find(getGraphics().ToString()).SendMessage("OnClick");
		//GameObject.Find ("AnimationsMenu").transform.Find(getAnimations().ToString()).SendMessage("OnClick");
		
		GameObject.Find ("SoundMenu").SendMessage("Set",getSound());
		GameObject.Find ("MusicMenu").SendMessage("Set",getMusic());
		
		
		//if (getMusic()) GameObject.Find ("Music").SendMessage("OnClick");
	}
	

	
	private void setMusic(bool state) {
		
		if (state) soundManager.playGameMusic();
		else soundManager.stopGameMusic();
		
		Debug.Log ("set music: " + state);
		
		PlayerPrefs.SetInt("Music", state ? 1 : -1);
		GameObject.Find ("MusicMenu").SendMessage("Set", state);
	}
	
	private void setSound(bool state) {
		
		Debug.Log ("set sound: " + state);
		
		PlayerPrefs.SetInt("Sound", state ? 1 : -1);
		GameObject.Find ("SoundMenu").SendMessage("Set", state);
	}
	
	public bool getSound() {
		return PlayerPrefs.GetInt("Sound", 1) == 1 ? true : false;
	}
	
	
	public bool getMusic() {
		return PlayerPrefs.GetInt("Music", 1) == 1 ? true : false;		
	}
	
	
	private void updateMovesDisplay() {
		
		scoreText.text = "" + numOfMoves;
		
	}
	
	public PolygonManager getPolygonManager() {
		return polygonManager;	
	}
	
	public SoundManager getSoundManager() {
		return soundManager;	
	}
	

	
	
	
}




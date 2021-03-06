using UnityEngine;
using System.Collections;

public class GameOptionsButton : MonoBehaviour {
	
	private bool isMenuOpen = false; //true if the menu is open, false if not
	public GameObject GameplayHolder;
	public GameObject GameScript;
	public GameObject SoundManager;
	
	public GameObject closeSprite;
	public GameObject openSprite;
	
	private GameScript script;
	
	// Use this for initialization
	void Start () {
		closeSprite.active = false;
		script = GameScript.GetComponent<GameScript>();
	}
	
	void ToggleState() {
		
		isMenuOpen = !isMenuOpen;
	}
	
	void OnEnable() {
		closeSprite.active = false;	
		openSprite.active = true;
		GameplayHolder.transform.localPosition = new Vector3 (0,-12,0);
		isMenuOpen = false;
		
	}
	
	void OnClick() {
		
//		if (script.getMenuAnimating()) return;
		
		SoundManager.SendMessage("playMenuButtonPress");
		
		if (isMenuOpen) {
			GameplayHolder.SendMessage("Right");	
			GameScript.SendMessage("startGame", this);
			openSprite.active = true;
			closeSprite.active = false;
			
		} else {
			GameplayHolder.SendMessage("Left");
			GameScript.SendMessage("pauseGame");
			closeSprite.active = true;
			openSprite.active = false;
		}
		
		ToggleState();
	}
	
	
}

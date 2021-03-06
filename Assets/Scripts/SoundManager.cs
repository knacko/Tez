using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	public AudioSource menuPress, tileFlip, gameMusic, levelWin;
	private GameScript script;
	
	public void playMenuButtonPress() {
		
		playSound (menuPress, 0.2f);	
	}
	
	public void playDelayMenuButtonPress() {
		playDelayedSound (menuPress, 0.25f,0.5f);	
		
	}	
	
	public void playTileFlip() {
		
		playSound (tileFlip,0.1f);
			
	}
	
	public void playGameMusic() {		
		Debug.Log ("Playing game music");
		gameMusic.Play();
	}
	
	public void stopGameMusic() {
		Debug.Log ("Stopping game music");
		gameMusic.Stop ();	
	}
	
	public void playLevelWin() {
		Debug.Log ("Play level win");
		playSound(levelWin, 0f);
	}
	
	// Use this for initialization
	void Start () {
	
		script = GameObject.FindWithTag("GameController").GetComponent<GameScript>();
		
		if (script.getMusic()) gameMusic.Play();
		
	}
	
	void playSound(AudioSource sound, float pitchRange) {
		if (!script.getSound ()) return;
		sound.pitch = 1f + Random.Range (-pitchRange, 0);
		sound.Play();
	}
	
	void playDelayedSound(AudioSource sound, float pitchRange, float delay) {
		StartCoroutine(playDelayedSoundHelper(sound, pitchRange, delay));
	}
	
	IEnumerator playDelayedSoundHelper(AudioSource sound, float pitchRange, float delay) {
		
		if (script.getSound ()) {
			
			yield return new WaitForSeconds(delay);
			
			sound.pitch = 1f + Random.Range (-pitchRange, 0);
			sound.Play();
		}
	}
}

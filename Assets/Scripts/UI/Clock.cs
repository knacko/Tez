using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {
	
	public UILabel label;
	
	void OnEnable() {
		setTime();
		StartCoroutine(checkTime());	
	}
	
	IEnumerator checkTime() {
		
		while(true)	{
			setTime();
			yield return new WaitForSeconds(30f);
		}
	}
	
	void setTime() {
		
		string time = System.DateTime.Now.ToString("h:mm");
		
		label.text = time;
		Debug.Log ("Current time: " + time);
	}
}

using UnityEngine;
using System.Collections;

public class BackgroundLerp : MonoBehaviour {

	private float xOffset = 0.0909f;
	private float yOffset = 0.0666f;
	
	private bool animating = false;
	
	public GameObject background0;
	public GameObject background1;
	
	private bool currentBg = false; //false for 0, true for 1;
	
	public float duration = 0.5f;
	public float minStaticTime = 2f;
	public float maxStaticTime = 5f;
	
	public bool test = false;
	
	
	/*
	 * 	y = 0.0666
		x = 0.0909
	 * */
	
	GameScript script;
	// Use this for initialization
	void Start () {
		animating = false;
		
		script = GameObject.FindWithTag("GameController").GetComponent<GameScript>();
		
		//background1.renderer.enabled = false;
		StartCoroutine(animateEvery_Seconds());
	}
	
	IEnumerator animateEvery_Seconds() {
		
		while (true) {
		
			yield return new WaitForSeconds(minStaticTime+Random.value*(maxStaticTime-minStaticTime));
			
			ChangeBackground();
			
		}
	}
	
	
	void ChangeBackground() {
		
		if (animating) return;
		
		if (currentBg) {
			StartCoroutine(ChangeBackgroundHelper(background1, background0));
		} else {
			StartCoroutine(ChangeBackgroundHelper(background0, background1));			
		}
		
		currentBg = !currentBg;
		
	}
	
	IEnumerator ChangeBackgroundHelper(GameObject fromBG, GameObject toBG) {
				
		animating = true;
		
		//Change the offset of the texture to be lerped to
		toBG.renderer.material.mainTextureOffset = new Vector2(xOffset*Random.Range (0,8), yOffset*Random.Range (0,8));
				
		fromBG.transform.position = new Vector3(0,0,-1);
		toBG.transform.position = new Vector3(0,0,0);
				
		Color toColor = toBG.renderer.material.color;
    	toColor.a = 1f;
   		toBG.renderer.material.color = toColor;
		
		for (float t = 0.0f; t < 1f; t += Time.deltaTime / duration)
		{	
			    Color fromColor = fromBG.renderer.material.color;
    			fromColor.a = 1f-t;
   				fromBG.renderer.material.color = fromColor;

				yield return null;
		}
		
		animating = false;
	}
			
			
		/*	
	
	void Up() {
		ChangeOffset(new Vector3(-xOffset,-yOffset,0),script.getScreenSlideLength() );
	}
	
	void Down() {
		ChangeOffset(new Vector3(xOffset,yOffset,0),script.getScreenSlideLength() );
	}
	
	void Left() {
		ChangeOffset(new Vector3(xOffset,-yOffset,0),script.getScreenSlideLength() );
	}
	
	void Right() {
		ChangeOffset(new Vector3(-xOffset,yOffset,0),script.getScreenSlideLength() );
	}
	
	void DoubleDown() {
		ChangeOffset(new Vector3(xOffset*2,yOffset*2,0),script.getScreenSlideLength()*2);
	}
	
	void ChangeOffset(Vector3 offset, float duration) {
				
		if (animating) return;
		
		StartCoroutine(ChangeOffsetHelper(offset, duration));	
	}
	
	IEnumerator ChangeOffsetHelper(Vector3 offset, float duration) {
		
		rotationSpeed = sFastRotation;
		
		//animating = true;
		
		//Vector3 currentOffset = renderer.material.GetTextureOffset("_MainTex");
				
		for (float t = 0.0f; t < 0.8f; t += Time.deltaTime / duration)
		{	
		//	renderer.material.SetTextureOffset ("_MainTex", MathS.ULerp(currentOffset, currentOffset + offset, MathS.bounceInOutLogic(t)));
			
			yield return null;
		}
		
		//animating = false;
		rotationSpeed = sSlowRotation;
	}
	*/
	void Update() {
		
		if (test) {
			ChangeBackground();
			test = false;			
		}
	}	
}

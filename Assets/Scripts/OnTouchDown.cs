#if UNITY_IPHONE
 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class OnTouchDown : MonoBehaviour
{
	void Update () {
		// Code for OnMouseDown in the iPhone. Unquote to test.
		RaycastHit hit = new RaycastHit();
		for (int i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch(i).phase.Equals(TouchPhase.Began)) {
			// Construct a ray from the current touch coordinates
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
			if (Physics.Raycast(ray, out hit)) {
				hit.transform.gameObject.SendMessage("OnMouseDown");
		      }
		   }
	   }
	}
}

#endif

#if UNITY_ANDROID

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(Camera))]
 
public class OnTouchDown : MonoBehaviour
{	
	float timeSinceLastHit = 0f;
	float timeBetweenHits = 0.2f;

	public LayerMask layer;

	Camera cam;
	GameObject sphere;
	
	void Start() {

		cam = GetComponent<Camera>();

		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.position = new Vector3(1000f, 1000f, 0f);
		sphere.transform.localScale = new Vector3(3f,3f,3f);
		sphere.renderer.material.color = Color.red;

	}

	//Catch single fingers
	void Update () {

		timeSinceLastHit += Time.deltaTime;

		if (Input.touchCount >= 1) {// && timeSinceLastHit > timeBetweenHits) {
			Debug.Log ("Screen touched " + Input.touchCount + " times.");
			for (int i = 0; i < Input.touchCount; i++) {
				if (Input.GetTouch(i).phase.Equals(TouchPhase.Began)) {
					Debug.Log ("Touch detected");
					RaycastHit hit = new RaycastHit();
					Ray ray = cam.ScreenPointToRay(Input.GetTouch(i).position);
					if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer)) {
						Debug.Log ("Touch consumed");
						timeSinceLastHit = 0f;
						sphere.transform.position = hit.point;
						hit.transform.gameObject.SendMessage("OnClick",null,SendMessageOptions.DontRequireReceiver);
						Debug.Log ("Camera touch: " + hit.transform.parent.gameObject.name + " has been touched at " + Time.time);
				    }
				}
			}
		}



	}
}

#endif


/*
 * 		// Code for OnMouseDown in the iPhone. Unquote to test.
		RaycastHit hit = new RaycastHit();
		
		for (int i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch(i).phase.Equals(TouchPhase.Began)) {
			// Construct a ray from the current touch coordinates
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
			if (Physics.Raycast(ray, out hit)) {
				hit.transform.gameObject.SendMessage("OnMouseDown");
		      }
		   }
	   }
	   */
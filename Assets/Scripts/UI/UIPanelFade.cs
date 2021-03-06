using UnityEngine;
using System.Collections;

public class UIPanelFade : MonoBehaviour {
	
	public GameObject ThisPanel = null;
	public GameObject TargetPanel = null;
	
	public float delayBefore = 0f; //delay before starting animations
	private float delayAfter = 0.25f; // delay between the transitions <=== BUGS OUT, WILL REMOVE THE RENDERER
	
	//UIPanel TargetPanel;
	//UIPanel ThisPanel;
	private float duration = 0.3f;
	
	private int panelCount = 0; //keeps track of the panels it's controlling, won't end transitions until all transitions are finished
		
	void OnClick() {
	
		if (ThisPanel != null) StartCoroutine(Transition(ThisPanel, delayBefore, true));
		if (TargetPanel != null) StartCoroutine(Transition(TargetPanel, delayBefore + delayAfter, false));
		
	}
	
	public void fadeOutPanel() {
		
		if (ThisPanel != null) StartCoroutine(Transition(ThisPanel, delayBefore, true));
		
	}
	
	public void fadeInPanel() {
		
		if (ThisPanel != null) StartCoroutine(Transition(ThisPanel, delayBefore, false));
		
	}
		
	//fadeOut - if the panel is fading out (ie. active right now, this will be true, false if not
	IEnumerator Transition(GameObject panel, float delay, bool fadeOut) {
		
		panelCount++;
		
		yield return new WaitForSeconds(delay); 
				
		panel.SetActive(true);
		//NGUITools.SetActive(panel,true);
		SetAlpha (panel, fadeOut ? 1 : 0);
		SetCollider(panel, false);
		
		float alpha = 0;
		
		while ((alpha += Time.deltaTime / duration) <= 1) {
			
			if (fadeOut) SetAlpha(panel,1-alpha);//(alpha*2-1));
			else SetAlpha(panel,alpha);//(alpha*2)-1);
			
			yield return null;
		}	
			
		panelCount--;
				
		while (panelCount != 0) {
			//do nothing
			yield return null;
		}		
		
		SetAlpha(panel, 1);
		SetCollider(panel, true);
		
		if (fadeOut) panel.SetActive (false);//NGUITools.SetActive(panel,false);
	}
	
	/*
	 * IEnumerator Transistion(GameObject g, float delay, float startA, float endA) {
		
		yield return new WaitForSeconds(delayBefore); 
		
		SetAlpha (TargetPanel, 0);
		SetCollider(TargetPanel, false);
		NGUITools.SetActive(TargetPanel,true);
		
		SetCollider(ThisPanel, false);
		
		float alpha = 0;
		
		while ((alpha += Time.deltaTime / duration) <= 1) {
			
			SetAlpha(ThisPanel,1-(alpha*2-1));
			SetAlpha(TargetPanel,(alpha*2)-1);
			
			yield return null;
		}	
				
		SetAlpha(TargetPanel,1);
		SetCollider(TargetPanel, true);
		
		SetAlpha (ThisPanel, 1);
		SetCollider(ThisPanel, true);
		NGUITools.SetActive(ThisPanel,false);
	}
	*/
	
	
	protected void SetCollider(GameObject gameObject, bool state) {
	     
	    if (gameObject.collider) {
	    	gameObject.collider.enabled = state;
	    }
     
    	foreach (Transform child in gameObject.transform) {
    		SetCollider(child.gameObject, state);
    	}
    }
	
	protected void SetAlpha(GameObject gameObject, float alpha) {
		
		UIWidget[] mWidgets = gameObject.GetComponentsInChildren<UIWidget>();
		
		foreach (UIWidget w in mWidgets)
        {
            Color c = w.color;
            c.a = alpha;
            w.color = c;
        }
	}	
}

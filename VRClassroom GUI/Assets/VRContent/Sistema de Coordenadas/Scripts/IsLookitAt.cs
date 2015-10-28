using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IsLookitAt : MonoBehaviour {
	public GameObject origin;
	public GameObject lastSelected;
	public Text output;
	bool targetWasHit; 

	// Use this for initialization
	void Start () {
		lastSelected = null;
		targetWasHit =  false; 
	}

	// Update is called once per frame
	void Update () {
		RaycastHit hit;
//		var cameraCenter = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, camera.nearClipPlane));
		//		if (Physics.Raycast(cameraCenter, this.transform.forward, out hit, 1000))
		var colliderHit = Physics.Raycast (origin.transform.position, origin.transform.forward, out hit);
		if (colliderHit && !targetWasHit) {
			targetWasHit =  true; 
			var obj = hit.transform.gameObject;
			if (lastSelected != obj) {
				deSelect( );					
				select( obj );
			}
		} else if (!colliderHit && targetWasHit) {
			targetWasHit =  false;
			deSelect( );	
		}
	}

	void deSelect( ) 
	{
		if (lastSelected != null) {
//			lastSelected.transform.Rotate (new Vector3 (0, 45, 0));
//			lastSelected.SetActive (true);
		}
		lastSelected = null;
		output.text = "Nothing";
	}

	void select( GameObject obj ) 
	{
		lastSelected = obj;
		output.text = obj.name != null? obj.name : "Nothing";
//		lastSelected.transform.Rotate (new Vector3 (0, -45, 0));
//		lastSelected.SetActive (false);
	}

}

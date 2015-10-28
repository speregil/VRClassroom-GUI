using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IsLookingAt : MonoBehaviour {
	public GameObject oculus;
	public GameObject normalCam;
	// Assumming one of the previous ones is not null, the following one points to it
	GameObject origin;
	public Text output;
	public GameObject output2; // Assuming it has a Text mesh
	GameObject lastSelected;
	bool targetWasHit;
	float lastTime;

	// distraction event
	public bool isStudentDistracted = false;
	float initialTimeDistraction;
	bool onDistractionEvent = false;
	public float deltaDistraction = 5.0f;

	public delegate void DistractionAction();
	public static event DistractionAction OnDistraction;


	// Use this for initialization
	void Start () {
		lastSelected = null;
		targetWasHit =  false; 
		lastTime = Time.time;
		if (oculus != null && oculus.activeSelf && oculus.activeInHierarchy) {
			origin = oculus;
			showText( "Oculus!" );
		}
		else if (normalCam != null && normalCam.activeSelf && normalCam.activeInHierarchy) {
			origin = normalCam;
			showText( "MainCamera!" );
		}
	}

	// Update is called once per frame
	void Update () {
		RaycastHit hit;
//		var cameraCenter = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, camera.nearClipPlane));
		//		if (Physics.Raycast(cameraCenter, this.transform.forward, out hit, 1000))
		var colliderHit = Physics.Raycast (origin.transform.position, origin.transform.forward, out hit, Mathf.Infinity);
//		Debug.DrawRay(origin.transform.position, origin.transform.forward, Color.green);
		if (colliderHit && Time.time - lastTime > 1.0) {
			targetWasHit =  true;
			lastTime = Time.time;
			var obj = hit.transform.gameObject;
			if (lastSelected != obj) {
				deSelect( );					
				select( obj );
			}
		} else if (!colliderHit && targetWasHit && Time.time - lastTime > 1.0) {
			targetWasHit =  false;
			lastTime = Time.time;
			deSelect( );	
			checkDistraction (null);
		}
		if (isStudentDistracted && onDistractionEvent && Time.time > initialTimeDistraction + deltaDistraction) {
			onDistractionEvent = false;
			if( OnDistraction != null ) {
				Debug.Log ("Distraction Triggered");
				OnDistraction();
			}
		}
	}

	void deSelect( ) 
	{
		if (lastSelected != null) {
//			lastSelected.transform.Rotate (new Vector3 (0, 92, 0));
//			lastSelected.SetActive (true);
			StudentScript script = (StudentScript)lastSelected.GetComponent (typeof(StudentScript));
			if( script != null )
				script.TheStudentIsNotLooking (origin.transform.position);
		}
		lastSelected = null;
		showText( "Nothing" );
	}

	void select( GameObject obj ) 
	{
		lastSelected = obj;
		showText ( obj.name != null? obj.name : "Nothing" );
//		lastSelected.transform.Rotate (new Vector3 (0, -92, 0));
		StudentScript script = (StudentScript)lastSelected.GetComponent (typeof(StudentScript));
		if( script != null )
			script.TheStudentIsLooking (origin.transform.position);
//		lastSelected.SetActive (false);
		checkDistraction (obj);
	}

	void checkDistraction( GameObject obj ) {
		if( obj == null || obj.name == null || !(obj.name.Equals("Teacher") || obj.name.Equals("Projector_Screen")) ) {
			isStudentDistracted = true;
			initialTimeDistraction = Time.time;
			onDistractionEvent = true;			
		}
		else {
			isStudentDistracted = false;
			onDistractionEvent = false;
		}
	}

	void showText( string t )
	{
		if( output != null )
			output.text = t;
		if (output2 != null)
			output2.GetComponent<TextMesh> ().text = t;
	}
	
}

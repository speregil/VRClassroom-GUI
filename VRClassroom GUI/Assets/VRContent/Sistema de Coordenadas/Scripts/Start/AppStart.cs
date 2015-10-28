using UnityEngine;
using System.Collections;

public class AppStart : MonoBehaviour {

	// Use this for initialization


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TestButton(GameObject g) {
		//Debug.Log ("Espa "+g.name);

		if(g.name=="ButtonEn"){
			GameObject.Find("PlaneO").GetComponent<lenguage>().l=0;
		}else{
			GameObject.Find("PlaneO").GetComponent<lenguage>().l=1;
		}
		
		Application.LoadLevel("Classroom");
		Object.DontDestroyOnLoad(GameObject.Find("PlaneO"));
	}
}

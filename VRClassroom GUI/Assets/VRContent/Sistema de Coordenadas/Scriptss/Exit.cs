using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {
	public Animation animation;
	public GameObject ObjectAnim;
	private bool play;
	private bool exit;

	// Use this for initialization
	void Start () {
		play=true;
	}
	
	// Update is called once per frame
	void Update () {
		int att=GameObject.Find("Teacher").GetComponent<speak>().att;
		int pause=GameObject.Find("Teacher").GetComponent<speak>().pause;
		bool f=GameObject.Find("Teacher").GetComponent<speak>().f;

		if(att>4&&pause==1&&play==true){
			ObjectAnim.GetComponent<Animation>().Play("exit");
			play=false;
		}

		if(f&&pause==1&&play==true){
			ObjectAnim.GetComponent<Animation>().Play("exit");
			play=false;

		}

		if(play==false&&!ObjectAnim.GetComponent<Animation>().isPlaying){
			GameObject.DestroyImmediate(GameObject.Find("PlaneO"));
			Application.LoadLevel("Start");
		}

	}

	void exitAnimation(){
		ObjectAnim.GetComponent<Animation>().Play("exit");
	}
}

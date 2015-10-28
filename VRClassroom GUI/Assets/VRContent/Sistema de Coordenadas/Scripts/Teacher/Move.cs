using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public Animator animator;

	private float timeToChange;
	private bool changeMove;

	// Use this for initialization
	void Start () {
		timeToChange = Random.Range(10f, 60f);
		changeMove = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (changeMove) {
			animator.SetBool("move", false);
			changeMove = false;
		}
		if (timeToChange <= 0f) {
			timeToChange = Random.Range(10f, 60f);
			animator.SetBool("move", true);
			changeMove = true;
		}
		timeToChange -= Time.deltaTime;
	}
}

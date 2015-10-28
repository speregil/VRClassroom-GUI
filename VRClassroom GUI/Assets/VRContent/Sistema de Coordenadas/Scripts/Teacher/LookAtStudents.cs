using UnityEngine;
using System.Collections;

public class LookAtStudents : MonoBehaviour {
	public GameObject Student;
	public GameObject[] Students;
	public GameObject Head;
	public GameObject Following;

	public bool pause;

	public GameObject presentation;

	private float timeLeft;
	private float timeRotationLeft;
	private int actualStudent;
	private Transform startTransform;
	private Transform finalTransform;

	// Use this for initialization
	void Start () {
		timeRotationLeft = 0f;
		timeLeft = 0f;
		actualStudent = 0;
		startTransform = Student.transform;
		finalTransform = Student.transform;
		pause = false;
	}
	
	// Update is called once per frame
	void Update () {
		 
	}

	void LateUpdate()
	{
		if (pause) {
			Head.transform.LookAt(Student.transform);
			return;
		}
		if(presentation.GetComponent<Presentation>().timeLeft <= 0 && this.GetComponent<speak>().pause == 1){
			Head.transform.LookAt(Student.transform);
			return;
		}
		Debug.Log(presentation.GetComponent<Presentation>().timeLeft + " " + this.GetComponent<speak>().pause);
		if (timeLeft <= 0f) {
			actualStudent = Random.Range (0, Students.Length + Students.Length / 3 + 1);
			timeLeft = Random.Range (1f, 10f);
			timeRotationLeft = 1f;
			startTransform = finalTransform;
			if(actualStudent < Students.Length){
				finalTransform = Students[actualStudent].transform;
			}
			else{
				finalTransform = Student.transform;
			}

		}

		timeLeft -= Time.deltaTime;
		timeRotationLeft -= Time.deltaTime;

		if (timeRotationLeft > 0) {
			Following.transform.position = Vector3.Lerp(startTransform.position, finalTransform.position, 1 - timeRotationLeft);
		}
		else{
			Following.transform.position = finalTransform.position;
		}

		Head.transform.LookAt(Following.transform);
	}
}

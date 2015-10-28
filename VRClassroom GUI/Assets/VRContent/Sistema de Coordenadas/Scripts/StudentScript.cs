using UnityEngine;
using System.Collections;
using System;

public class StudentScript : MonoBehaviour {
	Transform head = null;
	float initTime;
	Vector3 theStudentPos;
	
	enum Behavior {PayingAttention, Stare, StareAngry, TellTeacher};
	Behavior curBehavior= Behavior.PayingAttention;
	// current step in the behavior... It is assumed that CurBehLastStep is the last step.
	const int CURBEH_LASTSTEP = 99;
	int curBehStep = 0;

	int interruptionTimes = 0;
	// variables for the staring behavior
	public float timeToStare = 1.5f;
	Quaternion initialDirection;
	Quaternion finalDirection;
	float delta = 0.0f;
	public float howFast = 5.0f;
	
	// Use this for initialization
	void Start () {
		head = transform.FindChild("Hips/Spine/Spine1/Spine2/Neck/Neck1/Head");
		initialDirection = head.rotation;
	}

	public void TheStudentIsLooking( Vector3 position) {
		if (curBehavior == Behavior.PayingAttention) {
			theStudentPos = position;
			curBehavior = pickOneBehavior();
		}
	}

	public void TheStudentIsNotLooking( Vector3 position) {
		if (curBehavior != Behavior.PayingAttention) {
			theStudentPos = position;
			curBehStep = CURBEH_LASTSTEP;
		}
	}

	Behavior pickOneBehavior() {
		Behavior randomB;
		switch (interruptionTimes) {
		case 0:
			randomB = Behavior.Stare; break;
		case 1:
			randomB = Behavior.StareAngry; break;
		case 2:
			randomB = Behavior.TellTeacher; break;
		default:
			Array values = Enum.GetValues (typeof(Behavior));
			System.Random random = new System.Random ();
			randomB = (Behavior)values.GetValue (random.Next (values.Length));
			break;
		}
		interruptionTimes++;
		return randomB;
	}

	public void StartStare() {
		// stare at the same height as the original avatar... Without this it is too high...
		Vector3 p = new Vector3(theStudentPos.x,transform.position.y,theStudentPos.z);
		initTime = Time.time;
		finalDirection = Quaternion.LookRotation( (p - transform.position), transform.up );
		delta = 0.0f;
	}
	
	public void ResetRotation() {
		curBehStep = CURBEH_LASTSTEP;
		initTime = Time.time;
		delta = 0.0f;
	}	

	// Update is called once per frame
	void Update () {
		switch (curBehavior) {
		case Behavior.Stare:
		case Behavior.TellTeacher:
//			Debug.Log ("Current State: " + curBehavior);
			switch(curBehStep) {
			case 0: initTime = Time.time; curBehStep = 1; break;
			case 1: if (Time.time - initTime > timeToStare) {
					curBehStep = 2;
				}
				break;
			case 2: StartStare(); curBehStep = 3; break;
			case 3: delta = (Time.time - initTime) / howFast;
				if( delta > 1.0f ) {
					delta = 1.0f;
					curBehStep = 4;
				}
				head.rotation = Quaternion.Slerp(initialDirection,finalDirection, delta );
				break;
			case CURBEH_LASTSTEP: ResetRotation(); curBehStep = 100; break;
			case 100: delta = (Time.time - initTime) / howFast;
				if( delta > 1.0f ) {
					delta = 1.0f;
					curBehStep = 0;
					curBehavior = Behavior.PayingAttention;
				}
				head.rotation = Quaternion.Slerp(finalDirection,initialDirection, delta );
				break;
			}
			break;
		case Behavior.StareAngry:
//			Debug.Log ("Current State: " + curBehavior);
			switch(curBehStep) {
			case 0: curBehavior = Behavior.Stare; curBehStep = 2; break;
			}
			break;
		}
/*
		if (toStare) {
			delta = (Time.time - initTime) / howFast;
			if( delta > 1.0f ) {
				delta = 1.0f;
				toStare = false;
			}
			head.rotation = Quaternion.Slerp(initialDirection,finalDirection, delta );
		}
		if(toInitialState) {
			delta = (Time.time - initTime) / howFast;
			if( delta > 1.0f ) {
				delta = 1.0f;
				toInitialState = false;
			}
			head.rotation = Quaternion.Slerp(finalDirection,initialDirection, delta );
		}
		*/
	}
}

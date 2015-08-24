using UnityEngine;
using System.Collections;

public class AddToMenu : MonoBehaviour {

	public  GameObject MenuElement;

	public void AddElement(){
		GameObject instantElement = (GameObject)Instantiate (MenuElement, Vector3.zero, Quaternion.identity);
		instantElement.transform.parent = gameObject.transform;
	}
}
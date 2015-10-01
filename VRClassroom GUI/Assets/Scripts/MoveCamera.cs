using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

    public float VelHorizontal;
    public float VelVertical;
	
	// Update is called once per frame
	void Update () {
        float inputHorizontal = Input.GetAxis("Mouse X") * VelHorizontal * Time.deltaTime;
        float inputVertical = Input.GetAxis("Mouse Y") * VelVertical * Time.deltaTime;

        this.gameObject.transform.Rotate(new Vector3(inputVertical*-1, inputHorizontal, 0));
	}
}

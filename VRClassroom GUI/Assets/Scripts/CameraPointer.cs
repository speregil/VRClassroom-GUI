using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraPointer : MonoBehaviour {

    public  Camera       PointerCamera;
    public  GameObject   pointer;
    private Image        imgPointer;
    private GameObject   seleccionActual;

	// Use this for initialization
	void Start () {
        imgPointer = pointer.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        Ray ray = PointerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            imgPointer.color = Color.white;
            seleccionActual = hit.collider.gameObject;
        }
        else
        {
            imgPointer.color = Color.black;
            seleccionActual = null;
        }

        if (Input.GetKeyDown("space") && seleccionActual != null)
        {
            Button btn = seleccionActual.GetComponent<Button>();
            if(btn != null)
            {
                btn.onClick.Invoke();
            }
        }
    }
}

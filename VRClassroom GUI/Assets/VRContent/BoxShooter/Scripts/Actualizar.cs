using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Actualizar : MonoBehaviour {

    public Text txt;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CambiarTexto(string nText)
    {
        txt.text = nText;
    }
}

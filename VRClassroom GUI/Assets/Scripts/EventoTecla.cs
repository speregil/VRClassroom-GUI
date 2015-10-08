using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventoTecla : MonoBehaviour {

    private  Text        Input;
    private Text        MiTexto;

	// Use this for initialization
	void Start () {
        MiTexto = GetComponentInChildren<Text>();
	}
	
	public void EnClick()
    {
        GameObject input = GameObject.Find("InputText");
        Input = input.GetComponent<Text>();
        string textoActual = Input.text;

        switch (MiTexto.text)
        {
            case "____":
                Input.text = textoActual + " ";
                break;

            case "<":
                Input.text = textoActual.Remove(textoActual.Length - 1);
                break;

            default:
                Input.text = textoActual + MiTexto.text;
                break;
        }                      
    }
}

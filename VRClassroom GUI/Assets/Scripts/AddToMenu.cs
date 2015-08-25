using UnityEngine;
using System.Collections.Generic;

public class AddToMenu : MonoBehaviour {

	public  GameObject			ElementoMenu;
	public	GameObject			ScrollPanel;

	private	float				DistanciaElementos;
	private	float				CambioEscala; 
	private	Vector3				EscalaInicial;
	private	Vector3				PosInicial;
	private	Vector3				PosSiguiente;
	private	float				SaltoElemento;

	void Start(){
		PosInicial = ScrollPanel.transform.position;
		RectTransform rt = ElementoMenu.GetComponent<RectTransform> ();
		float width = rt.rect.width;
		PosInicial.x = PosInicial.x + (width * -3);
		SaltoElemento = width + DistanciaElementos;
		EscalaInicial = new Vector3 (1.0f, 1.0f, 1.0f);

	}

	public void AumentarElementos(){
		PosInicial.x += SaltoElemento;
		EscalaInicial.x /= CambioEscala;
		EscalaInicial.y /= CambioEscala;
	}

	public void AddElement(){
		GameObject instantElement = (GameObject)Instantiate (ElementoMenu, PosInicial, Quaternion.identity);
		instantElement.transform.SetParent(ScrollPanel.transform);
		instantElement.transform.localScale = EscalaInicial;
		AumentarElementos();
		SendMessage ("Agregar", instantElement);
	}

	public void ObtenerDistancia(float distancia){
		DistanciaElementos = distancia;
	}

	public void ObtenerEscala(float escala){
		CambioEscala = escala;
	}
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ManagerMenu : MonoBehaviour {
	
	public  	GameObject					ElementoMenu;
	public		GameObject					ScrollPanel;
	public		float						DistanciaElementos;
	public		float						CambioEscala; 

	private 	LinkedList<GameObject>		ListaElementos;
	private 	LinkedListNode<GameObject>	ElementoActual;
	private		Vector3						EscalaInicial;
	private		Vector3						PosInicial;
	private		float						SaltoElemento;


	void Start () {
		ListaElementos = new LinkedList<GameObject> ();
		ElementoActual = null;

		PosInicial = ScrollPanel.transform.position;
		RectTransform rt = ElementoMenu.GetComponent<RectTransform> ();
		float width = rt.rect.width;
		PosInicial.x = PosInicial.x + (width * -3);
		SaltoElemento = width + DistanciaElementos;
		EscalaInicial = new Vector3 (1.0f, 1.0f, 1.0f);
	}

	public void Agregar(){
		ListaElementos.AddLast(ElementoMenu);


		GameObject instantElement = (GameObject)Instantiate (ElementoMenu, PosInicial, Quaternion.identity);
		instantElement.transform.SetParent(ScrollPanel.transform);
		instantElement.transform.localScale = EscalaInicial;

		NuevoElementoActual();
	}

	public void NuevoElementoActual(){
		Image imgElemento = ElementoActual.Value.GetComponent<Image> ();
		imgElemento.color = Color.red;
		if(ElementoActual != null)
			MoverIzquierda (ElementoActual);
		ElementoActual = ListaElementos.Last;
	}

	public void MoverIzquierda(LinkedListNode<GameObject> elemento){
		GameObject valor = elemento.Value;

		if (elemento == ElementoActual) {
			LinkedListNode<GameObject> anterior = elemento.Previous;
			if (anterior != null)
				anterior.Value.SetActive (false);
		}

		LinkedListNode<GameObject> siguiente = elemento.Next;
		if (siguiente != null) {
			Vector3 posActual = valor.transform.localPosition;
			posActual.x += SaltoElemento*-1;

			MoverIzquierda(siguiente);
		}


	}
}
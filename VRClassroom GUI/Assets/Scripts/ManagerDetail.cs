using UnityEngine;
using System.Collections.Generic;

public class ManagerDetail : MonoBehaviour {

	public  	GameObject						PrefElemento;
	public  	GameObject						PrefTema;
	public  	GameObject						FlechaAdelante;
	public  	GameObject						FlechaAtras;
	public		GameObject						PanelItems;

	private 	LinkedList<GameObject>			ListaElementos;
	private		Vector3[]						PosicionesPanel;
	private		int								IndicePos;
	private		LinkedListNode<GameObject>[]	ColumnaActual;

	// Use this for initialization
	void Start () {
		ListaElementos = new LinkedList<GameObject> ();
		PosicionesPanel = new Vector3[6];
		float altura = PrefElemento.GetComponent<RectTransform> ().rect.height;
		float ancho = PrefElemento.GetComponent<RectTransform> ().rect.width;
		Vector3	initial = PanelItems.transform.localPosition;
		initial.x = initial.x + 50;
		initial.y = initial.y + 100;
		PosicionesPanel[0] = initial;
		initial.y = initial.y - altura - 80;
		PosicionesPanel [1] = initial;
		initial.y = initial.y + altura + 80;
		initial.x = initial.x + ancho + 40;
		PosicionesPanel [2] = initial;
		initial.y = initial.y - altura - 80;
		PosicionesPanel [3] = initial;
		initial.y = initial.y + altura + 80;
		initial.x = initial.x + ancho + 40;
		PosicionesPanel [4] = initial;
		initial.y = initial.y - altura - 80;
		PosicionesPanel [5] = initial;
		IndicePos = 0;

		ColumnaActual = new LinkedListNode<GameObject>[2];
	}
	
	public void AgregarTema(){
		GameObject nuevoTema = (GameObject)Instantiate (PrefTema);
		nuevoTema.transform.SetParent(PanelItems.transform, false);

		RectTransform rt = nuevoTema.GetComponent<RectTransform> ();
		rt.sizeDelta = new Vector2 (120, 120);

		ListaElementos.AddLast (nuevoTema);

		if (IndicePos == 0) {
			ColumnaActual [0] = ListaElementos.Last;
			nuevoTema.transform.localPosition = PosicionesPanel[IndicePos];
			IndicePos++;
		} else if (IndicePos == 1) {
			ColumnaActual [1] = ListaElementos.Last;
			nuevoTema.transform.localPosition = PosicionesPanel[IndicePos];
			IndicePos++;
		} else if (IndicePos > 5) {
			nuevoTema.SetActive (false);
		}else{
			nuevoTema.transform.localPosition = PosicionesPanel[IndicePos];
			IndicePos++;
		}


	}

	public void AgregarElemento(){
		GameObject nuevoTema = (GameObject)Instantiate (PrefElemento);
		nuevoTema.transform.SetParent(PanelItems.transform, false);
		
		RectTransform rt = nuevoTema.GetComponent<RectTransform> ();
		rt.sizeDelta = new Vector2 (100, 100);

		ListaElementos.AddLast (nuevoTema);

		if (IndicePos == 0) {
			ColumnaActual [0] = ListaElementos.Last;
			nuevoTema.transform.localPosition = PosicionesPanel[IndicePos];
			IndicePos++;
		} else if (IndicePos == 1) {
			ColumnaActual [1] = ListaElementos.Last;
			nuevoTema.transform.localPosition = PosicionesPanel[IndicePos];
			IndicePos++;
		}
		else if (IndicePos > 5)
			nuevoTema.SetActive (false);
		else{
			nuevoTema.transform.localPosition = PosicionesPanel[IndicePos];
			IndicePos++;
		}


	}

	public void Avanzar(){

		ColumnaActual [0].Value.SetActive (false);
		ColumnaActual [1].Value.SetActive (false);

		MoverIzquierda (ColumnaActual [1].Next, 0);

	}

	public void MoverIzquierda(LinkedListNode<GameObject> elemento, int pos){
		LinkedListNode<GameObject> siguiente = elemento.Next;
		elemento.Value.SetActive (true);
		elemento.Value.transform.localPosition = PosicionesPanel [pos];

		if (pos == 0)
			ColumnaActual [0] = elemento;
		else if (pos == 1)
			ColumnaActual [1] = elemento;

		if (siguiente != null)
			MoverIzquierda (siguiente, pos + 1);
	}

	public void DibujarLista(){

	}
}
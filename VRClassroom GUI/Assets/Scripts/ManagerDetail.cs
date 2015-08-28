using UnityEngine;
using System.Collections.Generic;

public class ManagerDetail : MonoBehaviour {

	public  	GameObject					PrefElemento;
	public  	GameObject					PrefTema;
	public  	GameObject					FlechaAdelante;
	public  	GameObject					FlechaAtras;
	public		GameObject					PanelItems;

	private 	LinkedList<GameObject>		ListaElementos;
	private		Vector3[]					PosicionesPanel;

	// Use this for initialization
	void Start () {
		ListaElementos = new LinkedList<GameObject> ();
		PosicionesPanel = new Vector3[6];
		Vector3	initial = PanelItems.transform.localPosition;
		initial.x = initial.x + 50;
		initial.y = initial.y + 120;
		PosicionesPanel[0] = initial;
	}
	
	public void AgregarTema(){
		GameObject nuevoTema = (GameObject)Instantiate (PrefTema);
		nuevoTema.transform.SetParent(PanelItems.transform);
		nuevoTema.transform.localPosition = PosicionesPanel[0];

		RectTransform rt = nuevoTema.GetComponent<RectTransform> ();
		rt.sizeDelta = new Vector2 (120, 120);
		ListaElementos.AddLast (nuevoTema);
	}

	public void DibujarLista(){

	}
}

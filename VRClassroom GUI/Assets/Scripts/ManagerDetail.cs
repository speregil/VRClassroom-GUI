using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using ProgressBar;

public class ManagerDetail : MonoBehaviour {

	public  	GameObject						PrefElemento;
	public  	GameObject						BotonSubir;
    public      GameObject                      BarraPorcentaje;
	public		GameObject						PanelItems;
	public		GameObject						MenuDetalle;
	public		float							Desplazamiento;
	private 	LinkedList<GameObject>			ListaElementos;
	private		Vector3[]						PosicionesPanel;
	private		int								IndicePos;
	private		LinkedListNode<GameObject>		PrimerElemento;
	private		LinkedListNode<GameObject>		UltimoElemento;
	private		bool							Desplazado;

	// Use this for initialization
	void Start () {
		ListaElementos = new LinkedList<GameObject> ();
		PosicionesPanel = new Vector3[6];
		float altura = PrefElemento.GetComponent<RectTransform> ().rect.height;
		float ancho = PrefElemento.GetComponent<RectTransform> ().rect.width;
		Vector3	initial = PanelItems.transform.localPosition;
		Desplazado = false;
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

	}
	
	public void AgregarTema(GameObject nuevoTema){

		nuevoTema.transform.SetParent(PanelItems.transform, false);
		nuevoTema.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		RectTransform rt = nuevoTema.GetComponent<RectTransform> ();
		rt.sizeDelta = new Vector2 (120, 120);

		ListaElementos.AddLast (nuevoTema);

		if (IndicePos == 0) {
			PrimerElemento = ListaElementos.Last;
			nuevoTema.transform.localPosition = PosicionesPanel[IndicePos];
			IndicePos++;
		} else if (IndicePos > 5) {
			nuevoTema.SetActive (false);
			//FlechaAdelante.SetActive(true);
		}else {
			UltimoElemento = ListaElementos.Last;
			nuevoTema.transform.localPosition = PosicionesPanel[IndicePos];
			IndicePos++;
		}
	}

	public void AgregarElemento(GameObject nuevoTema){
		nuevoTema.transform.SetParent(PanelItems.transform, false);
		
		RectTransform rt = nuevoTema.GetComponent<RectTransform> ();
		rt.sizeDelta = new Vector2 (100, 100);

		ListaElementos.AddLast (nuevoTema);

		if (IndicePos == 0) {
			PrimerElemento = ListaElementos.Last;
			nuevoTema.transform.localPosition = PosicionesPanel[IndicePos];
			IndicePos++;
		} else if (IndicePos > 5) {
			nuevoTema.SetActive (false);
			//FlechaAdelante.SetActive(true);
		}else {
			UltimoElemento = ListaElementos.Last;
			nuevoTema.transform.localPosition = PosicionesPanel[IndicePos];
			IndicePos++;
		}
	}

	public void Avanzar(){

		PrimerElemento.Value.SetActive (false);
		//FlechaAtras.SetActive (true); 

		MoverIzquierda (PrimerElemento.Next, 0);

	}

	public void MoverIzquierda(LinkedListNode<GameObject> elemento, int pos){
		LinkedListNode<GameObject> siguiente = elemento.Next;
		elemento.Value.SetActive (true);
		elemento.Value.transform.localPosition = PosicionesPanel [pos];

		if (pos == 0)
			PrimerElemento = elemento;
		else
			UltimoElemento = elemento;

		if (pos < 5) {
			if (siguiente != null)
				MoverIzquierda (siguiente, pos + 1);
		} else if (siguiente == null) {
			//FlechaAdelante.SetActive (false);
		}
	}

	public void Retroceder(){
		UltimoElemento.Value.SetActive (false);
		//UltimoElemento.Previous.Value.SetActive (false);
		//FlechaAdelante.SetActive (true);
		
		MoverDerecha (UltimoElemento.Previous, 5);
	}

	public void MoverDerecha(LinkedListNode<GameObject> elemento, int pos){
		LinkedListNode<GameObject> anterior = elemento.Previous;
		elemento.Value.SetActive (true);
		elemento.Value.transform.localPosition = PosicionesPanel [pos];
		
		if (pos == 5)
			UltimoElemento = elemento;
		else
			PrimerElemento = elemento;
		
		if (pos > 0) {
			if (anterior != null)
				MoverDerecha (anterior, pos - 1);
		} else if (anterior == null) {
			//FlechaAtras.SetActive (false);
		}
	}

	public void LimpiarDetalle(){
		foreach (GameObject item in ListaElementos) {
			item.SetActive(false);
		}
		ListaElementos = new LinkedList<GameObject> ();
		PrimerElemento = null;
		UltimoElemento = null;
		IndicePos = 0;
	}

	public void DesplazarMenu(int posicion){
		if (!Desplazado) {
			Vector3 nuevaPos = MenuDetalle.transform.position;
			if (posicion == 0) {
				nuevaPos.y += Desplazamiento;
			} else {
				nuevaPos.y -= Desplazamiento;
			}
			iTween.MoveTo (MenuDetalle, nuevaPos, 1);
		}
	}

	public void BajarNivel(){
		Button btnSubir = BotonSubir.GetComponent<Button>();
		btnSubir.interactable = true;
		DesplazarMenu (0);
		Desplazado = true;
	}

	public void SubirNivel(){
        GameObject menu = GameObject.Find("MainCanvas");
        ManagerMenu mm = menu.GetComponent<ManagerMenu>();

        if (!mm.EnAnimacion)
        {
            if (Desplazado)
            {
                Desplazado = false;
                DesplazarMenu(1);
                if (mm.SubirNivel())
                {
                    Button btnSubir = BotonSubir.GetComponent<Button>();
                    btnSubir.interactable = false;
                }
            }
            else
            {
                if (mm.RecuperarNivel())
                {
                    Button btnSubir = BotonSubir.GetComponent<Button>();
                    btnSubir.interactable = false;
                }
            }
        }	
	}
}
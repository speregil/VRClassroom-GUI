using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/**
 * Maneja todas las operaciones del menu principal horizontal
 * */
public class ManagerMenu : MonoBehaviour {

	//---------------------------------------------------------------------------------
	// Atributos
	//---------------------------------------------------------------------------------

	public  	GameObject					ElementoMenu;  			//Prefab de un elemento del menu
	public		GameObject					ScrollPanel;			//Panel que contiene los elementos
	public		GameObject					Flecha;					//Icono que muestra que hay mas objetos escondidos hacia la derecha;
	public		float						DistanciaElementos;		//Distancia entre los elementos del menu
	public		float						CambioEscala; 			//Cambio en la escala de los distintos elementos a lo largo de la horizontal

	private 	LinkedList<GameObject>		ListaElementos;			//Lista logica que contiene y maneja los elementos
	private 	LinkedListNode<GameObject>	ElementoActual;			//Elemento que el usuario ve actualmente
	private		Vector3						EscalaInicial;    		//Determina la escala en que se dibujara un nuevo elemento que se agregue
	private		Vector3						PosInicial;  			//Determina la posicion donde se dibujara un nuevo elemento que se agregue
	private		float						SaltoElemento;			//Valor real que separa dos elementos (ancho del boton + DistanciaElementos)

	//----------------------------------------------------------------------------------
	// Inicializacion
	//----------------------------------------------------------------------------------

	void Start () {

		// Inicializa los atributos con sus valores por defecto y de primer uso

		ListaElementos = new LinkedList<GameObject> ();
		ElementoActual = null;

		PosInicial = ScrollPanel.transform.localPosition;
		RectTransform rt = ElementoMenu.GetComponent<RectTransform> ();
		float width = rt.rect.width;
		PosInicial.x = PosInicial.x + (width * -4);
		SaltoElemento = width + DistanciaElementos;
		EscalaInicial = new Vector3 (1.0f, 1.0f, 1.0f);
	}

	//-----------------------------------------------------------------------------------
	// Metodos
	//-----------------------------------------------------------------------------------

	/**
	 * Agrega un nuevo elemento al menu
	 * Actualmente lo coloca en la ultima posicion de la lista
	 * */
	public void Agregar(GameObject instantElement){

		instantElement.transform.SetParent(ScrollPanel.transform, false);
		instantElement.transform.localPosition = PosInicial;
		instantElement.transform.localScale = EscalaInicial;
		ListaElementos.AddLast(instantElement);

		if(ListaElementos.Count == 1)
			ElementoActual = ListaElementos.First;

		NuevoElemento();
	}

	/**
	 * Hace las modificaciones necesarias en el menu para que se visualice un nuevo 
	 * elemento agregado
	 * */
	public void NuevoElemento(){
		Image imgComp = ElementoActual.Value.GetComponent<Image> ();
		imgComp.color = Color.red;
		PosInicial.x += SaltoElemento;
		EscalaInicial.x /= CambioEscala;
		EscalaInicial.y /= CambioEscala;
	}

	/**
	 * Mueve la seleccion actual del menu de a un elemento en direccion al final de la lista
	 * */
	public void Avanzar(){
		GameObject elemActual = ElementoActual.Value;
		LinkedListNode<GameObject> siguiente = ElementoActual.Next;
		if (siguiente != null) {
			// Mueve hacia atras el elemento actual y lo des-selecciona
			Vector3 nuevaPos = elemActual.transform.localPosition;
			nuevaPos.x -= SaltoElemento;
			elemActual.transform.localPosition = nuevaPos;
			Vector3 nuevaEscala = elemActual.transform.localScale;
			nuevaEscala.x /= CambioEscala;
			nuevaEscala.y /= CambioEscala;
			elemActual.transform.localScale = nuevaEscala;

			Image imgComp = elemActual.GetComponent<Image> ();
			imgComp.color = Color.blue;

			// Selecciona el siguiente elemento
			imgComp = siguiente.Value.GetComponent<Image> ();
			imgComp.color = Color.red;
			ElementoActual = siguiente;

			//Si hay mas elementos a la derecha, activa la indicacion visual
			if(ElementoActual.Previous != null){
				if(ElementoActual.Previous.Previous != null)
					Flecha.SetActive(true);
			}

			// Mueve recursivamente todo el menu hacia la izquierda
			MoverIzquierda (siguiente);
		}
		else
			Debug.Log("Ultimo elemento");
	}

	/**
	 * Mueve todos los elementos hacia la izquierda de forma recursiva
	 * a partir del elemento pasado por paramentro
	 * */
	public void MoverIzquierda(LinkedListNode<GameObject> elemento){
		GameObject valor = elemento.Value;

		//Mueve el elemento
		Vector3 nuevaPos = valor.transform.localPosition;
		nuevaPos.x -= SaltoElemento;
		valor.transform.localPosition = nuevaPos;
		Vector3 nuevaEscala = valor.transform.localScale;
		nuevaEscala.x *= CambioEscala;
		nuevaEscala.y *= CambioEscala;
		valor.transform.localScale = nuevaEscala;

		//Llamado recursivo
		LinkedListNode<GameObject> siguiente = elemento.Next;
		if (siguiente != null) {
			MoverIzquierda (siguiente);
		} 
		else {
			//Si llego al final, ajusta el menu para poder recibir un elemento nuevo
			PosInicial.x -= SaltoElemento;
			EscalaInicial.x *= CambioEscala;
			EscalaInicial.y *= CambioEscala;
		}
	}

	/**
	 * Mueve la seleccion actual del menu de a un elemento en direccion al inicio de la lista
	 * */
	public void Retroceder(){
		GameObject elemActual = ElementoActual.Value;
		LinkedListNode<GameObject> anterior = ElementoActual.Previous;

		if (anterior != null) {
			// Mueve hacia adelante el elemento anterior y lo selecciona
			Vector3 nuevaPos = anterior.Value.transform.localPosition;
			nuevaPos.x += SaltoElemento;
			anterior.Value.transform.localPosition = nuevaPos;
			Vector3 nuevaEscala = anterior.Value.transform.localScale;
			nuevaEscala.x *= CambioEscala;
			nuevaEscala.y *= CambioEscala;
			anterior.Value.transform.localScale = nuevaEscala;

			Image imgComp = anterior.Value.GetComponent<Image> ();
			imgComp.color = Color.red;

			//Des-selecciona el elemento actual
			imgComp = elemActual.GetComponent<Image> ();
			imgComp.color = Color.blue;

			//Mueve recursivamente todo el menu hacia la derecha
			MoverDerecha(ElementoActual);

			ElementoActual = anterior;

			//Si no hay elementos escondidos a la derecha, apaga la señar en la interfaz
			if(ElementoActual.Previous != null){
				if(ElementoActual.Previous.Previous == null)
					Flecha.SetActive(false);
			}
		}
		else
			Debug.Log("Primer elemento");
	}

	/**
	 * Mueve todos los elementos hacia la derecha de forma recursiva
	 * a partir del elemento pasado por paramentro
	 * */
	public void MoverDerecha(LinkedListNode<GameObject> elemento){
		GameObject valor = elemento.Value;

		//Mueve el elemento
		Vector3 nuevaPos = valor.transform.localPosition;
		nuevaPos.x += SaltoElemento;
		valor.transform.localPosition = nuevaPos;
		Vector3 nuevaEscala = valor.transform.localScale;
		nuevaEscala.x /= CambioEscala;
		nuevaEscala.y /= CambioEscala;
		valor.transform.localScale = nuevaEscala;

		//Llamado recursivo
		LinkedListNode<GameObject> siguiente = elemento.Next;
		if (siguiente != null) {
			MoverDerecha (siguiente);
		} 
		else {
			//Si llego al final, ajusta el menu para poder recibir un elemento nuevo
			PosInicial.x += SaltoElemento;
			EscalaInicial.x /= CambioEscala;
			EscalaInicial.y /= CambioEscala;
		}
	}	
}
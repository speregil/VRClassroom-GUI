using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

/**
 * Maneja todas las operaciones del menu principal horizontal
 * */
public class ManagerMenu : MonoBehaviour {

	//---------------------------------------------------------------------------------
	// Atributos
	//---------------------------------------------------------------------------------
	
	public		GameObject						ScrollPanel;			//Panel que contiene los elementos
	public		GameObject						Flecha;					//Icono que muestra que hay mas objetos escondidos hacia la derecha;
	public		float							DistanciaElementos;		//Distancia entre los elementos del menu
	public		float							CambioEscala; 			//Cambio en la escala de los distintos elementos a lo largo de la horizontal
	public		float							AnchoElementos;			//Ancho del prefab de los elemetos
	public		float							velMovimiento;			//Velocidad a la que se desplazan los elementos al moverse
	public 		float							velRotacion;
	public 		float							primeraRotacion;		//Angulo de la primera rotacion que hacen los elementos al moverse hacia la izquierda
	public 		float							segundaRotacion;
	private		Stack<LinkedList<GameObject>>	PilaListas;				//Pila que maneja las distintas jerarquias del sistema de archivos
	private 	LinkedList<GameObject>			ListaElementos;			//Lista logica que contiene y maneja los elementos
	private 	LinkedListNode<GameObject>		ElementoActual;			//Elemento que el usuario ve actualmente
	private		Vector3							EscalaInicial;    		//Determina la escala en que se dibujara un nuevo elemento que se agregue
	private		Vector3							PosInicial;  			//Determina la posicion donde se dibujara un nuevo elemento que se agregue
	private		float							SaltoElemento;			//Valor real que separa dos elementos (ancho del boton + DistanciaElementos)

	//----------------------------------------------------------------------------------
	// Inicializacion
	//----------------------------------------------------------------------------------

	void Start () {

		// Inicializa los atributos con sus valores por defecto y de primer uso

		PilaListas = new Stack<LinkedList<GameObject>> ();
		ListaElementos = new LinkedList<GameObject> ();
		PilaListas.Push (ListaElementos);
		ElementoActual = null;
	}

	//-----------------------------------------------------------------------------------
	// Metodos
	//-----------------------------------------------------------------------------------

	public void Update(){

	}

	/**
	 * Agrega un nuevo elemento al menu
	 * Actualmente lo coloca en la ultima posicion de la lista
	 * */
	public void Agregar(GameObject instantElement){

		instantElement.transform.SetParent(ScrollPanel.transform, false);
		RectTransform rt = instantElement.GetComponent<RectTransform> ();
		rt.sizeDelta = new Vector2 (185, 185);
		instantElement.transform.localPosition = PosInicial;
		instantElement.transform.localScale = EscalaInicial;

		ListaElementos.AddLast(instantElement);

		if (ListaElementos.Count == 1) {
			ElementoActual = ListaElementos.First;
			Tema nuevoActual = ListaElementos.First.Value.GetComponent<Tema>();
			nuevoActual.EsActual = true;
		}

		NuevoElemento();
	}

	/**
	 * Hace las modificaciones necesarias en el menu para que se visualice un nuevo 
	 * elemento agregado
	 * */
	public void NuevoElemento(){
		SeleccionarItem (ElementoActual.Value, true);
		PosInicial.x += SaltoElemento;
		EscalaInicial.x /= CambioEscala;
		EscalaInicial.y /= CambioEscala;
	}
	
	/**
	 * Mueve la seleccion actual del menu de a un elemento en direccion al final de la lista
	 * */
	public void Avanzar(){
		GameObject elemActual = ElementoActual.Value;
		LinkedListNode<GameObject> anterior = ElementoActual.Previous;
		LinkedListNode<GameObject> siguiente = ElementoActual.Next;
		if (siguiente != null) {
			// Mueve hacia atras el elemento actual y lo des-selecciona

			SeleccionarItem(elemActual, false);

			// Mueve todo el menu hacia la izquierda
			GameObject[] objSiguientes = new GameObject[ListaElementos.Count];
			ListaElementos.CopyTo(objSiguientes, 0);
			foreach(GameObject actual in ListaElementos){
				StartCoroutine(MoverIzquierda(actual));
			}

			StartCoroutine(RotarElemento(elemActual, primeraRotacion));

			if(anterior !=null)
				StartCoroutine(RotarElemento(ElementoActual.Previous.Value, segundaRotacion));

			// Selecciona el siguiente elemento
			SeleccionarItem(siguiente.Value, true);
			ElementoActual = siguiente;

			PosInicial.x -= SaltoElemento;
			EscalaInicial.x *= CambioEscala;
			EscalaInicial.y *= CambioEscala;
			
		} else {
			Debug.Log ("Ultimo elemento");
		}
	}

	/**
	 * Corutina que mueve hacia la izquierda un espacio el elemento pasado por parametro
	 * */
	public IEnumerator MoverIzquierda(GameObject elemento){
		Vector3 nuevaPos = elemento.transform.position;
		nuevaPos.x -= SaltoElemento;

		while (Vector3.Distance(elemento.transform.position, nuevaPos) > 0.01f) {
			elemento.transform.position = Vector3.MoveTowards (elemento.transform.position, nuevaPos, velMovimiento * Time.deltaTime);
			yield return null;
		}
	}

	/**
	 * Mueve la seleccion actual del menu de a un elemento en direccion al inicio de la lista
	 * */
	public void Retroceder(){
		GameObject elemActual = ElementoActual.Value;
		LinkedListNode<GameObject> anterior = ElementoActual.Previous;
		if (anterior != null) {
			// Mueve hacia atras el elemento actual y lo des-selecciona
			
			SeleccionarItem(elemActual, false);
			
			// Selecciona el siguiente elemento
			SeleccionarItem(anterior.Value, true);
			ElementoActual = anterior;
			
			// Mueve todo el menu hacia la izquierda
			foreach(GameObject actual in ListaElementos){
				StartCoroutine(MoverDerecha(actual));
			}
			
			PosInicial.x += SaltoElemento;
			EscalaInicial.x /= CambioEscala;
			EscalaInicial.y /= CambioEscala;
			
		} else {
			Debug.Log ("Ultimo elemento");
		}
	}

	/**
	 * Corutina que mueve hacia la derecha un espacio el elemento pasado por parametro
	 * */
	public IEnumerator MoverDerecha(GameObject elemento){
		Vector3 nuevaPos = elemento.transform.position;
		nuevaPos.x += SaltoElemento;
		
		while (Vector3.Distance(elemento.transform.position, nuevaPos) > 0.01f) {
			elemento.transform.position = Vector3.MoveTowards (elemento.transform.position, nuevaPos, velMovimiento * Time.deltaTime);
			yield return null;
		}
	}

	public IEnumerator RotarElemento(GameObject elemento, float rotacion){
		Quaternion qTo = Quaternion.AngleAxis(rotacion, elemento.transform.up) * transform.rotation;
		while (Quaternion.Angle(elemento.transform.rotation, qTo) > 0.01f) {
			elemento.transform.rotation = Quaternion.RotateTowards (elemento.transform.rotation, qTo, velRotacion * Time.deltaTime);
			yield return null;
		}
	}

	public void BajarNivel(Tema temaPadre){
		List<GameObject> nuevoContenido = temaPadre.Contenido;
		LimpiarMenu ();
		foreach (GameObject item in nuevoContenido) {
			Tema actual = item.GetComponent<Tema>();
			if(actual !=null){
				item.SetActive(true);
				actual.EnDetalle = false;
				actual.PrimerClick = false;
				Agregar(item);
			}
		}
		PilaListas.Push (ListaElementos);
	}

	public void SubirNivel(){
		PilaListas.Pop ();
		LimpiarMenu ();
		ManagerDetail md = this.gameObject.GetComponent<ManagerDetail> ();
		md.LimpiarDetalle ();
		LinkedList<GameObject> nuevoContenido = PilaListas.Pop();
		foreach (GameObject item in nuevoContenido) {
			Tema actual = item.GetComponent<Tema>();
			if(actual !=null){
				item.SetActive(true);
				actual.EnDetalle = false;
				actual.PrimerClick = false;
				Agregar(item);
			}
		}
		PilaListas.Push (ListaElementos);
	}

	public void LimpiarMenu(){
		foreach (GameObject nodo in ListaElementos) {
			nodo.SetActive(false);
		}

		ListaElementos = new LinkedList<GameObject> ();
		SetParametrosIniciales ();
	}

	public void SeleccionarItem(GameObject item, bool seleccionar){
		if (seleccionar) {
			Image imgComp = item.GetComponent<Image> ();
			imgComp.color = Color.red;
			Tema mTema = item.GetComponent<Tema> ();
			mTema.EsActual = true;
		} else {
			Image imgComp = item.GetComponent<Image> ();
			imgComp.color = Color.blue;
			Tema mTema = item.GetComponent<Tema> ();
			mTema.EsActual = false;
		}
	}

	public void SetParametrosIniciales(){
		PosInicial = ScrollPanel.transform.localPosition;
		SaltoElemento = AnchoElementos + DistanciaElementos;
		EscalaInicial = new Vector3 (1.0f, 1.0f, 1.0f);
	}
}
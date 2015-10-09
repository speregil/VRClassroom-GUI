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
	public		GameObject						ItemView;				//Objeto padre del menu horizontal y vertical
	public		GameObject						ItemPanel;
	public		float							DistanciaElementos;		//Distancia entre los elementos del menu
	public		float							CambioEscala; 			//Cambio en la escala de los distintos elementos a lo largo de la horizontal
	public		float							AnchoElementos;			//Ancho del prefab de los elemetos
	public		float							velMovimiento;			//Velocidad a la que se desplazan los elementos al moverse
	public 		float							velRotacion;
	public 		float							primeraRotacion;		//Angulo de la primera rotacion que hacen los elementos al moverse hacia la izquierda
    public      float                           segundaRotacion;       //Angulo de la primera rotacion que hacen los elementos al moverse hacia la izquierda
    public 		float							Desplazamiento;
    public      bool                            EnAnimacion;
	private		Stack<LinkedList<GameObject>>	PilaListas;				//Pila que maneja las distintas jerarquias del sistema de archivos
	private 	LinkedList<GameObject>			ListaElementos;			//Lista logica que contiene y maneja los elementos
	private 	LinkedList<GameObject>			ListaDetalle;
	private 	LinkedListNode<GameObject>		ElementoActual;			//Elemento que el usuario ve actualmente
	private		LinkedListNode<GameObject>		DetalleActual;
	private		Vector3							EscalaInicial;    		//Determina la escala en que se dibujara un nuevo elemento que se agregue
	private		Vector3							PosInicial;  			//Determina la posicion donde se dibujara un nuevo elemento que se agregue
	private		Vector3							PosDetalle;
	private		float							SaltoElemento;			//Valor real que separa dos elementos (ancho del boton + DistanciaElementos)
	private		bool							Desplazado;
	//----------------------------------------------------------------------------------
	// Inicializacion
	//----------------------------------------------------------------------------------

	void Start () {

		// Inicializa los atributos con sus valores por defecto y de primer uso

		PilaListas = new Stack<LinkedList<GameObject>> ();
		ListaElementos = new LinkedList<GameObject> ();
		ListaDetalle = new LinkedList<GameObject> ();
		//PilaListas.Push (ListaElementos);
		ElementoActual = null;
		Desplazado = false;
        EnAnimacion = false;
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
        instantElement.GetComponent<Collider>().enabled = true;
		//instantElement.transform.localScale = EscalaInicial;

		ListaElementos.AddLast(instantElement);
      
		if (ListaElementos.Count == 1) {
			ElementoActual = ListaElementos.First;
			Tema mtActual = ListaElementos.First.Value.GetComponent<Tema>();
			Elemento meActual = ListaElementos.First.Value.GetComponent<Elemento>();
			if(mtActual != null)
				mtActual.EsActual = true;
			else
				meActual.EsActual = true;
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
		//EscalaInicial.x /= CambioEscala;
		//EscalaInicial.y /= CambioEscala;
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

				SeleccionarItem (elemActual, false);
				MoverIzquierda (elemActual);
				RotarElemento (elemActual, primeraRotacion);

				if (anterior != null) {
					MoverIzquierda (anterior.Value);
                    RotarElemento(anterior.Value, segundaRotacion);
				}

				// Selecciona el siguiente elemento
				SeleccionarItem (siguiente.Value, true);
				ElementoActual = siguiente;

				while (siguiente != null) {
					MoverIzquierda (siguiente.Value);
					siguiente = siguiente.Next;
				}

				while (anterior !=null) {
					//ReducirEscala (anterior.Value);
					MoverFrente (anterior.Value);
					anterior = anterior.Previous;
				}

				PosInicial.x -= SaltoElemento;
				EscalaInicial.x *= CambioEscala;
				EscalaInicial.y *= CambioEscala;

				Tema mt = ElementoActual.Value.GetComponent<Tema>();
				if(mt != null && mt.Seleccionado){
					LimpiarMenuVertical();
					mt.AbrirContenido();
				}
			
			} else {
				Debug.Log ("Ultimo elemento");
			}
	}

	public void AvanzarDetalle(){
		GameObject elemActual = DetalleActual.Value;
		LinkedListNode<GameObject> anterior = DetalleActual.Previous;
		LinkedListNode<GameObject> siguiente = DetalleActual.Next;
		if (siguiente != null) {
			SeleccionarItem (elemActual, false);
			SaltarFila (elemActual);

			while (anterior !=null) {
				MoverArriba (anterior.Value);
				anterior = anterior.Previous;
			}

			SeleccionarItem (siguiente.Value, true);
			DetalleActual = siguiente;

			while (siguiente != null) {
				MoverArriba (siguiente.Value);
				siguiente = siguiente.Next;
			}

			PosDetalle.y += SaltoElemento;
		}
		else {
			Debug.Log ("Ultimo elemento en detalle");
		}
	}

	/**
	 * Corutina que mueve hacia la izquierda un espacio el elemento pasado por parametro
	 * */
	public void MoverIzquierda(GameObject elemento){
		Vector3 nuevaPos = elemento.transform.position;
		nuevaPos.x -= SaltoElemento;
		iTween.MoveTo (elemento, nuevaPos,velMovimiento);
        StartCoroutine(Esperar(1.02f));
	}

	/**
	 * Corutina que mueve hacia la izquierda un espacio el elemento pasado por parametro
	 * */
	public void MoverFrente(GameObject elemento){
		Vector3 nuevaPos = elemento.transform.position;
		nuevaPos.z -= SaltoElemento;
		iTween.MoveTo (elemento, nuevaPos,velMovimiento);
        StartCoroutine(Esperar(1.02f));
    }

	/**
	 * Corutina que mueve hacia la izquierda un espacio el elemento pasado por parametro
	 * */
	public void MoverAtras(GameObject elemento){
		Vector3 nuevaPos = elemento.transform.position;
		nuevaPos.z += SaltoElemento;
		iTween.MoveTo (elemento, nuevaPos,velMovimiento);
        StartCoroutine(Esperar(1.02f));
    }

	public void MoverArriba(GameObject elemento){
		Vector3 nuevaPos = elemento.transform.position;
		nuevaPos.y += SaltoElemento;
		iTween.MoveTo (elemento, nuevaPos,velMovimiento);
        StartCoroutine(Esperar(1.02f));
    }

	public void SaltarFila(GameObject elemento){
		Vector3 nuevaPos = elemento.transform.position;
		nuevaPos.y += SaltoElemento*1.5f;
		iTween.MoveTo (elemento, nuevaPos,velMovimiento);
        StartCoroutine(Esperar(1.02f));
    }

	/**
	 * Mueve la seleccion actual del menu de a un elemento en direccion al inicio de la lista
	 * */
	public void Retroceder(){
			GameObject elemActual = ElementoActual.Value;
			LinkedListNode<GameObject> anterior = ElementoActual.Previous;
			LinkedListNode<GameObject> siguiente = ElementoActual.Next;
			if (anterior != null) {
				// Mueve hacia atras el elemento actual y lo des-selecciona
			
				SeleccionarItem (elemActual, false);
				MoverDerecha (elemActual);

				MoverDerecha (anterior.Value);
				RotarElemento (anterior.Value, 0);
			    
                if(anterior.Previous != null)
                {
                    RotarElemento(anterior.Previous.Value, primeraRotacion);
                }

				// Selecciona el anterior elemento
				SeleccionarItem (anterior.Value, true);
				ElementoActual = anterior;

				while (siguiente != null) {
					MoverDerecha (siguiente.Value);
					siguiente = siguiente.Next;
				}

				anterior = anterior.Previous;
				while (anterior != null) {
					//AumetarEscala (anterior.Value);
					MoverAtras (anterior.Value);
					anterior = anterior.Previous;
				}
			
				PosInicial.x += SaltoElemento;
				EscalaInicial.x /= CambioEscala;
				EscalaInicial.y /= CambioEscala;

				Tema mt = ElementoActual.Value.GetComponent<Tema>();
				if(mt != null && mt.Seleccionado){
					LimpiarMenuVertical();
					mt.AbrirContenido();
				}
			
			} else {
				Debug.Log ("Primer elemento");
			}
	}

	public void RetrocederDetalle(){
		GameObject elemActual = DetalleActual.Value;
		LinkedListNode<GameObject> anterior = DetalleActual.Previous;
		LinkedListNode<GameObject> siguiente = DetalleActual.Next;
		if (anterior != null) {
			SeleccionarItem (elemActual, false);
			MoverAbajo (elemActual);

			BajarFila(anterior.Value);
			SeleccionarItem (anterior.Value, true);
			DetalleActual = anterior;

			while (siguiente != null) {
				MoverAbajo (siguiente.Value);
				siguiente = siguiente.Next;
			}

			anterior = anterior.Previous;
			while (anterior != null) {
				MoverAbajo (anterior.Value);
				anterior = anterior.Previous;
			}

			PosInicial.x -= SaltoElemento;
		}
		else {
			Debug.Log ("Primer elemento");
		}
	}

	/**
	 * Corutina que mueve hacia la derecha un espacio el elemento pasado por parametro
	 * */
	public void MoverDerecha(GameObject elemento){
		Vector3 nuevaPos = elemento.transform.position;
		nuevaPos.x += SaltoElemento;
		iTween.MoveTo (elemento, nuevaPos,velMovimiento);
        StartCoroutine(Esperar(1.02f));
    }

	public void MoverAbajo(GameObject elemento){
		Vector3 nuevaPos = elemento.transform.position;
		nuevaPos.y -= SaltoElemento;
		iTween.MoveTo (elemento, nuevaPos,velMovimiento);
        StartCoroutine(Esperar(1.02f));
    }

	public void BajarFila(GameObject elemento){
		Vector3 nuevaPos = elemento.transform.position;
		nuevaPos.y -= SaltoElemento*1.5f;
		iTween.MoveTo (elemento, nuevaPos,velMovimiento);
        StartCoroutine(Esperar(1.02f));
    }

	public void RotarElemento(GameObject elemento, float rotacion){
		Vector3 rot = new Vector3 (0,rotacion,0);
		iTween.RotateTo (elemento, rot, velRotacion);
        StartCoroutine(Esperar(1.02f));
    }

	public void ReducirEscala(GameObject elemento){
		Vector3 nuevaEscala = elemento.transform.localScale;
		nuevaEscala.x /= CambioEscala;
		nuevaEscala.y /= CambioEscala;
		elemento.transform.localScale = nuevaEscala;
	}

	public void AumetarEscala(GameObject elemento){
		Vector3 nuevaEscala = elemento.transform.localScale;
		nuevaEscala.x *= CambioEscala;
		nuevaEscala.y *= CambioEscala;
		elemento.transform.localScale = nuevaEscala;
	}

	public void DetectarPosicion(GameObject objeto, int eje){
		if (eje == 0) {
			if (EstaAdelante (objeto)) {
				Avanzar ();
			} else {
				Retroceder ();
			}
		} else {
			if (EstaAbajo (objeto)) {
				AvanzarDetalle ();
			} else {
				RetrocederDetalle ();
			}
		}
	}

	public bool EstaAdelante(GameObject temaMenu){
		LinkedListNode<GameObject> actual = ElementoActual.Next;
		while (actual != null) {
			Tema mtActual = actual.Value.GetComponent<Tema>();
			Elemento meActual = actual.Value.GetComponent<Elemento>();
			Tema mtMenu = temaMenu.GetComponent<Tema>();
			Elemento meMenu = temaMenu.GetComponent<Elemento>();
			if(mtActual != null){
				if(mtMenu != null){
					if(mtActual.Nombre.Equals(mtMenu.Nombre))
						return true;
				}
			}
			else{
				if(meMenu != null){
					if(meActual.Nombre.Equals(meMenu.Nombre))
						return true;
				}
			}
			actual = actual.Next;
		}
		return false;
	}

	public bool EstaAbajo(GameObject elementoVertical){
		LinkedListNode<GameObject> actual = DetalleActual.Next;
		while (actual != null) {
			Tema mtActual = actual.Value.GetComponent<Tema>();
			Elemento meActual = actual.Value.GetComponent<Elemento>();
			Tema mtVertical = elementoVertical.GetComponent<Tema>();
			Elemento meVertical = elementoVertical.GetComponent<Elemento>();
			if(mtActual != null){
				if(mtVertical != null){
					if(mtActual.Nombre.Equals(mtVertical.Nombre))
						return true;
				}
			}
			else{
				if(meVertical != null){
					if(meActual.Nombre.Equals(meVertical.Nombre))
						return true;
				}
			}
			actual = actual.Next;
		}
		return false;
	}

	public void BajarNivel(){
		DesplazarMenu (0);
		Desplazado = true;

		foreach (GameObject item in ListaElementos) {
			Tema mt = item.GetComponent<Tema>();
			if(mt != null)
				mt.Seleccionado = true;
		}
	}

	public bool SubirNivel(){
		Desplazado = false;
		DesplazarMenu (1);

		foreach (GameObject item in ListaElementos) {
			Tema mt = item.GetComponent<Tema>();
			if(mt != null)
				mt.Seleccionado = false;
		}
		LimpiarMenuVertical ();

		if (PilaListas.Count < 1)
			return true;
		
		return false;
	}

	public void ReemplazarNivel(){
		PilaListas.Push (ListaElementos);
		LimpiarMenuHorizontal ();
        LinkedList<GameObject> temp = ListaDetalle;
        LimpiarMenuVertical();
        foreach (GameObject objeto in temp){
            objeto.SetActive(true);
            Tema esTema = objeto.GetComponent<Tema>();
			Elemento esElemento = objeto.GetComponent<Elemento>();

            if (esTema != null)
            {
                esTema.EnDetalle = false;
                esTema.EsActual = false;
                esTema.Seleccionado = false;
            }
            else
            {
                esElemento.EnDetalle = false;
                esElemento.EsActual = false;
            }

			Agregar(objeto);
		}
	}

    public bool RecuperarNivel()
    {
        LimpiarMenuHorizontal();
        LinkedList<GameObject> temp = PilaListas.Pop();
        foreach(GameObject item in temp)
        {
            item.SetActive(true);
            Tema esTema = item.GetComponent<Tema>();
            Elemento esElemento = item.GetComponent<Elemento>();

            if (esTema != null)
            {
                esTema.EnDetalle = false;
                esTema.EsActual = false;
                esTema.Seleccionado = false;
            }
            else
            {
                esElemento.EnDetalle = false;
                esElemento.EsActual = false;
            }

            Agregar(item);
        }

        if (PilaListas.Count < 1)
            return true;

        return false;
    }

	public void LimpiarMenuHorizontal(){
		foreach (GameObject nodo in ListaElementos) {
			nodo.SetActive(false);
		}

		ListaElementos = new LinkedList<GameObject> ();
        PosInicial = Vector3.zero;
	}

	public void LimpiarMenuVertical(){
		foreach (GameObject nodo in ListaDetalle) {
			nodo.SetActive(false);
		}
		
		ListaDetalle = new LinkedList<GameObject> ();
		PosDetalle = Vector3.zero;
	}

	public void SeleccionarItem(GameObject item, bool seleccionar){

		Tema mTema = item.GetComponent<Tema> ();
		Elemento mElemento = item.GetComponent<Elemento>();

		if (seleccionar) {
            if (mTema != null)
                mTema.Seleccionar(true);
            else
                mElemento.Seleccionar(true);
		} else {
			if(mTema !=null)
                mTema.Seleccionar(false);
            else
                mElemento.Seleccionar(false);
        }
	}

	public void AbrirContenido(List<GameObject> listaElementos){
		foreach (GameObject item in listaElementos) {
			item.SetActive (true);
            item.GetComponent<Collider>().enabled = true;
			Tema esTema = item.GetComponent<Tema> ();
			Elemento esElemento = item.GetComponent<Elemento> ();
			
			if (esTema != null) {
				SeleccionarItem (item, false);
				esTema.EnDetalle = true;
			} else {
				esElemento.EnDetalle = true;
				SeleccionarItem(esElemento.gameObject, false);
			}

			item.transform.SetParent(ItemPanel.transform, false);
			RectTransform rt = item.GetComponent<RectTransform> ();
			rt.sizeDelta = new Vector2 (185, 185);
			item.transform.localPosition = PosDetalle;
			item.transform.localRotation = Quaternion.identity;
			item.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);

			PosDetalle.y -= SaltoElemento;

			ListaDetalle.AddLast(item);

			if(ListaDetalle.Count == 1){
				DetalleActual = ListaDetalle.First;
				SeleccionarItem(item, true);
			}
		}
	}

	public void DesplazarMenu(int posicion){
		if (!Desplazado) {
			Vector3 nuevaPos = ScrollPanel.transform.position;
			if (posicion == 0) {
				nuevaPos.y += Desplazamiento;
			} else {
				nuevaPos.y -= Desplazamiento;
			}
			iTween.MoveTo (ScrollPanel, nuevaPos, velMovimiento);
		}
	}

    public void MostarMenu(bool mostrar)
    {
        ItemView.SetActive(mostrar);
    }

    public Tema PadreActual()
    {
        if (ElementoActual != null)
        {
            Tema mt = ElementoActual.Value.GetComponent<Tema>();
            Elemento me = ElementoActual.Value.GetComponent<Elemento>();

            if (mt != null)
                return mt.TemaPadre;

            return me.TemaPadre;
        }

        return null;
    }

	public void SetParametrosIniciales(){
		PosInicial = Vector3.zero;
		PosDetalle = Vector3.zero;
		SaltoElemento = AnchoElementos + DistanciaElementos;
		EscalaInicial = new Vector3 (1.0f, 1.0f, 1.0f);
	}

    public IEnumerator Esperar(float tiempo)
    {
        EnAnimacion = true;
        yield return new WaitForSeconds(tiempo);
        EnAnimacion = false;
    }
}
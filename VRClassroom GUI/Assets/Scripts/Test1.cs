using UnityEngine;
using System.Collections;
using System;

public class Test1 : MonoBehaviour {

	public  	GameObject						PrefElemento;
	public  	GameObject						PrefTema;

	// Use this for initialization
	void Start () {
	
	}
	
	public void OnTest(){
		GameObject canvas = GameObject.Find ("Canvas");
		ManagerMenu menu = canvas.GetComponent<ManagerMenu> ();
		ManagerEdition edition = canvas.GetComponent<ManagerEdition> ();

		GameObject nuevoTema = edition.CrearTema ("Tema1", "Autor1", new DateTime (2015, 8, 31));
		Tema mainTema = nuevoTema.GetComponent<Tema> ();

		GameObject childTema = edition.CrearTema ("Tema1.1", "Autor1.1", new DateTime (2015, 8, 31));
		Tema chTema = childTema.GetComponent<Tema> ();
		chTema.TemaPadre = mainTema;
		mainTema.AgregarContenido (childTema);

		GameObject childElemento = edition.CrearElemento("Elemento1.1", "Descripcion1.1");
		Elemento mainElemento = childElemento.GetComponent<Elemento> ();
		mainTema.AgregarContenido (childElemento);
		
		childElemento = edition.CrearElemento("Elemento1.1.1", "Descripcion1.1.1");
		mainElemento = childElemento.GetComponent<Elemento> ();
		chTema.AgregarContenido (childElemento);
		
		childElemento = edition.CrearElemento("Elemento1.1.2", "Descripcion1.1.2");
		mainElemento = childElemento.GetComponent<Elemento> ();
		chTema.AgregarContenido (childElemento);

		childTema = edition.CrearTema ("Tema 1.2", "Autor1.2", new DateTime (2015, 9, 1));
		chTema = childTema.GetComponent<Tema> ();
		chTema.TemaPadre = mainTema;
		mainTema.AgregarContenido (childTema);

		childElemento = edition.CrearElemento("Elemento1.2.1", "Descripcion1.2.1");
		mainElemento = childElemento.GetComponent<Elemento> ();
		chTema.AgregarContenido (childElemento);

		menu.Agregar (nuevoTema);



		nuevoTema = edition.CrearTema ("Tema2", "Autor2", new DateTime (2015, 9, 4));
		mainTema = nuevoTema.GetComponent<Tema> ();

		childTema = edition.CrearTema ("Tema2.1", "Autor2.1", new DateTime (2015, 9, 4));
		chTema = childTema.GetComponent<Tema> ();
		chTema.TemaPadre = mainTema;
		mainTema.AgregarContenido (childTema);

		childTema = edition.CrearTema ("Tema2.2", "Autor2.", new DateTime (2015, 9, 4));
		chTema = childTema.GetComponent<Tema> ();
		chTema.TemaPadre = mainTema;
		mainTema.AgregarContenido (childTema);

		childTema = edition.CrearTema ("Tema2.3", "Autor2.3", new DateTime (2015, 9, 4));
		chTema = childTema.GetComponent<Tema> ();
		chTema.TemaPadre = mainTema;
		mainTema.AgregarContenido (childTema);

		childTema = edition.CrearTema ("Tema2.4", "Autor2.4", new DateTime (2015, 9, 4));
		chTema = childTema.GetComponent<Tema> ();
		chTema.TemaPadre = mainTema;
		mainTema.AgregarContenido (childTema);

		childTema = edition.CrearTema ("Tema2.5", "Autor2.5", new DateTime (2015, 9, 4));
		chTema = childTema.GetComponent<Tema> ();
		chTema.TemaPadre = mainTema;
		mainTema.AgregarContenido (childTema);

		childTema = edition.CrearTema ("Tema2.6", "Autor2.6", new DateTime (2015, 9, 4));
		chTema = childTema.GetComponent<Tema> ();
		chTema.TemaPadre = mainTema;
		mainTema.AgregarContenido (childTema);

		childElemento = edition.CrearElemento("Elemento2.1", "Descripcion2.1");
		mainElemento = childElemento.GetComponent<Elemento> ();
		mainTema.AgregarContenido (childElemento);

		childElemento = edition.CrearElemento("Elemento2.2", "Descripcion2.2");
		mainElemento = childElemento.GetComponent<Elemento> ();
		mainTema.AgregarContenido (childElemento);

		childElemento = edition.CrearElemento("Elemento2.3", "Descripcion2.3");
		mainElemento = childElemento.GetComponent<Elemento> ();
		mainTema.AgregarContenido (childElemento);

		menu.Agregar (nuevoTema);
	}
}
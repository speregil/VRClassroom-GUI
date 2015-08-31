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

		GameObject nuevoTema = GameObject.Instantiate (PrefTema);

		Tema mainTema = nuevoTema.GetComponent<Tema> ();
		mainTema.Autor = "Autor1";
		mainTema.FechaCreacion = new DateTime (2015, 8, 31);
		mainTema.Nombre = "Tema 1";

		GameObject childTema = GameObject.Instantiate (PrefTema);
		Tema chTema = childTema.GetComponent<Tema> ();
		chTema.Autor = "Autor1.1";
		chTema.FechaCreacion = new DateTime (2015, 8, 31);
		chTema.Nombre = "Tema 1.1";
		mainTema.AgregarContenido (childTema);

		GameObject childElemento = GameObject.Instantiate (PrefElemento);
		Elemento mainElemento = childElemento.GetComponent<Elemento> ();
		mainElemento.Nombre = "Elemento1.1";
		mainElemento.Descripcion = "Descripcion1.1";
		mainTema.AgregarContenido (childElemento);

		childElemento = GameObject.Instantiate (PrefElemento);
		mainElemento = childElemento.GetComponent<Elemento> ();
		mainElemento.Nombre = "Elemento1.1.1";
		mainElemento.Descripcion = "Descripcion1.1.1";
		chTema.AgregarContenido (childElemento);

		menu.Agregar (nuevoTema);
	}
}
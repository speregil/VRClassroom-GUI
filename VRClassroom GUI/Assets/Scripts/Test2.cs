using UnityEngine;
using System.Collections;
using System;

public class Test2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void OnTest(){
		ManagerMenu menu = GetComponent<ManagerMenu> ();
		ManagerEdition editor = GetComponent<ManagerEdition> ();

		menu.AnchoElementos = editor.GetPrefabWidth ();
		menu.SetParametrosIniciales ();

		GameObject nuevoTema = editor.CrearTema ("Test1", "Sebastian Gil Parga", new DateTime (2015, 9, 10));
		Tema mtNuevo = nuevoTema.GetComponent<Tema> ();
		GameObject TemaHijo = editor.CrearTema ("Test1.1", "Sebastian Gil Parga", new DateTime (2015, 9, 17));
		Tema mtHijo = TemaHijo.GetComponent<Tema>();
		mtHijo.TemaPadre = nuevoTema.GetComponent<Tema>();
		mtNuevo.AgregarContenido (TemaHijo);
		GameObject ElementoHijo = editor.CrearElemento ("Elemento 1.1", "Descripcion 1.1");
		mtNuevo.AgregarContenido (ElementoHijo);
		menu.Agregar (nuevoTema);

		nuevoTema = editor.CrearTema ("Test2", "Sebastian Gil Parga", new DateTime (2015, 9, 10));
		mtNuevo = nuevoTema.GetComponent<Tema> ();
		TemaHijo = editor.CrearTema ("Test2.1", "Sebastian Gil Parga", new DateTime (2015, 9, 17));
		mtHijo = TemaHijo.GetComponent<Tema>();
		mtHijo.TemaPadre = nuevoTema.GetComponent<Tema>();
		mtNuevo.AgregarContenido (TemaHijo);
		ElementoHijo = editor.CrearElemento ("Elemento 2.1", "Descripcion 1.1");
		mtNuevo.AgregarContenido (ElementoHijo);
		menu.Agregar (nuevoTema);


		nuevoTema = editor.CrearTema ("Test3", "Sebastian Gil Parga", new DateTime (2015, 9, 10));
		mtNuevo = nuevoTema.GetComponent<Tema> ();
		TemaHijo = editor.CrearTema ("Test3.1", "Sebastian Gil Parga", new DateTime (2015, 9, 17));
		mtHijo = TemaHijo.GetComponent<Tema>();
		mtHijo.TemaPadre = nuevoTema.GetComponent<Tema>();
		mtNuevo.AgregarContenido (TemaHijo);
		ElementoHijo = editor.CrearElemento ("Elemento 3.1", "Descripcion 1.1");
		mtNuevo.AgregarContenido (ElementoHijo);
		menu.Agregar (nuevoTema);

		nuevoTema = editor.CrearTema ("Test4", "Sebastian Gil Parga", new DateTime (2015, 9, 10));
		mtNuevo = nuevoTema.GetComponent<Tema> ();
		TemaHijo = editor.CrearTema ("Test4.1", "Sebastian Gil Parga", new DateTime (2015, 9, 17));
		mtHijo = TemaHijo.GetComponent<Tema>();
		mtHijo.TemaPadre = nuevoTema.GetComponent<Tema>();
		mtNuevo.AgregarContenido (TemaHijo);
		ElementoHijo = editor.CrearElemento ("Elemento 4.1", "Descripcion 1.1");
		mtNuevo.AgregarContenido (ElementoHijo);
		menu.Agregar (nuevoTema);
	}
}
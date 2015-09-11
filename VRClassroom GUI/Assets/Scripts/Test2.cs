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
		menu.Agregar (nuevoTema);

		nuevoTema = editor.CrearTema ("Test2", "Sebastian Gil Parga", new DateTime (2015, 9, 10));
		menu.Agregar (nuevoTema);

		nuevoTema = editor.CrearTema ("Test3", "Sebastian Gil Parga", new DateTime (2015, 9, 10));
		menu.Agregar (nuevoTema);

		nuevoTema = editor.CrearTema ("Test4", "Sebastian Gil Parga", new DateTime (2015, 9, 10));
		menu.Agregar (nuevoTema);
	}
}
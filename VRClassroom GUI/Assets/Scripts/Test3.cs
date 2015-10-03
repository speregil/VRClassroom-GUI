using UnityEngine;
using System.Collections;
using System;
using ProgressBar;

public class Test3 : MonoBehaviour {

    private ManagerMenu     menu;
    private ManagerEdition  editor;

	// Use this for initialization
	void Start () {
        menu = GetComponent<ManagerMenu>();
        editor = GetComponent<ManagerEdition>();
    }

	public void OnTest(){
		menu.AnchoElementos = editor.GetPrefabWidth ();
		menu.SetParametrosIniciales ();

        GameObject Tema1 = CrearTema("Tema 1", "Sebastian", new DateTime(2015, 9, 24),null);
        GameObject Tema2 = CrearTema("Tema 2", "Sebastian", new DateTime(2015, 9, 24), null);
        GameObject Tema3 = CrearTema("Tema 3", "Sebastian", new DateTime(2015, 9, 24), null);
        GameObject Tema4 = CrearTema("Tema 4", "Sebastian", new DateTime(2015, 9, 24), null);
        GameObject Elemento5 = CrearElemento("Elemento 5", "Descripcion 5", null);


        GameObject Tema1a = CrearTema("Tema 1a", "Sebastian", new DateTime(2015, 9, 24), Tema1);
        GameObject Tema1b = CrearTema("Tema 1b", "Sebastian", new DateTime(2015, 9, 24), Tema1);
        GameObject Elemento1a = CrearElemento("Elemento 1a", "Descripcion 1a", Tema1);

        GameObject Elemento1aa = CrearElemento("Elemento 1aa", "Descripcion 1aa", Tema1a);
        GameObject Elemento1ab = CrearElemento("Elemento 1ab", "Descripcion 1ab", Tema1a);

        GameObject Elemento2a = CrearElemento("Elemento 2a", "Descripcion 2a", Tema2);
        GameObject Elemento2b = CrearElemento("Elemento 2b", "Descripcion 2b", Tema2);
        GameObject Elemento2c = CrearElemento("Elemento 2c", "Descripcion 2c", Tema2);
        GameObject Elemento2d = CrearElemento("Elemento 2d", "Descripcion 2d", Tema2);
        GameObject Elemento2e = CrearElemento("Elemento 2e", "Descripcion 2e", Tema2);
        GameObject Elemento2f = CrearElemento("Elemento 2f", "Descripcion 2f", Tema2);
        GameObject Elemento2g = CrearElemento("Elemento 2g", "Descripcion 2g", Tema2);
        GameObject Elemento2h = CrearElemento("Elemento 2h", "Descripcion 2h", Tema2);

        menu.Agregar(Tema1);
        menu.Agregar(Tema2);
        menu.Agregar(Tema3);
        menu.Agregar(Tema4);
        menu.Agregar(Elemento5);
    }

    public GameObject CrearTema(string nombre, string autor, DateTime fecha, GameObject padre)
    {
        GameObject nuevoTema = editor.CrearTema(nombre, autor, fecha);
        if(padre != null) {
            Tema mt = nuevoTema.GetComponent<Tema>();
            Tema mtPadre = padre.GetComponent<Tema>();
            mt.TemaPadre = mtPadre;
            mtPadre.AgregarContenido(nuevoTema);
        }
        return nuevoTema;
    }

    public GameObject CrearElemento(string nombre, string descripcion, GameObject padre)
    {
        GameObject nuevoElemento = editor.CrearElemento(nombre, descripcion);
        if(padre != null)
        {
            Elemento me = nuevoElemento.GetComponent<Elemento>();
            Tema mtPadre = padre.GetComponent<Tema>();
            me.TemaPadre = mtPadre;
            mtPadre.AgregarContenido(nuevoElemento);
        }
        return nuevoElemento;
    }

	public void progreso(){
        Debug.Log("Entre");
		ProgressBarBehaviour pbg = GameObject.Find ("Panel").GetComponent<ProgressBarBehaviour>();
		pbg.IncrementValue (5.0f);
	}
}

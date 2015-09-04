using UnityEngine;
using System.Collections;
using System;

public class ManagerEdition : MonoBehaviour {

	public	GameObject	PrefTema;
	public	GameObject	PrefElemento;

	// Use this for initialization
	void Start () {
	
	}

	public GameObject CrearTema(string nNombre, string nAutor, DateTime nFecha){
		GameObject nuevoTema = GameObject.Instantiate (PrefTema);
		Tema mTema = nuevoTema.GetComponent<Tema> ();
		mTema.Nombre = nNombre;
		mTema.Autor = nAutor;
		mTema.FechaCreacion = nFecha;
		return nuevoTema;
	}

	public GameObject CrearElemento(string nNombre, string nDescripcion){
		GameObject nuevoElemento = GameObject.Instantiate (PrefElemento);
		Elemento mElemento = nuevoElemento.GetComponent<Elemento> ();
		mElemento.Nombre = nNombre;
		mElemento.Descripcion = nDescripcion;
		return nuevoElemento;
	}
}
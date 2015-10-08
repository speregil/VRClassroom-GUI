using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ManagerEdition : MonoBehaviour {

	public	GameObject	PrefTema;
	public	GameObject	PrefElemento;
    public  GameObject  MenuCrear;
    public  GameObject  MainOptions;
    public  GameObject  PanelCrear;
    public  GameObject  Principal;
    public  GameObject  Detalle;
    private string      Estado;

    private const string ABIERTO = "ABIERTO";
    private const string CERRADO = "CERRADO";
    private const string INPUT = "INPUT";
    private const string CREAR_TEMA = "CREAR_TEMA";
    private const string CREAR_ELEMENTO = "CREAR_ELEMENTO";

    // Use this for initialization
    void Start () {
        Estado = CERRADO;
	}

	public GameObject CrearTema(string nNombre, string nAutor, DateTime nFecha){
		GameObject nuevoTema = GameObject.Instantiate (PrefTema);
		Tema mTema = nuevoTema.GetComponent<Tema> ();
		mTema.Nombre = nNombre;
		mTema.Autor = nAutor;
		mTema.FechaCreacion = nFecha;

		Text mText = nuevoTema.GetComponentInChildren<Text> ();
		mText.text = nNombre;

        nuevoTema.GetComponent<Collider>().enabled = false;
		return nuevoTema;
	}

	public GameObject CrearElemento(string nNombre, string nDescripcion){
		GameObject nuevoElemento = GameObject.Instantiate (PrefElemento);
		Elemento mElemento = nuevoElemento.GetComponent<Elemento> ();
		mElemento.Nombre = nNombre;
		mElemento.Descripcion = nDescripcion;

		Text mText = nuevoElemento.GetComponentInChildren<Text> ();
		mText.text = nNombre;

        nuevoElemento.GetComponent<Collider>().enabled = false;
        return nuevoElemento;
	}

    public void AbrirMenuCrear()
    {
        MenuCrear.SetActive(true);
        Principal.SetActive(false);
        Detalle.SetActive(false);
        Estado = ABIERTO;
    }

    public void AbrirInputCrear()
    {
        MainOptions.SetActive(false);
        PanelCrear.SetActive(true);
        Estado = INPUT;
    }
    public void Volver()
    {
        switch (Estado)
        {
            case ABIERTO:
                Principal.SetActive(true);
                Detalle.SetActive(true);
                MenuCrear.SetActive(false);
                break;
            case INPUT:
                MainOptions.SetActive(true);
                PanelCrear.SetActive(false);
                Estado = ABIERTO;
                break;
            default:
                break;
        }
        
    }

	public float GetPrefabWidth(){
		GameObject testPrefab = GameObject.Instantiate (PrefTema);
		RectTransform rt = testPrefab.GetComponent<RectTransform> ();
		return rt.rect.width;
	}
}
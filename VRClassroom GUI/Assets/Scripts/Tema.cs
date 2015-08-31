using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Tema : MonoBehaviour{
	
	public		string				Nombre;
	public		string				Autor;
	public		DateTime			FechaCreacion;
	public		int					NumElementos;
	public		float				PorcentajeCompleto;
	public		bool				EnDetalle;

	private		List<GameObject>	Contenido;
	private		GameObject			PanelInfo;
	private		GameObject			PanelDetalle;

	void Awake (){
		Debug.Log ("Nuevo tema");
		Nombre = "SinNombre";
		Autor = "SinAutor";
		FechaCreacion = new DateTime(1991,8,22);
		NumElementos = 0;
		PorcentajeCompleto = 0.0f;
		Contenido = new List<GameObject>();
		PanelInfo = GameObject.Find ("InfoPanel");
		PanelDetalle = GameObject.Find ("Canvas");
		EnDetalle = false;
	}

	public void AgregarContenido(GameObject nuevoContenido){
		Contenido.Add (nuevoContenido);
		NumElementos++;
	}

	public void MostrarInfo(){
		PanelInformacion panel = PanelInfo.GetComponent<PanelInformacion> ();
		panel.MostrarInfoTema (this.gameObject);
	}

	public void AbrirContenido(){
		ManagerDetail detalle = PanelDetalle.GetComponent<ManagerDetail> ();
		foreach (GameObject item in Contenido) {
			Tema esTema = item.GetComponent<Tema>();
			Elemento esElemento = item.GetComponent<Elemento>();

			if(esTema != null){
				esTema.EnDetalle = true;
				detalle.AgregarTema(item);
			}else{
				esElemento.EnDetalle = true;
				detalle.AgregarElemento(item);
			}
		}
	}

	public void EnClick(){
		if (EnDetalle)
			MostrarInfo ();
		else{
			AbrirContenido ();
		}
	}
}
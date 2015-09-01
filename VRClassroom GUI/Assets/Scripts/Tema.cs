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
	public		bool				Seleccionado;
	public		bool				PrimerClick;
	public		List<GameObject>	Contenido;
	public		Tema				TemaPadre;

	private		GameObject			PanelInfo;
	private		GameObject			MainCanvas;

	void Awake (){
		Nombre = "SinNombre";
		Autor = "SinAutor";
		FechaCreacion = new DateTime(1991,8,22);
		NumElementos = 0;
		PorcentajeCompleto = 0.0f;
		Contenido = new List<GameObject>();
		TemaPadre = null;
		PanelInfo = GameObject.Find ("InfoPanel");
		MainCanvas = GameObject.Find ("Canvas");
		EnDetalle = false;
		Seleccionado = false;
		PrimerClick = false;
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
		ManagerDetail detalle = MainCanvas.GetComponent<ManagerDetail> ();
		Seleccionado = true;
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

		MostrarInfo ();
	}

	public void	BajarNivel(){
		ManagerMenu menu = MainCanvas.GetComponent<ManagerMenu> ();
		menu.BajarNivel (TemaPadre);
	}

	public void EnClick(){

		if (PrimerClick) {
			BajarNivel();
		}
		else if (EnDetalle) {
			MostrarInfo ();
			PrimerClick = true;
			EnDetalle = false;
		}
		else if(!Seleccionado){
			AbrirContenido ();
		}
	}
}
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
	public		bool				EsActual;
	public		bool				EnDetalle;
	public		bool				Seleccionado;
	public		bool				PrimerClick;
	public 		bool				Desplazado;
	public		List<GameObject>	Contenido;
	public		Tema				TemaPadre;

	private		GameObject			DetailCanvas;
	private		GameObject			MainCanvas;

	void Awake (){
		Nombre = "SinNombre";
		Autor = "SinAutor";
		FechaCreacion = new DateTime(1991,8,22);
		NumElementos = 0;
		PorcentajeCompleto = 0.0f;
		Contenido = new List<GameObject>();
		TemaPadre = null;
		DetailCanvas = GameObject.Find ("DetailCanvas");
		MainCanvas = GameObject.Find ("MainCanvas");
		EsActual = false;
		EnDetalle = false;
		Seleccionado = false;
		PrimerClick = false;
	}

	public void AgregarContenido(GameObject nuevoContenido){
		Elemento esElemento = nuevoContenido.GetComponent<Elemento>();

		if (esElemento != null)
			Contenido.Add (nuevoContenido);
		else {
			GameObject ultimoTema = Contenido.FindLast(
				delegate(GameObject go){
				Tema mt = go.GetComponent<Tema>();
				return mt != null;
				});
			if(ultimoTema != null){
				int i = Contenido.IndexOf(ultimoTema);
				Contenido.Insert(i, nuevoContenido);
			}
			else
				Contenido.Add(nuevoContenido);
		}
		NumElementos++;
	}

	public void MostrarInfo(){
		PanelInformacion panel = DetailCanvas.GetComponentInChildren<PanelInformacion> ();
		panel.MostrarInfoTema (this.gameObject);
	}

	public void AbrirContenido(){
		ManagerDetail detalle = DetailCanvas.GetComponent<ManagerDetail> ();
		ManagerMenu menu = MainCanvas.GetComponent<ManagerMenu> ();

		detalle.LimpiarDetalle ();
		menu.AbrirContenido (Contenido);

		MostrarInfo ();
	}

	public void	BajarNivel(){
		ManagerMenu menu = MainCanvas.GetComponent<ManagerMenu> ();
		PrimerClick = false;
		EnDetalle = false;
		ManagerDetail detalle = DetailCanvas.GetComponent<ManagerDetail> ();
		detalle.LimpiarDetalle ();
		PanelInformacion panel = DetailCanvas.GetComponentInChildren<PanelInformacion> ();
		panel.LimpiarInfo ();
	
		menu.BajarNivel ();
		detalle.BajarNivel ();

		AbrirContenido ();
	}

	public void EnClick(){
		if (EnDetalle) {
			if(PrimerClick){
				BajarNivel();
			}
			else{
				MostrarInfo();
				PrimerClick = true;
			}
		} else {
			if(EsActual){
				AbrirContenido();
			}
		}
	}	
}
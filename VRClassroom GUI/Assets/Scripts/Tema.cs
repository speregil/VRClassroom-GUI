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
		PanelInformacion panel = PanelInfo.GetComponent<PanelInformacion> ();
		panel.MostrarInfoTema (this.gameObject);
	}

	public void AbrirContenido(){
		ManagerDetail detalle = MainCanvas.GetComponent<ManagerDetail> ();
		detalle.LimpiarDetalle ();
		ManagerMenu menu = MainCanvas.GetComponent<ManagerMenu> ();
		foreach (GameObject item in Contenido) {
			item.SetActive(true);
			Tema esTema = item.GetComponent<Tema>();
			Elemento esElemento = item.GetComponent<Elemento>();

			if(esTema != null){
				menu.SeleccionarItem(item, false);
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
		PrimerClick = false;
		EnDetalle = false;
		ManagerDetail detalle = MainCanvas.GetComponent<ManagerDetail> ();
		detalle.LimpiarDetalle ();
		PanelInformacion panel = PanelInfo.GetComponent<PanelInformacion> ();
		panel.LimpiarInfo ();
		menu.BajarNivel (TemaPadre);
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
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

	private		List<GameObject>	Contenido;
	private		GameObject			PanelInfo;
	void Start (){
		Nombre = "SinNombre";
		Autor = "SinAutor";
		FechaCreacion = new DateTime(1991,8,22);
		NumElementos = 0;
		PorcentajeCompleto = 0.0f;
		Contenido = new List<GameObject>();
		PanelInfo = GameObject.Find ("InfoPanel");
	}

	public void AgregarContenido(GameObject nuevoContenido){
		Contenido.Add (nuevoContenido);
		NumElementos++;
	}

	public void MostrarInfo(){
		PanelInformacion panel = PanelInfo.GetComponent<PanelInformacion> ();
		panel.MostrarInfoTema (this.gameObject);
	}
}
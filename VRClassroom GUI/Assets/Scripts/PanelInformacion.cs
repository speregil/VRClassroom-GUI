using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelInformacion : MonoBehaviour {

	public	GameObject	NombreTema;
	public 	GameObject	AutorTema;
	public	GameObject	FechaTema;
	public	GameObject	NumeroTema;
	public	GameObject	PorcentajeTema;

	public	GameObject	NombreElemento;
	public	GameObject	DescripcionElemento;
	public	GameObject	ToogleCompletado;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MostrarInfoTema(GameObject temaSeleccionado){
		Tema tm = temaSeleccionado.GetComponent<Tema> ();
		ActivarInfoElemento (false);
		ActivarInfoTema (true);

		Text label = NombreTema.GetComponent<Text> ();
		label.text = tm.Nombre;

		label = AutorTema.GetComponent<Text> ();
		label.text = "Creado por: " + tm.Autor;

		label = FechaTema.GetComponent<Text> ();
		label.text = "Creado el: " + tm.FechaCreacion.Day + "/" + tm.FechaCreacion.Month + "/" + tm.FechaCreacion.Year;

		label = NumeroTema.GetComponent<Text> ();
		label.text = "No. de elementos: " + tm.NumElementos;

		label = PorcentajeTema.GetComponent<Text> ();
		label.text = tm.PorcentajeCompleto + "% Completado";
	}

	public void MostrarInfoElemento(GameObject temaSeleccionado){
		Elemento el = temaSeleccionado.GetComponent<Elemento> ();
		ActivarInfoTema (false);
		ActivarInfoElemento (true);

		Text label = NombreElemento.GetComponent<Text> ();
		label.text = el.Nombre;

		label = DescripcionElemento.GetComponent<Text> ();
		label.text = el.Descripcion;

		Toggle tg = ToogleCompletado.GetComponentInChildren<Toggle>();
		tg.isOn = el.Completado;
	}

	public void ActivarInfoTema(bool activar){
		NombreTema.SetActive (activar);
		AutorTema.SetActive(activar);
		FechaTema.SetActive (activar);
		NumeroTema.SetActive (activar);
		PorcentajeTema.SetActive (activar);
	}

	public void ActivarInfoElemento(bool activar){
		NombreElemento.SetActive (activar);
		DescripcionElemento.SetActive (activar);
		ToogleCompletado.SetActive (activar);
	}
}
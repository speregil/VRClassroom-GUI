using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelInformacion : MonoBehaviour {

    public  GameObject  InfoTema;
    public  GameObject  InfoElemento;

    public	GameObject	NombreTema;
	public 	GameObject	AutorTema;
	public	GameObject	FechaTema;
	public	GameObject	NumeroTema;
	public	GameObject	PorcentajeTema;

	public	GameObject	NombreElemento;
	public	GameObject	DescripcionElemento;
	public	GameObject	ToogleCompletado;


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
		label.text = (tm.PorcentajeCompleto) + "% Completado";
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
        InfoTema.SetActive(activar);
	}

	public void ActivarInfoElemento(bool activar){
        InfoElemento.SetActive(activar);
	}

	public void MarcarCompletado(){
		Toggle tg = ToogleCompletado.GetComponentInChildren<Toggle>();
		tg.isOn = true;
	}

	public void LimpiarInfo(){
		ActivarInfoElemento (false);
		ActivarInfoTema (false);
	}
}
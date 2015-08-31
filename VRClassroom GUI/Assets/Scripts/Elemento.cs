using UnityEngine;
using System.Collections;

public class Elemento : MonoBehaviour {

	public	string		Nombre;
	public	string		Descripcion;
	public	bool		Completado;
	public	bool		EnDetalle;

	private	GameObject	PanelInfo;
	
	void Awake () {
		Nombre = "SinNombre";
		Descripcion = "SinDescripcion";
		Completado = false;
		EnDetalle = false;
		PanelInfo = GameObject.Find ("InfoPanel");
	}

	public void MostrarInfo(){
		PanelInformacion panel = PanelInfo.GetComponent<PanelInformacion> ();
		panel.MostrarInfoElemento (this.gameObject);
	}
}
using UnityEngine;
using System.Collections;

public class Elemento : MonoBehaviour {

	public	string		Nombre;
	public	string		Descripcion;
	public	bool		Completado;

	private	GameObject	PanelInfo;
	
	void Start () {
		Nombre = "SinNombre";
		Descripcion = "SinDescripcion";
		Completado = false;
		PanelInfo = GameObject.Find ("InfoPanel");
	}

	public void MostrarInfo(){
		PanelInformacion panel = PanelInfo.GetComponent<PanelInformacion> ();
		panel.MostrarInfoElemento (this.gameObject);
	}
}
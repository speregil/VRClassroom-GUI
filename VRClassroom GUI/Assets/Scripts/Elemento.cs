using UnityEngine;
using System.Collections;

public class Elemento : MonoBehaviour {

	public	string		Nombre;
	public	string		Descripcion;
	public	bool		Completado;
	public	bool		EnDetalle;
	public 	bool		EsActual;
	
	private	GameObject	DetailCanvas;
	private	GameObject	MainCanvas;
	
	void Awake () {
		Nombre = "SinNombre";
		Descripcion = "SinDescripcion";
		Completado = false;
		EnDetalle = false;
		DetailCanvas = GameObject.Find ("DetailCanvas");
		MainCanvas = GameObject.Find ("MainCanvas");
	}

	public void MostrarInfo(){
		PanelInformacion panel = DetailCanvas.GetComponentInChildren<PanelInformacion> ();
		panel.MostrarInfoElemento (this.gameObject);
	}

	public void EnClick(){
		ManagerMenu menu = MainCanvas.GetComponent<ManagerMenu> ();
		ManagerDetail detale = DetailCanvas.GetComponent<ManagerDetail> ();
		
		if (EnDetalle) {
			if(EsActual){
				MostrarInfo();
			}
			else{
				menu.AvanzarDetalle();
				MostrarInfo();
			}
		} 
	}	
}
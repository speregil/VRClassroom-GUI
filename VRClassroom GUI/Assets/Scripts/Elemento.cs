using UnityEngine;
using System.Collections;

public class Elemento : MonoBehaviour {

	public	string		Nombre;
	public	string		Descripcion;
	public	bool		Completado;
	public	bool		EnDetalle;
	public 	bool		EsActual;
    public  Tema        TemaPadre;

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
			if (EsActual) {
				MostrarInfo ();
			} else {
				menu.DetectarPosicion(this.gameObject,1);
				MostrarInfo ();
			}
		} else {
			if(EsActual){

			}
			else{
				menu.DetectarPosicion(this.gameObject,0);
				menu.LimpiarMenuVertical();
			}
		}
	}	
}
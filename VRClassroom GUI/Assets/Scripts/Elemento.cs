using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Elemento : MonoBehaviour {

	public	string		Nombre;
	public	string		Descripcion;
	public	bool		Completado;
	public	bool		EnDetalle;
	public 	bool		EsActual;
    public  Tema        TemaPadre;
    public  Sprite      sprNormal;
    public  Sprite      sprActual;

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

	public void Completar(){
		Completado = true;
		PanelInformacion panel = DetailCanvas.GetComponentInChildren<PanelInformacion> ();
		panel.MarcarCompletado ();

		if (TemaPadre != null) {
			Tema mt = TemaPadre.GetComponent<Tema>();
			mt.CalcularProgreso();
		}
	}

    public void Seleccionar(bool seleccion)
    {
        Image img = GetComponent<Image>();
        EsActual = seleccion;
        if (seleccion)
            img.sprite = sprActual;
        else
            img.sprite = sprNormal;
    }

    public void EnClick()
    {
        ManagerMenu menu = MainCanvas.GetComponent<ManagerMenu>();

        if (!menu.EnAnimacion)
        {
            if (EnDetalle)
            {
                if (EsActual)
                {
                    MostrarInfo();
                    Completar();
                }
                else
                {
                    menu.DetectarPosicion(this.gameObject, 1);
                    MostrarInfo();
                }
            }
            else
            {
                if (EsActual)
                {
                    MostrarInfo();
                    Completar();
                }
                else
                {
                    menu.DetectarPosicion(this.gameObject, 0);
                    menu.LimpiarMenuVertical();
                    MostrarInfo();
                }
            }
        }
    }	
}
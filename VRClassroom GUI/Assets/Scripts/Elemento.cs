using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Elemento : MonoBehaviour {

	public	string		Nombre;
	public	string		Descripcion;
    public  string      NombreEjecutable;
	public	bool		Completado;
	public	bool		EnDetalle;
	public 	bool		EsActual;
    public  Tema        TemaPadre;
    public  Sprite      sprNormal;
    public  Sprite      sprActual;
    public  GameObject  ObjetoIcono;
    public  Sprite      Icono;

    private	GameObject	DetailCanvas;
	private	GameObject	MainCanvas;
	
	void Awake () {
		Nombre = "SinNombre";
		Descripcion = "SinDescripcion";
        NombreEjecutable = "";
		Completado = false;
		EnDetalle = false;
		DetailCanvas = GameObject.Find ("DetailCanvas");
		MainCanvas = GameObject.Find ("MainCanvas");
	}

	public void MostrarInfo(){
		PanelInformacion panel = DetailCanvas.GetComponentInChildren<PanelInformacion> ();
		panel.MostrarInfoElemento (this.gameObject);
	}

    public void SetIcono(Sprite nIcono)
    {
        Icono = nIcono;
        Image img = ObjetoIcono.GetComponent<Image>();
        img.sprite = Icono;
    }

	public void Completar(){
		Completado = true;
		PanelInformacion panel = DetailCanvas.GetComponentInChildren<PanelInformacion> ();
		panel.MarcarCompletado ();

        GameObject main = GameObject.Find("MainCanvas");
        ManagerMenu mm = main.GetComponent<ManagerMenu>();
        mm.ElementoAbierto = Nombre;

        if (TemaPadre != null) {
			Tema mt = TemaPadre.GetComponent<Tema>();
            if (mt != null)
            {
                mt.CalcularProgreso();
                mt.ActualizarPanelProgreso();
            }
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

    public void Ejecutar()
    {
        Completar();
        if (!NombreEjecutable.Equals(""))
        {
            Application.LoadLevel(NombreEjecutable);
        }
    }

    public void EnClick()
    {
        ManagerMenu menu = MainCanvas.GetComponent<ManagerMenu>();
        ManagerReproduccion rep = DetailCanvas.GetComponent<ManagerReproduccion>();

        if (!menu.EnAnimacion && !ManagerContexto.ACTIVO)
        {
            if (EnDetalle)
            {
                if (EsActual)
                {
                    MostrarInfo();
                    menu.ElementoAbierto = Nombre;
                    rep.Abrir();
                }
                else
                {
                    menu.DetectarPosicion(this.gameObject, 1);
                    MostrarInfo();
                    menu.ElementoAbierto = Nombre;
                    rep.Abrir();
                }
            }
            else
            {
                if (EsActual)
                {
                    MostrarInfo();
                    menu.ElementoAbierto = Nombre;
                    rep.Abrir();
                }
                else
                {
                    menu.DetectarPosicion(this.gameObject, 0);
                    menu.LimpiarMenuVertical();
                    MostrarInfo();
                    menu.ElementoAbierto = Nombre;
                    rep.Abrir();
                }
            }
        }
    }	
}
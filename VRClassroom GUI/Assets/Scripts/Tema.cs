using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using ProgressBar;

public class Tema : MonoBehaviour{
	
	public		string				Nombre;
	public		string				Autor;
	public		DateTime			FechaCreacion;
	public		int					NumElementos;
	public		float				PorcentajeCompleto;
	public		bool				EsActual;
	public		bool				EnDetalle;
	public		bool				Seleccionado;
	public 		bool				Desplazado;
	public		List<GameObject>	Contenido;
	public		Tema				TemaPadre;
    public      Sprite              sprNormal;
    public      Sprite              sprActual;
    public      GameObject          ObjetoIcono;
    public      Sprite              Icono;

	private		GameObject			DetailCanvas;
	private		GameObject			MainCanvas;

	void Awake (){
		Nombre = "SinNombre";
		Autor = "SinAutor";
		FechaCreacion = new DateTime(1991,8,22);
		NumElementos = 0;
		PorcentajeCompleto = 0.0f;
		Contenido = new List<GameObject>();
		TemaPadre = null;
		DetailCanvas = GameObject.Find ("DetailCanvas");
		MainCanvas = GameObject.Find ("MainCanvas");
		EsActual = false;
		EnDetalle = false;
		Seleccionado = false;
	}

   /* public void InicializarReferencias()
    {
        DetailCanvas = GameObject.Find("DetailCanvas");
        MainCanvas = GameObject.Find("MainCanvas");
    }*/

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
		PanelInformacion panel = DetailCanvas.GetComponentInChildren<PanelInformacion> ();
		panel.MostrarInfoTema (this.gameObject);
	}

    public void SetIcono(Sprite nIcono)
    {
        Icono = nIcono;
        Image img = ObjetoIcono.GetComponent<Image>();
        img.sprite = Icono;
    }

    public void AbrirContenido(){
		ManagerDetail detalle = DetailCanvas.GetComponent<ManagerDetail> ();
		ManagerMenu menu = MainCanvas.GetComponent<ManagerMenu> ();
        ManagerReproduccion rep = DetailCanvas.GetComponent<ManagerReproduccion>();

        menu.AbrirContenido (Contenido);
        menu.ElementoAbierto = Nombre;
        detalle.AbrirInfoProgreso(Nombre, Contenido);
        rep.Abrir();
		MostrarInfo ();
	}

	public void CalcularProgreso(){
		int totalElementos = Contenido.Count;
		int totalCompleto = 0;
		foreach (GameObject item in Contenido) {
			Tema mt = item.GetComponent<Tema>();
			Elemento me = item.GetComponent<Elemento>();

			if(mt != null){
				if(mt.PorcentajeCompleto >= 1)
					totalCompleto += 1;
			}
			else{
				if(me.Completado)
					totalCompleto += 1;
			}
		}

        if(totalElementos > 0)
		    PorcentajeCompleto = (totalCompleto * 100) / totalElementos;

        ProgressBarBehaviour pbg = this.gameObject.GetComponentInChildren<ProgressBarBehaviour>();
        float valorActual = pbg.Value;
        pbg.DecrementValue(valorActual);
        pbg.IncrementValue(PorcentajeCompleto);

		if (TemaPadre != null) {
			Tema mt = TemaPadre.GetComponent<Tema>();
            if(EnDetalle)
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

    public void ActualizarPanelProgreso()
    {
        ManagerDetail detalle = DetailCanvas.GetComponent<ManagerDetail>();
        detalle.AbrirInfoProgreso(Nombre, Contenido);
    }

	public void EnClick(){
		ManagerMenu menu = MainCanvas.GetComponent<ManagerMenu> ();
		ManagerDetail detale = DetailCanvas.GetComponent<ManagerDetail> ();
        ManagerReproduccion rep = DetailCanvas.GetComponent<ManagerReproduccion>();

        if (!menu.EnAnimacion && !ManagerContexto.ACTIVO)
        {
            if (EnDetalle)
            {
                if (EsActual)
                {
                    menu.ReemplazarNivel();
                    menu.SubirNivel();
                    detale.SubirNivel();
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
                    if (Seleccionado)
                    {
                        MostrarInfo();
                        detale.LimpiarDetalle();
                        detale.AbrirInfoProgreso(Nombre, Contenido);
                        rep.Abrir();
                    }
                    else
                    {
                        AbrirContenido();
                        menu.BajarNivel();
                        detale.BajarNivel();
                    }
                }
                else
                {
                    detale.LimpiarDetalle();
                    menu.DetectarPosicion(this.gameObject, 0);
                }
            }
        }
	}	
}
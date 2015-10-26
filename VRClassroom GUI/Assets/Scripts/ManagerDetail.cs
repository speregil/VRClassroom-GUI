using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using ProgressBar;

public class ManagerDetail : MonoBehaviour {

    public      GameObject                      PrefBarra;
	public  	GameObject						BotonSubir;
	public		GameObject						MenuDetalle;
    public      GameObject                      PanelInfo;
    public      GameObject                      PanelProgreso;
    public      GameObject                      Grafica1;
    public      GameObject                      Grafica2;
    public      GameObject                      Grafica3;
    public      GameObject                      Grafica4;
    public      GameObject                      BotonPendientes;
    public      GameObject                      BotonGuardados;
    public		float							Desplazamiento;
    private     Stack<GameObject>               DatosGuardados;
	private 	List<GameObject>			    DatosPendientes;
    private     GameObject[]                    DatosActuales;
    private     ProgressBarBehaviour            pbb1;
    private     ProgressBarBehaviour            pbb2;
    private     ProgressBarBehaviour            pbb3;
    private     ProgressBarBehaviour            pbb4;
	private		bool							Desplazado;

	// Use this for initialization
	void Start () {
        DatosGuardados = new Stack<GameObject>();
        DatosPendientes = new List<GameObject>();
        DatosActuales = new GameObject[4];
		Desplazado = false;
        pbb1 = Grafica1.GetComponent<ProgressBarBehaviour>();
        pbb2 = Grafica2.GetComponent<ProgressBarBehaviour>();
        pbb3 = Grafica3.GetComponent<ProgressBarBehaviour>();
        pbb4 = Grafica4.GetComponent<ProgressBarBehaviour>();
    }

    public void AbrirInfoProgreso(string nombreTema, List<GameObject> ContenidoTema)
    {
        DatosActuales = new GameObject[4];

        pbb1.DecrementValue(pbb1.Value);
        pbb2.DecrementValue(pbb2.Value);
        pbb3.DecrementValue(pbb3.Value);
        pbb4.DecrementValue(pbb4.Value);

        Grafica1.SetActive(false);
        Grafica2.SetActive(false);
        Grafica3.SetActive(false);
        Grafica4.SetActive(false);

        Text titulo = PanelProgreso.GetComponentInChildren<Text>();
        titulo.text = nombreTema;

        int i = 0;
        foreach(GameObject item in ContenidoTema)
        {
            if (i < DatosActuales.Length)
            {
                DatosActuales[i] = item;
                i+= 1;
            }
            else
            {
                DatosPendientes.Add(item);
            }
        }

        GameObject elemetoActual = DatosActuales[0];

        if (elemetoActual != null)
        {
            Tema mt = elemetoActual.GetComponent<Tema>();
            Elemento me = elemetoActual.GetComponent<Elemento>();
            Grafica1.SetActive(true);
            Text txt = Grafica1.GetComponentInChildren<Text>();

            if (mt != null)
            {
                pbb1.IncrementValue(mt.PorcentajeCompleto);
                txt.text = mt.Nombre;
            }
            else
            {
                if (me.Completado)
                    pbb1.IncrementValue(100.0f);
                txt.text = me.Nombre;
            }

            elemetoActual = DatosActuales[1];

            if (elemetoActual != null)
            {
                mt = elemetoActual.GetComponent<Tema>();
                me = elemetoActual.GetComponent<Elemento>();
                Grafica2.SetActive(true);
                txt = Grafica2.GetComponentInChildren<Text>();

                if (mt != null)
                {
                    pbb2.IncrementValue(mt.PorcentajeCompleto);
                    txt.text = mt.Nombre;
                }
                else
                {
                    if (me.Completado)
                        pbb2.IncrementValue(100.0f);
                    txt.text = me.Nombre;
                }


                elemetoActual = DatosActuales[2];

                if (elemetoActual != null)
                {
                    mt = elemetoActual.GetComponent<Tema>();
                    me = elemetoActual.GetComponent<Elemento>();
                    Grafica3.SetActive(true);
                    txt = Grafica3.GetComponentInChildren<Text>();

                    if (mt != null)
                    {
                        pbb3.IncrementValue(mt.PorcentajeCompleto);
                        txt.text = mt.Nombre;
                    }
                    else
                    {
                        if (me.Completado)
                            pbb3.IncrementValue(100.0f);
                        txt.text = me.Nombre;
                    }


                    elemetoActual = DatosActuales[3];

                    if (elemetoActual != null)
                    {
                        mt = DatosActuales[3].GetComponent<Tema>();
                        me = DatosActuales[3].GetComponent<Elemento>();
                        Grafica4.SetActive(true);
                        txt = Grafica4.GetComponentInChildren<Text>();

                        if (mt != null)
                        {
                            pbb4.IncrementValue(mt.PorcentajeCompleto);
                            txt.text = mt.Nombre;
                        }
                        else
                        {
                            if (me.Completado)
                                pbb4.IncrementValue(100.0f);
                            txt.text = me.Nombre;
                        }
                    }
                }
            }
        }

        if (DatosPendientes.Count > 0)
            BotonPendientes.SetActive(true);
    }

    public void MostarDatosPendientes()
    {
        List<GameObject> listaActual = DatosPendientes;
        DatosPendientes = new List<GameObject>();

        foreach (GameObject item in DatosActuales)
        {
            DatosGuardados.Push(item);
        }

        BotonGuardados.SetActive(true);

        Text titulo = PanelProgreso.GetComponentInChildren<Text>();
        AbrirInfoProgreso(titulo.text, listaActual);

        if (DatosPendientes.Count > 0)
            BotonPendientes.SetActive(true);
        else
            BotonPendientes.SetActive(false);
    }

    public void MostrarDatosGuardados()
    {
        List<GameObject> datosRecuperados = new List<GameObject>();
        int i = 3;
        while(i > -1)
        {
            GameObject actual = DatosActuales[i];
            if (actual != null)
                DatosPendientes.Insert(0, actual);
            datosRecuperados.Insert(0, DatosGuardados.Pop());
            i -= 1;
        }

        Text titulo = PanelProgreso.GetComponentInChildren<Text>();
        AbrirInfoProgreso(titulo.text, datosRecuperados);

        if (DatosGuardados.Count > 0)
            BotonGuardados.SetActive(true);
        else
            BotonGuardados.SetActive(false);
    }

    public void LimpiarDetalle()
    {
        DatosActuales = new GameObject[4];
        DatosPendientes = new List<GameObject>();
        DatosGuardados = new Stack<GameObject>();
        BotonGuardados.SetActive(false);
        BotonPendientes.SetActive(false);
    }

    public void DesplazarMenu(int posicion){
		if (!Desplazado) {
			Vector3 nuevaPos = MenuDetalle.transform.position;
			if (posicion == 0) {
				nuevaPos.y += Desplazamiento;
			} else {
				nuevaPos.y -= Desplazamiento;
			}
			iTween.MoveTo (MenuDetalle, nuevaPos, 1);
		}
	}

	public void BajarNivel(){
        BotonSubir.SetActive(true);
        DesplazarMenu (0);
		Desplazado = true;
	}

	public void SubirNivel(){
        GameObject menu = GameObject.Find("MainCanvas");
        ManagerMenu mm = menu.GetComponent<ManagerMenu>();

        if (!mm.EnAnimacion)
        {
            if (Desplazado)
            {
                Desplazado = false;
                DesplazarMenu(1);
                if (mm.SubirNivel())
                {
                    BotonSubir.SetActive(false);
                }
            }
            else
            {
                if (mm.RecuperarNivel())
                {
                    BotonSubir.SetActive(false);
                }
            }
        }	
	}

    public void MostarDetalle(bool mostrar)
    {
        PanelInfo.SetActive(mostrar);
        PanelProgreso.SetActive(mostrar);
    }
}
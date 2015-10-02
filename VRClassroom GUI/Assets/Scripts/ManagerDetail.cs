using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using ProgressBar;

public class ManagerDetail : MonoBehaviour {

    public      GameObject                      PrefBarra;
	public  	GameObject						BotonSubir;
	public		GameObject						PanelItems;
	public		GameObject						MenuDetalle;
    public      GameObject                      PanelProgreso;
    public      GameObject                      Grafica1;
    public      GameObject                      Grafica2;
    public      GameObject                      Grafica3;
    public      GameObject                      Grafica4;
    public		float							Desplazamiento;
    private     Stack<GameObject>               DatosGuardados;
	private 	Queue<GameObject>			    DatosPendientes;
    private     ProgressBarBehaviour            pbb1;
    private     ProgressBarBehaviour            pbb2;
    private     ProgressBarBehaviour            pbb3;
    private     ProgressBarBehaviour            pbb4;
	private		bool							Desplazado;

	// Use this for initialization
	void Start () {
        DatosGuardados = new Stack<GameObject>();
        DatosPendientes = new Queue<GameObject>();
		Desplazado = false;
        pbb1 = Grafica1.GetComponent<ProgressBarBehaviour>();
        pbb2 = Grafica2.GetComponent<ProgressBarBehaviour>();
        pbb3 = Grafica3.GetComponent<ProgressBarBehaviour>();
        pbb4 = Grafica4.GetComponent<ProgressBarBehaviour>();
    }

    public void AbrirInfoProgreso()
    {
        PanelProgreso.SetActive(true);
        pbb1.IncrementValue(15.0f);
        pbb2.IncrementValue(85.0f);
        pbb3.IncrementValue(100.0f);
        pbb4.IncrementValue(65.0f);
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
		Button btnSubir = BotonSubir.GetComponent<Button>();
		btnSubir.interactable = true;
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
                    Button btnSubir = BotonSubir.GetComponent<Button>();
                    btnSubir.interactable = false;
                }
            }
            else
            {
                if (mm.RecuperarNivel())
                {
                    Button btnSubir = BotonSubir.GetComponent<Button>();
                    btnSubir.interactable = false;
                }
            }
        }	
	}
}
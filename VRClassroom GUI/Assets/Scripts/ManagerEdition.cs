using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ManagerEdition : MonoBehaviour {

	public	GameObject	    PrefTema;
	public	GameObject	    PrefElemento;
    public  GameObject      MenuCrear;
    public  GameObject      MainOptions;
    public  GameObject      PanelCrear;
    public  GameObject      InputNombre;
    public  GameObject      Principal;
    public  GameObject      Detalle;
    public  GameObject      Temporal;
    public  string          NombreUsuario;
    private string          Estado;
    private string          CreacionActual;
    private ManagerMenu     mPrincipal;
    private ManagerDetail   mDetalle;

    private const string ABIERTO = "ABIERTO";
    private const string CERRADO = "CERRADO";
    private const string INPUT = "INPUT";
    private const string CREAR_TEMA = "CREAR_TEMA";
    private const string CREAR_ELEMENTO = "CREAR_ELEMENTO";

    // Use this for initialization
    void Awake()
    {
        mPrincipal = Principal.GetComponent<ManagerMenu>();
        mDetalle = Detalle.GetComponent<ManagerDetail>();
        Estado = CERRADO;

        mPrincipal.Inicializar();
        mPrincipal.AnchoElementos = GetPrefabWidth();
        mPrincipal.SetParametrosIniciales();
    }

    void Start () {
        Test4 test = GetComponent<Test4>();
        test.OnTest();
    }

	public GameObject CrearTema(string nNombre, string nAutor, DateTime nFecha){
		GameObject nuevoTema = GameObject.Instantiate (PrefTema);
		Tema mTema = nuevoTema.GetComponent<Tema> ();
		mTema.Nombre = nNombre;
		mTema.Autor = nAutor;
		mTema.FechaCreacion = nFecha;
       // mTema.InicializarReferencias();

		Text mText = nuevoTema.GetComponentInChildren<Text> ();
		mText.text = nNombre;

        Tema padre = mPrincipal.PadreActual();

        if (padre != null)
        {
            mTema.TemaPadre = padre;
            padre.AgregarContenido(nuevoTema);
        }

        nuevoTema.GetComponent<Collider>().enabled = false;
        nuevoTema.transform.SetParent(Temporal.transform);
		return nuevoTema;
	}

    public void AceptarInput()
    {
        string nombre = InputNombre.GetComponent<Text>().text;
        DateTime fechaActual = DateTime.Now;

        switch (CreacionActual)
        {
            case CREAR_TEMA:
                GameObject nuevoTema = CrearTema(nombre, NombreUsuario, fechaActual);
                ManagerMenu menu = Principal.GetComponent<ManagerMenu>();
                menu.Agregar(nuevoTema);
                Volver();
                break;
            case CREAR_ELEMENTO:
                break;
            default:
                break;
        }
    }

	public GameObject CrearElemento(string nNombre, string nDescripcion){
		GameObject nuevoElemento = GameObject.Instantiate (PrefElemento);
		Elemento mElemento = nuevoElemento.GetComponent<Elemento> ();
		mElemento.Nombre = nNombre;
		mElemento.Descripcion = nDescripcion;

		Text mText = nuevoElemento.GetComponentInChildren<Text> ();
		mText.text = nNombre;

        nuevoElemento.GetComponent<Collider>().enabled = false;
        nuevoElemento.transform.SetParent(Temporal.transform);
        return nuevoElemento;
	}

    public void AbrirMenuCrear()
    {
        if (!(ManagerReproduccion.ACTIVO || ManagerContexto.ACTIVO))
        {
            MenuCrear.SetActive(true);
            mPrincipal.MostarMenu(false);
            mDetalle.MostarDetalle(false);
            Estado = ABIERTO;
        }
    }

    public void AbrirInputCrear(int objetivo)
    {
        MainOptions.SetActive(false);
        PanelCrear.SetActive(true);
        Estado = INPUT;

        switch (objetivo)
        {
            case 0:
                CreacionActual = CREAR_TEMA;
                break;
            case 1:
                CreacionActual = CREAR_ELEMENTO;
                break;
            default:
                break;
        }
    }
    public void Volver()
    {
        switch (Estado)
        {
            case ABIERTO:
                Estado = CERRADO;
                mPrincipal.MostarMenu(true);
                mDetalle.MostarDetalle(true);
                MenuCrear.SetActive(false);
                break;
            case INPUT:
                MainOptions.SetActive(true);
                PanelCrear.SetActive(false);
                Estado = ABIERTO;
                break;
            default:
                break;
        }
        
    }

	public float GetPrefabWidth(){
		GameObject testPrefab = GameObject.Instantiate (PrefTema);
		RectTransform rt = testPrefab.GetComponent<RectTransform> ();
		return rt.rect.width;
	}
}
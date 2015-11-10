using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ManagerContexto : MonoBehaviour {

   
    public GameObject   BotonSalir;
    public GameObject   BotonVolver;
    public GameObject   BotonNotas;
    public GameObject   BotonWeb;
    public GameObject   PanelGrabacion;
    public GameObject   BotonGrabar;
    public GameObject   BotonDetener;
    public GameObject   BotonCerrarGrabacion;
    public GameObject   TextoNombre;
    public GameObject   MainMenu;
    public GameObject   MenuContexto;
    public Sprite       BotonNormal;
    public Sprite       BotonSeleccionado;
    public GameObject   ObjReticulo;
    public string       ElementoAbierto;

    private LinkedListNode<GameObject> BotonActual;
    private LinkedListNode<GameObject> BotonAbierto;
    private LinkedList<GameObject> MenuActual;
    private LinkedList<GameObject> MenuPrincipal;
    private LinkedList<GameObject> MenuGrabacion;
    private List<string> GrabacionesPendientes;
    private int contadorScroll;
    private GameObject CameraMenu;
    private GameObject AuxiliarMainCamera;

    public static string Estado;
    public static bool ESTA_ACTIVO = false;
    public const string APAGADO = "APAGADO";
    public const string ENCENDIDO = "ENCENDIDO";
    public const string GRABACION = "GRABACION";
    public const string BLOQUEADO = "BLOQUEADO";

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        MenuPrincipal = new LinkedList<GameObject>();
        MenuGrabacion = new LinkedList<GameObject>();
        GrabacionesPendientes = new List<string>();

        MenuPrincipal.AddLast(BotonSalir);   
        MenuPrincipal.AddLast(BotonVolver);     
        MenuPrincipal.AddLast(BotonNotas);
        MenuPrincipal.AddLast(BotonWeb);

        MenuGrabacion.AddLast(BotonCerrarGrabacion);
        MenuGrabacion.AddLast(BotonDetener);
        MenuGrabacion.AddLast(BotonGrabar);

        MenuActual = MenuPrincipal;
        BotonActual = MenuActual.First;
        Image img = BotonActual.Value.GetComponentInChildren<Image>();
        img.sprite = BotonSeleccionado;

        PanelGrabacion.SetActive(false);
        MenuContexto.SetActive(false);
        Estado = APAGADO;
        contadorScroll = 0;
    }

    public void OnLevelWasLoaded(int level)
    {
        Canvas cv = GetComponent<Canvas>();
        cv.worldCamera = Camera.main;
        ManagerMenu mm = MainMenu.GetComponentInChildren<ManagerMenu>();
        ElementoAbierto = mm.ElementoAbierto;
        if(level > 1)
            MainMenu.SetActive(false);

    }

    void Update()
    {
        Button mButton;

        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetMouseButtonDown(1))
        {
            

            switch (Estado)
            {
                case APAGADO:
                    MenuContexto.SetActive(true);
                    //ObjReticulo.SetActive(false);
                    ESTA_ACTIVO = true;
                    Estado = ENCENDIDO;
                    break;
                case ENCENDIDO:
                    mButton = BotonActual.Value.GetComponent<Button>();
                    BotonAbierto = BotonActual;
                    mButton.onClick.Invoke();
                    break;
                case GRABACION:
                    mButton = BotonActual.Value.GetComponent<Button>();
                    mButton.onClick.Invoke();
                    break;
            }
        }

        if (ESTA_ACTIVO)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                CambiarSeleccion(1);
                contadorScroll += 20;
                ESTA_ACTIVO = false;
            }

            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                CambiarSeleccion(-1);
                contadorScroll += 20;
                ESTA_ACTIVO = false;
            }     
        }
        else
        {
            contadorScroll -= 1;

            if (contadorScroll <= 0)
                ESTA_ACTIVO = true;
        }
    }

    public void CambiarSeleccion(int direccion)
    {
        Debug.Log("Cambiando seleccion");
        Image img = BotonActual.Value.GetComponentInChildren<Image>();
        img.sprite = BotonNormal;

        LinkedListNode<GameObject> temp = direccion > 0 ? BotonActual.Next : BotonActual.Previous;

        if(temp != null)
        {
            BotonActual = temp;
            img = BotonActual.Value.GetComponentInChildren<Image>();
            img.sprite = BotonSeleccionado;
        }
        else
        {
            BotonActual =  direccion > 0 ? MenuActual.First : MenuActual.Last;
            img = BotonActual.Value.GetComponentInChildren<Image>();
            img.sprite = BotonSeleccionado;
        }
    }

    public void Salir()
    {
        MenuContexto.SetActive(false);
        //ObjReticulo.SetActive(true);
        Estado = APAGADO;
        ESTA_ACTIVO = false;
    }

    public void Volver()
    {
        MainMenu.SetActive(true);
        ManagerMenu mm = MainMenu.GetComponentInChildren<ManagerMenu>();
        
        foreach(string nota in GrabacionesPendientes)
        {
            mm.GuardarNota(nota);
        }
        
        Estado = APAGADO;
        ESTA_ACTIVO = false;
        MenuContexto.SetActive(false);
        //ObjReticulo.SetActive(true);
        Application.LoadLevel("MainMenu");
    }

    public void Notas()
    {
        Estado = GRABACION;
        PanelGrabacion.SetActive(true);
        MenuContexto.SetActive(false);
        MenuActual = MenuGrabacion;

        foreach(GameObject boton in MenuActual)
        {
            Image temp = boton.GetComponentInChildren<Image>();
            temp.sprite = BotonNormal;
        }

        BotonActual = MenuActual.First;
        Image img = BotonActual.Value.GetComponentInChildren<Image>();
        img.sprite = BotonSeleccionado;

        ManagerMenu mm = MainMenu.GetComponentInChildren<ManagerMenu>();
        Text nombreTitulo = TextoNombre.GetComponent<Text>();
        if (mm != null)
            nombreTitulo.text = mm.ElementoAbierto;
        else
            nombreTitulo.text = ElementoAbierto;
    }

    public void archivarGrabacion(string nota)
    {
        GrabacionesPendientes.Add(nota);
    }

    public void Web()
    {
        Debug.Log("Modulo web todavia no existe");
    }
    
    public void CerrarVentana(GameObject ventana)
    {
        Estado = ENCENDIDO;
        ventana.SetActive(false);
        MenuContexto.SetActive(true);
        MenuActual = MenuPrincipal;
        BotonActual = BotonAbierto;
    }

    public IEnumerator Esperar(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
    }
}

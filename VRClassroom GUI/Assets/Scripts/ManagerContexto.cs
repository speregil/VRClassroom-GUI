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
    public string       ElementoAbierto;

    private LinkedListNode<GameObject> BotonActual;
    private LinkedListNode<GameObject> BotonAbierto;
    private LinkedList<GameObject> MenuActual;
    private LinkedList<GameObject> MenuPrincipal;
    private LinkedList<GameObject> MenuGrabacion;
    private List<string> GrabacionesPendientes;

    public static string Estado;
    public static bool ACTIVO = false;
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

        MenuGrabacion.AddLast(BotonGrabar);
        MenuGrabacion.AddLast(BotonDetener);
        MenuGrabacion.AddLast(BotonCerrarGrabacion);

        MenuActual = MenuPrincipal;
        BotonActual = MenuActual.First;
        Image img = BotonActual.Value.GetComponentInChildren<Image>();
        img.sprite = BotonSeleccionado;

        PanelGrabacion.SetActive(false);
        MenuContexto.SetActive(false);
        Estado = APAGADO;
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

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            switch (Estado)
            {
                case APAGADO:
                    MenuContexto.SetActive(true);
                    ACTIVO = true;
                    Estado = ENCENDIDO;
                    break;
                case ENCENDIDO:
                    CambiarSeleccion();
                    break;
                case GRABACION:
                    CambiarSeleccion();
                    break;
            }
        }

        if (ACTIVO) { 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Button mButton = BotonActual.Value.GetComponent<Button>();

                switch (Estado)
                {
                    case ENCENDIDO:
                        BotonAbierto = BotonActual;
                        mButton.onClick.Invoke();
                        break;
                    case GRABACION:
                        mButton.onClick.Invoke();
                        break;
                }
            }
        }
    }

    public void CambiarSeleccion()
    {
        Image img = BotonActual.Value.GetComponentInChildren<Image>();
        img.sprite = BotonNormal;

        LinkedListNode<GameObject> temp = BotonActual.Next;
        if(temp != null)
        {
            BotonActual = temp;
            img = BotonActual.Value.GetComponentInChildren<Image>();
            img.sprite = BotonSeleccionado;
        }
        else
        {
            BotonActual = MenuActual.First;
            img = BotonActual.Value.GetComponentInChildren<Image>();
            img.sprite = BotonSeleccionado;
        }
    }

    public void Salir()
    {
        MenuContexto.SetActive(false);
        Estado = APAGADO;
        ACTIVO = false;
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
        ACTIVO = false;
        MenuContexto.SetActive(false);
        Application.LoadLevel("MainMenu");
    }

    public void Notas()
    {
        Estado = GRABACION;
        PanelGrabacion.SetActive(true);
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
        MenuActual = MenuPrincipal;
        BotonActual = BotonAbierto;
    }
}

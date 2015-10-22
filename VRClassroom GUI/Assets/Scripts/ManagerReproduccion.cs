using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ManagerReproduccion : MonoBehaviour {

    public GameObject PanelReproducciones;
    public GameObject Elemento1;
    public GameObject Elemento2;
    public GameObject Elemento3;
    public GameObject Elemento4;
    public GameObject BotonAdelante;
    public GameObject BotonAtras;
    public GameObject BotonSalir;
    public GameObject TextoElemento;
    public GameObject TextoAviso;
    public Sprite BotonNormal;
    public Sprite BotonSeleccionado;
    public Sprite LabelSeleccionado;
    public Sprite LabelNormal;

    private LinkedListNode<GameObject> BotonActual;
    private LinkedList<GameObject> ElementosMenu;
    private Stack<string> DatosGuardados;
    private List<string> DatosPendientes;
    private string[] DatosActuales;

    public static bool ACTIVO = false;
    // Use this for initialization
    void Start () {
        ElementosMenu = new LinkedList<GameObject>();
        ElementosMenu.AddLast(Elemento1);
        ElementosMenu.AddLast(Elemento2);
        ElementosMenu.AddLast(Elemento3);
        ElementosMenu.AddLast(Elemento4);
        ElementosMenu.AddLast(BotonAtras);
        ElementosMenu.AddLast(BotonAdelante);
        ElementosMenu.AddLast(BotonSalir);

        Image img;
        if (ElementosMenu.First.Value.activeSelf)
        {
            BotonActual = ElementosMenu.First;
            img = BotonActual.Value.GetComponent<Image>();
            img.sprite = LabelSeleccionado;
        }
        else
        {
            BotonActual = ElementosMenu.Last;
            img = BotonActual.Value.GetComponentInChildren<Image>();
            img.sprite = BotonSeleccionado;
        }

        DatosGuardados = new Stack<string>();
        DatosPendientes = new List<string>();
        DatosActuales = new string[4];

        PanelReproducciones.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (ACTIVO)
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                CambiarSeleccion();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Button esBoton = BotonActual.Value.GetComponent<Button>();
                Button esLabel = BotonActual.Value.GetComponentInChildren<Button>();
                if (esBoton != null)
                {
                    esBoton.onClick.Invoke();
                }
                else
                {
                    esLabel.onClick.Invoke();
                }
            }
        }
    }

    public void Abrir()
    {
        if (!(ACTIVO || ManagerContexto.ACTIVO))
        {
            ManagerContexto.Estado = ManagerContexto.BLOQUEADO;
            PanelReproducciones.SetActive(true);
            ACTIVO = true;

            GameObject main = GameObject.Find("MainCanvas");
            ManagerMenu mm = main.GetComponent<ManagerMenu>();
            TextoElemento.GetComponent<Text>().text = mm.ElementoAbierto;
            List<string> listaDatos = mm.RecuperarNotas();
            CargarLista(listaDatos);
            DibujarDatos();

            if (DatosPendientes.Count > 0)
                BotonAdelante.SetActive(true);
        }
    }

    public void Cerrar()
    {
        ACTIVO = false;
        DatosGuardados = new Stack<string>();
        DatosPendientes = new List<string>();
        DatosActuales = new string[4];
        BotonAdelante.SetActive(false);
        BotonAtras.SetActive(false);
        ManagerContexto.Estado = ManagerContexto.APAGADO;
        PanelReproducciones.SetActive(false);
    }

    public void CargarLista(List<string> listaDatos)
    {
        DatosActuales = new string[4];
        int i = 0;
        foreach (string item in listaDatos)
        {
            if (i < DatosActuales.Length)
            {
                DatosActuales[i] = item;
                i += 1;
            }
            else
            {
                DatosPendientes.Add(item);
            }
        }
    }

    public void DibujarDatos()
    {
        Elemento1.SetActive(false);
        Elemento2.SetActive(false);
        Elemento3.SetActive(false);
        Elemento4.SetActive(false);

        Text txt;
        if (DatosActuales[0] != null)
        {
            Elemento1.SetActive(true);
            txt = Elemento1.GetComponentInChildren<Text>();
            txt.text = DatosActuales[0];

            if (DatosActuales[1] != null)
            {
                Elemento2.SetActive(true);
                txt = Elemento2.GetComponentInChildren<Text>();
                txt.text = DatosActuales[1];

                if (DatosActuales[2] != null)
                {
                    Elemento3.SetActive(true);
                    txt = Elemento3.GetComponentInChildren<Text>();
                    txt.text = DatosActuales[2];

                    if (DatosActuales[3] != null)
                    {
                        Elemento4.SetActive(true);
                        txt = Elemento4.GetComponentInChildren<Text>();
                        txt.text = DatosActuales[3];
                    }
                }
            }
        }
    }

    public void CambiarSeleccion()
    {
        Button esBoton = BotonActual.Value.GetComponent<Button>();
        Button esLabel = BotonActual.Value.GetComponentInChildren<Button>();
        Image img;
        if (esBoton != null)
        {
            img = BotonActual.Value.GetComponentInChildren<Image>();
            img.sprite = BotonNormal;
        }
        else
        {
            img = BotonActual.Value.GetComponent<Image>();
            img.sprite = LabelNormal;
        }

        bool encontroActivo = false;

        while (!encontroActivo)
        {
            LinkedListNode<GameObject> temp = BotonActual.Next;
            if (temp != null)
            {
                BotonActual = temp;
                if (BotonActual.Value.activeSelf)
                {
                    encontroActivo = true;
                    esBoton = BotonActual.Value.GetComponent<Button>();
                    esLabel = BotonActual.Value.GetComponentInChildren<Button>();
                    if (esBoton != null)
                    {
                        img = BotonActual.Value.GetComponentInChildren<Image>();
                        img.sprite = BotonSeleccionado;
                    }
                    else
                    {
                        img = BotonActual.Value.GetComponent<Image>();
                        img.sprite = LabelSeleccionado;
                    }
                }
            }
            else
            {
                BotonActual = ElementosMenu.First;

                if (BotonActual.Value.activeSelf)
                {
                    encontroActivo = true;
                    img = BotonActual.Value.GetComponent<Image>();
                    img.sprite = LabelSeleccionado;
                }
                else
                {
                    BotonActual = ElementosMenu.Last;
                    img = BotonActual.Value.GetComponentInChildren<Image>();
                    img.sprite = BotonSeleccionado;
                    encontroActivo = true;
                }
            }
        }
    }

    public void Avanzar()
    {
        List<string> listaActual = DatosPendientes;
        DatosPendientes = new List<string>();

        foreach (string item in DatosActuales)
        {
            DatosGuardados.Push(item);
        }

        BotonAtras.SetActive(true);
        CargarLista(listaActual);
        DibujarDatos();

        if (DatosPendientes.Count > 0)
            BotonAdelante.SetActive(true);
        else
        {
            BotonAdelante.SetActive(false);
            BotonAtras.GetComponentInChildren<Image>().sprite = BotonSeleccionado;
            BotonActual = BotonActual.Previous;
        }
    }

    public void Retroceder()
    {
        List<string> datosRecuperados = new List<string>();
        int i = 3;
        while (i > -1)
        {
            string actual = DatosActuales[i];
            if (actual != null)
                DatosPendientes.Insert(0, actual);
            datosRecuperados.Insert(0, DatosGuardados.Pop());
            i -= 1;
        }

        BotonAdelante.SetActive(true);
        CargarLista(datosRecuperados);
        DibujarDatos();

        if (DatosGuardados.Count > 0)
            BotonAtras.SetActive(true);
        else
        {
            BotonAtras.SetActive(false);
            BotonAdelante.GetComponentInChildren<Image>().sprite = BotonSeleccionado;
            BotonActual = BotonActual.Next;
        }
    }
}

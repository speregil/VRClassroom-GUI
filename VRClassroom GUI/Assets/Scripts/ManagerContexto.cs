using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ManagerContexto : MonoBehaviour {

    public GameObject   BotonSalir;
    public GameObject   BotonVolver;
    public GameObject   BotonNotas;
    public GameObject   BotonWeb;
    public GameObject   MainMenu;
    public GameObject   MenuContexto;
    public Sprite       BotonNormal;
    public Sprite       BotonSeleccionado;
    public string       Estado;

    private Button mBotonSalir;
    private Button mBotonVolver;
    private Button mBotonNotas;
    private Button mBotonWeb;
    private LinkedListNode<GameObject> BotonActual;
    private LinkedList<GameObject> Menu;

    public const string APAGADO = "APAGADO";
    public const string ENCENDIDO = "ENCENDIDO";

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Menu = new LinkedList<GameObject>();

        mBotonSalir = BotonSalir.GetComponent<Button>();
        Menu.AddLast(BotonSalir);
        mBotonVolver = BotonVolver.GetComponent<Button>();
        Menu.AddLast(BotonVolver);
        mBotonNotas = BotonNotas.GetComponent<Button>();
        Menu.AddLast(BotonNotas);
        mBotonWeb = BotonWeb.GetComponent<Button>();
        Menu.AddLast(BotonWeb);

        BotonActual = Menu.First;
        Image img = BotonActual.Value.GetComponentInChildren<Image>();
        img.sprite = BotonSeleccionado;

        MenuContexto.SetActive(false);
        Estado = APAGADO;
    }

    public void OnLevelWasLoaded(int level)
    {
        Canvas cv = GetComponent<Canvas>();
        cv.worldCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            switch (Estado)
            {
                case APAGADO:
                    MenuContexto.SetActive(true);
                    Estado = ENCENDIDO;
                    break;
                case ENCENDIDO:
                    CambiarSeleccion();
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Estado.Equals(ENCENDIDO))
            {
                Button mButton = BotonActual.Value.GetComponent<Button>();
                mButton.onClick.Invoke();
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
            BotonActual = Menu.First;
            img = BotonActual.Value.GetComponentInChildren<Image>();
            img.sprite = BotonSeleccionado;
        }
    }

    public void Salir()
    {
        MenuContexto.SetActive(false);
        Estado = APAGADO;
    }

    public void Volver()
    {
        MainMenu.SetActive(true);
        MenuContexto.SetActive(false);
        Estado = APAGADO;
        Application.LoadLevel("MainMenu");
    }
    public void Notas()
    {
        Debug.Log("Modulo de notas todavia no existe");
    }

    public void Web()
    {
        Debug.Log("Modulo web todavia no existe");
    } 
}

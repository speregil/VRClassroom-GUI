using UnityEngine;
using System;
using System.Collections;

public class Test4 : MonoBehaviour
{

    public GameObject MainCanvas;
    public Sprite IconoSolarSystem;
    private ManagerMenu menu;
    private ManagerEdition editor;

    // Use this for initialization
    void Start()
    {
        
    }

    public void OnTest()
    {
        menu = MainCanvas.GetComponent<ManagerMenu>();
        editor = GetComponent<ManagerEdition>();

        GameObject TemaPrueba = CrearTema("Tema Prueba", "Sebastian", new DateTime(2015, 10, 13), null);
        GameObject VRClassroom = CrearTema("VRClassroom", "Andrés Gomez", new DateTime(2015, 06, 13), null);

        GameObject Elemento2a = CrearElemento("Elemento 2a", "Descripcion 2a", TemaPrueba);
        GameObject Elemento2b = CrearElemento("Elemento 2b", "Descripcion 2b", TemaPrueba);
        GameObject Elemento2c = CrearElemento("Elemento 2c", "Descripcion 2c", TemaPrueba);
        GameObject Elemento2d = CrearElemento("Elemento 2d", "Descripcion 2d", TemaPrueba);
        GameObject Elemento2e = CrearElemento("Elemento 2e", "Descripcion 2e", TemaPrueba);
        GameObject Elemento2f = CrearElemento("Elemento 2f", "Descripcion 2f", TemaPrueba);
        GameObject Elemento2g = CrearElemento("Elemento 2g", "Descripcion 2g", TemaPrueba);
        GameObject Elemento2h = CrearElemento("Elemento 2h", "Descripcion 2h", TemaPrueba);
        GameObject Elemento2i = CrearElemento("Elemento 2i", "Descripcion 2i", TemaPrueba);

        GameObject SolarSystem = CrearElemento("Solar System", "Un modelo del sistema solar que muestra el funcionamiento basico de Unity", VRClassroom);
        Elemento ss = SolarSystem.GetComponent<Elemento>();
        ss.NombreEjecutable = "Solar_System";
        ss.SetIcono(IconoSolarSystem);

        menu.Agregar(TemaPrueba);
        menu.Agregar(VRClassroom);
    }

    public GameObject CrearTema(string nombre, string autor, DateTime fecha, GameObject padre)
    {
        GameObject nuevoTema = editor.CrearTema(nombre, autor, fecha);
        if (padre != null)
        {
            Tema mt = nuevoTema.GetComponent<Tema>();
            Tema mtPadre = padre.GetComponent<Tema>();
            mt.TemaPadre = mtPadre;
            mtPadre.AgregarContenido(nuevoTema);
        }
        return nuevoTema;
    }

    public GameObject CrearElemento(string nombre, string descripcion, GameObject padre)
    {
        GameObject nuevoElemento = editor.CrearElemento(nombre, descripcion);
        if (padre != null)
        {
            Elemento me = nuevoElemento.GetComponent<Elemento>();
            Tema mtPadre = padre.GetComponent<Tema>();
            me.TemaPadre = mtPadre;
            mtPadre.AgregarContenido(nuevoElemento);
        }
        return nuevoElemento;
    }
}

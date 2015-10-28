using UnityEngine;
using System;
using System.Collections;

public class Test4 : MonoBehaviour
{

    public GameObject MainCanvas;
    public Sprite IconoSolarSystem;
    public Sprite IconoWebPage;
    public Sprite IconoClassroom;
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

        menu.Agregar(TemaPrueba);
        menu.Agregar(VRClassroom);

        GameObject SubTema1 = CrearTema("SubTema 1", "Sebastian", new DateTime(2015, 10, 23), TemaPrueba);
        GameObject SubTema2 = CrearTema("SubTema 2", "Sebastian", new DateTime(2015, 10, 23), TemaPrueba);
        GameObject Elemento2a = CrearElemento("Elemento 2a", "Descripcion 2a", SubTema1);
        GameObject Elemento2b = CrearElemento("Elemento 2b", "Descripcion 2b", SubTema1);
        GameObject Elemento2c = CrearElemento("Elemento 2c", "Descripcion 2c", SubTema2);
        GameObject Elemento2d = CrearElemento("Elemento 2d", "Descripcion 2d", SubTema2);

        GameObject Coordenadas = CrearTema("Sistemas de coordenadas", "Sebastian", new DateTime(2015, 10, 23), VRClassroom);
        GameObject Ejemplos = CrearTema("Ejemplos", "Sebastian", new DateTime(2015, 10, 23), VRClassroom);

        GameObject SolarSystem = CrearElemento("Solar System", "Un modelo del sistema solar que muestra el funcionamiento basico de Unity", Ejemplos);
        Elemento ss = SolarSystem.GetComponent<Elemento>();
        ss.NombreEjecutable = "Solar_System";
        ss.SetIcono(IconoSolarSystem);

        GameObject WebPage = CrearElemento("Sistema de coordenadas en Unity", "Documentación de como Unity3D implementa y usa sus sitema de coordenadas", Coordenadas);
        Elemento wp = WebPage.GetComponent<Elemento>();
        wp.NombreEjecutable = "Web_Page_Viewer";
        wp.SetIcono(IconoWebPage);

        GameObject classroom = CrearElemento("Sistema de coordenadas Leccion 1", "Lección de introducción al sistema de coordenadas", Coordenadas);
        Elemento cr = classroom.GetComponent<Elemento>();
        cr.NombreEjecutable = "Classroom";
        cr.SetIcono(IconoClassroom);
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

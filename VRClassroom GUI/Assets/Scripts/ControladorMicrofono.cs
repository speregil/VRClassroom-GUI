//	Pequeño controlador sencillo que permite tomar el sonido del
//  microfono y grabarlo en wav haciendo uso de SavWav modificado.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

//	Esta clase se encarga de controlar el comportamiento del microfono
//	y ofrecer funcionalidades basicas sobre este.
public class ControladorMicrofono : MonoBehaviour
{

    //---------------------------------------------------------------------------------------
    // Atributos
    //---------------------------------------------------------------------------------------

    public AudioSource aud;
    public Text nombreArchivo;
    public Text textoAviso;
    private List<AudioClip> audList;
    private bool isRecording;
    private int nivelActual;
    private string nombreUsuario;
    private string nombreCurso;

    //---------------------------------------------------------------------------------------
    // Constructor
    //---------------------------------------------------------------------------------------

    // Use this for initialization
    void Start()
    {
        isRecording = false;
        audList = new List<AudioClip>();
        nivelActual = 0;
        nombreUsuario = "";
        nombreCurso = "";
    }

    //---------------------------------------------------------------------------------------
    // Metodos
    //---------------------------------------------------------------------------------------

    // Update is called once per frame
    void Update()
    {
        
    }

    //	Inicia el proceso de grabacion
    public void Grabar()
    {
        isRecording = true;
        audList.Clear();
        nivelActual = Application.loadedLevel;
        textoAviso.text = "Grabando...";
        StartCoroutine(Grabando());
    }

    //	Finaliza el proceso de grabacion
    public void Detener()
    {
        isRecording = false;
        textoAviso.text = "Guardando...";
        Microphone.End(null);
    }

    //	Reproduce la informacion grabada
    public void Reproducir()
    {
        StartCoroutine(Reproduciendo());
    }

    //	Graba
    IEnumerator Grabando()
    {
        AudioClip audClip;
        while (isRecording)
        {
            audClip = Microphone.Start(null, false, 10, 44100);
            yield return new WaitForSeconds(10);
            audList.Add(audClip);
        }
        
        DateTime fechaActual = DateTime.Now;
        SavWav.SaveList("Resources/Data/" + nombreArchivo.text + " " + fechaActual.Year + "-" + fechaActual.Month + "-" + fechaActual.Day + " " + fechaActual.Hour + "" + fechaActual.Minute, audList);
        textoAviso.text = "Listo";
    }

    //	Reproduce
    IEnumerator Reproduciendo()
    {
        WWW www = new WWW("file://");
        yield return www;
        AudioClip audioClip = www.GetAudioClip(false, false);
        aud.clip = audioClip;
        aud.Play();
        yield return new WaitForSeconds(aud.clip.length);
    }

    //Retorna el numero de grabacion actual para este modulo
    private int NumeroGrabacion()
    {
        string[] archivos = Directory.GetFiles(Path.Combine(Application.dataPath, "Data"));
        int numTemp = -1;
        string prefijo = nombreUsuario + "_" + nombreCurso + "_" + nivelActual + "_";
        Debug.Log("pf: " + prefijo);
        foreach (string archivo in archivos)
        {
            string textTemp = archivo.Substring(archivo.LastIndexOf("\\") + 1);
            Debug.Log(textTemp);
            if (textTemp.StartsWith(prefijo) && textTemp.EndsWith(".wav"))
            {
                int posicionInicial = prefijo.Length;
                int distancia = textTemp.LastIndexOf(".wav") - posicionInicial;
                Debug.Log("pI: " + posicionInicial + " dis: " + distancia);
                textTemp = textTemp.Substring(posicionInicial, distancia);
                Debug.Log("num: " + textTemp);
                if (int.Parse(textTemp) > numTemp)
                    numTemp = int.Parse(textTemp);
            }
        }

        return numTemp + 1;
    }
}
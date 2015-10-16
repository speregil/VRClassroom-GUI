//	Pequeño controlador sencillo que permite tomar el sonido del
//  microfono y grabarlo en wav haciendo uso de SavWav modificado.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

//	Esta clase se encarga de controlar el comportamiento del microfono
//	y ofrecer funcionalidades basicas sobre este.
public class ControladorMicrofono : MonoBehaviour
{

    //---------------------------------------------------------------------------------------
    // Atributos
    //---------------------------------------------------------------------------------------

    public AudioSource aud;
    private List<AudioClip> audList;
    private bool isRecording;
    private int nivelActual;
    private string nombreUsuario;
    private string nombreCurso;
    private int numGrabacion;

    //---------------------------------------------------------------------------------------
    // Constructor
    //---------------------------------------------------------------------------------------

    // No permite que el objeto se destruya al cambiar la escena
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        isRecording = false;
        audList = new List<AudioClip>();
        nivelActual = 0;
        nombreUsuario = "";
        nombreCurso = "";
        numGrabacion = 0;
    }

    //---------------------------------------------------------------------------------------
    // Metodos
    //---------------------------------------------------------------------------------------

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            if (isRecording)
            {
                Detener();
            }
            else
            {
                Grabar();
            }
        }
    }

    //	Inicia el proceso de grabacion
    public void Grabar()
    {
        isRecording = true;
        if (Application.loadedLevel != nivelActual)
        {
            audList.Clear();
            nivelActual = Application.loadedLevel;
            numGrabacion = NumeroGrabacion();
        }
        StartCoroutine(Grabando());
    }

    //	Finaliza el proceso de grabacion
    public void Detener()
    {
        isRecording = false;
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
        SavWav.SaveList("Record/" + nombreUsuario + "_" + nombreCurso + "_" + nivelActual + "_" + numGrabacion, audList);
    }

    //	Reproduce
    IEnumerator Reproduciendo()
    {
        WWW www = new WWW("file://" + Application.dataPath + "/Record/" + nombreUsuario + "_" + nombreCurso + "_" + nivelActual + "_" + numGrabacion + ".wav");
        yield return www;
        AudioClip audioClip = www.GetAudioClip(false, false);
        aud.clip = audioClip;
        aud.Play();
        yield return new WaitForSeconds(aud.clip.length);
    }

    //Retorna el numero de grabacion actual para este modulo
    private int NumeroGrabacion()
    {
        string[] archivos = Directory.GetFiles(Path.Combine(Application.dataPath, "Record"));
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
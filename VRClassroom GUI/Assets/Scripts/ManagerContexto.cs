using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManagerContexto : MonoBehaviour {

    public GameObject BotonPrincipal;
    public GameObject MainMenu;
    private Button mBoton;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        mBoton = BotonPrincipal.GetComponent<Button>();
    }

    public void OnLevelWasLoaded(int level)
    {
        Canvas cv = GetComponent<Canvas>();
        //cv.worldCamera = Camera.main;
    }

    public void Click()
    {
        Debug.Log("Boing");
        MainMenu.SetActive(true);
        Application.LoadLevel("MainMenu");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) )
        {   
           mBoton.onClick.Invoke();
        }
    }
}

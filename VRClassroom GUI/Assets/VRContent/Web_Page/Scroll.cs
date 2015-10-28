using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scroll : MonoBehaviour {

    public int SCROLL_MAX;
    public int SCROLL_MIN;

    private int scrollValue;
    private bool paraArriba = false;
    private bool paraAbajo = true;
    private ScrollRect sr;

	// Use this for initialization
	void Start () {
        scrollValue = SCROLL_MIN;
        sr = this.gameObject.GetComponent<ScrollRect>();
	}
	
	// Update is called once per frame
	void Update () {

        if (paraAbajo && Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            scrollValue += 1;
        }
        else if (paraArriba && Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            scrollValue -= 1;
        }

        if (scrollValue >= SCROLL_MAX)
        {
            sr.enabled = false;
            paraAbajo = false;
        }
        else if (scrollValue <= SCROLL_MIN)
        {
            sr.enabled = false;
            paraArriba = false;
        }
        else
        {
            sr.enabled = true;
            paraArriba = true;
            paraAbajo = true;
        }
    }
}

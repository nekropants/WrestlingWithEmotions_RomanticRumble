using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour
{
    public bool isCloseButton = false;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
        GetComponent<Bounce>().enabled = true;
    }
    void OnMouseDown()
    {
        if(isCloseButton)
            DemoVid.instance.Close();
        else

            DemoVid.instance.Open();
    }
    void OnMouseExit()
    {
        GetComponent<Bounce>().enabled = false;
    }
}

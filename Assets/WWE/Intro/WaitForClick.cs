using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Dance { 
 public class WaitForClick : MonoBehaviour
{


    public GameObject filmRoll;

    public bool loadTrailer = false;

    public static bool OVERRIDE = false;
    // Use this for initialization
    void Start()
    {
        if (OVERRIDE)
            Trigger();

        

    }
	
	// Update is called once per frame
	void Update () {
	    if (  Input.GetKey(KeyCode.Mouse0) )
	    {
	        Trigger();

	    }
	}

    public void Trigger()
    {

        if(loadTrailer)
        {
            Application.LoadLevel("Trailer");
        }
        else
        {

        
        filmRoll.gameObject.SetActive(true);
      //  otheCam.gameObject.SetActive(false);
        gameObject.SetActive(false);

    }
    }
}

}
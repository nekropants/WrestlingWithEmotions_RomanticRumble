using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Outro : MonoBehaviour
{


     float Timer = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	    while (Timer > 0)
	    {
            Timer -= Time.deltaTime;
	        return;
	    }

	    if (Input.GetKeyDown(KeyCode.Mouse0))
	    {
            SceneManager.LoadScene(0);   
	    }
	
	}
}

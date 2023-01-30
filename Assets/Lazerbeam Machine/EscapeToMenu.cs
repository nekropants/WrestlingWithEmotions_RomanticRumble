﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EscapeToMenu : MonoBase {

	// Use this for initialization
	void Start () {
	
	}
	
	float inputTimer = 0;
	Vector3 prevMouse;
	// Update is called once per frame
	void Update () {
		
		
		inputTimer += Time.deltaTime;
		if( AnyInputDown )
		{
			inputTimer = 0;
		}
		
		if(Input.GetKey(KeyCode.Mouse0))
			inputTimer = 0;
		
		if(Input.mousePosition != prevMouse)
			inputTimer = 0;
		
		prevMouse = Input.mousePosition;
		
		if(inputTimer > 120)
		{
			inputTimer = 0;
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
	}
    // Update is called once per frame


    private void OnGUI()
    {

    }
    public static bool OnUp = false;
	void LateUpdate () {
		
		if (Input.GetKeyDown(KeyCode.Escape) )
		{

            if (DebugConfig.ExitOnComplete)
                Application.Quit();
            else

                SceneManager.LoadScene(0);
			
		}
		
	 //   if (Input.GetKeyDown(KeyCode.F) && OnUp == false)
	 //   {
		//	print("on down");
		//OnUp = true;	
	 //       SceneManager.LoadScene(0);
	 //   }
		
		// if (Input.GetKeyUp(KeyCode.F) && OnUp == true)
	 //   {
		//	print("on Up");
			
		//OnUp = false;	
	 //       SceneManager.LoadScene(0);
	 //   }
	}
}

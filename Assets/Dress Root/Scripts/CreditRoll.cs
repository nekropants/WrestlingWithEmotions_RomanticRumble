using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Dance { 
 public class CreditRoll : MonoBehaviour
{

    public float m = 1;

	// Use this for initialization
	void Start ()
	{
	    prevTime = Time.realtimeSinceStartup;


	}

    private float prevTime;

    private float timer = 20;
    private float displacement = 0;
	
	// Update is called once per frame
	void Update ()
	{

        float deltaTime = Time.realtimeSinceStartup - prevTime;

	    deltaTime = Mathf.Clamp(deltaTime, 0, 0.1f);
        prevTime = Time.realtimeSinceStartup;



	    transform.localPosition += deltaTime * Vector3.up*m;
	    displacement += deltaTime*m;
        timer -= deltaTime;

	    if (displacement > 8000)
	    {
	        Time.timeScale = 1;
            SceneManager.LoadScene("ExitClub");
        }
    }
}

}
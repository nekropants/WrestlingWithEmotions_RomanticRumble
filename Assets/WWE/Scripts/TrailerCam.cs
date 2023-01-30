using UnityEngine;
using System.Collections;

public class TrailerCam : MonoBehaviour {


    public static TrailerCam instance;
public GameObject interior;
public float speed =1;
float timer = 0;
public float duration =2;
	// Use this for initialization
	void Start () {
        interior.SetActive(false);
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	timer += Time.deltaTime;
    


    
    GetComponent<Camera>().orthographicSize -= Time.deltaTime*speed;
    
    if(timer >duration)
    {

    }
    }

    public void Switch()
    {
        gameObject.SetActive(false);
        interior.SetActive(true);

        //AudioSequence.instance.TurnOffLowPass();
    }
}



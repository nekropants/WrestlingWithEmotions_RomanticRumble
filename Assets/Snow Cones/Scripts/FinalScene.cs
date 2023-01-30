using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class FinalScene : MonoBase
{

    public MeshRenderer Fade;

    private float timer = 0;

    public Transform cone1;
    public Transform cone2;
    public Vector3 anchor1;


    public Camera cam;
	// Use this for initialization
    // Use this for initialization

    public AnimationCurve hopCurve;



    IEnumerator Start()
    {
        print("Start");
	    yield return new WaitForSeconds(1);


        while (RightDown == false &&  UpDown == false)
        {
            yield return null;
        }

        float hopeLerp = 0;

        anchor1 = cone1.localPosition;
       

        while (hopeLerp < 1)
        {
            hopeLerp += Time.deltaTime *4f;
            hopeLerp = Mathf.Clamp01(hopeLerp);
            cone1.localPosition = anchor1 + Vector3.right * 95 * hopeLerp + hopCurve.Evaluate(hopeLerp)*30*Vector3.up;

            yield return null;

        }

        yield return new WaitForSeconds(1.2f);

        hopeLerp = 0;
        while (hopeLerp < 1)
        {
            hopeLerp += Time.deltaTime * 4f;
            hopeLerp = Mathf.Clamp01(hopeLerp);
            cone2.localRotation = Quaternion.Euler(0, 0, hopeLerp * 7);

            yield return null;

        }
        yield return new WaitForSeconds(1);

        print("Start");

        Parrallax[] parrallax = GetComponentsInChildren<Parrallax>();
        foreach (Parrallax parrallax1 in parrallax)
        {
            if (parrallax1 != null)
                parrallax1.paused = false;
        }


	    Fade.gameObject.SetActive(true);
	    Fade.material.SetAlpha(0);

        while (timer < 1)
	    {
	    timer += Time.deltaTime*0.1f;

	        timer = Mathf.Clamp01(timer);
            Fade.material.SetAlpha(timer / 2f);

            yield return null;
	    }
        yield return new WaitForSeconds(0.5f);
        print("Start");

        Vector3 anchor = cam.transform.position + Vector3.down * timer * 1200;

        timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime*1;
            cam.transform.position = Vector3.Lerp(cam.transform.position, anchor, Time.deltaTime * 5);

            timer = Mathf.Clamp01(timer);
            yield return null;

        }
        print("end");

        yield return new WaitForSeconds(1);
        while (AnyInputDown == false)
        {
            yield return null;
        }

        if(DebugConfig.ExitOnComplete == false)
            Application.LoadLevel(Application.loadedLevel);
        else
            Application.Quit();
    }
	
	
	// Update is called once per frame
	void Update ()
	{


	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dance { 
 public class Eyes : MonoBehaviour {

	// Use this for initialization
	public Transform target;

    public bool manualControl = false;

	Eye[] eyes;

	public bool lookAtPlayer = false;

	public SpriteRenderer openEyes; 
	public SpriteRenderer confusedEyes; 
	public SpriteRenderer happyEyes; 
	public SpriteRenderer angryEyes; 
	public SpriteRenderer happyBlinkEyes; 
	public SpriteRenderer sadBlinkEyes; 
	public SpriteRenderer currentEyes; 
	public SpriteRenderer determinedEyes;

    public List<SpriteRenderer> states = new List<SpriteRenderer>();

	public Sprite[] openEyeOptions;

	public bool followMouse = false;
    public float lookRange = 11f;
    void Start () {
		eyes = GetComponentsInChildren<Eye>();
		openEyes.sprite = openEyeOptions[Random.Range(0,3)];
		states.Add(openEyes);
		states.Add(confusedEyes);
		states.Add(sadBlinkEyes);
		states.Add(happyBlinkEyes);
		states.Add(angryEyes);
		states.Add(happyEyes);
		states.Add(determinedEyes);

        SetEyes(openEyes);

		StartCoroutine(RunBlinking());
	}

	public void SetEyes(SpriteRenderer expression)
	{

		currentEyes = expression;
		SetEyesInternal(expression);
	}

	 void SetEyesInternal(SpriteRenderer expression)
	{

		foreach (SpriteRenderer s in states)
		{
            if(s)
			    s.enabled = (s == expression);
		}
	}

    public bool closeEyesHappy = false;
   

	float blinkTimer = 0;
	 float blinkInterval = 4;

	IEnumerator RunBlinking()
	{
		while (true)
		{

			if(Random.value < 0.9f)
			{
			yield return new WaitForSeconds(blinkInterval);
			yield return new WaitForSeconds(blinkInterval*Random.value);
			}
			else
			{
			yield return new WaitForSeconds(0.2f);
				
			}
			SetEyesInternal(happyBlinkEyes);

			foreach (Eye pupil in eyes)
				pupil.gameObject.SetActive(false);

		    while (closeEyesHappy)
		    {
		        yield return null;
		    }

			yield return new WaitForSeconds(0.1f);



			SetEyes(currentEyes);

			foreach (Eye pupil in eyes)
				pupil.gameObject.SetActive(true);
		}
	}

	// Update is called once per frame
    void Update()
    {

        bool lookingAtSomething = false; 
        if (followMouse)
        {
            foreach (Eye eye in eyes)
            {
                eye.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            }
            lookingAtSomething = true;
        }


        if (manualControl)
        {
            if (target)
            {
                Debug.DrawLine(transform.position, target.transform.position);
                foreach (Eye eye in eyes)
                {
                    eye.LookAt(target.transform.position);
                }
                lookingAtSomething = true;

            }

        }

        if (lookAtPlayer)
        {

            if (Vector3.Distance(transform.position, Player.instance.Head.transform.position) < lookRange)
            {
                foreach (Eye eye in eyes)
                {
                    eye.LookAt(Player.instance.Head.position);
                }
                lookingAtSomething = true;
            }

        }

      


     
  

        if (lookingAtSomething == false) 
        {
            foreach (Eye eye in eyes)
            {
                eye.Clear();
            }
        }

        foreach (Eye eye in eyes)
        {

            eye.smileEyes = (currentEyes == happyEyes);
        }

    }
}

}
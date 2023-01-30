using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dance { 
 public class Mouth : MonoBehaviour {

	// Use this for initialization

	public SpriteRenderer neutral; 
	public SpriteRenderer smile; 
	public SpriteRenderer disgust; 
	public SpriteRenderer shocked; 
	public SpriteRenderer awkward; 
	public SpriteRenderer bored; 
	public SpriteRenderer openSmile; 
	public SpriteRenderer talk; 
	public SpriteRenderer current ; 

	public List<SpriteRenderer> states = new List<SpriteRenderer>();

    public bool talking = false;

	public bool followMouse = false;
	void Start () {
		states.Add(neutral);
		states.Add(smile);
		states.Add(shocked);
		states.Add(awkward);
		states.Add(bored);
		states.Add(disgust);
		states.Add(talk);
		states.Add(openSmile);

	    if (current == null)
	        current = openSmile; 

		Set(current);
            

        StartCoroutine(RunTalking());
	}

	public void Set(SpriteRenderer expression)
	{

		current = expression;
		SetInternal(expression);
	}

	 void SetInternal(SpriteRenderer expression)
	{

		foreach (SpriteRenderer s in states)
		{
			s.enabled = (s == expression);
		}
	}

    private float speakTimer = 0;
    public void Speak( float duration)
    {
        speakTimer = duration;
    }

    IEnumerator RunTalking()
    {
        while (true)
        {
            if (speakTimer > 0)
            {

                switch (Random.Range(0, 4))
                {
                    case 0:
                        SetInternal(talk);
                        break;

                    case 1:
                        SetInternal(smile);
                        break;

                    case 2:
                        SetInternal(openSmile);
                        break;

                    case 3:
                        SetInternal(awkward);
                        break;

                }

            }
            else
            {
                Set(current);
            }

            yield return new WaitForSeconds(talkInterval);


            // z

        }
    }


	 float talkInterval = 0.086f;


	// Update is called once per frame
	void Update () {


        if(speakTimer > 0)
	        speakTimer -= Time.deltaTime;





	}
}

}
using UnityEngine;
using System.Collections;

public class Buzzer : MonoBehaviour
{


    public static Buzzer instance;

    public Animator buzzAnimation;

    private float timer = 0;
    public float interval = 0;


    private Shake shake;


    private SpriteRenderer sprite;

    public bool debug = false;
	// Use this for initialization
	void Start ()
	{
	    instance = this;
        buzzAnimation.gameObject.SetActive(false);
	    shake = GetComponent<Shake>();
                shake.enabled = false;

	    sprite = GetComponent<SpriteRenderer>();
	    sprite.enabled = false;

	}

    // Update is called once per frame
    void Update () {


        if (debug)
        {
            debug = false;
            Buzz();
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                shake.enabled = false;
                buzzAnimation.gameObject.SetActive(false);
                sprite.enabled = false;

            }
        }
	}


    public void Buzz()
    {
	    buzzAnimation.gameObject.SetActive(true);
        timer = interval;
        shake.enabled = true;
	    sprite.enabled = true;
    }
}

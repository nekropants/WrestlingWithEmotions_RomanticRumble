using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dance { 
 public class backgrounds : MonoBehaviour
{

    public  List< Sprite >frames;
    private int frame = 0;
    private float timer = 0;
    public float interval = (1/120f)*128;

    public bool offset = false;

    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start ()
	{
	    for (int i = 0; i < 10; i++)
	    {
	        int x =Random.Range(0, frames.Count);
	        Sprite sp = frames[x];
            frames.RemoveAt(x);
            frames.Add(sp);


	    }

	    spriteRenderer = GetComponent<SpriteRenderer>();

	    if (offset)
	        timer += interval/2f;

	}
	
	// Update is called once per frame
	void Update ()
	{

	    timer += Time.deltaTime;    
	    while (timer >= interval)
	    {
	        timer -= interval;
	        frame ++;

        }



        frame %= frames.Count;

	    spriteRenderer.sprite = frames[frame];
	}
}

}
using UnityEngine;
using System.Collections;
using WWE;
public class SelectorButton : MonoBehaviour
{

    public Sprite[] sprites;
    private int frame;
    private float timer = 0;
    private float interval = 0.12f;

    private SpriteRenderer spriteRenderer;
    
    private bool mouseOver = true;
	// Use this for initialization
	void Start ()
	{
	    spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{

	    timer += Time.deltaTime;
	    if (mouseOver)
	    {

	        while (timer > interval)
	        {
	            timer -= interval;

	            frame++;
	        }

	        frame %= 4;
            spriteRenderer.sprite = sprites[1+ frame];
        }
        else
	    {
	        spriteRenderer.sprite = sprites[0];
	    }
        mouseOver = false;
    }

    void OnMouseOver()
    {
        mouseOver = true;
    }

    void OnMouseDown()
    {
        SelectorNudge.instance.Nudge((int)Mathf.Sign(transform.localScale.x));

        if(transform.localScale.x < 0)
            DressWrestler.instance.Prev();
        else
        {
            DressWrestler.instance.Next();
        }
    }


}

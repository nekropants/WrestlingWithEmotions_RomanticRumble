using UnityEngine;
using System.Collections;

using WWE;
public class ThumbButton : MonoBehaviour
{

    public Sprite[] sprites;
    private int frame;
    private float timer = 0;
    private float interval = 0.12f;

    private SpriteRenderer spriteRenderer;

public bool backButton= false;
    private bool mouseOver = true;
	// Use this for initialization
	void Start ()
	{
	    spriteRenderer = GetComponent<SpriteRenderer>();
	}
            float bounceTimer = 1f;
	
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

	        frame %= sprites.Length;
            spriteRenderer.sprite = sprites[frame];
        }
        else
	    {
	        spriteRenderer.sprite = sprites[0];
	    }
        
        if(mouseOver)
        transform.localScale = Vector3.one*1.2f;
else
{
        transform.localScale = Vector3.one*1;
    
}        
        mouseOver = false;
        
        
        
   
            if (bounceTimer < 1)
            {
                bounceTimer += Time.deltaTime * 2;

                bounceTimer = Mathf.Clamp(bounceTimer, 0, 1);
                float scale = Mathf.Sin(bounceTimer * 2 * Mathf.PI) * 0.5f;
                scale *= (1 - bounceTimer);
                scale += 1.2f;
                transform.localScale = scale * Vector3.one;


            }
    }

    void OnMouseOver()
    {
        mouseOver = true;
    }

    void OnMouseDown()
    {
        bounceTimer = 0;
        if(backButton)
            DressWrestler.instance.Back();
        else
        {
                    DressWrestler.instance.Accept();
            
        }
    }


}

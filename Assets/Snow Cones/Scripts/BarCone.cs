
using UnityEngine;
using System.Collections;

public class BarCone : MonoBase
{

    public bool isPlayerControlled = false;
    private Rigidbody2D rbody2d;

    public float lerp = 1;
    public Vector3 direction = Vector3.zero;
    public Vector3 anchor = Vector3.zero;
    private Collider2D col;
    public float timer = 0;
    public float rate = 0.2f;
    private int frame = 0;
    private SpriteSM sprite;

    public bool hasMilkshake = false; 
    
    public static BarCone playerInstance;

	// Use this for initialization
	void Start ()
	{
	    rbody2d = GetComponent<Rigidbody2D>();
	    col = GetComponent<Collider2D>();
	    sprite = GetComponent<SpriteSM>();
        
        if(isPlayerControlled)
            playerInstance = this;
	}
	
	// Update is called once per frame
	void Update () {

	    if (isPlayerControlled)
	    {


            if (hasMilkshake)
                sprite.SetLowerLeftPixel_Y(704 + 128);


	        float speed = 300000;

           Vector3 newDirection = Vector3.zero;

	       // if (lerp == 1)
	            if (Up)
                    newDirection += Vector3.up * 12;

	            if (Left)
                    newDirection += Vector3.left * 16;

	            if (Down)
                    newDirection += Vector3.down * 12;

	            if (Right)
                    newDirection += Vector3.right * 16;

                //if (newDirection != Vector3.zero)
                //{
                //    col.enabled = false;
                //    if (Physics2D.Raycast(transform.position, newDirection, 12) == false)
                //    {
                //        lerp = 0;
                //        anchor = transform.position;
                //        direction = newDirection;
                //    }
                //    col.enabled = true;

	            //}

            //if (lerp < 1)
            //{
            //    lerp += Time.deltaTime*3;
            //    lerp = Mathf.Clamp01(lerp);
            //    transform.position = anchor + direction*lerp;
            //}
	        if (Mathf.Abs(rbody2d.velocity.magnitude) > 10)
	        {

                timer += Time.deltaTime;
                while (timer >= rate)
                {
                    timer -= rate;
                    frame++;
                }

                frame %= 2;


                sprite.SetLowerLeftPixel_X(64 * frame);

	            int facing = (int) Mathf.Sign(rbody2d.velocity.x);
	            Vector3 scale = transform.localScale;
	            scale.x = Mathf.Abs(scale.x)*facing;
	            transform.localScale = scale;
	        }
	        newDirection.Normalize();

                rbody2d.AddForce(newDirection*Time.deltaTime*speed);

	        // transform.transform.position += direction*speed*Time.deltaTime;
	    }


	}
}

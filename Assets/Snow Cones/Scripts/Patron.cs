using System.Timers;
using UnityEngine;
using System.Collections;

public class Patron : MonoBehaviour
{

    public float bpm = 0.4f;
    public float timer = 0;
    public float offset = 0;
    private int frame = 0;
    public int frames = 2;
    private SpriteSM sprite;

    private Collider2D col;
    public SpriteSM hair;
    public SpriteSM face;
    public SpriteSM glasses;
    public bool animate = true;
    public int gestures = 7;
    // Use this for initialization
	void Start ()
	{
	    sprite = GetComponent<SpriteSM>();
	    timer = bpm;
	    col = GetComponent<Collider2D>();


	    hair.SetLowerLeftPixel_X(64*Random.Range(0,4));


	    if (Random.value < 0.9f)
	        glasses.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
        timer += Time.deltaTime * frames;
	    while (timer >= bpm)
	    {
	        timer -= bpm;
	       // frame++;
	       // TryStep();
	    }

        if(animate)
	        RunFace();
        
        frame %= frames;

        sprite.SetLowerLeftPixel(128 * frame, sprite.lowerLeftPixel.y);
	}


    private float faceTimer = 0;
    private void RunFace()
    {
        faceTimer -= Time.deltaTime;
        if (faceTimer <= 0)
        {
            int index = Random.Range(0, gestures);
            face.SetLowerLeftPixel_X(index*64);
            faceTimer =(Random.Range(1, 8f));
        }
    }

    private void TryStep()
    {
        if (Random.value < 0.2f)
        {
            int dir = Random.Range(0, 4);

            Vector3 direction;
            if (dir == 0)
                direction = Vector3.down*12;
            else if (dir == 1)
                direction = Vector3.left*16;
            else if (dir == 2)
                direction = Vector3.right*16;
            else 
                direction = Vector3.up*12;

            print("Try step");
            col.enabled = false;

            if (Physics2D.Raycast(transform.position, direction.normalized, 12) == false)
            {
                print("Raycast");
                transform.position += direction;
            }
            col.enabled = true;

        }
    }
}

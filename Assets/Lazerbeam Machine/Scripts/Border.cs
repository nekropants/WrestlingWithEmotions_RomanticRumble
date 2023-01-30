using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Sprite[] frames;
    private int frame = 0;
    private float timer = 0;
    public float interval = 0.12f;
    // Use this for initialization
    void Start ()
    {
        renderer = GetComponent<SpriteRenderer>();
        frame = Random.Range(0, 4);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    timer += Time.deltaTime;
	    while (timer > interval)
	    {
	        timer -= interval;
            frame++;
	        frame %= 4;

            renderer.sprite = frames[frame];
	    }
	}
}

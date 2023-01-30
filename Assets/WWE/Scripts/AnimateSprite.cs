using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimateSprite : MonoBehaviour
{

    public Sprite[] sprite;
    private float timer = 0;
    private float interval = 0.12f;

    private SpriteRenderer renderer;

    private int frame = 0;
    // Use this for initialization
    void Start ()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;


        while (timer > interval)
        {
            timer -= interval;
            frame++;

            frame %= 2;

            renderer.sprite = sprite[frame];
        }
	
	}
}

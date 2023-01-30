using UnityEngine;
using System.Collections;

public class BarFloorColorShift : MonoBehaviour
{

    public Color[] colors;
    private int index = 0;

    private SpriteRenderer sprite;

    public float interval = 1.712f;

    private float timer = 0;

	// Use this for initialization
	void Start ()
	{
	    sprite = GetComponent<SpriteRenderer>();
	    index = Random.Range(0, colors.Length);
	    ChangeColor();
	}

    private void ChangeColor()
    {
        index %= colors.Length;

        sprite.color = colors[index];
    }

    // Update is called once per frame
	void Update ()
	{
	    timer += Time.deltaTime;
	    while (timer > interval)
	    {
	        timer -= interval;
	        index++;
            ChangeColor();
        }
	}
}

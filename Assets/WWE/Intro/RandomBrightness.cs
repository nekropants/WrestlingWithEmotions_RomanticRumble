using UnityEngine;
using System.Collections;

namespace Dance { 
 public class RandomBrightness : MonoBehaviour
{

    public float freq = 1;
    public float amplitude = 0.1f;
    private float timer = 0;

    private SpriteRenderer renderer;
    // Use this for initialization
    void Start ()
    {

        renderer = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (timer > freq)
        {
            timer -= freq;
            renderer.color = new Color(1, 1, 1, Random.Range(0, amplitude));
        }
    }
}

}
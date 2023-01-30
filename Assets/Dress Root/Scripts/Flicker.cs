using UnityEngine;
using System.Collections;

namespace Dance { 
 public class Flicker : MonoBehaviour
{
    public SpriteRenderer[] spriteRenderers;
      float rand;

    private float timer = 0;
    public float interval = 1/12f;
    private bool isRandomizer = false;

    public float range = 0.5f;
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{

	    timer += Time.deltaTime;

	    if (timer >= interval)
	    {
	        timer -= interval;
	        rand = Random.Range(range, 1f);
	        Color color = Color.white;
	        color.a = rand;

	        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
	        {
	            spriteRenderer.color = color;
	        }
	    }
	}

    void LateUpdate()
    {
    }
}

}
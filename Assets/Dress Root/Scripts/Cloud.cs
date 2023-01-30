using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dance { 
 public class Cloud : MonoBehaviour
{

    public float speed = 0;
    public float m = 1;

    public Sprite replaceMent;

    public Vector3 offset;


    public  static  List<Cloud> clouds = new List<Cloud>(); 
    private SpriteRenderer sprite;
    // Use this for initialization
    void Start ()
    {
        clouds.Add(this);
        sprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
	{

        transform.position += Vector3.right*Time.deltaTime*speed*m;

	    if (transform.localPosition.x > 18)
	        transform.localPosition -= 18*3*Vector3.right;
	}

    public void Replace()
    {
        sprite.sprite = replaceMent;

    }

    public static void ReplaceAll()
    {
        foreach (Cloud cloud in clouds)
        {
            cloud.Replace();
        }
    }


    void OnDestroy()
    {
        clouds.Remove(this);

    }
}

}
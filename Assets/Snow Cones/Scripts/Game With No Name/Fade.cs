using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {


    public float speed = 3;
    SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Color c = sprite.color;
        c.a -= Time.deltaTime * speed;
        sprite.color = c;

        if( c.a <= 0)
            Destroy(gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class LazerParticle : MonoBehaviour
{
    public float timer;
    public Vector3 velocity;
    public float rot = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.position += velocity*Time.deltaTime;
	    transform.Rotate(0,0, rot*Time.deltaTime);
	    timer -= Time.deltaTime;
        if (timer < 0)
            Destroy(gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class ParticleGenerator : MonoBehaviour
{

    public Sprite[] sprites;

    private float timer = 0;
    public float interval = 0.3f;

    public Vector3 range = Vector3.zero;

    public Vector3 minVelocity = Vector3.zero;
    public Vector3 maxVelocity = Vector3.zero;
    public float life = 1;

    public float scale = 1;
    public float rotation = 1;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	    timer += Time.deltaTime;

	    while (timer > interval)
	    {
	        timer -= interval;
	        CreateParticle();

	    }

	}

    void CreateParticle()
    {
        SpriteRenderer spriteRenderer = (new GameObject("Particle")).AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        spriteRenderer.transform.parent = transform;

        Vector3 pos;
        pos.x = Random.Range(-range.x, range.x);
        pos.y = Random.Range(-range.y, range.y);
        pos.z = Random.Range(-range.z, range.z);
        spriteRenderer.transform.localPosition = pos;
        spriteRenderer.transform.localScale = Vector3.one*scale;

        LazerParticle particle = spriteRenderer.gameObject.AddComponent<LazerParticle>();
        particle.velocity.x = Random.Range(minVelocity.x, maxVelocity.x);
        particle.velocity.y = Random.Range(minVelocity.y, maxVelocity.y);
        particle.velocity.z = Random.Range(minVelocity.z, maxVelocity.z);
        particle.timer = life;
        
        particle.transform.Rotate(0, 0, 360f*Random.value);
        particle.rot =rotation;

    }

    public void OnDrawGizmos()
    {

        Gizmos.DrawWireCube(transform.position, range*2);
    }
}

using UnityEngine;
using System.Collections;


namespace WWE
{


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

        public AudioClip flashSound;

        float lastFlashTime = 0;

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


            if (Time.time - lastFlashTime > 0.1)
            {
                AudioController.Play(flashSound, Random.Range(0.5f, 1f), Random.Range(0.9f, 1.1f));

                lastFlashTime = Time.time + Random.value * 0.2f; ;
            }
    }

    public void OnDrawGizmos()
    {

        Gizmos.DrawWireCube(transform.position, range*2);
    }
}

}

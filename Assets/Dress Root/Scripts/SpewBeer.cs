using UnityEngine;
using System.Collections;


namespace Dance { 
 public class SpewBeer : MonoBehaviour
{
    private Vector3 prevDirection;
    private Vector3 prevPosition;
    private float maxVel = 0;

    public Rigidbody2D[]  beerParticles;

    // Use this for initialization
    void Awake ()
    {
        prevPosition = transform.position;
        beerParticles = GetComponentsInChildren<Rigidbody2D>();

        foreach (var var in beerParticles)
        {
            var.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{

	    Vector3 direction = transform.position - prevPosition;
	    maxVel = Mathf.Max(direction.magnitude, maxVel);

        print(maxVel);

        float dot = Vector3.Dot(prevDirection.normalized, direction.normalized);
        float dot2 = Vector3.Dot(prevDirection.normalized, transform.up);
        if (dot < 0 && dot2 > 0)
        {
            Debug.DrawRay(transform.position , prevDirection.normalized*2, Color.cyan, 2);

            for (int i = 0; i < 1; i++)
            {

                foreach (var beer in beerParticles)
                {
                    Rigidbody2D r = Instantiate(beer);
                    r.gameObject.SetActive(true);
                    Vector3 force = transform.up*50 + Random.Range(-1, 1)*transform.right;
                    r.AddForce(maxVel* force * Random.Range(0.7f, 1.3f), ForceMode2D.Impulse);
                    r.MovePosition(transform.position + (Vector3)Random.insideUnitCircle * 0.05f);
                }
            }

            maxVel = 0;

        }
        prevPosition = transform.position;
        prevDirection = direction;

    }
}

}
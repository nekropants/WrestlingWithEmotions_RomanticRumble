using UnityEngine;
using System.Collections;


namespace WWE
{


    public class LazerParticle : MonoBehaviour
    {
        public float timer;
        public Vector3 velocity;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.position += velocity*Time.deltaTime;
            timer -= Time.deltaTime;
            if (timer < 0)
                Destroy(gameObject);
        }
    }
}
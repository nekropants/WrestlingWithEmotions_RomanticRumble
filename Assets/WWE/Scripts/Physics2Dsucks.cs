using UnityEngine;
using System.Collections;

namespace WWE
{
    public class Physics2Dsucks : MonoBehaviour
    {
        private Rigidbody2D rigid;
        public float damping;
        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();

        }

        // Update is called once per frame
        void Update()
        {
            //   rigid.angularDrag = damping;
        }
    }
}
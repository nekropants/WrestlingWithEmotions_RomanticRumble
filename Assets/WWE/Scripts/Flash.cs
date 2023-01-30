using UnityEngine;
using System.Collections;


namespace WWE
{

    public class Flash : MonoBehaviour
    {
        public float flashTimer = 0;
        public float flashSpeed = 1;
        private SpriteRenderer sprite;
        // Use this for initialization
        void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (flashTimer > 0)
            {
                flashTimer -= Time.deltaTime*flashSpeed;
                sprite.color = new Color(1, 1, 1, flashTimer);
            }
        }

        public void TriggerFlash()
        {
            flashTimer = 1f;
        }
    }
}

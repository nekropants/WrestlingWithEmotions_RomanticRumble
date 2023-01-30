using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace WWE
{
    public class Table : MonoBehaviour
    {

        public static Table instance;

        public Text nameOnForm;

        public DrawToTexture form;

        // Use this for initialization

        void Awake()
        {
            instance = this;
        }

        public static void Shake()
        {
            instance.GetComponent<Shake>().AddShake(true);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
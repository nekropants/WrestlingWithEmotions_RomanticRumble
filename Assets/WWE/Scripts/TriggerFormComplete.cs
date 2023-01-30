using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace WWE
{
    public class TriggerFormComplete : MonoBehaviour
    {

        public static bool completed = false;

        // Use this for initialization
        void Start()
        {


        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) && mouseDownAndOver)
            {
                completed = true;
                Gaze.instance.formCompleted = true;
                mouseDownAndOver = false;
            }

        }

        private bool mouseDownAndOver = false;
        void OnMouseOver()
        {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                mouseDownAndOver = true;
            }
        }
    }
}
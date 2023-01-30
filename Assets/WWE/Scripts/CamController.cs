using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace WWE
{

    public class CamController : MonoBehaviour
    {
        public bool run = false;

        public static CamController instance;

        public float yOffset = -1073;
        public bool focusOnLogo = true;
        public GameObject motionLines;
        // Use this for initialization
        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            transform.localPosition = Vector3.up*yOffset;

            if(motionLines)
                motionLines.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if(run == false)
                return;

            Vector3 target = Vector3.zero;

            if (focusOnLogo)
                target = Vector3.up*yOffset;

            transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime*2);

            if (focusOnLogo)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (motionLines)
                        motionLines.gameObject.SetActive(true);
                    focusOnLogo = false;
                    Manny.TriggerManny();
                }
            }
        }
    }
}

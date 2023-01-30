using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace WWE
{


    public class GetIntoRing : MonoBehaviour
    {
        public Transform bottomRope;
        public Transform wrestler;

        public float ropeSpeed = 1;
        public float wresterSpeed = -1;
        public float timeM = 1;
        public float camM = 1;

        private float timer = 0;


        public Transform Arms;
        public Transform pretty;
        public Transform freek;
        public Transform senor;
        public Transform dweeb;
        public Transform bear;
        public Transform piggy;
        public Transform ray;
         public Transform stoney;
        public Transform sunshine;
        public Transform jake;
        // Use this for initialization

        Vector3 target;

        void Setup()
        {
       

            Arms.gameObject.SetActive(false);
            pretty.gameObject.SetActive(false);
            freek.gameObject.SetActive(false);
            senor.gameObject.SetActive(false);
            bear.gameObject.SetActive(false);
            bear.gameObject.SetActive(false);
            piggy.gameObject.SetActive(false);
            ray.gameObject.SetActive(false);
            jake.gameObject.SetActive(false);
            stoney.gameObject.SetActive(false);
            sunshine.gameObject.SetActive(false);

            Transform t = null;
            switch (SceneController.winner)
            {
                case Characters.None:
                    
                    break;
                case Characters.Arms:
                    t = Arms;
                    Arms.gameObject.SetActive(true);
                    break;
                case Characters.Piggy:
                    t = piggy;
                    piggy.gameObject.SetActive(true);
                    break;
                case Characters.PrettyGuy:
                    t = pretty;
                    pretty.gameObject.SetActive(true);
                    break;
                case Characters.Ray:
                    t = ray;
                    ray.gameObject.SetActive(true);
                    break;
                case Characters.FreakShow:
                    t = freek;
                    freek.gameObject.SetActive(true);
                    break;
                case Characters.Dweeb:
                    t = dweeb;
                    dweeb.gameObject.SetActive(true);
                    break;
                case Characters.SenorMurder:
                    t = senor;
                    senor.gameObject.SetActive(true);
                    break;
                case Characters.Bear:
                    t = bear;
                    bear.gameObject.SetActive(true);
                    break;
                     case Characters.Stoney:
                    t = stoney;
                    stoney.gameObject.SetActive(true);
                    break;
                case Characters.SenorSunshine:
                    t = sunshine;
                    sunshine.gameObject.SetActive(true);
                    break;
                case Characters.JakeTheGerbil:
                    t = jake;
                    jake.gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            Vector3 pos = t.transform.localPosition;
            pos.x = 0;
            t.transform.localPosition = pos;
        }

        private bool setup = false;
        // Update is called once per frame
        void Update()
        {
            if (setup == false)
            {
            Setup();
                setup = true;
            }

            bottomRope.transform.position = Vector3.Lerp(bottomRope.transform.position, target, Time.deltaTime*1);

            timer += Time.deltaTime;
            if (timer > 4)
            {
                SceneManager.LoadScene("EndRingPunchYou");
            }
            if (timer > 0.5f)
            {
            }
            else
            {
            }
        }
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace WWE
{

    public class Portraits : MonoBehaviour
    {

        public static Portraits instance;

        public WrestlerButton[] selectionOrder = {null, null, null};

        public static bool selectedWrestlers = false;

        private List<WrestlerButton> wrestlerButtons = new List<WrestlerButton>();

        public SpriteRenderer selectedWresterStarburst;

        public SpriteRenderer fader;
        // Use this for initialization

        public Transform[] targets;


        public Sprite [] defaultSprites;
        public Sprite [] mouseOverSprites;
        public Sprite [] selectedSprites;

        void Start()
        {
            instance = this;

            wrestlerButtons = new List<WrestlerButton>(GetComponentsInChildren<WrestlerButton>());

            wrestlerButtons.Sort((A, B) =>
            {
                if (A.transform.position.y == B.transform.position.y)
                {
                    return A.transform.position.x.CompareTo(B.transform.position.x);
                }

                return -A.transform.position.y.CompareTo(B.transform.position.y);

            });

            for( int i = 0; i < wrestlerButtons.Count; i++)
            {
                int index = (int)SceneController.instance.wrestlers[i].character - 1;
                wrestlerButtons[i].wrestler = SceneController.instance.wrestlers[i];
                //print
                wrestlerButtons[i].Setup(  SceneController.instance.wrestlers[i].character, defaultSprites[index], mouseOverSprites[index], selectedSprites[index]);
            }

            selectedWresterStarburst.gameObject.SetActive(false);

            gameObject.SetActive(false);

            selectedWrestlers = false;

         //   Trigger();
        }

        public static void Trigger()
        {
            instance.gameObject.SetActive(true);

            instance.StartCoroutine(instance.LerpInWrestlers());

        }


        void Update()
        {

            if (selectedWrestlers == false && Input.GetKey(KeyCode.Mouse0) == false)
            {
                selectedWrestlers = true;
                for (int i = 0; i < 3; i++)
                {
                    if (selectionOrder[i] == null)
                    {
                        selectedWrestlers = false;
                    }
                }



            }
        }


        public void ToggleWrestler(WrestlerButton wrestler)
        {
            for (int i = 0; i < 3; i++)
            {
                if (selectionOrder[i] == wrestler)
                {
                    selectionOrder[i] = null;
                    wrestler.placementNumber = -1;

                    return;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (selectionOrder[i] == null)
                {
                    selectionOrder[i] = wrestler;
                    wrestler.placementNumber = i;


                    return;
                }
            }
        }



        private WrestlerButton selectedButton;
        public bool letPlayerChooseFavourites = false;

        IEnumerator LerpInWrestlers()
        {
            float offset = 1500;


            List<Vector3> defaults = new List<Vector3>();
            for (int i = 0; i < wrestlerButtons.Count; i++)
            {
                //
                // if(wrestlerButtons[i].wrestler.character == Characters.Stoney)
                // {
                //     wrestlerButtons[i].wrestler.opinion -= 20;
                // } 

                defaults.Add(wrestlerButtons[i].transform.localPosition);
                wrestlerButtons[i].transform.localPosition += Vector3.up*offset;
            }

            FinalDecisionForm.instance.Hide();
          

            MannyReturn.TriggerManny();


            MannyReturn.instance.text.text = ScriptReader.mannyReturn[0];
            MannyReturn.instance.Speak();
            yield return StartCoroutine(WrestlerInfo.WaitForClickRoutine());


            MannyReturn.instance.text.text = ScriptReader.mannyReturn[1];
            MannyReturn.instance.Speak();
            yield return StartCoroutine(WrestlerInfo.WaitForClickRoutine());

            MannyReturn.instance.text.text = ScriptReader.mannyReturn[2];
            MannyReturn.instance.Speak();
            yield return StartCoroutine(WrestlerInfo.WaitForClickRoutine());

            yield return new WaitForSeconds(0.1f);

            float timer = 0;
            while (timer < 4)
            {
                timer += Time.deltaTime;
                for (int i = 0; i < wrestlerButtons.Count; i++)
                {
                    if (i * 0.2f < timer)
                        wrestlerButtons[i].transform.localPosition =
                            Vector3.Lerp(wrestlerButtons[i].transform.localPosition, defaults[i], Time.deltaTime * 5);
                }
                yield return null;
            }



            while (selectedWrestlers == false)
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(GrayOutWrestlers());
            yield return StartCoroutine(WrestlerInfo.WaitForClickRoutine());

            MannyReturn.instance.text.text = ScriptReader.mannyReturn[3];
            MannyReturn.instance.Speak();

            yield return StartCoroutine(WrestlerInfo.WaitForClickRoutine());


            yield return StartCoroutine(ShowFavouriteWresters());

            yield return StartCoroutine(WrestlerInfo.WaitForClickRoutine());

            MannyReturn.instance.text.text = ScriptReader.mannyReturn[4];
            MannyReturn.instance.Speak();
            yield return StartCoroutine(WrestlerInfo.WaitForClickRoutine());

            MannyReturn.instance.text.text = "";
            
        }

        IEnumerator ShowFavouriteWresters()
        {

            wrestlerButtons.Sort((A, B) => { return A.wrestler.opinion.CompareTo(A.wrestler.opinion); });

            int count = 1;
            /*
            foreach (WrestlerButton wrestlerButton in wrestlerButtons)
            {

                if (wrestlerButton.placementNumber != -1)
                {
                    float timer = 0;
                    print(wrestlerButton.wrestler.opinion + "  " + count);

                    while (timer < count)
                    {
                        print(timer);

                        timer += Time.deltaTime*20;
                        wrestlerButton.transform.localScale += Time.deltaTime*Vector3.one*2;

                        yield return null;
                    }
                    count++;
                yield return new WaitForSeconds(0.2f);
                }

            }
            */

            for (int i = 0; i < wrestlerButtons.Count; i++)
            {
                print(wrestlerButtons[i].wrestler.name + " " + wrestlerButtons[i].wrestler.opinion);
                if (wrestlerButtons[i].placementNumber == 0)
                {
                    selectedButton = wrestlerButtons[i];
                }
                else
                {
                    wrestlerButtons[i].Disappoint();
                }

            }
            yield return new WaitForSeconds(0.1f);

            selectedWresterStarburst.gameObject.SetActive(true);
            selectedWresterStarburst.transform.position = selectedButton.transform.position;


            SceneController.winner = selectedButton.character;
            yield return new WaitForSeconds(3f);
            StartCoroutine(FadeToBlack());

        }

        public IEnumerator GrayOutWrestlers()
        {

            //for (int i = 0; i < wrestlerButtons.Count; i++)
            //{
            //    if (wrestlerButtons[i].placementNumber == -1)
            //    {
            //        wrestlerButtons[i].sprite.color = Color.gray;

            //        yield return new WaitForSeconds(0.066f);
            //    }
            //}
            int count = 0;
            for (int i = 0; i < wrestlerButtons.Count; i++)
            {
                if (wrestlerButtons[i].placementNumber == -1)
                {
                    count++;
                    StartCoroutine(FallAway(0.05f* count, wrestlerButtons[i]));
                }
            }
                yield return null;

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < wrestlerButtons.Count; i++)
            {
                if (wrestlerButtons[i].placementNumber != -1)
                {
                    count++;
                    StartCoroutine(MoveToTarget(0.05f * count, wrestlerButtons[i]));
                }
            }
        }

        private IEnumerator FallAway(float delay, WrestlerButton wrestler)
        {
            yield return new  WaitForSeconds(delay);

            float timer = 0;
            while (timer < 1)
            {
                timer += Time.deltaTime*2;
                wrestler.transform.localPosition += Vector3.down*Time.deltaTime*5000;

                yield return null;
            }

        }

        private IEnumerator MoveToTarget(float delay, WrestlerButton wrestler)
        {
            yield return new WaitForSeconds(delay);
            Vector3 start = wrestler.transform.localPosition;
            float timer = 0;
            while (timer < 1)
            {
                timer += Time.deltaTime * 2;
                timer = Mathf.Clamp01(timer);
                wrestler.transform.localPosition = Vector3.Lerp(start, targets[wrestler.placementNumber].localPosition, timer);

                yield return null;
            }

        }

        private IEnumerator MoveOffScreen(float delay, WrestlerButton wrestler, int index)
        {
            yield return new WaitForSeconds(delay);

            float timer = 0;
            while (timer < 1)
            {
                timer += Time.deltaTime * 2;
                wrestler.transform.localPosition =   Vector3.left * Time.deltaTime * 5000;

                yield return null;
            }

        }

        public IEnumerator ShowSelectedWrestlers()
        {

  
            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < wrestlerButtons.Count; i++)
            {
                positions.Add(wrestlerButtons[i].transform.localPosition);
            }
            //wrestlerButtons.Sort((A, B) =>
            //{
            //    return A.wrestler.opinion.CompareTo(B.wrestler.opinion);
            //});


            for (int i = 0; i < wrestlerButtons.Count; i++)
            {
                print(wrestlerButtons[i].wrestler.name + " " + wrestlerButtons[i].wrestler.opinion);
            }

            StartCoroutine(LerpPortraits(positions));



            for (int i = 0; i < 8; i++)
            {
                WrestlerButton highest = null;
                float best = -1000;
                print("---  " );

                for (int j = i; j < 8; j++)
                {
                print(wrestlerButtons[j].wrestler.opinion + "     "  + best);
                    if (wrestlerButtons[j].wrestler.opinion >= best)
                    {
                        best = wrestlerButtons[j].wrestler.opinion;
                        highest = wrestlerButtons[j];
                    }
                }


                wrestlerButtons.Remove(highest);
                wrestlerButtons.Insert(i, highest);
                yield return new WaitForSeconds(0.4f);

            }



            for (int i = 0; i < wrestlerButtons.Count; i++)
            {
                print(wrestlerButtons[i].wrestler.name + " " + wrestlerButtons[i].wrestler.opinion);
                if (wrestlerButtons[i].placementNumber != -1)
                {
                    selectedButton = wrestlerButtons[i];
                    break;
                }
            }
            yield return new WaitForSeconds(0.1f);

            selectedWresterStarburst.gameObject.SetActive(true);
            selectedWresterStarburst.transform.position = selectedButton.transform.position;


SceneController.winner = selectedButton.character;
            yield return new WaitForSeconds(3f);
            StartCoroutine(FadeToBlack());

            yield return null;
        }

        IEnumerator  LerpPortraits(List<Vector3> positions)
        {
            float timer = 0;
            while (timer < 20)
            {
                for (int i = 0; i < wrestlerButtons.Count; i++)
                {
                    //    if (timer > i*0.2f)
                    {

                        wrestlerButtons[i].transform.localPosition =
                            Vector3.MoveTowards(wrestlerButtons[i].transform.localPosition,
                                positions[i], Time.deltaTime * 2300);
                    }
                    //       else
                    {
                        // wrestlerButtons[i].transform.localPosition += Vector3.down*2*Time.deltaTime;
                    }

                }
                timer += Time.deltaTime;
                yield return null;
            }


        }

        IEnumerator FadeToBlack()
        {
            float timer = 0;
            while (timer < 1)
            {
                timer += Time.deltaTime;
                timer = Mathf.Clamp01(timer);
                fader.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), timer);
                yield return null;
            }

            yield return new WaitForSeconds(2f);


            SceneManager.LoadScene("EndMatch");


        }
    }
}

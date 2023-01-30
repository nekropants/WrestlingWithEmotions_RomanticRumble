using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace WWE
{
    public class SceneController : MonoBehaviour
    {
        public List<WrestlerInfo> wrestlers;

        public static SceneController instance;

        public AudioSource songAudioSource;

        public RectTransform table;

        public DrawToTexture form;

        public WrestlerInfo arms;
        public WrestlerInfo senorMurder;
        public WrestlerInfo senorSunshine;
        public WrestlerInfo stoney;
        public WrestlerInfo freakshow;
        public List<WrestlerInfo> fillerWestlers;
        static bool showStoney=false;
        static bool hasRandomized=false;

        public static Characters winner = Characters.None;
        
        public AudioClip mannysTheme;

        public Color inkColor= Color.black;

        // Use this for initialization
        void Awake()
        {
            
            Descriptors.RandomSeed = Random.Range(0, 1000);
            
            instance = this;

            if (Application.isEditor == false)
                currentSceneIndex = 0;
                

            foreach (var wrestlerInfo in wrestlers)
            {
                wrestlerInfo.gameObject.SetActive(false);
            }
            wrestlers.Clear();

            if(hasRandomized == false)
            {
                hasRandomized = true;
                showStoney = Random.value > 0.5f;
            }
            bool senors = false;
            if(showStoney)
            {
                wrestlers.Add(stoney);
            }
            else   
            {
                wrestlers.Add(arms);
            }
            showStoney = !showStoney;


            if(Random.value > 0.5f)
            {
                senors = true;
            }

            senors = true;

            if (senors)
            {
                for (int i = 0; i < 1; i++)
                {
                    int index = Random.Range(0, fillerWestlers.Count);

                    WrestlerInfo w = fillerWestlers[index];
                    fillerWestlers.RemoveAt(index);
                    wrestlers.Add(w);
                }

                wrestlers.Add(senorMurder);


                for (int i = 0; i < 2; i++)
                {
                    int index = Random.Range(0, fillerWestlers.Count);

                    WrestlerInfo w = fillerWestlers[index];
                    fillerWestlers.RemoveAt(index);
                    wrestlers.Add(w);
                }


                Debug.Log("AddSenorSunshine");
                wrestlers.Add(senorSunshine);
            }
            else
            {
                  for(int i = 0; i <4; i++)
                {
                    int index = Random.Range(0, fillerWestlers.Count);

                    WrestlerInfo w = fillerWestlers[index];
                    fillerWestlers.RemoveAt(index);
                    wrestlers.Add(w);
                }

                    wrestlers.Add(freakshow);
                
            }

            foreach (var wrestlerInfo in wrestlers)
            {
            }         

            songAudioSource = GetComponent<AudioSource>();


         
            winner = Characters.None;


            if (Application.isEditor == false)
                currentSceneIndex = 0;
        }

        private AudioClip nextSongClip;


        public static void BringInWrestler()
        {
            instance.ShowNextScene();
        }

        public int currentSceneIndex = 1;
        private WrestlerInfo currentScene;

        public Descriptors formDescriptors;
        
        public void ShowNextScene()
        {
            if (currentSceneIndex < wrestlers.Count)
            {
                if (currentScene)
                    currentScene.gameObject.SetActive(false);

                wrestlers[currentSceneIndex].gameObject.SetActive(true);

                currentScene = wrestlers[currentSceneIndex];
                currentScene.SetInfo(ScriptReader.dateInteractions[((int)currentScene.character) - 1], currentSceneIndex);
                
                formDescriptors.seed = currentSceneIndex;
                currentScene.transform.localPosition = Vector3.zero;

                currentSceneIndex++;

                table.parent = currentScene.transform;

                table.SetSiblingIndex(1);
                if (currentScene.song != null)
                {
                    nextSongClip = currentScene.song;

                }
                songAudioSource.loop = true;
            }
            else
            {
                print("Done");
                Gaze.instance.lookDown = false;
                Portraits.Trigger();
                songAudioSource.Stop();
                songAudioSource.clip = mannysTheme;
                songAudioSource.Play();
            }


        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) && Application.isEditor)
            {
                Portraits.Trigger();
            }
        }



        public void HitBuzzer()
        {
            Buzzer.instance.Buzz();
         //   songAudioSource.Stop();
            AudioController.Play(AudioController.Instance.buzzer, 0.3f);
        }

        public void PlaySitDown()
        {
            songAudioSource.clip = nextSongClip;
            songAudioSource.Play();
            AudioController.Play(AudioController.Instance.sitDownSound, 0.3f);

            if(ExitTrailer.instance)
                ExitTrailer.instance.themeInstance.gameObject.SetActive(false);
              
        }
    }
}
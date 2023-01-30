using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


namespace WWE
{

    public class OutroAudioSource : MonoBehaviour
    {
        public static OutroAudioSource instance;
        public AudioSource audio;
        public AudioClip loop;

        // Use this for initialization
        void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audio = GetComponent<AudioSource>();
        }

        public void Begin()
        {
            StartCoroutine(LoopRoutine());
        }

        IEnumerator LoopRoutine()
        {
            audio.Play();
            audio.loop = true;

            yield return new WaitForSeconds(audio.clip.length);
            audio.clip = loop;
            audio.Play();
        }

        // Update is called once per frame
        void OnLevelWasLoaded(int level)
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if(currentScene.Contains("End") == false)
                Destroy(gameObject);

        }
    }
}
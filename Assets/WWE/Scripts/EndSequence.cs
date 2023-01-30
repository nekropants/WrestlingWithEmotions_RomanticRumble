using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


namespace WWE
{


public class EndSequence : MonoBehaviour
{
    public AudioClip[] lightSounds;

        public SpriteRenderer doors;
    public SpriteRenderer sparks;
    public GameObject screens;
    public ParticleGenerator  particles;
    public GameObject  crowdHyped;
    public AudioClip crowdRoar;

        // Use this for initialization
        void Start ()
	{
	    StartCoroutine(Sequence());

    }

float timer =0;
    IEnumerator Sequence()
    {
          OutroAudioSource.instance.Begin();

        doors.gameObject.SetActive(false);
            sparks.gameObject.SetActive(false);
        screens.gameObject.SetActive(false);


          while (timer < 3.65f)
          {
              yield return null;
          }
        particles.gameObject.SetActive(false);
        doors.gameObject.SetActive(true);

            foreach (var lightSound in lightSounds)
            {
                AudioController.Play(lightSound);

            }
            
              while (timer < 7.3f)
          {
              yield return null;
          }
            AudioSource crowd =  AudioController.Play(crowdRoar);
            crowdHyped.gameObject.SetActive(true);

            sparks.gameObject.SetActive(true);
        screens.gameObject.SetActive(true);


     while (timer < 10)
          {
              yield return null;
          }
            SceneManager.LoadScene("EndGetInRing");
        crowd.volume *= 0.7f;


    }
	
	// Update is called once per frame
	void Update () {
	    timer += Time.deltaTime;
	}
}
}

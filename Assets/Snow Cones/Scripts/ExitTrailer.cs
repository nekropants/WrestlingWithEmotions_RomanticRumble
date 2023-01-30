using UnityEngine;
using System.Collections;
using WWE;

public class ExitTrailer : MonoBehaviour {


        public AudioClip intro;
        public AudioClip loop;
        public AudioSource themeInstance;


    public static ExitTrailer instance;
    public  GameObject exitScene;
    public  GameObject mainCam;


    void Awake()
    {
        instance = this;
    }

    private bool begun = false;
        // Use this for initialization
        IEnumerator Begin()
        {
            begun = true;

        themeInstance = AudioController.Play(intro);

            yield return new WaitForSeconds(intro.length);
        themeInstance.clip = loop;
        themeInstance.Play();
        themeInstance.loop = true;
        }


    private float timer = 0;
    public void Update()
    {

        if (begun)
        {
            timer += Time.deltaTime;

            if (timer > 10)
            {
                ChangeScene();
                enabled = false;
            }
        }
    }


    public void PlayMainTheme()
    {
        StartCoroutine(Begin());
    }


    public void ChangeScene()
    {
        exitScene.SetActive(false);
        mainCam.SetActive(true);
        CamController.instance.run = true;
        ZoomCam.instance.TeleportLogo();
    }


    public void PlayAudio()
    {
      

    }

    public Animator radioAnim;

    public void Trigger()
    {
        radioAnim.enabled = true;
    }


}

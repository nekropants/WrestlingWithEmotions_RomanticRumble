using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Dance { 
 public class TitleScreen : MonoBehaviour
{
    public SpriteRenderer tlb;

    public SpriteRenderer flash;
    public SpriteRenderer blackOut;
    public SpriteRenderer dressTo;
    public SpriteRenderer express;
    public SpriteRenderer dancing;
    public SpriteRenderer success;

    public AudioClip[] neonBuzz;

    private AudioSource audio;

    IEnumerator FadeOutLightening()
    {
        flash.enabled = true;
        float timer = 1;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Clamp01(timer);
            flash.color = new Color(1,1, 1, timer);
            yield return null;
        }

        flash.enabled = false;
    }

    // Use this for initialization
    IEnumerator Start ()
    {

        audio = gameObject.AddComponent<AudioSource>();

        tlb.gameObject.SetActive(false);
        dressTo.gameObject.SetActive(false);
        express.gameObject.SetActive(false);
        dancing.gameObject.SetActive(false);
        success.gameObject.SetActive(false);


        //yield return new WaitForSeconds(.5f);

        StartCoroutine(FadeOutLightening());

      //  yield return new WaitForSeconds(0.1f);


        float delay = 1;

       // yield return new WaitForSeconds(delay);
        tlb.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(delay);
        dressTo.gameObject.SetActive(true);
        audio.PlayOneShot(neonBuzz[0]);
        AudioController.instance.neon.Play();

        yield return new WaitForSeconds(delay);
        express.gameObject.SetActive(true);
        audio.PlayOneShot(neonBuzz[1]);
        AudioController.instance.FadeInBarMusic();
        

        yield return new WaitForSeconds(delay);
        dancing.gameObject.SetActive(true);
        audio.PlayOneShot(neonBuzz[2]);

        yield return new WaitForSeconds(delay);
        success.gameObject.SetActive(true);
        audio.PlayOneShot(neonBuzz[3]);
        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;
        }
        blackOut.enabled = true;
        yield return null;

        SceneManager.LoadScene("Bar");
        //StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Bar");

        asyncOperation.allowSceneActivation = false;
     //   asyncOperation.
        while (asyncOperation.isDone == false)
        {
            print(asyncOperation.progress);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}

}
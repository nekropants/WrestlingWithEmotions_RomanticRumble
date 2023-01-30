using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Dance { 
 public class PlayVideo : MonoBehaviour
{
    public PlayFrames movie;


    public AudioClip sound;
    public Material material;



    public PlayFrames VignetteIn;
    public PlayFrames Vignetteout;
    private float timer = 0;

    public AnimationCurve curve;
    public float curveM = 1;
    public GameObject tlbLogo;

    public SpriteRenderer blackOut;
    public Shake shake;
    public GameObject whiteOut;
    private bool initialized = false;
    // Use this for initialization
    IEnumerator Start ()
	{
        // yield return new WaitForSeconds(2f);
        tlbLogo.SetActive(false);

        material = GetComponent<Renderer>().sharedMaterial;
        movie.Stop();
        movie.Play();
        StartCoroutine(ChangeAnimations());

        initialized = true;
        //  Restart();

        yield return new WaitForSeconds(1.5f);
        tlbLogo.SetActive(true);
       // shake.AddShake(true);

    }

    void Restart()
    {
        timer = 0;
    }

    IEnumerator LowerVolume()
    {
        float timer = 1;
        while ( timer > 0)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Clamp01(timer);

            GetComponent<AudioSource>().volume = timer;
            yield return null;
        }
    }

    IEnumerator ChangeAnimations()
    {
        VignetteIn.Play();
        blackOut.gameObject.SetActive(false);
        yield return new WaitForSeconds(3.5f);
        // animator.runtimeAnimatorContller = titleAnim;

        Vignetteout.Play();

      //  blackOut.gameObject.SetActive(true);
        //tlbLogo.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        Next();
    }


void Next()
{

        GetComponentInParent<IntroScene>().walkInRainScene.gameObject.SetActive(true);
        GetComponentInParent<IntroScene>().gameObject.SetActive(false);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
    private IEnumerator WaitForAnimation(Animation animation)
    {
        do
        {
            yield return null;
        } while (animation.isPlaying);
    }


    // Update is called once per frame
    void Update ()
    {
        if(initialized == false)
            return;
        
     //   float y = curve.Evaluate(timer)*curveM;
     //   material.SetTextureOffset("_MainTex", new Vector2(0, y));
	    //timer += Time.deltaTime;

     //   material.mainTexture.wrapMode = TextureWrapMode.Repeat;
        if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.Mouse0))
        {
Next();
            
        }


            if (Input.GetKeyDown(KeyCode.F6))
            SceneManager.LoadScene("Intro");

	}
}

}
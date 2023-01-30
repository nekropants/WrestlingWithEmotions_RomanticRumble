using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Dance { 
 public class ShowBar : MonoBehaviour
{
    public AnimationCurve curve;

    public float lerp = 0;
    public static ShowBar instance;
    private float delay = 0.75f;

    public Sprite[] explosionFrames;
    public Image explosion;
    public Image normalglass;
    public Image brokenGlass;
    // Use this for initialization
    // Use this for initialization
    void Start () {
	    transform.localRotation = Quaternion.Euler(0, 0, (1-0)*-90);
	    instance = this;
	}

    // Update is called once per frame
    void Update ()
	{
        if (Application.isEditor && Input.GetKeyDown(KeyCode.X))
        {
            StartExplosionAnim();
        }

        if (delay > 0)
        {
            delay -= Time.deltaTime;
            return;

        }
        if (lerp < 1)
        {

            lerp += Time.deltaTime*2;
            lerp = Mathf.Clamp01(lerp);
            transform.localRotation = Quaternion.Euler(0, 0, (1 - curve.Evaluate(lerp))*-90);
        }

	}


    public void StartExplosionAnim()
    {
        StartCoroutine(RunExplosionAnim());
    }

    IEnumerator  RunExplosionAnim()
    {
        normalglass.gameObject.SetActive(false);
        brokenGlass.gameObject.SetActive(true);

        explosion.enabled = true;
        float frameRate = 1/24f;
        explosion.sprite = explosionFrames[0];
        yield return new WaitForSeconds(frameRate);
        yield return null; explosion.sprite = explosionFrames[1];
        yield return new WaitForSeconds(frameRate);
        int frame = 0;

        while (true)
        {
            yield return null; explosion.sprite = explosionFrames[frame + 2];
            yield return new WaitForSeconds(frameRate);
            frame++;
            frame %= 4;
        }


    }
}

}
using UnityEngine;
using System.Collections;

namespace Dance { 
 public class EndScreen : MonoBehaviour
{


    public float distance;
    private Vector3 offset;

    public float speed = 0.5f;

    public AnimationCurve curve;
    float lerp = -3f;
	// Use this for initialization

    void Awake()
    {
        print(DanceEvaluator.gaveUp);

    }
    void Start () {
	    AudioController.instance.danceTrack1.Stop();
	    AudioController.instance.danceTrack2.Stop();
		AudioController.instance.OutroTrack.outputAudioMixerGroup = AudioController.instance.OutroTrackFilteredMixer;
	   // AudioLowPassFilter filter = gameObject.GetComponent<AudioLowPassFilter>();
	    StartCoroutine(LerpInGlitchStatic());
	}

    IEnumerator LerpInGlitchStatic()
    {

        yield return new WaitForSeconds(4f);
        float timer = 0;

        AudioController.instance.glitchStatic.Play();
        AudioController.instance.glitchStatic.volume = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime / 5f;
            timer = Mathf.Clamp01(timer);

            AudioController.instance.glitchStatic.volume = timer;

            yield return null;
        }


    }

    // Update is called once per frame
    void Update ()
	{

        
	    lerp += Time.deltaTime;

	    if (lerp < 0)
	    {
	        return;
	    }

	    transform.position -= offset;
	    offset = Vector3.up*curve.Evaluate(lerp* speed) *distance; 
	    transform.position += offset;
    }
}

}
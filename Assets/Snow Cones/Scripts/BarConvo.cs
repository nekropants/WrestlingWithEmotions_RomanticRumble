using UnityEngine;
using System.Collections;

public class BarConvo : MonoBase
{

    public Transform face;
    public Transform speechBubble;
    private Vector3 defaultScale;

    public BarCone player;
	// Use this for initialization
	void Start ()
	{
	    defaultScale = speechBubble.transform.localScale;
	    speechBubble.transform.localScale = Vector3.zero;
	    player.enabled = false;

	    StartCoroutine(RunBarConvo());
	}
	
	// Update is called once per frame
	IEnumerator RunBarConvo ()
	{

        yield return new WaitForSeconds(.5f);
        float speed = 10;

	    while (AnyInputDown == false)
	    {
            yield return null;
	    }


	    float lerp = 0;


	    while (lerp < 1)
	    {
            lerp += Time.deltaTime * speed;
	        lerp = Mathf.Clamp01(lerp);
	        speechBubble.transform.localScale = defaultScale*lerp;
            yield return null;
        }



	    yield return new WaitForSeconds(.5f);
        float timer = 0;


	    Vector3 faceAnchor = face.transform.position;

        while (timer < Mathf.PI/2f)
        {
            timer += Time.deltaTime * 25;

            timer = Mathf.Clamp(timer, 0,  Mathf.PI/2f);
            Vector3 offset = Vector3.up * Mathf.Sin(timer) * 3.5f;

            face.transform.position = offset + faceAnchor;
            yield return null;
        }
	    int nods = 10;
        yield return new WaitForSeconds(1.5f);

	    while (timer < nods*Mathf.PI)
	    {
	        timer += Time.deltaTime*25;

	        timer = Mathf.Clamp(timer, 0, nods*Mathf.PI);
	        Vector3 offset = Vector3.up* Mathf.Sin(timer)*2;

	        face.transform.position = offset + faceAnchor;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        while (lerp > 0)
        {
            lerp -= Time.deltaTime * speed;
            lerp = Mathf.Clamp01(lerp);
            speechBubble.transform.localScale = defaultScale * lerp;
            yield return null;
        }

        player.enabled = true;


	}
}

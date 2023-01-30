using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Dance { 
 public class DanceBar : MonoBehaviour {

	// Use this for initialization

    public Image overflow;

    private float current = 1;
    private float delay = 0;

    private Vector3 defaultScale;
    public float m = 1;

    float h = 0;
    float s = 0;
    float v = 0;


    public bool scollColor = false;
    private Image image;

    public void NotifyChange( float _delay)
    {
        if (draining == false)
        {
            delay = _delay + 0.001f;
        }


    }

    private bool hasExploded = false;


    void Start ()
	{
	    defaultScale = transform.localScale;

	    image = GetComponent<Image>();

        Color.RGBToHSV(image.color, out h, out s, out v);
    }

    private bool draining = false;
	
	// Update is called once per frame
	void Update () {

        if(DanceEvaluator.instance == false)
            return;

	    if (delay > 0)
	    {
	        delay -= Time.deltaTime;

	        if (delay <= 0)
	        {
	            draining = true;
	        }
	    }

        float danceScore = (DanceEvaluator.instance.danceScore + 1f) / 1.3f;

        if (draining)
	    {

            current = Mathf.MoveTowards(current, danceScore, Time.deltaTime*0.2f*m);
	        if (current == danceScore)
	        {
	            draining = false;
	        }
            

            transform.localScale =  Vector3.Scale( defaultScale,new Vector3( Mathf.Clamp01(current),1,1));

	    }

	    if (scollColor)
	    {



	        if (danceScore > 1)
	        {

	            if (hasExploded == false)
	            {
	                hasExploded = true;
                    ShowBar.instance.StartExplosionAnim();
	            }
	            h += Time.deltaTime*danceScore*danceScore;
	            h = Mathf.Repeat(h, 1f);
	        }
	        float H = h;

	        if (danceScore <= 1)
	        {
	            H = Mathf.Repeat(h + danceScore*0.35f, 1f);

	        }



	        Color col = Color.HSVToRGB(H, s, v);
	        overflow.color = col;
	        image.color = col;
	    }
	}
}

}
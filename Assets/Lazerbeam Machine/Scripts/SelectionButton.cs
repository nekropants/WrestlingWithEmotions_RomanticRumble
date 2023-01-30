using UnityEngine;
using System.Collections;

public class SelectionButton : MonoBehaviour
{

    public MeshRenderer video;

Vector3 defaultScale;
    public SpriteRenderer highlight;
    public GameObject text;

    // Use this for initialization
    void Start () {
        defaultScale = transform.localScale;
	    //if (video)
	    //{
     //       MovieTexture movie = video.material.mainTexture as MovieTexture;
     //       movie.loop = true;
     //       movie.Play();
     //   }
    }


    public bool launchLazerJam = false;
	// Update is called once per frame
	void Update ()
	{

	    float size = 1;
	    if (highlighted)
	        size = 1.3f;

	    transform.localScale = Vector3.Lerp(transform.localScale, defaultScale*size, Time.deltaTime*10);
        highlight.enabled = mouseOver;


        if(text)
            text.gameObject.SetActive(highlighted);

        mouseOver = false;
    }

    public bool mouseOver;
    public bool keyboardOver;



    bool highlighted
    {
        get
        {
            return mouseOver || keyboardOver;
        }
    }
    void OnMouseOver()
    {

        mouseOver = true;
    }

    public void OnMouseDown()
    {
        print("mouse down");
        if(launchLazerJam)
        {
            GameLauncher.instance.Launchvideo();
        }
        else
        {
            GameLauncher.instance.Launch(this);
        }
    }
}

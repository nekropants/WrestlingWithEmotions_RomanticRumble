using UnityEngine;
using System.Collections.Generic;

namespace Dance { 
 public class AnimateSpriteFrames : MonoBehaviour
{
    public List<Sprite> frames = new List<Sprite>();

    private float timer = 0;
    public float interval = 1/25f;
    public int frame = 0;
    private SpriteRenderer renderer;

    public bool loop = true;
    public bool play = false;
    public bool sort = true;

    public  bool autoDestroy = false;

    public float length
    {
        get { return interval*frames.Count; }
    }
    // Use this for initialization
    void Start () {

        if(sort)
            frames.Sort((A, B) => { return (A+"").CompareTo(B +""); });
        renderer = GetComponent<SpriteRenderer>();
       // renderer.sharedMaterial.mainTexture = frames[0];
	        SetFrame();
    }

    // Update is called once per frame
    void Update ()
	{
        if(play== false)
            return;
        
	    timer += Time.deltaTime;
	    while (timer > interval)
	    {
	        timer -= interval;
	        frame++;

            if(autoDestroy && frame >= frames.Count)
                Destroy(gameObject);

            if(loop == false)
	            frame = Mathf.Clamp(frame, 0, frames.Count-1);
            else
            {
                frame %= frames.Count;
            }
	        SetFrame();

	    }


    }


    void SetFrame()
    {
        renderer.sprite = frames[frame];
        renderer.enabled = frames[frame] != null;
    }

    public void Stop()
    {
        play = false;
    }

    public void Play()
    {
        print("play"); 
        play = true;

    }
}

}
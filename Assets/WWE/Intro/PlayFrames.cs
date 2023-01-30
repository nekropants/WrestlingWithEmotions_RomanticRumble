using UnityEngine;
using System.Collections.Generic;

namespace Dance { 
 public class PlayFrames : MonoBehaviour
{
    public List<Texture2D> frames = new List<Texture2D>();

    private float timer = 0;
    public float interval = 1/25f;
    private int frame = 0;
    private MeshRenderer renderer;

    public bool loop = true;
    public bool play = false;
    public bool sort = true;
    public bool reverse = false;

        public float length
    {
        get { return interval*frames.Count; }
    }
    // Use this for initialization
    void Start () {

        if(sort)
            frames.Sort((A, B) => { return (A+"").CompareTo(B +""); });

            if (reverse)
                frames.Reverse();
        renderer = GetComponent<MeshRenderer>();
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
        renderer.material.mainTexture = frames[frame];
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
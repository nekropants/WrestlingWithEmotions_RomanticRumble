using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroVideo : MonoBase
{


int skipPressCount = 0;

    public static IntroVideo instance;

    public MeshRenderer renderer;

    public bool finished = false;
	// Use this for initialization
	IEnumerator Start ()
	{
	    instance = this;
        renderer = GetComponentInChildren<MeshRenderer>();
     //   MovieTexture movie =  renderer.material.mainTexture as MovieTexture;
	    
     //   movie.Play();
	    //AudioSource audio = gameObject.AddComponent<AudioSource>();
	    //audio.clip = movie.audioClip;
     //   audio.Play();
	    //audio.volume /= 2f;
        //yield return  new WaitForSeconds(movie.duration);
   //    Skip();

        yield break;
	}
	
	void Skip()
	{
		print("----");
	    finished = true;
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
		if(ActionDown)
		{
			skipPressCount ++;
			if(skipPressCount > 1)
			{
        		Skip();
 			}
		}
		
	
	}
}

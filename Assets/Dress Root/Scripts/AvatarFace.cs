using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Dance { 
 public class AvatarFace : MonoBehaviour
{
    public Image[] faces;
    public int currentFace = 3;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{



        if (DanceEvaluator.instance)
	    {
	       float  t = (1 - (DanceEvaluator.instance.danceScore + 1f)/2f);
	        currentFace = Mathf.RoundToInt(t*6);
	    }


	    for (int i = 0; i < 7; i++)
	    {
	        faces[i].enabled = i == currentFace;
	    }
	}
}

}
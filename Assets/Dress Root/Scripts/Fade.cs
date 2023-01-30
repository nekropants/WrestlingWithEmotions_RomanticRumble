using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Dance { 
 public class Fade : MonoBehaviour
{
    private float timer = 0;
    private Image image;
    private float prevTime = 0;
    // Use this for initialization
    void Start ()
	{
	    image = GetComponent<Image>();
        prevTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update ()
	{

        float deltaTime = Time.realtimeSinceStartup - prevTime;
        prevTime = Time.realtimeSinceStartup;

        timer += deltaTime * 0.5f;
	    timer = Mathf.Clamp01(timer);
        image.color = new Color(0, 0, 0, timer*0.7f);
	}
}

}
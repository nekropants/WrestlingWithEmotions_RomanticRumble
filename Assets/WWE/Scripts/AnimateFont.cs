using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimateFont : MonoBehaviour
{

    public Font[] fonts;
    private float timer = 0;
    private float interval = 0.1f;

    private Text text;

    private int frame = 0;
    // Use this for initialization
    void Start ()
    {
        text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;


        return;
        while (timer > interval)
        {
            timer -= interval;
            frame++;

            frame %= 2;

            text.font = fonts[frame];
        }
	
	}
}

using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Dance { 
 public class SpeechBubble : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = Vector3.zero;
    private Vector3 defaultScale;
    public bool hidden = true;
    private float timer = 0;
    public float frequency = 2;
    public AnimationCurve curve;
    public Transform bubble;
    public Text text;

    public bool followRotation = true;

    public bool mute = false;

    public Mouth mouth;

    public int index;

    public bool autoHide = false;
    float _timer = 2f;

    public bool hideOnStart = true;
    private bool mouseOver = false;

    public int Index
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }

    public void Hide()
    {
        hidden = true;
    }

    public void Show(string txt)
    {
        hidden = false;
        text.text = txt;

        if(mouth)
            mouth.Speak(0.5f + txt.Length*0.05f);
        if(mute == false)
            AudioController.instance.bubbleSound.Play();
    }
    // Use this for initialization

    void Awake()
    {
            defaultScale = bubble.localScale;
        bubble.localScale = Vector3.zero;

    }
    void Start ()
    {
        if (hideOnStart)
        {
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
        if(target)
        {

            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime*5);
            if(followRotation) transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime*0.3f);
        }

	    if (hidden)
	    {
            timer -= Time.deltaTime*20;
            timer = 0;
	    }
	    else
	    {
            timer += Time.deltaTime* frequency;
        }

        timer = Mathf.Clamp01(timer);

	    float lerp = timer;

	    if (hidden == false)
	    {
	        lerp = curve.Evaluate(timer);
	    }

	    if (mouseOver)
	        lerp += 0.15F;

	    if (autoHide)
	    {
	        if (_timer > 0)
	        {
	            _timer -= Time.deltaTime;
	            if (_timer <= 0)
	            {
	                Hide();
	            }
	        }
	    }

	    bubble.localScale = defaultScale*lerp;
	}


    public void OnMouseEnter()
    {
        mouseOver = true;
    }

    public void OnMouseExit()
    {
        mouseOver = false;
    }

    public void OnMouseClick()
    {
        mouseOver = false;
        if(ChatToDate.instance)
            ChatToDate.instance.SetThought(index);

        if (ChatToDate.instance)
            ChatToDate.instance.SetThought(index);

        if (DanceEvaluator.instance)
        {
            DanceEvaluator.instance.SetThought(index);
        }
    }


}

}
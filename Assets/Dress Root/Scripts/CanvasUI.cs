using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Dance { 
 public class CanvasUI : MonoBehaviour
{

    public AvatarFace date1Sprite; 
    public AvatarFace date2Sprite;
    public ShowAvatar avatar;
    public static CanvasUI instance;
    public Transform UIroot;
    public static bool showDate1 = true;
    public ShowAvatar textBox;
    Text _text;
    public AvatarFace activeFace;
    public Mouth mouth;

    public string text
    {
        set { 
            textBox.showing = true;
            if (mouth)
                mouth.Speak(0.5f + value.Length * 0.05f);


            AudioController.instance.bubbleSound.Play();

            _text.text = value;
        }
    }

    // Use this for initialization
    void Awake ()
	{
        instance = this;
        _text = textBox.GetComponentInChildren<Text>();
        UIroot.gameObject.SetActive(true);

	}

    void Start()
    {
        if(ChatToDate.instance == null)
            Show(showDate1);
    }

    public void Hide()
    {
        avatar.showing = false;
        avatar.hiding = true;
    }

    public void Show(bool date1)
    {

        showDate1 = date1;
        avatar.showing = true;
        avatar.hiding = false;

        print("showDate1  " + showDate1);
        if (showDate1)
        {
            date1Sprite.gameObject.SetActive(true);
            date2Sprite.gameObject.SetActive(false);
            activeFace = date1Sprite;
            UIroot.localScale = new Vector3(-1, 1, 1);
            _text.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            activeFace = date2Sprite;
            date1Sprite.gameObject.SetActive(false);
            date2Sprite.gameObject.SetActive(true);
            UIroot.localScale = new Vector3(1, 1, 1);
            _text.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    // Update is called once per frame
    void Update () {
	    
	}
}

}
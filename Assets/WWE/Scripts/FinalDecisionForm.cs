using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinalDecisionForm : MonoBehaviour
{

    public static FinalDecisionForm instance;

    public Image form;
    public Image contents;
    public Text nameText;
    public GameObject stickerTransform;
    
    public Descriptors descriptors;

    // Use this for initialization
    void Awake ()
	{
	    instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Show(Sprite _contents, string name, GameObject stickers, int index)
    {
        form.gameObject.SetActive(true);
        nameText.text = name;
        contents.sprite = _contents;

        if(stickerTransform)
            stickerTransform.gameObject.SetActive(false);

        if (stickers)
        {
            stickerTransform = stickers;
            stickers.transform.parent = form.transform;
            stickers.transform.position = contents.transform.position;

            stickers.transform.localScale = Vector3.one;
            stickerTransform.gameObject.SetActive(true);


        }
        
        descriptors.seed = index;
        descriptors.Refresh();
    }

    public void Hide()
    {
        form.gameObject.SetActive(false);
    }


}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dance { 
 public class HurlInsults : MonoBehaviour
{

    private int index = 0;
    public SpeechBubble prefab;
    string[,] combos =
     
    {
        {"Sloppy","Holy","Macaroni","90"},
        {"Cold","Hot","Legs","180"},
        {"Weak","Strong","Coffee","360"},
        {"Sagging","Springing","Digits","720"},
    };
    public static HurlInsults instance;

	// Use this for initialization
	void Start ()
	{
      
	    instance = this;
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void CreateInsult(Transform commenter)
    {
        return;
        
        SpeechBubble inst = Instantiate(prefab);
        inst.transform.SetParent(prefab.transform.parent, false);
        inst.gameObject.SetActive(true);
        index %= combos.GetLength(0);

        inst.autoHide = true;
        inst.Show( combos[index, 0] + " "+ combos[index, 2] + "!");
        inst.target = commenter;
        inst.hideOnStart = false;
        inst.transform.position = commenter.transform.position;
        float dir = Mathf.Sign(Player.instance.transform.position.x - commenter.transform.position.x);

        if (dir < 0)
        {
            inst.transform.localScale.Scale(new Vector3(-1, 1, 1));
            inst.text.transform.localScale.Scale( new Vector3( -1, 1 ,1));
        }

        index++;
    }
}

}
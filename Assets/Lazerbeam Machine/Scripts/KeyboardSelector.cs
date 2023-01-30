using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardSelector : MonoBase {

    List<SelectionButton> buttons;

    int index = 0;
    bool activated = false;
	// Use this for initialization
	void Start () {
        buttons = new List<SelectionButton>(GetComponentsInChildren<SelectionButton>());
		
	}
	
	// Update is called once per frame
	void Update () {

        if(activated)
        {
            if (LeftDown)
                index--;

            if (RightDown)
                index++;

            index %= buttons.Count;


            if(this.ActionDown)
            {
                buttons[index].OnMouseDown();
            }
        }

		if(AnyInputDown)
        {
            activated = true;
            SetSelected();
        }
	}

    void SetSelected()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].mouseOver =false;

            buttons[i].keyboardOver = i == index;
        }
    }
}

using UnityEngine;
using System.Collections;

namespace Dance { 
 public class TriggerPurpleLeather : MonoBehaviour {

	public bool purpleLeather = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private bool on = false;
	bool triggered = false;
	void OnTriggerEnter2D(Collider2D collider)
	{
        if(ChatToDate.instance.everyOneWalkedOff )
            return;

	    on = true;
             if (ChatToDate.instance.chatting)
            return;

            CanvasUI.instance.Show(purpleLeather);

		if(triggered)
			return;

			triggered = true;
		if(purpleLeather)
		{
			ChatToDate.instance.RunPurpleLeather();
		}
		else
		{
			ChatToDate.instance.RunHotShot();
		}

	}

    void OnTriggerExit2D(Collider2D collider)
    {
        if (ChatToDate.instance.engaged)
            return;

        if (on)
        {
        CanvasUI.instance.Hide();
            on = false;
        }
    }


}

}
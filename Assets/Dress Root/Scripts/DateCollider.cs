using UnityEngine;
using System.Collections;

namespace Dance { 
 public class DateCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
		if(ChatToDate.instance.everyOneWalkedOff)
           return;

		if(other.GetComponent<Rigidbody2D>() == false )
			return;
        
         Character character = GetComponentInParent<Character>();

        if(character)
        	ChatToDate.instance.StartConversation(character);

    }
}

}
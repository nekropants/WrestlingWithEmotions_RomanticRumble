using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Dance { 
 public class GoDanceCollider : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(ChatToDate.instance.everyOneWalkedOff == false)
           return;
            enabled = false;
        Wipe.intance.WipeOn();
        
        
    }


}

}
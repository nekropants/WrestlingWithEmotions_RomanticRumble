using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

//[RequireComponent(typeof(Collider2D ))]
namespace Dance { 
 public class ClickableThought : MonoBehaviour
{
    private SpeechBubble  thought;
    private bool over = false;
    // Use this for initialization
    void Start ()
    {
        thought = GetComponentInParent<SpeechBubble>();
    }

  


    public void OnPointerClick()
    {

        thought.OnMouseClick();
   


    }



    public void OnPointerEnter()
    {
        thought.OnMouseEnter();
       // throw new System.NotImplementedException();
    }

    public void OnPointerExit()
    {

        thought.OnMouseExit();


        // throw new System.NotImplementedException();
    }


}

}
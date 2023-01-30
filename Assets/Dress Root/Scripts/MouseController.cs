using UnityEngine;
using System.Collections;

namespace Dance { 
 public class MouseController : MonoBehaviour
{
    private Clickable clickable;
    private ClickableThought thought;
    public Camera camera;
    public static bool mouseOverLimb = false ;

    // Use this for initialization
    void Start () {
	
	}

    private bool mouseWasDown = false;
	// Update is called once per frame
	void Update ()
	{

	    Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	    Collider2D collider = Physics2D.OverlapPoint(world);


	    mouseOverLimb = false;

        if(clickable)

            clickable.OnPointerExit();

        if (thought)
        {
            thought.OnPointerExit();

        }
        if (collider)
	    {
	        clickable = collider.GetComponent<Clickable>();
            thought = collider.GetComponent<ClickableThought>();



            if (clickable)
	        {
	            mouseOverLimb = true;
                clickable.OnPointerEnter();

            }

	        if (thought)
	        {
                thought.OnPointerEnter();

            }

        }
	    else
	    {
	        clickable = null;
	        thought = null;
	    }

	    if (Input.GetKeyDown(KeyCode.Mouse0))
	    {
            if(clickable)
                clickable.OnPointerClick();

            if(thought)
                thought.OnPointerClick();
        }

	    mouseWasDown = Input.GetKey(KeyCode.Mouse0);



	}


    void OnDestroy()
    {
	    mouseOverLimb = false;
    }
}

}
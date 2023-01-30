using UnityEngine;
using System.Collections;

namespace Dance { 
 public class BarTender : MonoBehaviour
{
    public Transform cash; 
    public Transform beerBottle;
    public Transform playerHand;

    public CircleCollider2D handCollider;
    private Animator anim;

    private bool hasCash = false;
    private bool hasBeer = false;

    private bool done = false;
    // Use this for initialization
    void Start ()
    {

        anim = GetComponent<Animator>();
        anim.enabled = false;
        beerBottle.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update () {


	    if (hasCash == false)
	    {

            if (handCollider.OverlapPoint(playerHand.transform.position))
	        {
	            cash.transform.parent = handCollider.transform;
	            hasCash = true;

	            anim.enabled = true;
	        }
	    }
        if (hasBeer )
        {
            if (handCollider.OverlapPoint(playerHand.transform.position))
            {
                beerBottle.transform.parent = playerHand;
                hasBeer = false;


                StartCoroutine(ChangeScene());
            }
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
                StartCoroutine(ChangeScene());

        }
    }


    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(3);
        Destroy(transform.parent.gameObject);
    }

    public void GetBeer()
    {
        hasBeer = true;
        cash.gameObject.SetActive(false);
        beerBottle.gameObject.SetActive(true);
    }
}

}
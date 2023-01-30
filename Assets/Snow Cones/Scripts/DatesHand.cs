using UnityEngine;
using System.Collections;

public class DatesHand : MonoBehaviour
{

    public FerrisWheelHandController playerHand;
    private int pullsAway = 0;
    public Animator coneAnimation;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        if(coolDown > 0)
	    coolDown -= Time.deltaTime;
	}

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    print("OnTriggerEnter2D "  + other);
    //}


    private float coolDown = 0;

    void OnTriggerStay2D(Collider2D other)
    {

        if (coolDown <= 0)
        {

            print("Play anim " + other);
            if (pullsAway == 0)
            {
                coneAnimation.Play("Hand Ainim Pull Away");
                pullsAway++;
                coolDown = 2;
                playerHand.PullAway = true;
            }
            else if (pullsAway == 1)
            {
                coneAnimation.Play("hand Anim Scratch");
                pullsAway++;
                coolDown = 2;
                playerHand.PullAway = true;
            }

            else if (pullsAway == 2)
            {
                coneAnimation.Play("Blush");
                pullsAway++;
                coolDown = 2;
                playerHand.holdHand = true;
                playerHand.handPos = transform.position;
                //playerHand.enabled = false;
                StartCoroutine(SwitchBackToMap());
            }
        }
    }

     IEnumerator SwitchBackToMap()
     {
         yield return new WaitForSeconds(5);
         SceneController.ChangeScene(SceneEnum.Map);
     }
}

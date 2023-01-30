using System;
using UnityEngine;
using System.Collections;

namespace Dance { 
 public class Player : MonoBehaviour
{


    public float speed =  1;
    public float speedY = 0.5f;


    public Eye leftEye;
    public Eye rightEye;

    public static Player instance;

    public Transform    Head;
    public bool canWalk = true;

    public Transform beer;

    public bool isClickable = false;

    public static bool hasBeer = false;

    public Fade creditFade;

    public Eyes playerEyes;

    public GameObject bestPhoto;
    public GameObject worstPhoto;
    // Use this for initialization
    void Start ()
    {
        instance = this;
        if(beer)
            beer.gameObject.SetActive(hasBeer);

        playerEyes = GetComponentInChildren<Eyes>();

    }

    public bool walking = false;

    private bool hasJumped = false;


    public void FreezeFrame()
    {
	        StartCoroutine(Jump());

    }

    public void BelieveInNothing()
    {
        StartCoroutine(BelieveInNothingRoutine());
    }
    IEnumerator  BelieveInNothingRoutine()
    {
        CanvasUI.instance.avatar.hiding = true;

        ShowBar.instance.gameObject.SetActive(false);
        Destroy(GetComponent<Rigidbody2D>());
        float timer = 0;

        Vector3 pos = transform.position;
        pos.z = -4.5f;
        transform.position = pos;


        yield return new WaitForSeconds(1f);
        while (timer < 1)
        {

            timer += Time.deltaTime*1.5f;
            timer = Mathf.Clamp01(timer);

            transform.rotation = Quaternion.Euler(0, 0, timer*-90);
            yield return null;
        }

        yield return new WaitForSeconds(10f);
        AudioController.instance.PlayOutroTrack();

        Time.timeScale = 0.00001f;
        creditFade.gameObject.SetActive(true);
        worstPhoto.gameObject.SetActive(true);

    }
    IEnumerator Jump()
    {
        if (hasJumped == false)
        {
            DanceEvaluator.instance.handsUp = true;
            yield return new WaitForSeconds(0.8f);
            Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
            foreach (var collider1 in colliders)
            {
                collider1.enabled = false;
            }
       

            DanceEvaluator.instance.CreateSpriteExplosion();
            isClickable = false;
            hasJumped = true;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up*15, ForceMode2D.Impulse);
            yield return  new WaitForSeconds(0.3f);

			AudioController.instance.PlayOutroTrack ();

             Time.timeScale = 0.00001f;
            creditFade.gameObject.SetActive(true);
            bestPhoto.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
	void Update ()
	{
	    if (Input.GetKey(KeyCode.Tab) && Application.isEditor)
	    {
	        StartCoroutine(Jump());
	    }

            if (rightEye) rightEye.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (leftEye) leftEye.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));


        walking = false;

        if((ChatToDate.instance && ChatToDate.instance.chatting ) || BuyDrinkController.instance || canWalk == false)
            return;
	    

	    bool left = Input.GetKey(KeyCode.A);
	    bool right = Input.GetKey(KeyCode.D);
	    bool down = Input.GetKey(KeyCode.S);
	    bool up = Input.GetKey(KeyCode.W);

	    Vector3 direction = Vector3.zero;

	    if (left)
	        direction.x--;


        if (right)
            direction.x++;

        if (up)
            direction.y++;

        if (down)
            direction.y--;

	    if (Input.GetKeyDown(KeyCode.Mouse0) && MouseController.mouseOverLimb)
	    {
	        mouseDownOverLimb = true;

	    }

        if(Input.GetKeyUp(KeyCode.Mouse0))
            mouseDownOverLimb = false;


        if (Input.GetKey(KeyCode.Mouse0) && mouseDownOverLimb == false)
        {
            Vector3 target;

            target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Vector3.up * 7;
            target.z = transform.position.z;

            direction.Normalize();
            direction.y *= speedY;

            //   transform.position += direction*Time.deltaTime*speed;

            walking = true;

            Vector3 pos = Vector3.MoveTowards( transform.position, target, Time.deltaTime*speed);
	        pos.y = Mathf.Clamp(pos.y, -6f, -1);
	        pos.x = Mathf.Clamp(pos.x, -12.5f, 16f);

	        transform.position = pos;

          
        }
       
    }


    private bool mouseDownOverLimb = false;
    public void ShowBeer()
    {
        if(beer)
        {
            beer.gameObject.SetActive(true);

        }
        print("show beer");

        hasBeer = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
    }
}

}
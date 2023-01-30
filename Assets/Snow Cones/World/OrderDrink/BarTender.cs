using UnityEngine;
using System.Collections;

public class BarTender : MonoBehaviour
{

    private int customersServed = 0;


    public Rigidbody2D wipingArm;
    public SpriteRenderer armAtSide;
    public Rigidbody2D hand;
    private Vector3 handPos;

    public static BarTender Instance;
	// Use this for initialization
	void Start ()
	{
	    handPos = hand.transform.position - transform.position;
        SetEyes(eyesForward);

        StartCoroutine(WipeBar());
        StartCoroutine(GetPatron());


	    Instance = this;
	}


    public SpriteRenderer eyesClosed;
    public SpriteRenderer eyesForward;
    public SpriteRenderer eyesRight;
    public SpriteRenderer eyesLeft;

	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator WipeBar()
    {
        while (true)
        {
            float speed = 3;
            float radius = 20f;
            float xOffset = -Mathf.Sin(Time.time * speed) * radius;
            float yOffset = Mathf.Cos(Time.time * speed) * radius*0.6f;
            hand.transform.position = transform.position + handPos + new Vector3(0 + xOffset, yOffset, 0);

            yield return null;
        }
    }

    public IEnumerator GetPatron()
    {

        SetEyes(eyesForward);


        print("GetPatron");
        ConeAtBar patron = null;
        while (patron == null)
        {
         patron = OrderDrinkController.Instance.GetThisrtyPatron();
            yield return new WaitForSeconds(1);
        }

        // checkIfPlayersHandIsUp
        if (customersServed >= 2)
        {
            if (OrderDrinkController.Instance.player.GetAttention && OrderDrinkController.Instance.player.moving == false)
            {
                patron = OrderDrinkController.Instance.player;
                patron.gettingServed = true;
            }
        }
        if (customersServed >= 3)
        {
            patron = OrderDrinkController.Instance.player;
            patron.gettingServed = true;
        }

        yield return new WaitForSeconds(.3f);
        if (patron.isPlayer == false)
            patron.RaiseHand(true);


        yield return new WaitForSeconds(1.5f);

        customersServed++;


      
        StartCoroutine(MoveToPatronRoutine(patron));
    }


    private Vector3 Offset = Vector3.zero;


    private void SetOffset(Vector3 newOffset)
    {
        transform.position -= Offset;
        Offset = newOffset;
        transform.position += Offset;
    }

    public IEnumerator MoveToPatronRoutine(ConeAtBar cone)
    {
       

        print("MoveToPatronRoutine " + cone);
        yield return null;
        Vector3 startpos = transform.position;
        Vector3 dir = cone.transform.position - transform.position;
        Vector3 targetPos = cone.transform.position - dir*40;

        float displacment = 0;
        float diff = dir.magnitude - 100;
        dir.Normalize();

        if (dir.x > 0)
        {
            SetEyes(eyesRight);
        }
        else
        {
            SetEyes(eyesLeft);
        }


        yield return new WaitForSeconds(1.5f);
        SetEyes(eyesClosed);
        wipingArm.gameObject.SetActive(false);
        armAtSide.gameObject.SetActive(true);
       // float dist = Vector3.Distance(startpos, targetPos);


        while (displacment < diff)
        {
            displacment += Time.deltaTime*300f;

            transform.position = startpos + dir*displacment;

            float yOffset = Mathf.Sin(displacment * Mathf.PI * 2 *0.01f);

            transform.SetY(startpos.y + yOffset*4f);
            yield return null;
        }



        cone.Serve();

        float shakeTimer = 0;
        while (shakeTimer < 1)
        {
            shakeTimer += Time.deltaTime * 0.5f;
            shakeTimer = Mathf.Clamp01(shakeTimer);

            SetOffset(Mathf.Sin(shakeTimer * 35f) * Vector3.up * 3f);
            yield return null;
        }


        StartCoroutine(PolishBar());

    }

    private void SetEyes(SpriteRenderer eyes)
    {
        eyesClosed.gameObject.SetActive(false);
        eyesForward.gameObject.SetActive(false);
        eyesRight.gameObject.SetActive(false);
        eyesLeft.gameObject.SetActive(false);

        eyes.gameObject.SetActive(true);
    }

    private IEnumerator PolishBar()
    {
        yield return new WaitForSeconds(Random.Range(1.1f, 1.8f));
        wipingArm.gameObject.SetActive(true);
        armAtSide.gameObject.SetActive(false);
        yield return new WaitForSeconds(Random.Range(2, 4f));

        StartCoroutine(GetPatron());

 



    }
}

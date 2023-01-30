using UnityEngine;
using System.Collections;

public class ConeAtBar : MonoBase
{


    public bool gettingServed = false;
    public bool served = false;
    public bool isPlayer = false;
    public bool moving = false;
    public bool handUp = false;


    private float lastHandUpTime = 0;

    public bool GetAttention
    {
        get
        {
            if (handUp)
                return true;

            return (lastHandUpTime + 1.5f) > Time.time;
        }

    }
    public bool hasBeenServed = false;
    public int lastSlot = -1;

    public SpriteRenderer armRaised;
    public SpriteRenderer armDown;
    public Transform milkshakeTransform;
    public Transform sunBurstTransform;


    public void MoveToPosition(Vector3 target)
    {


        StartCoroutine(MoveToPositionRoutine(target, 1));

    }

    IEnumerator   MoveToPositionRoutine( Vector3 to, float speed)
    {
        moving = true;
        Vector3 startpos = transform.position;
        Vector3 dir = to - transform.position;

        float displacment = 0;
        float diff = dir.magnitude;
        dir.Normalize();

        // float dist = Vector3.Distance(startpos, targetPos);


        while (displacment < diff)
        {
            displacment += Time.deltaTime * 500f;

            transform.position = startpos + dir * displacment;

            float yOffset = Mathf.Sin(displacment * Mathf.PI * 2 * 0.01f);

            float downAmount = displacment/diff;
            downAmount = -Mathf.Sin(downAmount*Mathf.PI)*50f;

            transform.SetY(startpos.y + yOffset * 4f + downAmount);
            yield return null;
        }


        moving = false;

    }

    public void Serve()
    {
        StartCoroutine(ServeRoutine());
    }

    private Vector3 Offset = Vector3.zero;


    private void SetOffset( Vector3 newOffset)
    {
        transform.position -= Offset;
        Offset = newOffset;
        transform.position += Offset;
    }

    public static void CompleteScene()
    {
        BarCone.playerInstance.hasMilkshake = true;
        SceneController.ChangeScene(SceneEnum.ShakeBar);
    }

    private IEnumerator ServeRoutine()
    {
        float shakeTimer = 0;
        while (shakeTimer < 1 )
        {
            shakeTimer += Time.deltaTime*0.5f;
            shakeTimer = Mathf.Clamp01(shakeTimer);

            SetOffset( -Mathf.Sin(shakeTimer*45f)*Vector3.up*4f);
            yield return null;
        }


        if (isPlayer)
        {
            StartCoroutine(SunburstRoutine());
        }
        else
            StartCoroutine(ServeRoutineScreenRoutine());

    }


    private IEnumerator SunburstRoutine()
    {
        milkshakeTransform.gameObject.SetActive(true);
        armDown.gameObject.SetActive(false);
        armRaised.gameObject.SetActive(false);

        Vector3 anchor = milkshakeTransform.transform.position;
        float lerp = 1;

        float offset = 300;
        milkshakeTransform.transform.position = anchor + Vector3.down * offset;
        yield return new WaitForSeconds(2f);

        while (lerp > 0)
        {
            milkshakeTransform.transform.position = anchor + Vector3.down * lerp * offset;
            lerp -= Time.deltaTime*8;
            yield return null;
        }

        sunBurstTransform.gameObject.SetActive(true);
      //  BarTender.Instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(2);

        CompleteScene();

    }

    public void MoveOffScreen()
    {
        StartCoroutine(ServeRoutineScreenRoutine());
    }

    private IEnumerator ServeRoutineScreenRoutine()
    {
        print("ServeRoutineScreenRoutine");
        yield return new WaitForSeconds(0.5f + Random.value);
        float moveDown = 0;
        while (moveDown < 1)
        {
            moveDown += Time.deltaTime * 1;
            moveDown = Mathf.Clamp01(moveDown);

            SetOffset(Vector3.down * moveDown * 800);

            yield return null;
        }

        OrderDrinkController.Instance.MoveOutOfSlot(this);

        yield return null;

        hasBeenServed = true;

        yield return new WaitForSeconds(Random.Range(3, 6f));

        StartCoroutine(FindNewSpot());
    }

    public void SnapOffscreen()
    {
        StartCoroutine(SnapOffscreenRoutine());
    }

    private IEnumerator SnapOffscreenRoutine()
    {
        SetOffset(Vector3.down * 1 * 800);
        OrderDrinkController.Instance.MoveOutOfSlot(this);

        yield return null;

        hasBeenServed = true;

        yield return new WaitForSeconds(8f);

        StartCoroutine(FindNewSpot());
    }

    private IEnumerator FindNewSpot()
    {
        print("FindNewSpot()");
        int slot = OrderDrinkController.Instance.GetEmptySlot(lastSlot);
        SetOffset(Vector3.zero);
        transform.position = OrderDrinkController.Instance.ClaimSlot(this, slot);

        int dist = 800;
        SetOffset(Vector3.down * dist);

        float lerp = 0;
        while (lerp < 1)
        {
            lerp += Time.deltaTime * 1f;
            lerp = Mathf.Clamp01(lerp);
            SetOffset(Vector3.down * dist * (1 - lerp));

            yield return null;
        }


        served = false;
        yield return null;
    }



    // Use this for initialization
	void Start ()
	{

        if (isPlayer == false && Random.value > 0.5f)
	    {
	        Vector3 scale = transform.localScale;
	        scale.x *= -1;
	        transform.localScale = scale;
        }
	}


    //private void RaiseArm()
    //{
    //    if(armRaised != null)
    //        armRaised.gameObject.SetActive(true);
    //    if(armDown != null)
    //        armDown.gameObject.SetActive(false);
    //}
    //  private void LowerArm()
    //{
    //    if(armRaised != null)
    //        armRaised.gameObject.SetActive(true);
    //    if(armDown != null)
    //        armDown.gameObject.SetActive(false);
    //}

    public void RaiseHand(bool random = false)
    {

        StartCoroutine(RaiseHandRoutine(random));
    }

    private IEnumerator RaiseHandRoutine( bool random = false)
    {
        handUp = true;
        armDown.enabled = false;
        armRaised.enabled = true;
        yield return new WaitForSeconds(2.5f);
        if (random)
            yield return new WaitForSeconds(0.5f + Random.value*1.5f);

        armDown.enabled = true;
        armRaised.enabled = false;
        handUp = false;
    }

    // Update is called once per frame
	void Update () {
	    if (isPlayer)
	    {

            handUp = Up;
            armDown.enabled = !handUp;
            armRaised.enabled = handUp;

            if(handUp)
                lastHandUpTime = Time.time;


            if(Input.GetKeyDown(KeyCode.F2) )

	        {
            CompleteScene();

	        }
	    }
	}

}

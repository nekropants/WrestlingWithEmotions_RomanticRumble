using UnityEngine;
using System.Collections;

public class HumanHeadController : MonoBase
{
    public  SceneEnum nextScene = SceneEnum.Kiss;

    public CircleCollider2D  trigger;

    public SpriteRenderer dateConcernFace; 

    public SpriteRenderer face;
    public Sprite mouthOpen;
    public Sprite mouthClosed;
    public Sprite mouthMunch;
    public Transform headAnchor;
    public HumanHands handController;
    public SpriteRenderer eyes;
    public AudioClip munchingSounds; 

    private int numberOfMeals = 0;

    public Rigidbody2D hand;

    enum State
    {
        Open,
        Closing,
        ClosedOnHAnd,
        Chewing,
    }
    private float lerpOntoHand = 0;

    State state = State.Open;


    private Vector3 defaultHeadPos;
	// Use this for initialization
	void Start () {
      //  StartCoroutine(Munch());
	    defaultHeadPos = headAnchor.localPosition;
	    face.sprite = mouthOpen;

	    dateConcernFace.enabled = false;
	}
	
	// Update is called once per frame
    private void Update()
    {

        if (state == State.Open || state == State.Chewing)
        {
            headAnchor.localPosition = Vector3.Lerp(headAnchor.localPosition, defaultHeadPos, Time.deltaTime );

        }
        if (state == State.Open && handController.hasLeafs)
        {
            if (Vector3.Distance(hand.position, trigger.transform.position) < trigger.radius)
            {
                StartCoroutine(CloseOnHand());
            }
        }


        if (state == State.Closing)
        {

            Vector3 delta = hand.transform.position - trigger.transform.position;
            headAnchor.position += delta * lerpOntoHand;
            print("lerpOntoHand " + lerpOntoHand);
        }

    }

    private IEnumerator CloseOnHand()
    {
        handController.BeingPulledOn = true;
        state = State.Closing;


        while (lerpOntoHand < 1f)
        {
            lerpOntoHand += Time.deltaTime*5f;
            lerpOntoHand = Mathf.Clamp01(lerpOntoHand);

            yield return null;
            
            

        }
        handController.FeedHuman();
        face.sprite = mouthClosed;





        while (!Left)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);
        if (numberOfMeals > 1)
        {
            dateConcernFace.enabled = true;
            yield return new WaitForSeconds(Random.Range(3, 5f));

        }
        handController.BeingPulledOn = false;
        handController.ReleaseHand();

        StartCoroutine(Munch());


        yield return null;
    }

    private IEnumerator Munch()
    {

        dateConcernFace.enabled = false;
        AudioController.Play(munchingSounds);
        numberOfMeals++;
        state = State.Chewing;
        eyes.enabled = true;
        int munches = Random.Range(8, 15);
        for (int i = 0; i < munches; i++)
        {

            face.sprite = mouthClosed;
            yield return new WaitForSeconds(0.1f);
            face.sprite = mouthMunch;
            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
        eyes.enabled = false;

        face.sprite = mouthOpen;
        state = State.Open;



        if (numberOfMeals > 2)
        {
            StartCoroutine(TransitionToNextScene());
        }
    }

    private IEnumerator TransitionToNextScene()
    {
        yield return new WaitForSeconds(1);
        SceneController.ChangeScene(nextScene);
    }


}

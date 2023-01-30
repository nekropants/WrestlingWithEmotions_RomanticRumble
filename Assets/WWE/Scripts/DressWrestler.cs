using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WWE
{
   


public class DressWrestler : MonoBehaviour
{
    private SpriteRenderer chosenMakeUp;
    private SpriteRenderer chosenMask;
    private SpriteRenderer chosenHeadgear;
    private SpriteRenderer chosenFacialHair;

    public Transform facialHair;
    public SpriteRenderer[] facialHairSprites;

    public Transform makeUp;
    public SpriteRenderer[] makeUpSprites;

    public Transform masks;
    public SpriteRenderer[] maskSprites;

    public Transform headGear;
    public SpriteRenderer[] headGearSprites;
    private int frame = 0;

    public static DressWrestler instance;
        public GameObject exitScene;

        public TextMesh name;

    public Transform flyer;

    private bool selected = false;
    // Use this for initialization

    public Transform root;

    public AudioClip flyerSound;

    public AnimationCurve curve;
    enum Item {Headgear, Mask, MakeUp, Beards, Done }
    Item currentItem = Item.Headgear;
    public GameObject selector;
	void Start ()
	{
	    instance = this;
        facialHairSprites = facialHair.GetComponentsInChildren<SpriteRenderer>(true);
          List<  SpriteRenderer> headGearSpritesList = new List<SpriteRenderer>( headGear.GetComponentsInChildren<SpriteRenderer>(true));
	    for (int i = headGearSpritesList.Count - 1; i >= 0; i--)
	    {
	       if(headGearSpritesList[i].transform.parent != headGear.transform)
                    headGearSpritesList.RemoveAt(i);

        }

	    headGearSprites = headGearSpritesList.ToArray();

        maskSprites = masks.GetComponentsInChildren<SpriteRenderer>(true);
        makeUpSprites = makeUp.GetComponentsInChildren<SpriteRenderer>(true);
        StartCoroutine(Routine());
        flyer.gameObject.SetActive(true);
	}



    private bool leaving = false;

    public static void Leave()
    {
        instance.leaving = false;

        if (instance.leaving == false)
        {
            ThoughtBubble.instance.Hide();
                instance.leaving = true;
                ExitTrailer.instance.Trigger();


                print("leave");

              //  instance.StartCoroutine(instance.StandUpRoutine());
        }
    }

    public void ChangeSceneRoutine()
    {
            instance.StartCoroutine(instance.ChangeScene());


        }
        IEnumerator ChangeScene()
    {

            DressWrestler.instance.gameObject.SetActive(false);
             exitScene.gameObject.SetActive(true);
                print("ChangeScene");

            exitScene.gameObject.SetActive(true);

            yield return null;
    }

    

    public static string chosenName = "Mr Psycho Death Dad";
	// Update is called once per frame
	void Update ()
	{
	   chosenName = "";

	    if (chosenHeadgear)
            chosenName += chosenHeadgear.name + " ";

            if (chosenMask)
                chosenName += chosenMask.name + " ";

            if (chosenMakeUp)
           chosenName += chosenMakeUp.name + " ";


        if (chosenFacialHair)
            chosenName += chosenFacialHair.name + " ";
            
        chosenName =    chosenName.Trim();
            name.text = chosenName;
    }



    IEnumerator MoveFlyer()
    {
        
        float timer = 0;
        
        
        Vector3 flyerPos =     flyer.transform.position ;
        while (timer < 1)
        {
            timer += Time.deltaTime*3;
            timer = Mathf.Clamp01(timer);
            
            flyer.transform.position =flyerPos + Vector3.down*10*timer;
            yield return null;
        }
        yield return null;
    }

    IEnumerator TrackInCamera()
    {

        float timer = 00;
        while (timer < 1)
        {
            timer += Time.deltaTime / (3.85f * 2f);
            timer = Mathf.Clamp01(timer);
            GetComponent<Camera>().orthographicSize = 5 + curve.Evaluate(timer) / 10;
            GetComponent<Camera>().orthographicSize = Mathf.Max(GetComponent<Camera>().orthographicSize, 5);

            yield return null;
        }

    }

    public IEnumerator Routine()
    {

        Fisto.instance.gameObject.SetActive(false);

        selector.SetActive(false);

        StartCoroutine(TrackInCamera());

        yield return WaitForClick();

        AudioController.Play(flyerSound, 0.5f);
        yield return StartCoroutine(MoveFlyer());

        yield return WaitForClick();
        ThoughtBubble.instance.Show("Okay, let’s do this!");

        yield return WaitForClick();
        ThoughtBubble.instance.Show("New town, new league, nobody knows me.");
        yield return WaitForClick();

        ThoughtBubble.instance.Show("--I can be whoever I want!");
        yield return WaitForClick();
        ThoughtBubble.instance.Show("So who will it be?");

        yield return WaitForClick();
        ThoughtBubble.instance.Hide();


        yield return null;

        SpriteRenderer[] sr;


        selector.SetActive(true);


        sr = facialHairSprites;

        while (currentItem != Item.Done)
        {
            switch (currentItem)
            {
                case Item.Beards:
                    sr = facialHairSprites;
                    break;

                case Item.Headgear:
                    sr = headGearSprites;
                    break;

                case Item.MakeUp:
                    sr = makeUpSprites;
                    break;

                case Item.Mask:
                    sr = maskSprites;
                    break;

            }

            frame += sr.Length*2;
            frame %= sr.Length;

            foreach (SpriteRenderer spriteRenderer in sr)
            {
                spriteRenderer.gameObject.SetActive(false);
            }
            sr[frame].gameObject.SetActive(true);


            switch (currentItem)
            {
                case Item.Beards:
                    chosenFacialHair = sr[frame];
                    break;

                case Item.Headgear:
                    chosenHeadgear = sr[frame];
                    break;

                case Item.MakeUp:
                    chosenMakeUp = sr[frame];
                    break;

                case Item.Mask:
                    chosenMask = sr[frame];
                    break;

            }
            yield return null;
        }



        yield return WaitForClick();

        selector.SetActive(false);



        ThoughtBubble.instance.Show("\"" + chosenName + "\" eh?");

        yield return WaitForClick();

        ThoughtBubble.instance.Show("Is this look going to find \nme the perfect match?");

        yield return WaitForClick();

        Fisto.instance.gameObject.SetActive(true);


        waitForMirrorPunch = true;
        while (waitForMirrorPunch)
        {
            yield return null;
        }


        if (leaving)
            yield break;



        ThoughtBubble.instance.Show("Dammit! Get it together \n" + chosenName + "!");

        float timeStame = Time.time;
        yield return WaitForClick();

        while (Time.time - timeStame < 1.4)
        {
            yield return null;
        }


        while (leaving == false)
        {

            waitForMirrorPunch = true;
            while (waitForMirrorPunch)
            {
                yield return null;
            }


            if (leaving)
                yield break;
            ThoughtBubble.instance.Show("No one's gonna love you until \nyou learn to love yourself!");

        }


        ThoughtBubble.instance.Hide();

    }

    private int punchCount = 0;
    private bool waitForMirrorPunch = true;
    public void PunchMirror()
    {
        waitForMirrorPunch = false;
        punchCount++;
        if (punchCount == 4)
        {
            Leave();
        }
    }


    Coroutine WaitForClick()
    {
        return StartCoroutine(WaitForClickRoutine());
    }
    public static IEnumerator WaitForClickRoutine()
    {
        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;
        }
        yield return null;
    }


    public void Next()
    {
        frame ++;
                    AudioController.Play(AudioController.Instance.popUpTones[2], 0.5f);

    }

    public void Prev()
    {
        frame --;

                    AudioController.Play(AudioController.Instance.popUpTones[1], 0.5f);
    }


  public void Back()
    {
        selected = true;
        
         switch (currentItem)
        {
            case Item.Beards:
                if(chosenFacialHair)
                    chosenFacialHair.gameObject.SetActive(false);
                chosenFacialHair = null;
            break;
            
            case Item.Headgear:
            if(chosenHeadgear)
                    chosenHeadgear.gameObject.SetActive(false);
                chosenHeadgear = null;
            break;
            
            case Item.MakeUp:
            if(chosenMakeUp)
                    chosenMakeUp.gameObject.SetActive(false);
                chosenMakeUp = null;
            break;
            
            case Item.Mask:
            if(chosenMask)
                    chosenMask.gameObject.SetActive(false);
                chosenMask = null;
            break;
            
        }
        
                    AudioController.Play(AudioController.Instance.selectText);
        
        
         int i = (int)currentItem;
        i--;
        if(i < 0)
            i = 0;
        currentItem = (Item)i;
    }
    public void Accept()
    {
        selected = true;
        int i = (int)currentItem;
        i++;
        
        currentItem = (Item)i;
        
                    AudioController.Play(AudioController.Instance.popUpTones[0], 0.5f);
        
    }
}
}
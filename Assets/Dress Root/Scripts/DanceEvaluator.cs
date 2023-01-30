using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

namespace Dance { 
 public class DanceEvaluator : MonoBehaviour
{
    public  SpeechBubble thoughtLeft;
    public SpeechBubble thoughtRight;
    public GameObject narrator;

    public SpriteRenderer fadeOut;

    public  float danceScore = 0;

	public static DanceEvaluator instance;

	public Transform successBar; 
    // Use this for initialization

    public bool gainPoints = false;
    public float speed = 1;

    public static float limbMovement;
    static List<float> samples = new List<float>();

    public Transform playerHips;
    public ZoomInOnHead zoom;

    public DanceBar frontBar;
    public DanceBar BackBar;

    public static int clicks = 0;

    public AnimateSpriteFrames explosionPrefab;

    public GameObject moon1;
    public GameObject moon2;
    private Mouth playerMouth;
    private Eyes playerEyes;
    public bool handsUp = false;
    public static bool gaveUp = false;

    void Awake()
	{
        narrator.gameObject.SetActive(false);
      //  thoughtLeft.Hide();
        //thoughtRight.gameObject.SetActive(false);
	    thoughtLeft.index = 1;
	    thoughtLeft.index = 2;
        instance = this;

	    limbMovement = 0;

        samples = new List<float>();

        clicks = 0;


        if (AudioController.instance.danceTrack1.isPlaying == false)
            AudioController.instance.danceTrack1.Play();

        moon1.gameObject.SetActive(false);
        moon2.gameObject.SetActive(false);
    }


    public  void CreateSpriteExplosion()
    {
        AnimateSpriteFrames explosion = Instantiate(explosionPrefab);
        explosion.transform.position = explosionPrefab.transform.position;
        explosion.gameObject.SetActive(true);
        explosion.transform.parent = explosionPrefab.transform.parent;
    }

    IEnumerator SuckAtDancing()
    {

        float startScore = 1+ danceScore;



      

        
        clicks = 0;


        for (int i = 0; i < 5; i++)
        {

            int count = 5;
            
            while (clicks  < count)
            {
                int prevClicks = clicks;
                yield return null;
                if(clicks != prevClicks)
                Chunks.instance.SetCount(clicks);
            }
            Chunks.instance.RunFlashing();

            clicks = 0;
            RemoveChunk((startScore / 5f));
            yield return null;

            if (i == 2)
            {
                playerMouth.Set(playerMouth.awkward);
            }


            if (i == 0)
            {
                StartCoroutine(PeopleStartLookingAtYou());
            }
            if (i == 1)
            {
                StartCoroutine(PeopleGetVisiblyDisgusted());
            }
            if (i == 2)
            {
                StartCoroutine(PeopleStartLeaving());
            }
            if (i == 3)
            {
                StartCoroutine(EveryOneLeavess());
            }
            if (i == 4)
            {
                StartCoroutine(DateLeaves());

            }
        }

        yield return new WaitForSeconds(1);

        HideUI();

    }

    IEnumerator BeAwesomeAtDancing()
    {

        clicks = 0;


        bool shownUI = false;
        int iterations = 0;
   
        playerEyes.SetEyes(playerEyes.determinedEyes);
        for (int i = 0; i < 9; i++)
        {

            int count = 5;
       
            while (clicks < count)
            {
                int prevClicks = clicks;
                yield return null;
                if (clicks != prevClicks)
                    Chunks.instance.SetCount(clicks);
            }
            Chunks.instance.RunFlashing();


            if (i == 1)
            {
                ShowUI();
            }
            if (i == 2)
            {

                playerMouth.Set(playerMouth.talk);

                StartCoroutine(DateReturns());
            }
            if (i == 3)
            {
                StartCoroutine(PeopleStartReturning());
            }
            if (i == 4)
            {
                StartCoroutine(EveryOneReturns());
            }
         
            if (i == 5)
            {
                StartCoroutine(PeopleAreSmiling());
                playerMouth.Set(playerMouth.smile);
                playerEyes.SetEyes(playerEyes.openEyes);

            }
            if (i == 6)
            {
                StartCoroutine(PeopleAreOpenSmiling());

            }
            if (i == 7)
            {
                StartCoroutine(PeopleStartJumping());
                playerMouth.Set(playerMouth.openSmile);
                playerEyes.closeEyesHappy = true;
            }

            clicks = 0;
            AddChunk(0.2f);
            CreateSpriteExplosion();
            yield return null;

        }



        yield return null;
    }

    IEnumerator Start()
    {
        AudioController.instance.crowd.Stop();


        playerMouth = Player.instance.GetComponentInChildren<Mouth>();
        playerEyes = Player.instance.GetComponentInChildren<Eyes>();
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(SuckAtDancing());

        yield return new WaitForSeconds(4f);
        StartCoroutine(Fade());
        slowTime = true;
        fuckingWithTime = true;
        zoom.lerpIn = true;

        Player.instance.isClickable = false;
        yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;
        }
        narrator.GetComponentInChildren<Text>().text =
            ("In that moment, shamed and alone on that dancefloor I remember thinking: ");
        narrator.gameObject.SetActive(true);

        yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;
        }
        narrator.GetComponentInChildren<Text>().text = ("I only had one option: ");

        yield return null;

        DanceBase[] added = Player.instance.GetComponentsInChildren<DanceBase>();
        foreach (DanceBase danceBase in added)
        {
            if (danceBase)
                Destroy(danceBase);
        }


        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;
        }



        thoughtLeft.Show("believe in yourself");
        thoughtRight.Show("believe in nothing");


        while (selectedThought == -1)
        {
            yield return null;
        }

        thoughtLeft.Hide();
        thoughtRight.Hide();
        narrator.gameObject.SetActive(false);
        AudioController.instance.ChangeSong();

        zoom.lerpIn = false;
        slowTime = false;
        StartCoroutine(FadeBack());
        yield return new WaitForSeconds(0.2f);

            yield return new WaitForSeconds(1f);
        fuckingWithTime = false;

        SetTimeScale(1f);

        print(selectedThought);
        if (selectedThought == 2)
        {

            gaveUp = false;


            Clickable[] hinges = Player.instance.GetComponentsInChildren<Clickable>();
            foreach (Clickable hinge in hinges)
            {
                hinge.moves.Clear();
                hinge.AddRadComps();
            }

            StartCoroutine(PulseLimbs(playerHips, 0.05f));
            yield return new WaitForSeconds(1f);

            Player.instance.isClickable = true;


            //   while ( impression < 5)
            //{
            //    yield return null;

            //    if (average > 0.4f)
            //        impression += Time.deltaTime;
            //}

            yield return StartCoroutine(BeAwesomeAtDancing());


           HideUI();

            moon1.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            moon2.gameObject.SetActive(true);

            Player.instance.isClickable = false;

            yield return new WaitForSeconds(4 );


            Player.instance.FreezeFrame();

        }
        else
        {
            gaveUp = true;

            Player.instance.BelieveInNothing();
            yield return new WaitForSeconds(2);

            StartCoroutine(PartyOnYourCorpse());
            yield return new WaitForSeconds(1);
            StartCoroutine(PeopleStartJumping());

        }

    }


    void HideUI()
    {
        CanvasUI.instance.avatar.showing = false;
        CanvasUI.instance.avatar.hiding = true;
        ShowBar.instance.gameObject.SetActive(false);
    }

    void ShowUI()
    {
        CanvasUI.instance.avatar.showing = true;
        CanvasUI.instance.avatar.hiding = false;
        ShowBar.instance.gameObject.SetActive(true);
    }

    IEnumerator PulseLimbs( Transform limb, float delay)
    {
        if (limb.GetComponent<Clickable>() != null)
            limb.GetComponent<Clickable>().ForcePulse();


        yield return new WaitForSeconds(delay);

        for (int i = 0; i < limb.childCount; i++)
        {
            StartCoroutine(PulseLimbs(limb.GetChild(i), delay));

        }

    }


    float impression = 0;
    private int selectedThought = -1;
    public void SetThought(int index)
    {
        selectedThought = index;
    }


    public void ForceFade()
    {
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {

        fadeOut.enabled = true;
        fadeOut.color = new Color(0, 0, 0, 0);
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime * 4;
            timer = Mathf.Clamp01(timer);
            fadeOut.color = new Color(0, 0, 0, timer * 0.8f);

            yield return null;
        }
    }


    IEnumerator FadeBack()
    {


        fadeOut.color = new Color(0, 0, 0, 0);
        float timer = 1;
        while (timer > 0)
        {
            timer -= Time.deltaTime * 4;
            timer = Mathf.Clamp01(timer);
            fadeOut.color = new Color(0, 0, 0, timer * 0.8f);
            yield return null;
        }
        fadeOut.enabled = false;
    }

    // Update is called once per frame
    void Update ()
	{

        if (Input.GetKeyDown(KeyCode.Space) && Application.isEditor)
        {
            RemoveChunk(0.1f);
        }
      
	   
        danceScore = Mathf.Clamp(danceScore, -1, 1);
		successBar.transform.localScale = new Vector3((danceScore + 1)*32,1,1 );

        if (fuckingWithTime)
        {

            float t = 1;
            if (slowTime)
               t = Mathf.Lerp(Time.timeScale, 0.05f, Time.deltaTime*3);
            else
            {
                t = Mathf.Lerp(Time.timeScale, 1, Time.deltaTime*3);
            }


            SetTimeScale(t);
        }

	}

    void SetTimeScale(float t)
    {
        Time.timeScale = t;
        AudioController.instance.danceTrack1.pitch = 1 - .8f * (1 - Time.timeScale);
       // AudioController.instance.danceTrack2.pitch = 1 - .8f * (1 - Time.timeScale);
    }


    void RemoveChunk(float t)
    {
        danceScore -= t;
        danceScore = Mathf.Clamp(danceScore, -1, 1);
        frontBar.NotifyChange(0);
        BackBar.NotifyChange(1);
        Phrases.instances.GenerateBad();

    }
    void AddChunk(float t)
    {
        danceScore += t;
        danceScore = Mathf.Clamp(danceScore, -1, 1);
        frontBar.NotifyChange(1);
        BackBar.NotifyChange(0);
        Phrases.instances.GenerateGood();

    }

    public bool fuckingWithTime = false;
    public bool slowTime;

    private float average = 0;

    void FixedUpdate()
    {

        samples.Add(limbMovement);

        if (samples.Count > 10)
            samples.RemoveAt(0);

        foreach (float f in samples)
        {
            average += f;
        }

    average /= samples.Count;

        limbMovement = 0;
    }

    void OnGUI()
    {
        //GUILayout.Label("" + average);
        //GUILayout.Label("" + impression);

    }


    IEnumerator RunMomentOfCrisis()
    {
        yield return null;
    }


    IEnumerator PeopleStartLookingAtYou()
    {

        foreach (DanceFloorNPC danceFloorNpc in DanceFloorNPC.all)
        {
            danceFloorNpc.state = DanceFloorNPC.States.lookAtPLayer;
            yield return new WaitForSeconds(0.2f*Random.value);
        }

    }

    IEnumerator PeopleGetVisiblyDisgusted()
    {
        foreach (DanceFloorNPC danceFloorNpc in DanceFloorNPC.all)
        {
            danceFloorNpc.state = DanceFloorNPC.States.lookDisgusted;
            yield return new WaitForSeconds(0.2f * Random.value);
        }
    }

    IEnumerator PeopleStartLeaving()
    {

        int count = 0;
        foreach (DanceFloorNPC danceFloorNpc in DanceFloorNPC.all)
        {
            if(danceFloorNpc.isDate)
                continue;

            count++;
            if(count > DanceFloorNPC.all.Count/2f )
                break;
            danceFloorNpc.state = DanceFloorNPC.States.walkOff;
            yield return new WaitForSeconds(0.2f * Random.value);
        }
    }

    IEnumerator EveryOneLeavess()
    {
        foreach (DanceFloorNPC danceFloorNpc in DanceFloorNPC.all)
        {
            if (danceFloorNpc.isDate)
                continue;


            danceFloorNpc.state = DanceFloorNPC.States.walkOff;
            yield return new WaitForSeconds(0.2f * Random.value);
        }
    }


    IEnumerator DateLeaves()
    {
        foreach (DanceFloorNPC danceFloorNpc in DanceFloorNPC.all)
        {
            if (danceFloorNpc.isDate == false)
                continue;


            danceFloorNpc.state = DanceFloorNPC.States.walkOff;
        }
            yield return new WaitForSeconds(0.2f * Random.value);
    }

    IEnumerator DateReturns()
    {
        foreach (DanceFloorNPC danceFloorNpc in DanceFloorNPC.all)
        {
            if (danceFloorNpc.isDate == false)
                continue;


            danceFloorNpc.state = DanceFloorNPC.States.lookSurprised;
        }
        yield return new WaitForSeconds(0.2f * Random.value);
    }

    IEnumerator PeopleStartReturning()
    {

        int count = 0;
        foreach (DanceFloorNPC danceFloorNpc in DanceFloorNPC.all)
        {
            if (danceFloorNpc.isDate)
                continue;

            count++;
            if (count > DanceFloorNPC.all.Count / 2f)
                break;
            danceFloorNpc.state = DanceFloorNPC.States.lookSurprised;
            yield return new WaitForSeconds(0.2f * Random.value);
        }
    }

    IEnumerator EveryOneReturns()
    {
        foreach (DanceFloorNPC danceFloorNpc in DanceFloorNPC.all)
        {
            if (danceFloorNpc.isDate)
                continue;


            danceFloorNpc.state = DanceFloorNPC.States.lookSurprised;
            yield return new WaitForSeconds(0.2f * Random.value);
        }
    }

    IEnumerator PeopleAreSmiling()
    {
        foreach (DanceFloorNPC danceFloorNpc in DanceFloorNPC.all)
        {
            danceFloorNpc.state = DanceFloorNPC.States.smile;
            yield return new WaitForSeconds(0.2f * Random.value);
        }
    }

    IEnumerator PeopleAreOpenSmiling()
    {
        foreach (DanceFloorNPC danceFloorNpc in DanceFloorNPC.all)
        {
            danceFloorNpc.state = DanceFloorNPC.States.openSmile;
            yield return new WaitForSeconds(0.2f * Random.value);
        }
    }

    IEnumerator PeopleStartJumping()
    {
        foreach (DanceFloorNPC danceFloorNpc in DanceFloorNPC.all)
        {
            danceFloorNpc.bounce = true;
            yield return new WaitForSeconds(0.2f * Random.value);
        }
    }


    IEnumerator PartyOnYourCorpse()
    {
        foreach (DanceFloorNPC danceFloorNpc in DanceFloorNPC.all)
        {
            danceFloorNpc.state = DanceFloorNPC.States.openSmile;
            yield return new WaitForSeconds(0.3f * Random.value);
        }
    }
}

}
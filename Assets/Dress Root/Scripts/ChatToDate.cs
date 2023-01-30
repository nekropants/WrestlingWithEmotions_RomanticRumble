using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Dance { 
 public class ChatToDate : MonoBehaviour
{
     SpeechBubble yourSpeechBubble;

    public SpeechBubble[] thoughts;

    public static ChatToDate instance;

    public SpriteRenderer fadeOut;

    public bool chatting = false;

    private int selectedThought = -1;

    private bool dateHasDrink = false;

    private bool givenBeer = false ;

    public GameObject NarrationBubble;

    public NpcWalk[] npcs;

    public Transform purpleLeatherHead;
    public Transform hotShotHead;

    public Character purpleLeather;
    public Character HotShot;

    void Awake()
    {
        instance = this;


    
    }

    private Character otherCharacter;
    // Use this for initialization
    IEnumerator Start()
    {
        NarrationBubble.gameObject.SetActive(false);
        Player.instance.isClickable = true;

        for (int i = 0; i < 3; i++)
        {
            thoughts[i].Index = i;
        }

        yield return null;

        if (AudioController.instance.insideClub.isPlaying == false)
            AudioController.instance.insideClub.Play();

        AudioController.instance.insideClub.volume = 1;
        AudioController.instance.neon.Stop();
        AudioController.instance.outsideClub.Stop();
        AudioController.instance.rainTrack.Stop();
        AudioController.instance.crowd.Play();
        yield return null;
        Player.instance.canWalk = false;
        yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;

        NarrationBubble.gameObject.SetActive(true);

        AudioController.instance.bubbleSound.Play();
        NarrationBubble.GetComponentInChildren<Text>().text = "Finally! I was inside!";


        yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;


        AudioController.instance.bubbleSound.Play();
        NarrationBubble.GetComponentInChildren<Text>().text = "I just stood there for a moment, thinking...";
        yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;


        float delay = 0.11f;
        thoughts[1].Show("I can't believe i'm here!");
        yield return WaitForClick();
        thoughts[1].Hide();

        thoughts[1].Show("Okay! Okay! Be cool. Don't look like a looser!");
        yield return WaitForClick();

        thoughts[1].Hide();

        thoughts[1].Show("Should I grab a drink? Or try chat to one of those cool kids?");

        yield return WaitForClick();

        thoughts[1].Hide();


        NarrationBubble.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Player.instance.canWalk = true;


    }

    IEnumerator WaitForClick()
    {
        yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;
        }
        yield return null;
    }

    public bool everyOneWalkedOff = false;
	
	public void RunPurpleLeather()
    {
        ForceFade(purpleLeather.transform);

        StartCoroutine(PurpleLeatherRoutine());
    }

    IEnumerator PurpleLeatherRoutine()
    {
        Player.instance.playerEyes.target = purpleLeatherHead;
        Player.instance.playerEyes.manualControl = true;
        Player.instance.canWalk = false;
        thoughts[1].Show("\"Purple Leather\" - one of the coolest of the cool.");
        yield return WaitForClick();
         thoughts[1].Hide();

        thoughts[1].Show("They first made waves in the scene with an outfit made of lettuce.");
         yield return WaitForClick();

         thoughts[1].Hide();

        thoughts[1].Show("I could never speak to them!");
       
         yield return WaitForClick();

         thoughts[1].Hide();
        
       
       yield return new WaitForSeconds(0.3f);
        Player.instance.canWalk = true;

        Player.instance.playerEyes.target = null;
        Player.instance.playerEyes.manualControl = false;
        ForceFadeBack();
    }

    public void RunHotShot()
    {
        ForceFade(HotShot.transform);
        StartCoroutine(HotShotRoutine());
    }

    IEnumerator HotShotRoutine()
    {
        Player.instance.canWalk = false;

        Player.instance.playerEyes.target = hotShotHead;
        Player.instance.playerEyes.manualControl = true;

        thoughts[1].Show("\"Hot Shot\" - local breakdancing hero and style icon.");
        yield return WaitForClick();
         thoughts[1].Hide();

        thoughts[1].Show("A legend in the scene.");
         yield return WaitForClick();

         thoughts[1].Hide();

        thoughts[1].Show("I don't know if I have the nerve speak to them!");
       
         yield return WaitForClick();

         thoughts[1].Hide();


        Player.instance.playerEyes.target = null;
        Player.instance.playerEyes.manualControl = false;

        yield return new WaitForSeconds(0.3f);
        Player.instance.canWalk = true;
        ForceFadeBack();
    }

    IEnumerator  HideDateBubble()
    {
        yield return new WaitForSeconds(0.75f);
        CanvasUI.instance.textBox.hiding = true;
    }

    public void SetThought(int thoughtIndex)
    {
        selectedThought = thoughtIndex;
    }


    private Eyes otherPlayerEyes;

    public bool engaged = false;
    private TalkToAnotherCharacter talkToAnotherCharacter;
    IEnumerator Conversation()
    {
        engaged = true;
        yourSpeechBubble = Player.instance.GetComponent<Character>().speechBubble;

        otherPlayerEyes = otherCharacter.GetComponentInChildren<Eyes>();
        CanvasUI.instance.mouth = otherCharacter.GetComponentInChildren<Mouth>();
        yourSpeechBubble.mouth = Player.instance.GetComponentInChildren<Mouth>();
        if (otherCharacter.isDate1)
            ChatScriptReader.instance.scripToLoad = ChatScriptReader.instance.purpleLeatherScript;
        else
        {
            ChatScriptReader.instance.scripToLoad = ChatScriptReader.instance.hotShotScript;
        }

        ChatScriptReader.instance.LoadMainScript();

       yield return new WaitForSeconds(0.3f);




        // --------------------------- Greeting ------------------------------

        float delay = 0.11f;
        thoughts[0].Show(ChatScriptReader.playerGreeting[0]);
        yield return new WaitForSeconds(delay);

        thoughts[1].Show(ChatScriptReader.playerGreeting[1]);
        yield return new WaitForSeconds(delay);

        thoughts[2].Show(ChatScriptReader.playerGreeting[2]);
       // yourSpeechBubble.Show("Hey");

        while (selectedThought < 0)
        {
            yield return null;

        }

        for (int i = 0; i < 3; i++)
        {
            thoughts[i].Hide();
        }
        CanvasUI.instance.textBox.hiding = true;



        yourSpeechBubble.Show(thoughts[selectedThought].text.text);
        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;

        }
        
        yourSpeechBubble.Hide();
        switch (selectedThought)
        {
            case 0:
                CanvasUI.instance.text = ChatScriptReader.dateGreeting[0];
                break;

            case 1:
                CanvasUI.instance.text = ChatScriptReader.dateGreeting[1];
                break;

            case 2:
                CanvasUI.instance.text = ChatScriptReader.dateGreeting[2];
                break;
        }

        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;

        }









        // ------------------------ Offer -------------------------------


        thoughts[0].Show(ChatScriptReader.playerOffer [0]);
        yield return new WaitForSeconds(delay);

        thoughts[1].Show(ChatScriptReader.playerOffer[1]);
        yield return new WaitForSeconds(delay);

        thoughts[2].Show(ChatScriptReader.playerOffer[2]);

        selectedThought = -1;
        while (selectedThought < 0)
        {
            yield return null;

        }


        for (int i = 0; i < 3; i++)
        {
            thoughts[i].Hide();
        }
        CanvasUI.instance.textBox.hiding = true;




        yourSpeechBubble.Show(thoughts[selectedThought].text.text);
        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;

        }

        bool alreadyHasBeer = Player.hasBeer;

        yourSpeechBubble.Hide();


        bool offeredBeer = false;
        if(selectedThought == 0) // wait For booze
        {

            offeredBeer = true;
            if (otherCharacter.isDate1)
            {
                CanvasUI.instance.text = "Hell yeah!";
            }
            else
            {
                CanvasUI.instance.text = "I'd love one!";
            }


            yield return null;

            while (Input.GetKeyDown(KeyCode.Mouse0) == false)
            {
                yield return null;

            }
            //   StartCoroutine(HideDateBubble());

            if (Player.hasBeer == false)
                {

                     CanvasUI.instance.textBox.hiding = true;
                     CanvasUI.instance.textBox.showing = false;
                    StartCoroutine(FadeBack());

            }

            if ( Player.hasBeer == false )// wait for player to return with beer 
            {
                while ( rentereredConve == false)
                {
                yield return null;
                }
            }



            yield return new WaitForSeconds(0.3f);
            StartCoroutine(GrabBeerRoutine());
            while (givenBeer == false)
            {
                yield return null;
            }

            otherCharacter.GetComponentInChildren<Mouth>().Set(otherCharacter.GetComponentInChildren<Mouth>().smile);
            CanvasUI.instance.activeFace.currentFace = 2;
        }

        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;

        }

        if (alreadyHasBeer && offeredBeer)
        {
            CanvasUI.instance.text = "Uhhh... Isn't this your beer?";
        }
        else
        {

            switch (selectedThought) // say thanks
            {
                case 0:
                    CanvasUI.instance.text = ChatScriptReader.dateOffer[0];
                    break;

                case 1:
                    CanvasUI.instance.text = ChatScriptReader.dateOffer[1];
                    break;

                case 2:
                    CanvasUI.instance.text = ChatScriptReader.dateOffer[2];
                    break;
            }
        }

        yield return null;


        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;

        }

        


        // ------------------------ Compliment -------------------------------

        otherCharacter.GetComponentInChildren<Mouth>().Set(otherCharacter.GetComponentInChildren<Mouth>().openSmile);
        CanvasUI.instance.activeFace.currentFace = 1;
        thoughts[0].Show(ChatScriptReader.playerCompliment[0]);
        yield return new WaitForSeconds(delay);

        thoughts[1].Show(ChatScriptReader.playerCompliment[1]);
        yield return new WaitForSeconds(delay);

        thoughts[2].Show(ChatScriptReader.playerCompliment[2]);

        selectedThought = -1;
        while (selectedThought < 0)
        {
            yield return null;

        }

        for (int i = 0; i < 3; i++)
        {
            thoughts[i].Hide();
        }
        CanvasUI.instance.textBox.hiding = true;

        yourSpeechBubble.Show(thoughts[selectedThought].text.text);
        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;

        }

        yourSpeechBubble.Hide();
        switch (selectedThought)
        {
            case 0:
                CanvasUI.instance.text = ChatScriptReader.dateCompliment[0];
                break;

            case 1:
                CanvasUI.instance.text = ChatScriptReader.dateCompliment[1];
                break;

            case 2:
                CanvasUI.instance.text = ChatScriptReader.dateCompliment[2];
                break;
        }

        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;

        }







        // ------------------------ Who are you here with -------------------------------

        CanvasUI.instance.text = ChatScriptReader.dateQuestion[0];

        yield return new WaitForSeconds(delay);

        thoughts[0].Show(ChatScriptReader.playerQuestion[0]);
        yield return new WaitForSeconds(delay);

        thoughts[1].Show(ChatScriptReader.playerQuestion[1]);
        yield return new WaitForSeconds(delay);

        thoughts[2].Show(ChatScriptReader.playerQuestion[2]);

        selectedThought = -1;
        while (selectedThought < 0)
        {
            yield return null;

        }
        CanvasUI.instance.textBox.hiding = true;

        for (int i = 0; i < 3; i++)
        {
            thoughts[i].Hide();
        }

        CanvasUI.instance.textBox.hiding = true;
        yourSpeechBubble.Show(thoughts[selectedThought].text.text);
        yield return null;
  AudioController.instance.PlayMuffledDanceTrack();
        StartCoroutine(npcsWalkOffRoutine());




        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;

        }

        yourSpeechBubble.Hide();




        // ------------------------ Song Change -------------------------------
      
        thoughts[0].Show(ChatScriptReader.playerSongChange[0]);
        yield return new WaitForSeconds(delay);

        thoughts[1].Show(ChatScriptReader.playerSongChange[1]);
        yield return new WaitForSeconds(delay);

        thoughts[2].Show(ChatScriptReader.playerSongChange[2]);

        selectedThought = -1;
        while (selectedThought < 0)
        {
            yield return null;

        }

        for (int i = 0; i < 3; i++)
        {
            thoughts[i].Hide();
        }
        yourSpeechBubble.Show(thoughts[selectedThought].text.text);
        yield return null;

        yield return new WaitForSeconds(0.3f);


        yourSpeechBubble.Hide();
        switch (selectedThought)
        {
            case 0:
                CanvasUI.instance.text = ChatScriptReader.dateSongChange[0];
                break;

            case 1:
                CanvasUI.instance.text = ChatScriptReader.dateSongChange[1];
                break;

            case 2:
                CanvasUI.instance.text = ChatScriptReader.dateSongChange[2];
                break;
        }

        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;

        }




        // ------------------------ Respond -------------------------------


        thoughts[0].Show(ChatScriptReader.playerDance[0]);
        yield return new WaitForSeconds(delay);

        thoughts[1].Show(ChatScriptReader.playerDance[1]);
        yield return new WaitForSeconds(delay);

        thoughts[2].Show(ChatScriptReader.playerDance[2]);

        selectedThought = -1;
        while (selectedThought < 0)
        {
            yield return null;

        }

        for (int i = 0; i < 3; i++)
        {
            thoughts[i].Hide();
        }
        CanvasUI.instance.textBox.hiding = true;

        yourSpeechBubble.Show(thoughts[selectedThought].text.text);
        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;

        }

        yourSpeechBubble.Hide();
        switch (selectedThought)
        {
            case 0:
                CanvasUI.instance.text = ChatScriptReader.dateDance[0];
                break;

            case 1:
                CanvasUI.instance.text = ChatScriptReader.dateDance[1];
                break;

            case 2:
                CanvasUI.instance.text = ChatScriptReader.dateDance[2];
                break;
        }

        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;

        }


        CanvasUI.instance.textBox.showing = false;
        CanvasUI.instance.avatar.showing = false;
        CanvasUI.instance.textBox.hiding = true;
        CanvasUI.instance.avatar.hiding = true;

        otherCharacter.GetComponent<NpcWalk>().StartWalk();

        SetDate.choseDate1 = otherCharacter.isDate1;

        StartCoroutine(FadeBack());
    everyOneWalkedOff = true;
        while (dateHasDrink == false)
        {
            yield return null;
        }
    }

    IEnumerator npcsWalkOffRoutine()
    {
            yield return new WaitForSeconds(0.2f);
        foreach (NpcWalk npc in npcs)
        {

            if (npc == null)
            {
                continue;
                
            }
            if(otherCharacter != null &&  npc.transform == otherCharacter.transform)
                continue;

            npc.StartWalk();
            yield return new WaitForSeconds(Random.Range(0.3f, 0.8f));
        }
    }

    public void ForceFade(Transform _otherChar)
    {
        StartCoroutine(Fade(_otherChar));
    }

    public void ForceFadeBack()
    {
        StartCoroutine(FadeBack());
    }

    private Transform otherChar;
    IEnumerator Fade(Transform _otherChar)
    {
        otherChar =_otherChar;
        talkToAnotherCharacter = otherChar.GetComponent<TalkToAnotherCharacter>();
        if (talkToAnotherCharacter) talkToAnotherCharacter.enabled = false;
        
        originalPlayerPos = Player.instance.transform.position;
        originalDatePos = otherChar.transform.position;

        Vector3 pos = otherChar.transform.position;
        pos.z = fadeOut.transform.position.z - 1;
        otherChar.transform.position = pos;

        pos = Player.instance.transform.position;
        pos.z = fadeOut.transform.position.z - 1;
        Player.instance.transform.position = pos;

        chatting = true;

        fadeOut.gameObject.SetActive(true);
            fadeOut.color = new Color(0, 0, 0, 0);
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime*2;
            timer = Mathf.Clamp01(timer);
            fadeOut.color = new Color(0, 0, 0, timer*0.8f);
            yield return null;
        }
    }

    IEnumerator GrabBeerRoutine()
    {
        Player.instance.playerEyes.target = Player.instance.beer;
        Player.instance.playerEyes.manualControl = true;

        otherPlayerEyes.manualControl = true;
        otherPlayerEyes.target = Player.instance.beer;

        Player.instance.isClickable = true;
        CircleCollider2D playerHand = Player.instance.GetComponent<Character>().hand;
        CircleCollider2D otherhand = otherCharacter.hand;

        float inceasedSize = 0;
        while (givenBeer == false)
        {
            
            print("try Grab beer " + Vector3.Distance(playerHand.transform.position , otherhand.transform.position));
            if(playerHand.OverlapPoint(otherhand.transform.position))
            {
                Player.instance.beer.transform.parent = otherhand.transform;
                Player.instance.beer.localPosition = new Vector3(0, 0, -0.1f);

                givenBeer = true;
            }

            if (inceasedSize < 2)
            {
                float d = Time.deltaTime*0.1f;
                inceasedSize += d;
                playerHand.radius += d;
            }
            yield return null;

        }

        Player.instance.playerEyes.target = null;
        Player.instance.playerEyes.manualControl = false;

        //    Player.instance.isClickable = false;

        otherPlayerEyes.manualControl = false;
        otherPlayerEyes.target = null;

    }

    private Vector3 originalPlayerPos;
    private Vector3 originalDatePos;
    IEnumerator FadeBack()
    {
        otherChar.transform.position = originalDatePos;
        Player.instance.transform.position = originalPlayerPos;

        chatting = false;


        fadeOut.color = new Color(0, 0, 0, 0);
        float timer = 1;
        while (timer > 0)
        {
            timer -= Time.deltaTime * 2;
            timer = Mathf.Clamp01(timer);
            fadeOut.color = new Color(0, 0, 0, timer * 0.8f);
            yield return null;
        }
        
        fadeOut.gameObject.SetActive(false);


        if (talkToAnotherCharacter) talkToAnotherCharacter.enabled = true;

    }
    bool rentereredConve = false;
    public void StartConversation( Character otherChar)
    {
        if(rentereredConve)
        {
            return;
        }

        if (otherCharacter == otherChar) // re enter with beer
        {
            if(Player.hasBeer == false)
            {
            return;

            }
            rentereredConve = true;
            
            
        }
        else if (otherCharacter != null)
        {
            return;
        }
        else
        {
            otherCharacter = otherChar;
            StartCoroutine(Conversation());
            

        }
        StartCoroutine(Fade(otherCharacter.transform));
    }
}

}
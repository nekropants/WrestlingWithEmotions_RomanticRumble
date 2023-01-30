using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Dance { 
 public class Phrases : MonoBehaviour
{

    // Use this for initialization

    public Phrase phrasePrefab;

    public Font[] fonts;
    int nextFont = 0;

    List<string> usedBad;
    List<string> usedGood;
    List<string> usedNouns;
    List<string> usedNumbers;

    public static Phrases instances;

    public Image flash;


    public int direction = 1;

    void Start()
    {
        usedBad = new List<string>(bad);
        usedNouns = new List<string>(nouns);
        usedGood = new List<string>(good);
        usedNumbers = new List<string>(points);
        phrasePrefab.gameObject.SetActive(false);
        instances = this;


    }

    // Update is called once per frame
    void Update()
    {

        if (Application.isEditor && Input.GetKeyDown(KeyCode.Space))
        {
            //	GenerateBad();
        }

    }
    public void GenerateBadNumer()
    {
        int a = Random.Range(0, usedBad.Count);
        int b = Random.Range(0, usedNouns.Count);
        string str = usedBad[a] + " " + usedNouns[b] + "!";
        str = str.ToUpper();

        usedBad.RemoveAt(a);
        usedNouns.RemoveAt(b);

        Phrase newPhrase = Instantiate(phrasePrefab);
        newPhrase.gameObject.SetActive(true);
        newPhrase.transform.SetParent(phrasePrefab.transform.parent, false);
        newPhrase.transform.position = phrasePrefab.transform.position +
                                       (Vector3)Random.insideUnitCircle.normalized * 200;
        newPhrase.text.text = str;
        newPhrase.text.font = fonts[nextFont];
        print(newPhrase.text.font);
        newPhrase.rotDir = direction = -direction;
        nextFont += Random.Range(0, fonts.Length - 1);
        nextFont %= fonts.Length;

        StartCoroutine(Flash());
    }
    public void GenerateBad()
    {
        int a = Random.Range(0, usedBad.Count);
        int b = Random.Range(0, usedNouns.Count);
        int c = Random.Range(0, usedNumbers.Count);
        string str = "The "+ usedBad[a] + " " + usedNouns[b] + "!\n-" + usedNumbers[c];
        str = str.ToUpper();

        usedBad.RemoveAt(a);
        usedNouns.RemoveAt(b);
        usedNumbers.RemoveAt(c);

        Phrase newPhrase = Instantiate(phrasePrefab);
		newPhrase.isBad = true;        
        newPhrase.gameObject.SetActive(true);
        newPhrase.transform.SetParent(phrasePrefab.transform.parent, false);
        newPhrase.transform.position = phrasePrefab.transform.position +
                                       (Vector3) Random.insideUnitCircle.normalized*4;
        newPhrase.text.text = str;
        newPhrase.text.font = fonts[nextFont];
        print(newPhrase.text.font);
        newPhrase.rotDir = direction = -direction;
        nextFont += Random.Range(0, fonts.Length - 1);
        nextFont %= fonts.Length;
        PulseCamera.instance.shake.AddShake(true);

        StartCoroutine(Flash());
    }


    public void GenerateGood()
    {
        int a = Random.Range(0, usedGood.Count);
        int b = Random.Range(0, usedNouns.Count);
        int c = Random.Range(0, usedNumbers.Count);
        string str = "The " + usedGood[a] + " " + usedNouns[b] + "!\n+" + usedNumbers[c];
        str = str.ToUpper();

        usedGood.RemoveAt(a);
        usedNouns.RemoveAt(b);
        usedNumbers.RemoveAt(c);

        Phrase newPhrase = Instantiate(phrasePrefab);
		newPhrase.isBad = false;        
        newPhrase.gameObject.SetActive(true);
        newPhrase.transform.SetParent(phrasePrefab.transform.parent, false);
        newPhrase.transform.position = phrasePrefab.transform.position +
                                       (Vector3) Random.insideUnitCircle.normalized* 4;
        newPhrase.text.text = str;
        newPhrase.text.font = fonts[nextFont];
        print(newPhrase.text.font);
        newPhrase.rotDir = direction = -direction;
        nextFont += Random.Range(0, fonts.Length - 1);
        nextFont %= fonts.Length;

        PulseCamera.instance.shake.AddShake(true);
        StartCoroutine(Flash());
    }


    public IEnumerator Flash()
    {

        yield return null;
        flash.enabled = true;

        float timer = 1;

        while (timer > 0)
        {
            timer -= Time.deltaTime*6;
            timer = Mathf.Clamp01(timer);
            yield return null;
            flash.color = new Color(1, 1, 1, timer*0.5f);
        }

        flash.enabled = false;
    }



    string[] bad =
    {
        "Sloppy",
        "Cold",
        "Weak",
        "Sagging",
        "Burnt",
        "Raw",
        "Sour",
        "Bound",
        "Rancid",
        "Stale",
        "Vegi",
        "Fat Free",
        "Shit",
        "Sloppy",
        "Negative",
        "Mundane",
        "Muggle",
        "Hopeless",
        "Vile",
        "Boring",
        "Jelly",
        "Ancient",
        "Crappy",
        "Static",
        "Mechanical",
        "Soft",
        "Vomit",
        "Stoic",
        "Frozen",
        "Shy",
        "Stinky",
        "Lazy",
        "Flacid",
        "Snail",
        "Sloth",
        "Sedated",
    };

    string[] good =
    {
        "Holy",
        "Hot",
        "Stong",
        "Spinning",
        "Flaming",
        "Perfect",
        "Sweet",
        "Footloose",
        "Kevin",
        "Fresh",
        "Meat",
        "Full Cream",
        "Golden",
        "Stiff",
        "Positive",
        "Deluxe",
        "Magic",
        "Radical",
        "Tasty",
        "X-treme",
        "Jumping",
        "Future",
        "Infinite",
        "Switch",
        "Spazzing",
        "Hard",
        "Sexy",
        "Flipping",
        "Lava",
        "Chatterbox",
        "Frantic",
        "Hyperactive",
        "Tic Tac",
        "Shaking",
        "Super-charged",
        "Acid"
    };

    string[] nouns =
    {
        "Macaroni",
        "Legs",
        "Coffee",
        "Digits",
        "Sausage",
        "Hot Cakes",
        "Feet",
        "Fancy Free",
        "Bacon",
        "Getup",
        "Tornado",
        "Froth",
        "Ticket",
        "Little Fingers",
        "Grandpa",
        "Disaster",
        "Mushroom",
        "Romantic",
        "Toes",
        "Nightmare",
        "Beans",
        "Sailor",
        "Strawberry",
        "Socks",
        "Spinster",
        "Package",
        "Shake",
        "Pantaloons",
        "Eruption",
        "Pie Hole",
        "Lumberjack",
        "Hurricane",
        "Tamale",
        "Spaghetti",
        "Sock-puppet",
        "Buns",
    };

    string[] points =

    {
        "90",
        "180",
        "360",
        "720",
        "1440",
        "50 50",
        "9000",
        "182",
        "9001",
        "420",
        "10810",
        "130",
        "10000",
        "5000",
        "41",
        "500",
        "1000",
        "100",
        "777",
        "2100",
        "4700",
        "1024",
        "3000",
        "1500",
         "100",
        "777",
        "2100",
        "4700",
        "1024",
        "3000",

    };

}

}
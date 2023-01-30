using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dance { 
 public class DanceFloorNPC : MonoBehaviour
{

    public static List<DanceFloorNPC> all = new List<DanceFloorNPC>();

    public float walkOffThreshHold = -0.75f;
    public float getStokedTheshold = 0.5f;
    public float lookAtPlayer = -0.0f;
    public float lookDisgusted = -0.2f;
    public float insultPlayer = -0.4f;
    public float walkOff = -0.8f;

    float walkLerp = 0;
    public float walkSpeed = 0.5f;
    public float bounceFreq = 1f;
    public float bounceAmp = 1f;
    public float distance = 0;
    Vector3 walkOffset;

    public Transform head;

    private bool thrownInsult = false;

    public int walDir = 1;

    private Character character;

    public bool isDate = false;
    private float bounceTimer = 0;

    Eyes eyes;
    Mouth mouth;
    // Use this for initialization
    void Start()
    {
        walkOffThreshHold -= Random.Range(0, 1f)*0.25f;

        if (isDate)
            walkOffThreshHold = -1;

        eyes = GetComponentInChildren<Eyes>();
        mouth = GetComponentInChildren<Mouth>();
        character = GetComponentInParent<Character>();


        lookAtPlayer = -0.0f - Random.value*0.1f;
        lookDisgusted = -0.2f - Random.value*0.1f;
        insultPlayer = -0.4f - Random.value*0.2f;
        walkOff = -0.8f - Random.value*0.2f;

            all.Add(this);
    }


    public enum States { DontCareAboutPlayer, lookAtPLayer, lookDisgusted, lookSurprised, walkOff, smile, openSmile }

    public States state = States.DontCareAboutPlayer;
        public bool bounce = false;


    // Update is called once per frame
    void Update()
    {



        if (state == States.lookAtPLayer)
        {

            if (eyes && mouth)
            {
                eyes.SetEyes(eyes.angryEyes);
                eyes.lookAtPlayer = true;

                mouth.Set(mouth.disgust);
            }



        }
        if (state == States.lookDisgusted)
            {
                if (eyes && mouth)
            {
                eyes.SetEyes(eyes.confusedEyes);
                eyes.lookAtPlayer = true;

                mouth.Set(mouth.awkward);
            }
        }

        if (state == States.lookSurprised)
        {
            if (eyes && mouth)
            {
                eyes.SetEyes(eyes.confusedEyes);
                eyes.lookAtPlayer = true;

                mouth.Set(mouth.talk);
            }
        }


        if (state == States.DontCareAboutPlayer)
            {
                if (eyes && mouth)
            {
                eyes.SetEyes(eyes.openEyes);
                eyes.lookAtPlayer = false;
                mouth.Set(mouth.smile);
            }
        }
        

        if (state == States.openSmile)
        {
            if (eyes && mouth)
            {
                eyes.SetEyes(eyes.happyEyes);
                eyes.lookAtPlayer = true;
                mouth.Set(mouth.openSmile);
            }
        }


        if (state == States.smile)
        {
            if (eyes && mouth)
            {
                eyes.SetEyes(eyes.happyEyes);
                eyes.lookAtPlayer = true;
                mouth.Set(mouth.smile);
            }
        }


        //if (isDate == false)
        if (state == States.walkOff)
        {
            walkLerp += Time.deltaTime*walkSpeed/2;
        }
        else
        {
            walkLerp -= Time.deltaTime*walkSpeed/2;
        }

        walkLerp = Mathf.Clamp01(walkLerp);

		character.transform.localPosition -= walkOffset;

		walkOffset = Vector3.right*walkLerp*distance*2f*walDir ;
        walkOffset += Vector3.up * (Mathf.Abs(Mathf.Sin(walkLerp * bounceFreq) * bounceAmp));

	    if (bounce)
	    {
	        bounceTimer += Time.deltaTime*walkSpeed;
            walkOffset += Vector3.up * (Mathf.Abs(Mathf.Sin(bounceTimer * bounceFreq) * bounceAmp));
        }

        character.transform.localPosition += walkOffset; 
	}

    void OnDestroy()
    {
        all.Remove(this);
    }
}

}
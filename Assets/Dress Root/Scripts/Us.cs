using UnityEngine;
using System.Collections;

namespace Dance { 
 public class Us : MonoBehaviour
{


    float walkLerp = 0;
    public float bounceFreq = 1f;
    public float bounceAmp = 1f;
    public float distance = 0;
    Vector3 walkOffset;

    public int walDir = 1;

    private Character character;

    public bool isDate = false;


    private float timer = 0;
    public Eyes eyes;
    public Mouth mouth;



    public   bool enterScene = false;
    // Use this for initialization d
    // Use this for initialization 
    void Start()
    {


        eyes = GetComponentInChildren<Eyes>();
        mouth = GetComponentInChildren<Mouth>();
        character = GetComponent<Character>();

        eyes.manualControl = true;

        transform.position += distance*Vector3.left*walDir;

    }

    // Update is called once per frame
    void Update()
    {

        while ( enterScene == false)
        {
            return;
            
        }

		character.transform.localPosition -= walkOffset;
        timer += Time.deltaTime;
        if (distance > 0)
        {

            float disp = Time.deltaTime*7;
            distance -= disp;
            transform.position += Vector3.right*disp*walDir;
           
        }

        walkOffset = Vector3.up * (Mathf.Abs(Mathf.Sin(distance * bounceFreq) * bounceAmp));

        character.transform.localPosition += walkOffset; 

	}
}

}
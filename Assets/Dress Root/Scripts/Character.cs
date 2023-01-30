using UnityEngine;
using System.Collections;

namespace Dance { 
 public class Character : MonoBehaviour
{

    public Transform LeftShouder;
    public Transform RightShoulder;
    public Transform LMid;
    public Transform RMID;
    public Transform l_lower;
    public Transform r_lower;
    public CircleCollider2D hand;
    public SpeechBubble speechBubble;
    public bool dance = false;
    public bool useAdditiveTime = false;

    public bool isDate1 = false;
    public bool throwArmsUp = true;

    private float speed = 1;
    private float delay = 0;

    // Use this for initialization
    void Start ()
    {
        Joint[] js = GetComponentsInChildren<Joint>();
        foreach (Joint joint in js)
        {
            if (joint.gameObject.name == "ArmUpper_L")
                LeftShouder = joint.transform;

            if (joint.gameObject.name == "ArmUpper_R")
                RightShoulder = joint.transform;

            if (joint.gameObject.name == "ArmMid_R")
                RMID = joint.transform;

            if (joint.gameObject.name == "ArmMid_L")
                LMid = joint.transform;

        }
        speed += Random.value;
        delay = Random.value*0.5f;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private float throwArms = 0;
    void LateUpdate()
    {
        if(throwArmsUp == false)
            return;

        if(DanceEvaluator.instance == null || DanceEvaluator.instance.handsUp == false)
            return;


        if (delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }
        throwArms += Time.deltaTime*3*speed;

        throwArms = Mathf.Clamp01(throwArms);
        if (RightShoulder) RightShoulder.transform.localRotation = Quaternion.Euler(0,0, throwArms*-90);
        if(LeftShouder ) LeftShouder.transform.localRotation = Quaternion.Euler(0,0, throwArms*90);

        if (RMID) RMID.transform.localRotation = Quaternion.Euler(0, 0 ,0);
        if (LMid) LMid.transform.localRotation = Quaternion.Euler(0, 0,0);
    }
}

}
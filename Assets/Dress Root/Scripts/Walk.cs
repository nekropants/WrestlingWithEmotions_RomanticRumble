using UnityEngine;
using System.Collections;

namespace Dance { 
 public class Walk : MonoBehaviour
{
    public Transform L_UpperLeg;
    public Transform L_Knee;
    public Transform L_LowerLeg;
    public Transform L_Shoulder;
    public Transform L_LowerArm;
    

    public Transform R_UpperLeg;
    public Transform R_Knee;
    public Transform R_LowerLeg;
    public Transform R_Shoulder;
    public Transform R_LowerArm;

    private float timer;
    public float frequency= 6;
    public float upperLegStart = 45;
    public float upperLegAmp = 45;
    public float lowerLegAmp = 45;
    public float lowerLegStart = 45;


    public float lowerArmAmp = 45;
    public float lowerArmStart = 45;

    public float shoulderAmp = 45;
    public float shoulderStart = 45;
    
    private float lerp = 0;

    private Player player;

    float restinghoulderRot = -70;

// Use this for initialization
void Start ()
{
    player = GetComponent<Player>();
}
	
	// Update is called once per frame
	void Update ()
	{
	    RunWalk();

	}


    void RunWalk()
    {
        if (player.walking )
        {
            lerp += Time.deltaTime*5;
        }
        else
        {
            lerp -= Time.deltaTime*5;

        }

        lerp = Mathf.Clamp01(lerp);

        timer += Time.deltaTime*frequency;
        float altTimer = timer + Mathf.PI;

        float sin = Mathf.Sin(timer);
        float altSin = Mathf.Sin(altTimer);



      

        if (lerp > 0)
        {
            L_Shoulder.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(restinghoulderRot, (sin * shoulderAmp + shoulderStart), lerp));
            R_Shoulder.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(-restinghoulderRot, (sin * shoulderAmp - shoulderStart), lerp));


            L_UpperLeg.localRotation = Quaternion.Euler(0, 0, (sin * upperLegAmp + upperLegStart) * lerp);
            R_UpperLeg.localRotation = Quaternion.Euler(0, 0, (sin * upperLegAmp - upperLegStart) * lerp);

            L_LowerLeg.localRotation = Quaternion.Euler(0, 0, (sin * lowerLegAmp + lowerLegStart) * lerp);
            R_LowerLeg.localRotation = Quaternion.Euler(0, 0, (sin * lowerLegAmp - lowerLegStart) * lerp);

            L_LowerArm.localRotation = Quaternion.Euler(0, 0, (sin * lowerArmAmp + lowerArmStart) * lerp);
            R_LowerArm.localRotation = Quaternion.Euler(0, 0, (sin * lowerArmAmp - lowerArmStart) * lerp);
        }


    }
}

}
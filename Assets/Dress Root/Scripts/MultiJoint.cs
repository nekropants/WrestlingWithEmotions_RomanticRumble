using UnityEngine;
using System.Collections;

namespace Dance { 
 public class MultiJoint : DanceBase
{




    private Quaternion offset = Quaternion.identity;



    public int piOffset = 0;


    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        if (randomize)
        {
            frequency *= Random.Range(0.5f, 1.5f) ;
            amplitude *= Random.Range(0.5f, 1.5f);
        }

      transform.localRotation *= Quaternion.Inverse(offset);


    }

    // Update is called once per frame
    protected override void Run()
    {
        base.Run();
    


        if (character && character.dance == false)
            return;


        timer += Time.deltaTime * frequency;


        transform.localRotation *= Quaternion.Inverse(offset);

        float amp = amplitude;
        if (flip)
            amp = -amplitude;

        offset = Quaternion.Euler(0, 0, amp * Mathf.Sin(timer));

        transform.localRotation *= offset;
        // transform.localRotation = defaultRot * offset;


    }
}

}
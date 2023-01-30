using UnityEngine;
using System.Collections;

public class ShiftOnFerrisWheelSeat : MonoBase
{
    private float lerp = 0;
    private Vector3 anchor;
    private bool bouncing = false;
    private Vector3 direction;

    public FerrisWheelHandController hand;
    private int hops = 0;

    public AnimationCurve bounceCurve ;

    // Use this for initialization
    private void Start()
    {
        anchor = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (bouncing == false)
        {
            if (LeftDown && hops < 3)
            {
                direction = Vector3.left;
                bouncing = true;
                hops++;
            }

            if (RightDown && hops > 0)
            {
                direction = Vector3.right;
                bouncing = true;
                hops--;
            }
        }

        //if (bouncing)
        //{

        //}

        if (bouncing)
            RunBounce();
    }

    private Vector3 yOffset = Vector3.zero;
    private void RunBounce()
    {
        lerp += Time.deltaTime*4;
        lerp = Mathf.Clamp01(lerp);

        anchor -= yOffset;
        yOffset = Vector3.up * bounceCurve.Evaluate(lerp)*5;
        anchor += yOffset;



        transform.position = anchor + lerp*Vector3.left*10;

        if (lerp == 1)
        {
            bouncing = false;
            lerp = 0;
            anchor = transform.position;

            if (hops == 3)
            {
                enabled = false;
                hand.enabled = true;

            }

        }

       
    }

    


}

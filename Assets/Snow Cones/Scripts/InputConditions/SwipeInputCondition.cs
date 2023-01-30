using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInputCondition : TriggerCondition
{
    
    Vector3 startPos;

    public   float requiredDistance = .1f;
    public   Vector3 displacement;
    public Vector2 direction  = Vector2.up;

    //List<Vector3> directionPoints = new List<Vector3>();

    public override bool IsSatisfied()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startPos = Input.mousePosition;

        }

        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                displacement = Input.mousePosition- startPos;


                if (Vector2.Angle(direction, displacement) < 45)
                {
                    if(displacement.magnitude > requiredDistance)
                    {
                        return true;
                    }
                }
            }
        }

        return false;

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startPos = Input.mousePosition;

        }

       
    }


}

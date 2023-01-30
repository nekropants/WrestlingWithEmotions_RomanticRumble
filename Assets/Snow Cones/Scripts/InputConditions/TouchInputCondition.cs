using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputCondition : TriggerCondition
{

    public bool onInputDown = false;

    public Rect rect;

    public bool debug = false;

    public override bool IsSatisfied()
    {

        if (onInputDown && Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            return false;
        }

        if (Input.GetKey(KeyCode.Mouse0) == false)
        {
            return false;
        }


        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);



        if (pos.x > rect.x && pos.x < rect.x + rect.size.x)
        {
            if (pos.y > rect.y && pos.y < rect.y + rect.size.y)
            {

                return true;
            }
        }

        return false;
    }



    // Update is called once per frame
    void Update () {


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        }

    }

    private void OnDrawGizmosSelected()
    {
        //Vector3 pos = Camera.main.ViewportToWorldPoint(rect.center);
        //Vector3 size = Camera.main.ViewportToWorldPoint(rect.size);

        //Gizmos.color = Color.white.WithAlpha(0.3f);
        //Gizmos.DrawCube(pos, size);


    }
}

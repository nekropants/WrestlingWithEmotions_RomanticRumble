using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCondition : TriggerCondition{

    public float duration = 1;
     float randomDuration = 1;
     float  timer =0;
    float enableTime = 0;

    public bool random = false;


    public override bool IsSatisfied()
    {
        if (random)
            return timer > randomDuration;
        else
            return timer > duration;

    }


    private void OnEnable()
    {

        if(random)
        {
            randomDuration = duration*Random.value;
        }
        timer = 0;
    }


    // Update is called once per frame
    void Update () {


        if (random)
        {

            if (timer <= randomDuration)
            {
                timer += Time.deltaTime;
            }
        }
        else
        {

            if (timer <= duration)
            {
                timer += Time.deltaTime;
            }
        }
    }
}

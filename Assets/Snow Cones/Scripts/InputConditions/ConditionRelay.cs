using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionRelay : TriggerCondition {

    public GameObject conditionsToObserve;
    [Space]
    public List<GameObject> orConditions;
    public List<GameObject> andCondition;

    public  bool _evaluateOrFirst = false;

    public bool debug = false;

    public override bool IsSatisfied()
    {

        bool result = false;

        if( conditionsToObserve)
            result =TriggerCondition.CheckTriggerConditions(conditionsToObserve);

      // Debug.Log("conditionsToObserve  " + conditionsToObserve);


        if(_evaluateOrFirst)
        {
            for (int i = 0; i < orConditions.Count; i++)
            {
                result = result || CheckTriggerConditions(orConditions[i]);
                if (debug)
                {
                   // Debug.Log("orConditions[i] " + orConditions[i] + " " + CheckTriggerConditions(orConditions[i]));
                }
                
            } 
        }
     


        for (int i = 0; i < andCondition.Count; i++)
        {
            result = result && CheckTriggerConditions(andCondition[i]);
        }


        if (_evaluateOrFirst ==false)
        {
            for (int i = 0; i < orConditions.Count; i++)
            {
                result = result || CheckTriggerConditions(orConditions[i]);

              // if(debug)
               // Debug.Log("orConditions[i] " + orConditions[i] + " " + CheckTriggerConditions(orConditions[i]));
            }
        }

        if (debug)
            Debug.Log(name + "  " + result);

            return result;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

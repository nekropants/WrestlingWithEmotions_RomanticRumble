using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerCondition : MonoBehaviour
{

    public bool checkIfFalse = false;
    public abstract bool IsSatisfied();


    public static bool CheckTriggerConditions(GameObject gameObject)
    {
        if (gameObject == null)
            return true;

        TriggerCondition[] triggerConditions = gameObject.GetComponents<TriggerCondition>();
        return CheckTriggerConditions(triggerConditions);


    }



    public static bool CheckTriggerConditions(TriggerCondition[] triggerConditions)
    {
        for (int i = 0; i < triggerConditions.Length; i++)
        {
            if (triggerConditions[i] )
            {

                if( triggerConditions[i].gameObject.activeInHierarchy == false)
                    continue;

                if( triggerConditions[i].enabled == false)
                {
                    continue;
                }
                if (triggerConditions[i].IsSatisfied() == triggerConditions[i].checkIfFalse)
                    return false;
            }
        }
        return true;
    }

    public bool Evaluate()
    {
        return CheckTriggerConditions(gameObject);
    }
}

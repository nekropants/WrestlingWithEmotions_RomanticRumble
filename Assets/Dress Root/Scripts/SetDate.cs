using UnityEngine;
using System.Collections;

namespace Dance { 
 public class SetDate : MonoBehaviour
{

    public Character date1;
    public Character date2;

    public Transform dateAnchor;
    public Transform notDateAnchor;
    public static bool choseDate1 = true; 
    // Use this for initialization
    void Awake()
    {
        if (choseDate1)
        {
            date1.transform.position = dateAnchor.transform.position;
            date1.GetComponentInChildren<DanceFloorNPC>().isDate = true;

            date2.transform.position = notDateAnchor.transform.position;
            date2.GetComponentInChildren<DanceFloorNPC>().isDate = false;
        }
        else
        {
            date2.transform.position = dateAnchor.transform.position;
            date2.GetComponentInChildren<DanceFloorNPC>().isDate = true;

            date1.transform.position = notDateAnchor.transform.position;
            date1.GetComponentInChildren<DanceFloorNPC>().isDate = false;
        }

    }

    // Update is called once per frame
	void Update () {
	
	}
}

}
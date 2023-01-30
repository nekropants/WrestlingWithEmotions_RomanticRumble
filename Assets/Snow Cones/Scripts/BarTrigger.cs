using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
using UnityEngine;
using System.Collections;

public class BarTrigger : MonoBehaviour
{

    public ReturnToDateTrigger returnToDateTrigger;
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        BarCone barCone =  other.GetComponent<BarCone>();
        if (barCone != null && barCone.isPlayerControlled)
        {

            Patron[] patrons = Resources.FindObjectsOfTypeAll<Patron>();

            foreach (Patron cone in patrons)
            {
                cone.gameObject.SetActive(true);
            }

            Destroy(this);
            returnToDateTrigger.gameObject.SetActive(true);
            SceneController.ChangeScene(SceneEnum.OrderDrink);
        }
    }
}

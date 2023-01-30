using UnityEngine;
using System.Collections;

public class ReturnToDateTrigger : MonoBehaviour {


    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        print("On Trigger enter " + other);
        BarCone barCone =  other.GetComponent<BarCone>();
        if (barCone != null && barCone.isPlayerControlled)
        {
            enabled = false;
            SceneController.ChangeScene(SceneEnum.DrinkingShake );
        }
    }
}

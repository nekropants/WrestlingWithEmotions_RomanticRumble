using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IterateScenes : MonoBehaviour {


    public static int current = 0;

    SceneEnum[] scenes = {
        SceneEnum.Intro,
        SceneEnum.TitleScreen,
        SceneEnum.Changing,
        SceneEnum.ParkGateMeet,
        SceneEnum.ParkGateMeetAwkwardHug,
        SceneEnum.Map,
        SceneEnum.ShakeBar,
        SceneEnum.OrderDrink,
        SceneEnum.ShakeBar,
        SceneEnum.DrinkingShake,
        SceneEnum.Map,
         SceneEnum.DinoWalking,
         SceneEnum.SnowAngels,
        SceneEnum.Map,
        SceneEnum.HumanFeedingApproach,
        SceneEnum.HumanFeeding,
        SceneEnum.Map,
        SceneEnum.FerrisWheel,
        SceneEnum.Map,
        SceneEnum.Kiss,
    };
	// Use this for initialization
	void Start () {
		
	}
    public float lastPressTime = 0;    
	// Update is called once per frame
	void Update () {

        // if(Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    if (Time.time - lastPressTime < 0.3f)
        //    {
        //        current++;
        //        SceneController.ChangeScene(scenes[current]);
        //    }
        //    lastPressTime = Time.time;
        //}

        if(Input.touchCount == 4)
        {
            SceneController.ChangeScene(scenes[(int)SceneEnum.Map]);
            wasReleased = false;
        }
        else
        {
            wasReleased = true;
        }

    }

    bool wasReleased = false; 
}

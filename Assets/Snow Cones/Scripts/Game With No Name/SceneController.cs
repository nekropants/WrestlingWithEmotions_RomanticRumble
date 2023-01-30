using System;
using UnityEngine;
using System.Collections;


public enum SceneEnum
{
    None, 
    Lake,
    FerrisWheel,
    CellPhoneIntro,
    ParkGateMeet,
    Swings,
    ShakeBar,
    Map,
    Kiss,
    ParkGateMeetAwkwardHug,
    Intro,
    TitleScreen,
    Changing,
    OrderDrink,
    DrinkingShake,
    HumanFeeding,
    HumanFeedingApproach,
    DinoWalking,
    SnowAngels,
}

public class SceneController : Singleton<SceneController> {


    void SetScene(SceneEnum _scene)
    {
        if(_scene == SceneEnum.None)
            return;
        
        SceneMngr newScene = null;

        switch (_scene)
        {
            case SceneEnum.Lake:
                newScene = Lake;
                break;
            case SceneEnum.ParkGateMeet:
                newScene = ParkGateMeet;
                break;
            case SceneEnum.ShakeBar:
                newScene = ShakeBar;
                break;
            case SceneEnum.Map:
                newScene = Map;
                break;
            case SceneEnum.Kiss:
                newScene = Kiss;
                break;
            case SceneEnum.CellPhoneIntro:
                newScene = CellPhoneIntro;
                break;
            case SceneEnum.ParkGateMeetAwkwardHug:
                newScene = ParkGateMeetAwkwardHug;
                break;
            case SceneEnum.FerrisWheel:
                newScene = FerrisWheel;
                break;
            case SceneEnum.Intro:
                newScene = Intro;
                break;
            case SceneEnum.TitleScreen:
                newScene = TitleScreen;
                break;
            case SceneEnum.Changing:
                newScene = Changing;
                break;
            case SceneEnum.OrderDrink:
                newScene = OrderDrink;
                break;
            case SceneEnum.DrinkingShake:
                newScene = DrinkingShake;
                break;
            case SceneEnum.HumanFeeding:
                newScene = HumanFeeding;
                break;
            case SceneEnum.HumanFeedingApproach :
                newScene = HumanFeedingApproach;
                break;

            case SceneEnum.DinoWalking:
                newScene = DinoWalking;
                break;

            case SceneEnum.SnowAngels:
                newScene = SnowAngels;
                break;
        }

        if (newScene != null)
        {
            currentScene = _scene;
            foreach (SceneMngr scene in Instance.Scenes)
            {
                scene.gameObject.SetActive(false);
            }
            newScene.gameObject.SetActive(true);
            newScene.OnSceneOpen();
        }
    }


    public SceneEnum currentScene = SceneEnum.CellPhoneIntro;

    SceneMngr[] Scenes;

    public string spacer = "";
    public SceneMngr Lake;
    public SceneMngr FerrisWheel;
    public SceneMngr Swings;
    public SceneMngr ShakeBar;
    public SceneMngr Kiss;
    public SceneMngr ParkGateMeet;
    public SceneMngr Map;
    public SceneMngr CellPhoneIntro;
    public SceneMngr ParkGateMeetAwkwardHug;
    public SceneMngr Intro;
    public SceneMngr TitleScreen;
    public SceneMngr Changing;
    public SceneMngr OrderDrink;
    public SceneMngr DrinkingShake;
    public SceneMngr HumanFeeding;
    public SceneMngr HumanFeedingApproach;
    public SceneMngr DinoWalking;
    public SceneMngr SnowAngels;



    // Use this for initialization
    void Awake()
    {
        
//        Debug.LogError("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        Scenes = Resources.FindObjectsOfTypeAll<SceneMngr>() as SceneMngr[];

        ChangeScene(currentScene);
    }



    public static void ChangeScene(SceneEnum newScene)
    {
        Instance.SetScene(newScene);
    }

}

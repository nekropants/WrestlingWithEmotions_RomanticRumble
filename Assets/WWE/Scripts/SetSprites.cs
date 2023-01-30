using System;
using UnityEngine;
using System.Collections;
using WWE;
using Object = UnityEngine.Object;

public enum Characters
{
    None, Arms, Piggy, PrettyGuy, Ray,SenorMurder,  Bear, Dweeb, FreakShow, Stoney, SenorSunshine, JakeTheGerbil
}


[ExecuteInEditMode]
public class SetSprites : MonoBehaviour
{


    public  Characters debugCharacter = Characters.PrettyGuy;
    public bool refresh = false;
    // Use this for initialization
    // Use this for initialization
    void Awake()
    {
        if (WWE.SceneController.winner == Characters.None)
            WWE.SceneController.winner = debugCharacter;



        SetCharacter(WWE.SceneController.winner);

    }

    // Update is called once per frame
    void Update()
    {
        if (refresh)
        {
            SetCharacter(debugCharacter);
        }
    }


    public void SetCharacter(Characters characters)
    {
        string sheetName = "King pretty guy fight";

        switch (characters)
        {
            case Characters.Arms:
                sheetName = "Arms Fight";
                break;
            case Characters.Piggy:
                sheetName = "Piggy Fight";
                break;
            case Characters.PrettyGuy:
                sheetName = "King pretty guy fight";
                break;
            case Characters.Ray:
                sheetName = "Rad Ray Fight";
                break;
            case Characters.FreakShow:
                sheetName = "Freak Show Fight";
                break;
            case Characters.SenorMurder:
                sheetName = "Senor Murder Fight";
                break;
            case Characters.Dweeb:
                sheetName = "The Dweeb Fight";
                break;
            case Characters.Bear:
                sheetName = "Bare Bear Fight";
                break;
                 case Characters.Stoney:
                sheetName = "Stoney Fight";
                break;
            case Characters.SenorSunshine:
                sheetName = "Senor Sunshine Fight";
                break;
            case Characters.JakeTheGerbil:
                sheetName = "Jake the Gerbil Fight";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }


        refresh = false;
        SpriteSwitcher[] switchers = FindObjectsOfType<SpriteSwitcher>();
        Object[] frames = Resources.LoadAll(sheetName);

        foreach(var f in frames)
        {
            
        }

        print(sheetName + " " + frames.Length);

        for (int i = 0; i < switchers.Length; i++)
        {
            switchers[i].SwitchTo(frames);
        }
    }
}

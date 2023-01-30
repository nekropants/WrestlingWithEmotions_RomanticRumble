using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;




namespace Dance { 
 public class ChatScriptReader : MonoBehaviour
{

     public  static  string[] playerGreeting = new string[3];
     public  static  string[] dateGreeting = new string[3];

     public  static  string[] playerOffer = new string[3];
     public  static  string[] dateOffer = new string[3];

     public  static  string[] playerCompliment = new string[3];
     public  static  string[] dateCompliment = new string[3];

     public  static  string[] playerQuestion = new string[3];
     public  static  string[] dateQuestion = new string[3];


     public  static  string[] playerSongChange = new string[3];
     public  static  string[] dateSongChange = new string[3];

     public  static  string[] playerDance = new string[3];
    public static  string[] dateDance = new string[3];

     public TextAsset hotShotScript;
     public TextAsset purpleLeatherScript;

    public TextAsset scripToLoad;

    public static ChatScriptReader instance;



    // Use this for initialization
    void Awake()
    {
        scripToLoad = hotShotScript as TextAsset;
        instance = this;
        

    }

    private string[,] scriptCells;
    public  void LoadMainScript()
    {
        StringReader reader = new StringReader(scripToLoad.text);
        scriptCells = new string[100, 20];

        string line = "";

        line = reader.ReadLine();

        int row = 0;

        while (line != null)
        {
            string[] tokens = line.Split('\t');

            for (int i = 0; i < tokens.Length; i++)
            {
                scriptCells[row, i] = tokens[i];
            }

            line = reader.ReadLine();

            row++;
        }




        // --------------------------------------------------

        int start = 2;
     Read(playerGreeting, dateGreeting, ref start);
     Read(playerOffer, dateOffer, ref start);
     Read(playerCompliment, dateCompliment, ref start);
     Read(playerQuestion, dateQuestion, ref start);
     Read(playerSongChange, dateSongChange, ref start);
     Read(playerDance, dateDance, ref start);




    }

    void Read(string [] player, string[] date, ref int start)
    {
        for (int i = 0; i < 3; i++)
        {
            player[i]   = scriptCells[i + start, 0];
            date[i]     = scriptCells[i + start, 1];

            print(player[i] + "   " +date[i]);
        }
        start += 4;
    }


}
}
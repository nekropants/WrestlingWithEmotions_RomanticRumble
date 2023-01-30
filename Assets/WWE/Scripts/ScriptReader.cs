using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace WWE
{

    public class DateInteraction
    {

        public string name = "name";
        public string intro = "";
        public string[] salutations = new string[3];

        public string dateQuestion = "";
        public string[] myAnswers = new string[3];
        public string[] dateResponses = new string[3];
        public int[] replyPoints = new int[3];
        public int[] salutationPoints = new int[3];

        public string[] myQuestions = new string[3];
        public string[] dateResponses2 = new string[3];
        public Dictionary<string, string> positiveResponses = new Dictionary<string, string>();
        public Dictionary<string, string> negativeResponses = new Dictionary<string, string>();
        public string nuetralResponse;
        
        public string[] goodByes = new string[3];
        public int[] goodByePoints = new int[3];
        

    }

    public class ScriptReader : MonoBehaviour
    {

        public TextAsset script;
        public TextAsset goodByeScript;

        public static List<DateInteraction> dateInteractions = new List<DateInteraction>();

        public static string[] mannysIntro;
        public static string[] mannyReturn;


        // Use this for initialization
        void Awake()
        {

            dateInteractions.Clear();

          ReadMainScript();
          ReadGoodByes();

        }
        
        void ReadMainScript()
        {
            StringReader reader = new StringReader(script.text);
            string[,] scriptCells = new string[200, 20];

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


            int mannyIntroLines = 6;
            mannysIntro = new string[mannyIntroLines];


            // --- Parse Manny ---
            // -- intro

            for (int i = 0; i < mannyIntroLines; i++)
            {
                mannysIntro[i] = scriptCells[i + 3, 3];

            }

            // --- return


            int mannysReturnLines = 5;
            mannyReturn = new string[mannysReturnLines];

            for (int i = 0; i < mannysReturnLines; i++)
            {
                mannyReturn[i] = scriptCells[i + 3, 4];

            }

            // --- Parse Wrestlers ---

            for (int wrestler = 0; wrestler < 11; wrestler++)
            {

                int wrestlerStart = 11;
                int r = wrestlerStart + wrestler*10;

                DateInteraction interaction = new DateInteraction();
                interaction.name = scriptCells[r + 0, 1];

            
                

                interaction.intro = scriptCells[r + 0, 3];
                interaction.salutations[0] = scriptCells[r + 0, 4];
                interaction.salutations[1] = scriptCells[r + 1, 4];
                interaction.salutations[2] = scriptCells[r + 2, 4];

    print("--- " +  interaction.name );
                print( interaction.salutations[0] );

                int.TryParse("" + scriptCells[r + 0, 6], out interaction.salutationPoints[0]);
                int.TryParse("" + scriptCells[r + 1, 6], out interaction.salutationPoints[1]);
                int.TryParse("" + scriptCells[r + 2, 6], out interaction.salutationPoints[2]);


                interaction.dateQuestion = scriptCells[r + 3, 3];

                interaction.myQuestions[0] = scriptCells[r + 3, 3];
                interaction.myQuestions[1] = scriptCells[r + 4, 3];
                interaction.myQuestions[2] = scriptCells[r + 5, 3];

                interaction.myAnswers[0] = scriptCells[r + 3, 4];
                interaction.myAnswers[1] = scriptCells[r + 4, 4];
                interaction.myAnswers[2] = scriptCells[r + 5, 4];

                interaction.dateResponses[0] = scriptCells[r + 3, 5];
                interaction.dateResponses[1] = scriptCells[r + 4, 5];
                interaction.dateResponses[2] = scriptCells[r + 5, 5];

                int.TryParse("" + scriptCells[r + 3, 6], out interaction.replyPoints[0]);
                int.TryParse("" + scriptCells[r + 4, 6], out interaction.replyPoints[1]);
                int.TryParse("" + scriptCells[r + 5, 6], out interaction.replyPoints[2]);


                interaction.myQuestions[0] = scriptCells[r + 6, 3];
                interaction.myQuestions[1] = scriptCells[r + 7, 3];
                interaction.myQuestions[2] = scriptCells[r + 8, 3];

                interaction.dateResponses2[0] = scriptCells[r + 6, 4];
                interaction.dateResponses2[1] = scriptCells[r + 7, 4];
                interaction.dateResponses2[2] = scriptCells[r + 8, 4];


                dateInteractions.Add(interaction);
            }
        }


  void ReadGoodByes()
        {
              StringReader reader = new StringReader(goodByeScript.text);
        string[,] scriptCells = new string[200, 10];

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


      
            // --- Parse Wrestlers ---

            for (int wrestler = 0; wrestler < 11; wrestler++)
            {

                int wrestlerStart = 2;
                int r = wrestlerStart + wrestler*12;

                DateInteraction interaction =  dateInteractions[wrestler];
                
                interaction.nuetralResponse = scriptCells[r, 4];
                

                // --- positve ---
                for (int i =0; i< 5; i++)
                {
                    string key =  scriptCells[r + i , 3];
                    string sentence =  scriptCells[r + i, 4];
                    interaction.positiveResponses.Add(key, sentence);
                    

                    
                }
                
                
                // --- player good byes ---
                for(int i = 0; i< 3; i++)
                {


                    interaction.goodByes[i] = scriptCells[r + i, 5];



                    if (scriptCells[r + i, 6] != "")               
                    interaction.goodByePoints[i]= int.Parse(scriptCells[r + i, 6]);  
                }
                
                
                r+=5;
                
                
                // --- negative ---
                
                 for(int i =0; i< 5; i++)
                {
                    string key =  scriptCells[r + i, 3];
                    string sentence =  scriptCells[r + i, 4];
                    interaction.negativeResponses.Add(key, sentence);

                }



            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
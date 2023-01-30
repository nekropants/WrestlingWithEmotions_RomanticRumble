using System;
using System.IO;
using UnityEngine;
using System.Collections;


public static class DebugConfig
{

  
    private static bool debugTools = false;
    public static bool DebugTools
    {
        get
        {
            Init();

            return debugTools;
        }
    }
    private static bool exitOnComplete = false;
    public static bool ExitOnComplete
    {
        get
        {
            Init();

            return exitOnComplete;
        }
    }

    private static bool escapeExits = false;
    public static bool EscapeExits
    {
        get
        {
            Init();

            return escapeExits;
        }
    }

    private static bool Initialized = false;

    private static void Init()
    {

        if (Initialized)
            return;

        try
        {

            // dont load file for matchmaking server
         
            RunInit();

            Initialized = true;

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

#if UNITY_EDITOR



    [UnityEditor.MenuItem("Tools/Open DebugConfig File")]
    public static void OpenDebugConfigFile()
    {
        string filePath = Application.dataPath + "/..";

        filePath += "/DebugConfig.txt";

        if (File.Exists(filePath))
        {

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "Notepad.exe"; process.StartInfo.Arguments = filePath;
            process.Start();

        }
        else
        {
            Debug.Log("No file found at " + filePath);
        }
    }

#endif

    static void RunInit()
    {

        string filePath = Application.dataPath + "/..";

        filePath += "/DebugConfig.txt";

        if (File.Exists(filePath))
        {


            StreamReader reader = new StreamReader(filePath);
            while (reader != null && reader.EndOfStream == false)
            {
                string line = reader.ReadLine();
                ProcessDebugCommand(line);
            }
            reader.Close();
        }


        // Add Variables


        StreamWriter writer = new StreamWriter(filePath);

        writer.WriteLine("debugTools " + debugTools);
     
        writer.WriteLine("exitOnComplete " + exitOnComplete);

        writer.WriteLine("escapeExits " + escapeExits);
        
        writer.Flush();
        writer.Close();

    }

    private static void ProcessDebugCommand(string commandline)
    {

        try
        {
            string[] tokens = commandline.Split(new char[] { ' ' });
            if (tokens.Length < 2)
                return;

            string command = tokens[0].ToLower().Trim();
            string action = tokens[1].ToLower().Trim();
            bool boolAction = action == "true";
            int intValue = 0;
            bool parsed = int.TryParse(action, out intValue);

             if (command.Contains("exitOnComplete".ToLower()))
            {
                exitOnComplete = boolAction;
            }
            else if (command.Contains("debugTools".ToLower()))
                debugTools = boolAction;
            else if (command.Contains("escapeExits".ToLower()))
                escapeExits = boolAction;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

    }
}

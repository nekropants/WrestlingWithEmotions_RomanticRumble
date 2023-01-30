using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.Diagnostics;
using System.IO;

public class GameLauncher : MonoBase {

    bool GameInProgress
    {
        get
        {
            if (videoInstance)
            {
                return true;
            }


            if (process == null)
                return false;

            if (process.HasExited)
                return false;

         

            return true;
        }
    }

    public List<SelectionButton> buttons;
    public List<string> launchers;


    public static GameLauncher instance;


    public GameObject videoPrefab;
    public GameObject videoInstance;

    string directory
    {
        get
        {
            return Directory.GetParent(Application.dataPath) + "//Launchers";
        }
    }
    // Use this for initialization
    void Start () {

        instance = this;
        buttons = new List<SelectionButton>(GetComponentsInChildren<SelectionButton>());
        TryLoadLaunchers();
    }

    void TryLoadLaunchers()
    {

        launchers = new List<string>(Directory.GetFiles(directory));

        for (int i = launchers.Count - 1; i >= 0; i--)
        {
            if (launchers[i].Contains(".meta"))
            {
                launchers.RemoveAt(i);
            }
        }
    }

    public void Launchvideo()
    {


        if (GameInProgress)
        {
            // process.

            return;
        }

        StartCoroutine(PlayVideoRoutine());
    }

    public void Launch(SelectionButton button)
    {
        

        int index = buttons.IndexOf(button);

        print(index);
        print(launchers[index]);

        LaunchGame(launchers[index]);

    }


    bool videoRunning = false;

    public IEnumerator PlayVideoRoutine()
    {

        videoInstance = Instantiate(videoPrefab);


        VideoPlayer player = videoInstance.GetComponentInChildren<VideoPlayer>();
        yield return new WaitForSeconds(.1f);

        while (this.ActionDown == false )
        {
            yield return null;
        }

        if(videoInstance)
        {
            videoInstance.gameObject.SetActive(false);
            yield return null;
            Destroy(videoInstance);
        }
    }
    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Space) && GameInProgress == false)
        {


            //LaunchGame("C:\\Users\\Nekropants\\Desktop\\Exit Test\\exit.exe");
        }

        if (launchers.Count == 0)
        TryLoadLaunchers();


            if (process != null)

        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                print("Escape");
                process.Kill();
            }

            timer += Time.deltaTime;
            if (AnyInputDown)
            {
                timer = 0;
            }

            if (timer > 90)
            {
                process.Kill();
            }
        }

    }

    float timer = 0;

    static Process process;

    void LaunchGame(string filename)
    {
        if(GameInProgress)
        {
           // process.

            return;
        }
         process = new Process();
        // Configure the process using the StartInfo properties.
        process.StartInfo.FileName = filename;
        process.StartInfo.Arguments = "-n";
        process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
        process.Start();
    }

    public IEnumerator PauseRoutine()
    {
        yield return new WaitForSeconds(2);

        Time.timeScale = 0.000001f;


        while (GameInProgress)
        {
            yield return null;

        }

        Time.timeScale = 1f;

    }

    private void OnGUI()
    {
        if (launchers.Count == 0)
            GUILayout.Label("no games loaded at " + directory);

    }
}

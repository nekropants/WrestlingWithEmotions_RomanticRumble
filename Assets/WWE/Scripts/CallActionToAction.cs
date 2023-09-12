using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CallActionToAction : MonoBehaviour
{
    private float scale =1;


    private IEnumerator Start()
    {
        yield return new WaitForSeconds(90);
        
        SceneManager.LoadScene(0);
    }

    private void OnMouseOver()
    {
        scale =1.2f;

    }

    private void OnMouseExit()
    {
        scale =1;
    }

    private void OnMouseDown()
    {
        scale = 0.7f;
        OpenLink();
    }
    
    
    private void OnMouseUp()
    {
        scale =1.2f;
    }


    private string link = "steam://openurl/https://store.steampowered.com/app/1431250/Wrestling_With_Emotions_New_Kid_on_the_Block";

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private bool openSteamApplication = true;
    
    public void OpenLink()
    {
        if (openSteamApplication)
        {
            Application.OpenURL("steam://openurl/https://store.steampowered.com/app/1431250/Wrestling_With_Emotions_New_Kid_on_the_Block");
        }
        else
        {
            Application.OpenURL("https://store.steampowered.com/app/1431250/Wrestling_With_Emotions_New_Kid_on_the_Block");
        }

        openSteamApplication = !openSteamApplication;
        // StartCoroutine(IEOpenLink());
    }
    public IEnumerator IEOpenLink()
    {
        yield return new WaitForSeconds(.2f);
    }

    public void Update()
    {
        transform.localScale = Vector3.one*scale;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}

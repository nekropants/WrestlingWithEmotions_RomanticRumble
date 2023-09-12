using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkToExternalWebsite : MonoBehaviour
{
    public string link = "https://boxrec.com/en/box-pro/356831";

    public void OpenLink()
    {
        Debug.Log("OpenURL " + link);
        Application.OpenURL(link);
    }
}

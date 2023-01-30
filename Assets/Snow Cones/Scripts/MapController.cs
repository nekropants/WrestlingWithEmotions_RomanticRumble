using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour
{


    public static MapController Instance ;

    public int openCount = 0;
    public   MapMarker hill;
    public bool goToHill = false;
    // Use this for initialization
    void Awake ()
    {
        Instance = this;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

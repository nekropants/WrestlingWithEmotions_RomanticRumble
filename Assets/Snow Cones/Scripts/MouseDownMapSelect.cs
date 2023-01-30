using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDownMapSelect : MonoBehaviour {


    private void OnMouseDown()
    {
        Debug.Log("index " );

        GetComponentInParent<MapMarker>().TouchHighlighted();
    }
}

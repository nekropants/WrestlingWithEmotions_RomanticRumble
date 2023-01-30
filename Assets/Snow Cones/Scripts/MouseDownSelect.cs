using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDownSelect : MonoBehaviour {

    public int index = 0;
    public WardrobeController wardrobe;

    private void OnMouseDown()
    {
        Debug.Log("index " + index);
        wardrobe.SetSelection(index);
    }
}

using UnityEngine;
using System.Collections;

namespace Dance { 
 public class UnParent : MonoBehaviour {


    void Awake()
    {
        transform.parent = null;

    }
	// Use this for initialization
	void Start () {


    }

    // Update is called once per frame
    void Update () {
	
	}
}

}
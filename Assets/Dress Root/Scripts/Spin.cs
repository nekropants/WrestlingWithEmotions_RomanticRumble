using UnityEngine;
using System.Collections;

namespace Dance { 
 public class Spin : DanceBase {

	// Use this for initialization

 

    protected override void Run()
    {
        base.Run();

        int dir =1;
        if(flip)
            dir = -1;
	    transform.Rotate(0, 0, Time.deltaTime*frequency*dir);
    }
}

}
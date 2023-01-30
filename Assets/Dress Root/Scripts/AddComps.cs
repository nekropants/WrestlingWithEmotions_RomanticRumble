using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dance { 
 public class AddComps : MonoBehaviour {


    public bool refresh = false;

    
    public int startingmove =0;

    public bool addComps = false;


    void Start()
    {

        if (addComps)
        {
            Clickable[] hinges = GetComponentsInChildren<Clickable>();
            foreach (Clickable hinge in hinges)
            {
                hinge.AddLameComps();
            }
        }
    }


    


    // Update is called once per frame
    void Update()
    {

        if (refresh)
        {
                refresh = false;
            DanceBase[]added = GetComponentsInChildren<DanceBase>();
            //foreach (DanceBase danceBase in added)
            //{
            //    if (danceBase)
            //        DestroyImmediate(danceBase);
            //}


            return;
            Clickable[] hinges = GetComponentsInChildren<Clickable>();
            bool flip = false;
            foreach (Clickable hinge in hinges)
            {

   

             
                AddJoint(hinge.gameObject, 1, 1, flip);
                AddJoint(hinge.gameObject, 2, 45, flip);
                AddJoint(hinge.gameObject, 2, 100, flip);
               // AddJoint(hinge.gameObject, 1, -90);

                
                AddSpin(hinge.gameObject, 100, flip);
                AddSpin(hinge.gameObject, -200, flip);
                hinge.startingMove = startingmove;
                hinge.allowStartingMove = true;

                flip = !flip;
            }
        }
    }

    void AddJoint(GameObject go, float f, float a, bool flip)
    {
        Joint j = go.AddComponent<Joint>();
        j.frequency = f;
        j.amplitude = a;
        j.randomize = false;
        j.flip = flip;
    }

    void AddSpin(GameObject go, float f, bool flip)
    {
        Spin j = go.AddComponent<Spin>();
        j.frequency = f;
        j.randomize = false;
        j.flip = flip;
    }
}

}
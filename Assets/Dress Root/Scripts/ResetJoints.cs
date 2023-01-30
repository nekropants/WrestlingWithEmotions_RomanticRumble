using UnityEngine;
using System.Collections;


namespace Dance { 
[ExecuteInEditMode]
 public class ResetJoints : MonoBehaviour
{


    public bool run = false;
    public bool randomize = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            run = false;

            Joint[] added = GetComponentsInChildren<Joint>();
            foreach (Joint danceBase in added)
            {

                danceBase.frequency = 1;
                danceBase.amplitude = 1;
                danceBase.randomize = false;
            }

        }

        if (randomize)
        {
            randomize = false;

            Joint[] added = GetComponentsInChildren<Joint>();
            foreach (Joint danceBase in added)
            {

                danceBase.frequency = Random.Range(1, 8f);
                danceBase.amplitude = Random.Range(0, 30);
                danceBase.randomize = false;
            }

        }
    }
}

}
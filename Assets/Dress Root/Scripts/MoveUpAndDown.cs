using UnityEngine;
using System.Collections;

namespace Dance { 
 public class MoveUpAndDown : DanceBase
{
    public float piOffset = 0;

    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
            base.Start();

        if (randomize)
        {


            frequency *= Random.Range(0.5f, 1.5f);
            amplitude *= Random.Range(0.5f, 1.5f);
            if (Random.value > 0.5f)
                amplitude = -amplitude;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (character && character.dance == false)
            return;

        timer += Time.deltaTime * frequency;


        transform.localPosition -= offset;
        offset = amplitude * Mathf.Sin(timer + piOffset * Mathf.PI) * Vector3.up;

        transform.localPosition += offset;


    }
}

}
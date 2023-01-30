using UnityEngine;
using System.Collections;

namespace Dance { 
 public class DanceBase : MonoBehaviour
{

    public bool flip = false;

    public float frequency = 1;
    public float amplitude = 10;


    protected float timer = 0;

    public bool randomize = true;
    protected Character character;

    // Use this for initialization
    protected virtual void  Start ()
    {
        character = GetComponentInParent < Character>(); 
       

       if(randomize)
       {

        if (Random.value > 0.5f)
            amplitude = -amplitude;
       }

    }

    // Update is called once per frame
    protected virtual void Update ()
	{
        Run();

	}

   protected  virtual void Run()
    {
        if (character && character.useAdditiveTime)
            timer += Time.deltaTime * frequency;
        else
            timer = Time.time * frequency;

    }

    public void Randomize(float m)
    {
        frequency *= 1 + Random.Range(-m, m);
        amplitude *= 1 + Random.Range(-m, m);
    }
}

}
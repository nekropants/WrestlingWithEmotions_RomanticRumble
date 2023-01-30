using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public enum Direction { Left, Right, Random }
    public Direction direction = Direction.Left;

    public float speed = 1;
    
	// Use this for initialization
	void Start () 
    {
	 if (direction == Direction.Random)
        {
            if (Random.value > 0.5f)
            {
                direction = Direction.Left;
            }
         else
                direction = Direction.Right;

        }
	}
	
	// Update is called once per frame
	void Update () {

        float offset = Time.deltaTime * speed;

        if (direction == Direction.Left)
            offset = -offset;
       

        transform.Rotate(Vector3.forward, offset);
	}
}

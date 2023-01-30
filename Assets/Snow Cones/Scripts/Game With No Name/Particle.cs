using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

    static float gravity = 8;
    float groundHeight;
    Vector3 velocity;

    public enum Type { Shit, Blood, Vomit, Semen }
    Type type = Type.Shit;
	// Use this for initialization
    public void Setup(Vector3 position,  Vector2 Velocity, float GroundHeight, Type _type) 
    {
        velocity = Velocity;
        groundHeight = GroundHeight;
        transform.position = position;
        type = _type;
	}
	
	// Update is called once per frame
	void Update ()
    {

        velocity.y -= gravity * Time.deltaTime;

        transform.position += velocity;

        if (transform.position.y < groundHeight)
        {
            Vector3 pos = transform.position;
            pos.y = groundHeight;
            if(type == Type.Shit)
                EffectsController.CreatePileOfShit(pos);
            else if (type == Type.Vomit )
                EffectsController.CreatePileOfVomit(pos);
            else if (type == Type.Semen)
                EffectsController.CreatePileOfSemen(pos);

            Destroy(gameObject);
        }
	}
}

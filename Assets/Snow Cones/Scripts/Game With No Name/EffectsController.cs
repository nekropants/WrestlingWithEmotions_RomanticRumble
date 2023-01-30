using UnityEngine;
using System.Collections.Generic;

public class EffectsController : Singleton<EffectsController>
{

    public List<SpriteRenderer> shitParticles = new List<SpriteRenderer>();
    public List<SpriteRenderer> shitPiles = new List<SpriteRenderer>();


    public List<SpriteRenderer> vomitParticles = new List<SpriteRenderer>();
    public List<SpriteRenderer> vomitPiles = new List<SpriteRenderer>();

    public List<SpriteRenderer> semenParticles = new List<SpriteRenderer>();
    public List<SpriteRenderer> semenPiles = new List<SpriteRenderer>();

    public SpriteRenderer pissPuddle;

    public static void CreatePuddleOfPiss(Vector3 origin)
    {
        SpriteRenderer spawned = Instantiate(Instance.pissPuddle, origin, Quaternion.identity) as SpriteRenderer;
    }

	public static void CreateShitParticle(Vector3 origin, int direction, float baseHeight)
    {
        int index = Random.Range(0, Instance.shitParticles.Count );
        SpriteRenderer prefab  = Instance.shitParticles[index];
        SpriteRenderer spawned = Instantiate(prefab) as SpriteRenderer;

        float speed =  Random.Range(0.2f, 0.9f)*2;

        Vector2 Velocity = new Vector2(direction * speed, 0);
        baseHeight += Random.Range(3, 10);
        spawned.gameObject.AddComponent<Particle>().Setup(origin, Velocity, baseHeight, Particle.Type.Shit);
    }



    public static void CreatePileOfShit(Vector3 origin)
    {
        int index = Random.Range(0, Instance.shitPiles.Count);
        SpriteRenderer prefab = Instance.shitPiles[index];
        SpriteRenderer spawned = Instantiate(prefab, origin, Quaternion.identity) as SpriteRenderer;
    }


    public static void CreateVomitParticle(Vector3 origin, int direction, float baseHeight)
    {
        int index = Random.Range(0, Instance.vomitParticles.Count);
        SpriteRenderer prefab = Instance.vomitParticles[index];
        SpriteRenderer spawned = Instantiate(prefab) as SpriteRenderer;

        float speed = Random.Range(-0.1f, 0.45f);

        Vector2 Velocity = new Vector2(direction * speed, 0);
        baseHeight += Random.Range(3, 10);
        spawned.gameObject.AddComponent<Particle>().Setup(origin, Velocity, baseHeight, Particle.Type.Vomit);
    }

    public static void CreatePileOfVomit(Vector3 origin)
    {
        int index = Random.Range(0, Instance.shitPiles.Count);
        SpriteRenderer prefab = Instance.vomitPiles[index];
        SpriteRenderer spawned = Instantiate(prefab, origin, Quaternion.identity) as SpriteRenderer;
    }



    public static void CreateSemenParticle(Vector3 origin, int direction, float baseHeight)
    {

        int index = Random.Range(0, Instance.semenParticles.Count);
        SpriteRenderer prefab = Instance.semenParticles[index];
        SpriteRenderer spawned = Instantiate(prefab) as SpriteRenderer;

        float speed = Random.Range(4.1f, 4.5f);

        Vector2 Velocity = new Vector2(direction * speed, 0);
        baseHeight += Random.Range(5, 8);
        spawned.gameObject.AddComponent<Particle>().Setup(origin, Velocity, baseHeight, Particle.Type.Semen);

    }

    public static void CreatePileOfSemen(Vector3 origin)
    {
        int index = Random.Range(0, Instance.semenPiles.Count);
        SpriteRenderer prefab = Instance.semenPiles[index];
        SpriteRenderer spawned = Instantiate(prefab, origin, Quaternion.identity) as SpriteRenderer;
    }


}

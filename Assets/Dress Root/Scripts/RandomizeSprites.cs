
using UnityEngine;
using System.Collections;

namespace Dance { 
[ExecuteInEditMode]
 public class RandomizeSprites : MonoBehaviour
{
    public Controller controller;

    public bool randomize = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (randomize)
        {
            randomize = false;
            Rand();
        }
    }


    public virtual void Rand()
    {


        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();


        foreach (SpriteRenderer renderer in renderers)
        {
            Object spriteSheet = controller.spriteSheets[Random.Range(0, controller.spriteSheets.Length)];
            Object[] frames = Resources.LoadAll(spriteSheet.name);
            print(spriteSheet.name + " " + frames.Length);
            foreach (Object sprite in frames)
            {
                if (sprite.name == renderer.sprite.name)
                {
                    renderer.sprite = sprite as Sprite;
                    break;
                }
            }

        }
    }


    public void TrySetSprite(ref Sprite sprite, Object[] swapsSprites)
    {
        if (sprite == null)
            return;

        foreach (Object s in swapsSprites)
        {
            if (s.name == sprite.name)
            {
                sprite = s as Sprite;
                return;
            }

        }
    }
}

}
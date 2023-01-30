using UnityEngine;
using System.Collections;



public class SpriteSwitcher : MonoBehaviour
{
    // Use this for initialization
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public virtual void SwitchTo(Object[] sprites)
    {


        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        foreach (Object sprite in sprites)
        {
            
            if (sprite.name == renderer.sprite.name)
            {
                renderer.sprite = sprite as Sprite;
                return;
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


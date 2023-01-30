using UnityEngine;
using System.Collections;

namespace Dance { 
[ExecuteInEditMode]
 public class AssignSprites : MonoBehaviour
{


    public Object assignedSheet;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (assignedSheet)
        {
            SwitchTo(assignedSheet);
            assignedSheet = null;
        }
    }


    public virtual void SwitchTo(Object spriteSheet)
    {
        Object[] frames = Resources.LoadAll(spriteSheet.name);


        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();


        foreach (SpriteRenderer renderer in renderers)
        {

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
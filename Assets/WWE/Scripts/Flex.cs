using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace WWE
{
    public class Flex : MonoBehaviour
    {
        public static  Transform selectedSticker;

        public Sprite stickerReach;
        public Sprite stickeGrab;
        public Sprite stickePlace;
        public Sprite flexFrame;
        public Sprite defaultFrame;
        private SpriteRenderer renderer;
        
       
        private bool canPlace = false;
        // Use this for initialization
        void Start()
        {
            renderer = GetComponent<SpriteRenderer>();
            defaultFrame = renderer.sprite;
        }

        // Update is called once per frame
        void Update()
        {



            if (Pencil.instance.down)
                renderer.sprite = flexFrame;
            else
                renderer.sprite = defaultFrame;

            if (stickerReach != null)
            {
                
                if (selectedSticker)
                {

                    Vector3 screenPos = Arm.clampedMousePos;
                    Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
                    worldPos.z = selectedSticker.transform.position.z;
                    selectedSticker.transform.position = worldPos;
                    renderer.sprite = stickeGrab;

                    if (canPlace)
                    {

                        if (Input.GetKey(KeyCode.Mouse0))
                        {
                            renderer.sprite = stickePlace;
                        }

                        if (Input.GetKeyUp(KeyCode.Mouse0))
                        {
                            if (Table.instance.form.GetComponent<BoxCollider2D>().OverlapPoint(worldPos))
                            {
                                 selectedSticker.transform.parent = WrestlerInfo.stickerTransform.transform;
                                selectedSticker.GetComponent<Shadow>().enabled = false;
                            }
                            selectedSticker = null;
                        }
                    }
                    else
                    {

                        if (Input.GetKeyUp(KeyCode.Mouse0))
                            canPlace = true;
                    }
                }
                else
                {

                    canPlace = false;

                    Transform closest = Stickers.instance.GetClosestSticker();
                    if (closest != null)
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            selectedSticker = closest;
                            Stickers.instance.BringToFront(selectedSticker);
                        }
                        else
                        {
                            renderer.sprite = stickerReach;

                        }
                    }
                    else
                    {
                    }
                }
            }
   
        }
    }
}
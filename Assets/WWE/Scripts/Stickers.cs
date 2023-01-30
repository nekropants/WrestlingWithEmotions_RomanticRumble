using UnityEngine;
using System.Collections;

namespace WWE
{

    public class Stickers : MonoBehaviour
    {
        public static Stickers instance;


        public Camera cam;

        private SpriteRenderer selectedSticker;
        // Use this for initialization
        void Start()
        {
            instance = this;

            for (int i = transform.childCount -1 ; i >= 0; i--)
            {
                transform.GetChild(i).SetSiblingIndex(Random.Range(0, transform.childCount));
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Transform GetClosestSticker()
        {
            Vector3 screenPos = Input.mousePosition;
            Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
            worldPos.z = 0;
            for (int i = transform.childCount -1; i >= 0; i--)
            {

                Vector3 stickerPos = transform.GetChild(i).transform.position;
                if (Vector2.Distance(stickerPos, worldPos) < 20)
                {

                    return transform.GetChild(i);
                }
            }

            return null;
        }


        public void  BringToFront(Transform sticker)
        {
            sticker.SetSiblingIndex(transform.childCount -1);
        }
    }
}

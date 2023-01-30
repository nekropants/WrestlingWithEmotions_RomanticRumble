using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace WWE
{

    public class DrawToTexture : MonoBehaviour
    {
        private Texture2D texture;



        public bool shareSprite = false;

        public Sprite sprite = null;

        private static Sprite sharedSprite;
        private int width;
        private int height;

        public Camera cam;
        // Use this for initialization
        void Awake()
        {

            RectTransform rect = transform as RectTransform;

            width = (int) rect.sizeDelta.x;
            height = (int) rect.sizeDelta.y;
            if (!shareSprite || sharedSprite == null)
            {




                Clear();

              //  sprite = Sprite.Create(texture, new Rect(0, 0, width, height), Vector2.zero);

                if (shareSprite)
                    sharedSprite = sprite;

            }

            if (shareSprite)
                sprite = sharedSprite;

            GetComponent<Image>().sprite = sprite;
            GetComponent<Image>().sprite.name = "fff";
            GetComponent<Image>().color = Color.white;
        }


        public void Clear()
        {
            texture = new Texture2D(width, height);
            texture.name = "fasdf";

            sprite = Sprite.Create(texture, new Rect(0, 0, width, height), Vector2.zero);
            GetComponent<Image>().sprite = sprite;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    texture.SetPixel(i, j, new Color(0, 0, 0, 0));
                }
            }
            texture.Apply();
        }


        private float pixelWidth;
        private float pixelHeight;
        float unitsToPixels;
        private Texture2D tex;



        int x0 = 0;
        int y0 = 0;


        public bool debug = false;
        // Update is called once per frame
        void Update()
        {


            Vector3 screenPos = Arm.clampedMousePos;
            Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
            worldPos.z = -10;

   Vector3 pixelPos = transform.InverseTransformPoint(worldPos);
                

                int x = (int) pixelPos.x;
                int y = (int) pixelPos.y;


            if (Pencil.instance.down && Gaze.instance.lookDown && Flex.selectedSticker == null)
            {
             
               


                Line(x0, y0, x, y);

            

                texture.Apply();


            }
            
                x0 = x;
                y0 = y;


        }


        bool Plot(int x, int y)
        {


            bool outOfRange = false;
            int range = 3;
            for (int i = -range/2; i <= range/2; i++)
            {
                for (int j = -range/2; j <= range/2; j++)
                {

                    int _x = x + i;
                    int _y = y + j;
                    if (_x < 0 || _y < 0 || _x >= width || _y >= height)
                    {
                        outOfRange = true;
                        continue;
                    }

                    texture.SetPixel(_x, _y, SceneController.instance.inkColor);

                }
            }
            return true;
        }

        private static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        public void Line(int x0, int y0, int x1, int y1)
        {
            bool steep = Mathf.Abs(y1 - y0) > Mathf.Abs(x1 - x0);
            if (steep)
            {
                Swap<int>(ref x0, ref y0);
                Swap<int>(ref x1, ref y1);
            }
            if (x0 > x1)
            {
                Swap<int>(ref x0, ref x1);
                Swap<int>(ref y0, ref y1);
            }
            int dX = (x1 - x0), dY = Math.Abs(y1 - y0), err = (dX/2), ystep = (y0 < y1 ? 1 : -1), y = y0;

            for (int x = x0; x <= x1; ++x)
            {
                if (!(steep ? Plot(y, x) : Plot(x, y))) return;
                err = err - dY;
                if (err < 0)
                {
                    y += ystep;
                    err += dX;
                }
            }


        }
    }
}
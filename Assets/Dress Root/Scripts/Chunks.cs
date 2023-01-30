using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Dance { 
 public class Chunks : MonoBehaviour
{
    public Image[] images;

    public Color flashColor = Color.green;
    public Color activeColor = Color.cyan;
    public Color inactuveColor = Color.grey;

    private float timer;
    public bool scrollColor = false;
    public float speed = 1;

    private bool on = false;
     int count = 0;


    public static Chunks instance;
    // Use this for initialization
    void Start ()
    {
        instance = this;
        SetCount(0);
    }
	
	// Update is called once per frame
    void Update()
    {

        if (scrollColor)
        {
            timer += Time.deltaTime;
            if (timer > speed)
            {

                foreach (Image image in images)
                {
                    if (on)
                        image.color = activeColor;
                    else
                        image.color = inactuveColor;
                }
                timer -= speed;

                on = !on;
            }

        }
 
    }


    public void SetCount(int c)
    {
   
        count = c;
        for (int i = 0; i < 5; i++)
        {
            if (i >= count)
                images[i].color = inactuveColor;
            else
            {
                images[i].color = activeColor;

            }
        }
        if (c <= 0 || count > 4)
            return;

        StartCoroutine(Flash(images[count - 1]));
    }


    IEnumerator Flash(Image image)
    {
        for (int i = 0; i < 2; i++)
        {
            image.color = activeColor;
            yield return new WaitForSeconds(0.1f);

            image.color = inactuveColor;
            yield return new WaitForSeconds(0.1f);
        }
        image.color = activeColor;
    }

    public void RunFlashing()
    {
        StartCoroutine(RunFlashingRoutine());
    }

    IEnumerator RunFlashingRoutine()
    {
        scrollColor = true;
        yield return new WaitForSeconds(1.5f);
        scrollColor = false;

        SetCount(0);
    }
}

}
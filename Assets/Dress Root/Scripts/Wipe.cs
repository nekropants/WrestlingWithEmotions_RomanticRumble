using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dance { 
 public class Wipe : MonoBehaviour
{

    public Image spriteRenderer;
    public static Wipe intance; 
	// Use this for initialization
	void Start ()
	{
	    intance = this;
        spriteRenderer.enabled = false;

        if(ChatToDate.instance == null)
            StartCoroutine(WipeOff());
	}


    public void WipeOn()
    {
        StartCoroutine(WipeOnRoutine());
    }
    IEnumerator WipeOnRoutine()
    {
        spriteRenderer.enabled = true;

            yield return null;
        float timer = 0;
        while (timer <1)
        {
            timer += Time.deltaTime/2f;

            timer = Mathf.Clamp01(timer);
            transform.localPosition = Vector3.right*(1 - timer)*5000; 
            yield return null;
        }

        SceneManager.LoadScene("Dance Floor");
    }


    IEnumerator WipeOff()
    {
        spriteRenderer.enabled = true;

        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;

            timer = Mathf.Clamp01(timer);
            transform.localPosition = -Vector3.right * (timer) * 5000;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}

}
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Dance { 
 public class BarCollider : MonoBehaviour
{

    public Camera camera;
    public Canvas canvas;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(ChatToDate.instance.everyOneWalkedOff)
            return;

        if(other.GetComponent<Rigidbody2D>() == null)
        return;
            
        StartCoroutine(LoadBarScene());
    }

    IEnumerator LoadBarScene()
    {
        GetComponent<Collider2D>().enabled = false;

        SceneManager.LoadScene("BuyDrink", LoadSceneMode.Additive);
        CanvasUI.instance.avatar.hiding = true;
         
        yield return null;
        camera.gameObject.SetActive(false);
        canvas.enabled = false;
        while (BuyDrinkController.instance)
        {
          
            yield return null;

        }

        if(ChatToDate.instance.engaged)
            CanvasUI.instance.avatar.showing = true;

        canvas.enabled = true;
        camera.gameObject.SetActive(true);
        Player.instance.ShowBeer();

    }

}

}
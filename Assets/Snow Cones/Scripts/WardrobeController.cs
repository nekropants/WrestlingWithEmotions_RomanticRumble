
using UnityEngine;
using System.Collections;

public class WardrobeController : MonoBase
{

    public GameObject chocSauce;
    public GameObject strawberrySauce;
    public GameObject caramelSauce;

    public GameObject nutSprinkels;
    public GameObject colorSprinkels;
    public GameObject chocSprinkels;

    public GameObject flakeInserted;
    public GameObject bowInserted;
    public GameObject cherryInserted;


    public GameObject chocSauceBottle;
    public GameObject strawberrySauceBottle;
    public GameObject caramelSauceBottle;

    public GameObject nutSprinkelsBowl;
    public GameObject colorSprinkelsBowl;
    public GameObject chocSprinkelsBowl;

    public GameObject cherryAccessory;
    public GameObject flakeAccessory;
    public GameObject bowAccessory;

    public Transform squirtTransform;

    public GameObject selectionHighlight;


    private float selectionspeed = 40;

	// Use this for initialization
	void Start ()
	{
        chocSauce.gameObject.SetActive(false);
        strawberrySauce.gameObject.SetActive(false);
        caramelSauce.gameObject.SetActive(false);


        chocSauceBottle.gameObject.SetActive(false);
        strawberrySauceBottle.gameObject.SetActive(false);
        caramelSauceBottle.gameObject.SetActive(false);


        nutSprinkels.gameObject.SetActive(false);
        colorSprinkels.gameObject.SetActive(false);
        chocSprinkels.gameObject.SetActive(false);

        nutSprinkelsBowl.gameObject.SetActive(false);
        colorSprinkelsBowl.gameObject.SetActive(false);
        chocSprinkelsBowl.gameObject.SetActive(false);

        cherryAccessory.gameObject.SetActive(false);
        flakeAccessory.gameObject.SetActive(false);
        bowAccessory.gameObject.SetActive(false);

        flakeInserted.gameObject.SetActive(false);
        cherryInserted.gameObject.SetActive(false);
        bowInserted.gameObject.SetActive(false);

        selectionHighlight.SetActive(false);

        StartCoroutine(SelectSuace());
	}


    private float sinTimer = 0;

    public void Wiggle(GameObject selected)
    {
        sinTimer += Time.deltaTime*4f;
        float sin = Mathf.Sin(sinTimer);
        selected .gameObject.transform.rotation = Quaternion.Euler(0, 0, sin*12);
    }
    
        int selection = 1;

    IEnumerator SelectSuace()
    {
        float interval = 0.15f;

        chocSauceBottle.gameObject.SetActive(true);
        strawberrySauceBottle.gameObject.SetActive(true);
        caramelSauceBottle.gameObject.SetActive(true);



        GameObject topping = null;
        GameObject selected = null;

        while (true)
        {
            if(LeftDown)
                selection--;
            if (RightDown)
                selection++;


            if (LeftDown || RightDown)
            {
                sinTimer = 0;
            }
            selection += 3;
            selection %= 3;


            Vector3 targetPos;
            if (selection == 0)
                targetPos = chocSauceBottle.transform.position;
            else if (selection == 1)
                targetPos = caramelSauceBottle.transform.position;
            else
                targetPos = strawberrySauceBottle.transform.position;



            SetSelectionTarget(targetPos);

            if (selection == 0)
            {
            selected = chocSauceBottle;
            topping = chocSauce;
        }
        else if (selection == 1)
        {
            selected = caramelSauceBottle;
            topping = caramelSauce;
        }
        else
        {
            selected = strawberrySauceBottle;
            topping = strawberrySauce;
        }


            chocSauceBottle.transform.rotation = Quaternion.identity;
            strawberrySauceBottle.transform.rotation = Quaternion.identity;
            caramelSauceBottle.transform.rotation = Quaternion.identity;

            Wiggle(selected);

            if (ActionDown || touchSelection)
            {
                touchSelection = false;
                break;
            }

            yield return null;
        }

     

  

        if (chocSauceBottle != selected)
            chocSauceBottle.gameObject.SetActive(false);
        if (caramelSauceBottle != selected)
            caramelSauceBottle.gameObject.SetActive(false);
        if (strawberrySauceBottle != selected)
            strawberrySauceBottle.gameObject.SetActive(false);

        selectionHighlight.gameObject.SetActive(false);
        selected.transform.position = squirtTransform.transform.position;
        selected.transform.rotation = squirtTransform.transform.rotation;
        topping.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);


        selected.gameObject.SetActive(false);

        yield return null;;

        StartCoroutine(SelectTopping());

    }


    bool touchSelection = false;
    public void SetSelection(int value)
    {

        if(selection == value)
        {
            touchSelection = true;
        }
        selection = value;
    }

    IEnumerator SelectAccessory()
    {
        float interval = 0.15f;

        cherryAccessory.gameObject.SetActive(true);
        flakeAccessory.gameObject.SetActive(true);
        bowAccessory.gameObject.SetActive(true);

        GameObject topping = null;
        GameObject selected = null;

        while (true)
        {

            if (LeftDown)
                selection--;
            if (RightDown)
                selection++;

            selection += 3;
            selection %= 3;


            if (LeftDown || RightDown)
            {
                sinTimer = 0;
            }

            Vector3 targetPos;
            if (selection == 0)
                targetPos = bowAccessory.transform.position;
            else if (selection == 1)
                targetPos = cherryAccessory.transform.position;
            else
                targetPos = flakeAccessory.transform.position;


            SetSelectionTarget(targetPos);

       
            if (selection == 0)
            {
                selected = bowAccessory;
                topping = bowInserted;
            }
            else if (selection == 1)
            {
                selected = cherryAccessory;
                topping = cherryInserted;
            }
            else
            {
                selected = flakeAccessory;
                topping = flakeInserted;
            }


            bowAccessory.transform.rotation = Quaternion.identity;
            cherryAccessory.transform.rotation = Quaternion.identity;
            flakeAccessory.transform.rotation = Quaternion.identity;

            Wiggle(selected);


            if (ActionDown || touchSelection)
            {
                touchSelection = false;
                break;
            }



            yield return null;
        }

   
     //   if (cherryAccessory != selected)
            cherryAccessory.gameObject.SetActive(false);
      //  if (bowAccessory != selected)
            bowAccessory.gameObject.SetActive(false);
      //  if (flakeAccessory != selected)
            flakeAccessory.gameObject.SetActive(false);

        selectionHighlight.gameObject.SetActive(false);
        selected.transform.position = squirtTransform.transform.position;
        selected.transform.rotation = squirtTransform.transform.rotation;
        topping.gameObject.SetActive(true);

        selected.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);

        SceneController.ChangeScene(SceneEnum.ParkGateMeet);
        yield return null;
    }



    IEnumerator SelectTopping()
    {
        float interval = 0.15f;

        nutSprinkelsBowl.gameObject.SetActive(true);
        colorSprinkelsBowl.gameObject.SetActive(true);
        chocSprinkelsBowl.gameObject.SetActive(true);

        GameObject topping = null;
        GameObject selected = null;
        while (true)
        {

            if (LeftDown)
                selection--;
            if (RightDown)
                selection++;


            if (LeftDown || RightDown)
            {
                sinTimer = 0;
            }

            selection += 3;
            selection %= 3;


            Vector3 targetPos;
            if (selection == 0)
                targetPos = chocSprinkelsBowl.transform.position;
            else if (selection == 1)
                targetPos = colorSprinkelsBowl.transform.position;
            else
                targetPos = nutSprinkelsBowl.transform.position;


            SetSelectionTarget(targetPos);



            if (selection == 0)
            {
                selected = chocSprinkelsBowl;
                topping = chocSprinkels;
            }
            else if (selection == 1)
            {
                selected = colorSprinkelsBowl;
                topping = colorSprinkels;
            }
            else
            {
                selected = nutSprinkelsBowl;
                topping = nutSprinkels;
            }



            chocSprinkelsBowl.transform.rotation = Quaternion.identity;
            colorSprinkelsBowl.transform.rotation = Quaternion.identity;
            nutSprinkelsBowl.transform.rotation = Quaternion.identity;

            Wiggle(selected);
            if (ActionDown || touchSelection)
            {
                touchSelection = false;
                break;
            }




            yield return null;
        }

    

        if (chocSprinkelsBowl != selected)
            chocSprinkelsBowl.gameObject.SetActive(false);
        if (colorSprinkelsBowl != selected)
            colorSprinkelsBowl.gameObject.SetActive(false);
        if (nutSprinkelsBowl != selected)
            nutSprinkelsBowl.gameObject.SetActive(false);

        selectionHighlight.gameObject.SetActive(false);
        selected.transform.position = squirtTransform.transform.position;
        selected.transform.rotation = squirtTransform.transform.rotation;
        topping.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);


        selected.gameObject.SetActive(false);

        yield return null; ;
        StartCoroutine(SelectAccessory());

    }

    private void SetSelectionTarget( Vector3 target)
    {
        if (selectionHighlight.gameObject.activeSelf == false)
        {
            selectionHighlight.SetActive(true);
            selectionHighlight.transform.position = target;
        }
        selectionHighlight.transform.position = Vector3.Lerp(selectionHighlight.transform.position, target,
               Time.deltaTime * 40);
    }

    // Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class PoseController : MonoBehaviour {

    public static bool open = false;

    static PoseController instance;
    public GameObject peoplesElbow;
    public GameObject lift;
    public GameObject flyKick;

    public static int direction = 1;
    // Use this for initialization
    void Start () {
        instance = this;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public static void PeopleElbow()
    {

        if (open)
            return;


        instance.StartCoroutine(instance.ShowRoutine(instance.peoplesElbow));
    }
    public static void FlyKick(int direction)
    {

        if (open)
            return;


        instance.StartCoroutine(instance.ShowRoutine(instance.flyKick, direction));
    }

    public static void Lift()
    {

        if (open)
            return;


        instance.StartCoroutine(instance.ShowRoutine(instance.lift));
    }


    IEnumerator ShowRoutine( GameObject toShow, int dir = 1)
    {
        open = true;
        toShow.gameObject.SetActive(true);
        direction = dir;
       
        toShow.transform.localScale = Vector3.Scale( toShow.transform.localScale, new Vector3(dir, 1, 1));

        yield return new WaitForSeconds(1f);

        while (Input.GetKey(KeyCode.Mouse0) == false)
        {
            yield return null;
        }
        
            yield return null;


        toShow.gameObject.SetActive(false);

        open = false;

    }
}

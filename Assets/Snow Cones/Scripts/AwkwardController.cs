using UnityEngine;
using System.Collections;

public class AwkwardController : MonoBase
{


    public Transform bluePose1;
    public Transform bluePose2;
    public Transform bluePose3;
    public Transform bluePose4;
    public Transform bluePose5;

    public Transform pinkPose1;
    public Transform pinkPose2;
    public Transform pinkPose3;
    public Transform pinkPose4;
    public Transform pinkPose5;

    public Transform highFiveImpact;

    public AudioClip highFiveSound;

	// Use this for initialization
	IEnumerator Start ()
	{

	    float interval = 0.2f;

	    DisableAll();
        bluePose1.gameObject.SetActive(true);
        pinkPose1.gameObject.SetActive(true);
        highFiveImpact.gameObject.SetActive(false);


	    yield return StartCoroutine(WaitForButton());
        DisableBlue();
        bluePose2.gameObject.SetActive(true);

        yield return new WaitForSeconds(interval);

	    DisablePink();
        pinkPose2.gameObject.SetActive(true);

        yield return StartCoroutine(WaitForButton());

        DisableBlue();
        bluePose3.gameObject.SetActive(true);


        yield return new WaitForSeconds(interval);
        DisablePink();
        pinkPose3.gameObject.SetActive(true);

        yield return StartCoroutine(WaitForButton());

        DisableBlue();
        bluePose4.gameObject.SetActive(true);

        yield return new WaitForSeconds(interval);
        DisablePink();
        pinkPose4.gameObject.SetActive(true);


        yield return StartCoroutine(WaitForButton());
        DisableBlue();
        bluePose5.gameObject.SetActive(true);
        yield return new WaitForSeconds(interval);

        DisablePink();
        pinkPose5.gameObject.SetActive(true);
        pinkPose5.gameObject.SetActive(true);


        highFiveImpact.gameObject.SetActive(true);
        AudioControllerShit.Play(highFiveSound);

        yield return new WaitForSeconds(0.3f);

        highFiveImpact.gameObject.SetActive(false);


        yield return new WaitForSeconds(0.6f);

        SceneController.ChangeScene(SceneEnum.Map );
	}

    private IEnumerator WaitForButton()
    {
        while (GetUniqueButton() == false)
        {
            yield return null;
        }

        yield return null;

    }

    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool upPressed = false;
    private bool downPressed = false;


    private bool GetUniqueButton()
    {

        if(TouchDown)
        {
            return true;
        }

        if (LeftDown && leftPressed == false)
        {
            leftPressed = true;
            return true;
        }
        if (RightDown && rightPressed == false)
        {
            rightPressed = true;
            return true;
        }
        if (UpDown && upPressed == false)
        {
            upPressed = true;
            return true;
        }
        if (DownDown && downPressed == false)
        {
            downPressed = true;
            return true;
        }
        return false;
    }


    private void DisableAll()
    {   bluePose1.gameObject.SetActive(false);
        bluePose2.gameObject.SetActive(false);
        bluePose3.gameObject.SetActive(false);
        bluePose4.gameObject.SetActive(false);
        bluePose5.gameObject.SetActive(false);

        pinkPose1.gameObject.SetActive(false);
        pinkPose2.gameObject.SetActive(false);
        pinkPose3.gameObject.SetActive(false);
        pinkPose4.gameObject.SetActive(false);
        pinkPose5.gameObject.SetActive(false);

    }

    private void DisablePink()
    {

        pinkPose1.gameObject.SetActive(false);
        pinkPose2.gameObject.SetActive(false);
        pinkPose3.gameObject.SetActive(false);
        pinkPose4.gameObject.SetActive(false);
        pinkPose5.gameObject.SetActive(false);

    }
    private void DisableBlue()
    {
        bluePose1.gameObject.SetActive(false);
        bluePose2.gameObject.SetActive(false);
        bluePose3.gameObject.SetActive(false);
        bluePose4.gameObject.SetActive(false);
        bluePose5.gameObject.SetActive(false);

    }
    // Update is called once per frame
	void Update () {
	
	}
}

using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CellController : MonoBase
{

    public ThumbController thumb;
    public Transform[] letterButtons;
    public Transform acceptButton;

    public Text messageNotification;
    public Text openLabel;
    public Text sendLabel;
    public Text replyLabel;
    public Text messageFromDate;
    public Text replyToDate;
    public string replyToDateMessage;
    public Transform sendIcon;
    public Transform IdleScreen;



    public AudioClip SendTone;
	// Use this for initialization
	void Start ()
	{
	    replyToDateMessage = replyToDate.text;
        
        StartCoroutine(RecieveMessageRoutine());
	}
	
	// Update is called once per frame
    private void Update()
    {
        if (thumb.targetButton == null)
        {
            if (LeftDown || RightDown || UpDown || DownDown)
            {
                //thumb.SetTarget(letterButtons.RandomElement());
            }
        }
    }

    private IEnumerator RecieveMessageRoutine()
    {
        messageNotification.gameObject.SetActive(false);
        messageFromDate.gameObject.SetActive(false);
        replyToDate.gameObject.SetActive(false);
        openLabel.gameObject.SetActive(false);
        sendLabel.gameObject.SetActive(false);
        replyLabel.gameObject.SetActive(false);
        replyToDate.gameObject.SetActive(false);
        sendIcon.gameObject.SetActive(false);
        IdleScreen.gameObject.SetActive(false);

        messageNotification.gameObject.SetActive(true);
        openLabel.gameObject.SetActive(true);

        yield return StartCoroutine(WaitForAcceptButton());
        

        openLabel.gameObject.SetActive(false);
        messageNotification.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        messageFromDate.gameObject.SetActive(true);

        yield return new WaitForSeconds(3.5f);
        replyLabel.gameObject.SetActive(true);

        yield return StartCoroutine(WaitForAcceptButton());

        messageFromDate.gameObject.SetActive(false);
        replyLabel.gameObject.SetActive(false);

        replyToDate.gameObject.SetActive(true);
        sendLabel.gameObject.SetActive(true);

        replyToDate.text = "";

        yield return null;

        int count = 0;

        while (count <= replyToDateMessage.Length)
        {

            replyToDate.text = replyToDateMessage.Substring(0, count);

            if (thumb.targetButton == null && AnyInputDown)
            {
                thumb.SetTarget(letterButtons.RandomElement());
                count+=2;
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.6f);

        yield return StartCoroutine(WaitForAcceptButton());

        replyToDate.gameObject.SetActive(false);
        sendLabel.gameObject.SetActive(false);


        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            sendIcon.gameObject.SetActive(true);

         
            yield return new WaitForSeconds(0.5f);
            sendIcon.gameObject.SetActive(false);
            if (i == 1)
            {
                AudioControllerShit.Play(SendTone);
            }
        }

        IdleScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        float timer = 0;

        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            scene.SceneCamera.transform.position += Vector3.up * Time.deltaTime * 5000;
            yield return null;
        }

        SceneController.ChangeScene(SceneEnum.Changing);
        yield return null;
    }

    private IEnumerator WaitForAcceptButton()
    {
        while ((AnyInputDown == false))
        {
            yield return null;
        }
        thumb.SetTarget(acceptButton);

        float failSafeTimer = 0;
        while (thumb.targetButton != null && failSafeTimer < 3)
        {
            yield return null;
            failSafeTimer = Time.deltaTime;
        }
    }
}

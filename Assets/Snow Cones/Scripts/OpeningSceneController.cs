using System.Runtime.Serialization.Formatters;
using UnityEngine;
using System.Collections;

public class OpeningSceneController : MonoBase
{

    public Transform cellPhone;
    public Transform cellPhoneAlert;

    public Transform happyFace;
    public Transform sadFace;
    public Transform closedEyes;

    AudioSource audio;
    public AudioSource cellAudioSource;

     public AudioClip cellBuzz;
     public AudioClip swish;

	// Use this for initialization
	void Start ()
	{
	    audio = GetComponent<AudioSource>();
	}

    private IEnumerator CellphoneBuzz()
    {
        float timer = 0;

        cellPhoneAlert.gameObject.SetActive(true);
        timer = 0;

        cellAudioSource.PlayOneShot(cellBuzz);
        while (timer < 1.5f)
        {
            timer += Time.deltaTime*1.5f;
            cellPhone.rotation = Quaternion.Euler(0, 0, Random.Range(-1f, 1)*2);
            yield return null;
        }
        cellPhoneAlert.gameObject.SetActive(false);

        sadFace.gameObject.SetActive(true);
        closedEyes.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.7f);

     

        cellPhoneAlert.gameObject.SetActive(true);
        timer = 0;
        cellAudioSource.PlayOneShot(cellBuzz);
        while (timer < 1.5f)
        {
            timer += Time.deltaTime*1.5f;
            cellPhone.rotation = Quaternion.Euler(0, 0, Random.Range(-1f, 1)*2);
            yield return null;
        }
        cellPhoneAlert.gameObject.SetActive(false);

        happyFace.gameObject.SetActive(true);
        sadFace.gameObject.SetActive(false);

        audio.PlayOneShot(swish);
        yield return new WaitForSeconds(2f);

        SceneController.ChangeScene(SceneEnum.TitleScreen);

        yield return null;
    }

    // Update is called once per frame

    private bool triggered = false;

    private void Update()
    {

        //if(IntroVideo.instance.finished == false)    
        //    return;

        if (AnyInputDown && triggered == false)
        {


            StartCoroutine(CellphoneBuzz());
            triggered = true;
        }
    }
}

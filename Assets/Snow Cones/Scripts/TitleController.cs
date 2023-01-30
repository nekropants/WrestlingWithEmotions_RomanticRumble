using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour
{

    public Transform Cone;
    public Transform Snow;

    public AudioClip intro1;
    public AudioClip intro2;
    AudioSource audio;

	// Use this for initialization
	IEnumerator Start ()
	{

        audio = GetComponent<AudioSource>();


        Cone.gameObject.SetActive(false);
        Snow.gameObject.SetActive(false);

        AudioControllerShit.Play(intro1);
        Snow.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Snow.gameObject.SetActive(false);


        AudioControllerShit.Play(intro2);
        Cone.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Cone.gameObject.SetActive(false);



        SceneController.ChangeScene(SceneEnum.CellPhoneIntro);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

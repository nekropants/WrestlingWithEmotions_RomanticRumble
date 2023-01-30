using UnityEngine;
using System.Collections;

public class HumanApproachController : MonoBehaviour
{


    public AudioClip audio1;
    public AudioClip audio2;
    public AudioClip audio3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayFootStep1()
    {
        print("PlayFootStep1");
        AudioController.Play(audio1, 0.5f);
    }


    public void PlayFootStep2()
    {
        print("PlayFootStep2");
        AudioController.Play(audio2, 0.5f);

    }

    public void PlayFootStep3()
    {
        AudioController.Play(audio3, 0.5f);
        
    }
    


    public void ChangeScene()
    {
        SceneController.ChangeScene(SceneEnum.HumanFeeding);
    }

}

using System;
using UnityEngine;
using System.Collections;

public class FadeToBlack : Singleton<FadeToBlack>
{
    private SpriteRenderer sprite;
	// Use this for initialization
    public static void Fade(float FadeSpeed)
    {
        Instance.StartCoroutine(Instance.FadeRoutine());
    }


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }


    IEnumerator FadeRoutine()
    {

        transform.position = SceneMngr.currentScene.SceneCamera.transform.position - Vector3.forward;
        sprite.enabled = true;
        float fade = 0;

        //while (fade < 1)
        //{
        //    fade += Time.deltaTime;
        //}
        yield return null;
    }

    public static void Hide()
    {
        Instance.sprite.enabled = false;

    }

    // Update is called once per frame
	void Update () {
	
	}
}

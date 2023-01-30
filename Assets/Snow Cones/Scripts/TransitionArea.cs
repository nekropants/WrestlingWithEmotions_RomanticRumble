using UnityEngine;
using System.Collections;

public class TransitionArea : MonoBehaviour {



    public SceneEnum nextScene = SceneEnum.None;
    public bool disableOnTrigger = true;
 
    SceneMngr scene;
    public GameObject[] objectsToSetActiveUponTransition;
    public MonoBehaviour[] behavioursToEnable;


	// Use this for initialization
	void Start () {
        scene = this.GetScene();
	}
	
	// Update is called once per frame
    private void Update()
    {
        if (disableOnTrigger == false)
            return;
        //if (GetComponent<Collider2D>().OverlapPoint(scene.player.transform.position))
        //{
        //    gameObject.SetActive(false);

        //    SceneController.ChangeScene(nextScene);

        //    foreach (GameObject go in objectsToSetActiveUponTransition)
        //    {
        //        go.SetActive(true);
        //    }

        //    foreach (MonoBehaviour mono in behavioursToEnable)
        //    {
        //        mono.enabled = true;
        //    }
        //}
    }
}

using UnityEngine;
using System.Collections;

public class SceneMngr : MonoBehaviour {

    private Camera sceneCamera = null;

    public static SceneMngr currentScene = null;

    public AudioClip song;

    public bool CheckForClicks = true;

    void OnEnable()
    {
        currentScene = this;
    }

    private void OnDisable()
    {
        print("Disable " + gameObject);
    }

    public Camera SceneCamera
    {
        get
        {
            if (sceneCamera == null)
                sceneCamera = GetComponentInChildren<Camera>();

            return sceneCamera;
        }
    }


    public void OnSceneOpen()
    {
        if(song != null)
            AudioControllerShit.PlaySong(song);
    }

    // Use this for initialization
	void Awake () 
    {
         //SceneCamera.cullingMask &=  ~(1 << LayerMask.NameToLayer("DontRender"));
        SceneCamera.cullingMask ^= 1 << LayerMask.NameToLayer("DontRender");

}

	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.BackQuote))
            SceneCamera.cullingMask ^= 1 << LayerMask.NameToLayer("DontRender");

        
	}

    public static TransitionArea[] TransitionAreas
    {
        get
        {
            return currentScene.GetComponentsInChildren<TransitionArea>();
        }
    }

}


public static class Exensions
{
    public static SceneMngr GetScene(this MonoBehaviour mono)
    {
        return GetComponentInHeirarchy<SceneMngr>(mono);
    }



    public static T GetComponentInHeirarchy<T>(this MonoBehaviour mono) where T: Component
    {
        Transform root = mono.transform;
        while (root.parent != null)
            root = root.parent;

        return root.GetComponentInChildren<T>();
    }


    public static Vector3 Round(this Vector3 v) 
    {
        v.x = Mathf.Round(v.x);
        v.y = Mathf.Round(v.y);
        v.z = Mathf.Round(v.z);

        return v;
    }


    public static Vector2 Round(this Vector2 v)
    {
        v.x = Mathf.Round(v.x);
        v.y = Mathf.Round(v.y);

        return v;
    }
}
using UnityEngine;
using System.Collections;

public class MonoBase : MonoBehaviour {


    public virtual bool LeftUp
    {
        get { return Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A); }
    }

    public virtual bool RightUp
    {
        get { return Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D); }
    }
    public virtual bool Left
    {
        get { return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A); }
    }

    public virtual bool Right
    {
        get { return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D); }
    }
    public virtual bool Up
    {
        get { return Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W); }
    }

    public virtual bool Down
    {
        get { return Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S); }
    }
    public virtual bool Touch
    {
        get { return Input.GetKey(KeyCode.Mouse0); }
    }
    public virtual bool TouchDown
    {
        get { return Input.GetKeyDown(KeyCode.Mouse0); }
    }


    public virtual bool LeftDown
    {
        get { return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A); }
    }

    public virtual bool RightDown
    {
        get { return Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D); }
    }
    public virtual bool UpDown
    {
        get { return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W); }
    }

    public virtual bool DownDown
    {
        get { return Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S); }
    }

    public virtual bool ActionDown
    {
        get { return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.G); }
    }


    public virtual bool AnyInputDown
    {
           get { return LeftDown || RightDown || UpDown || DownDown || ActionDown || TouchDown; }

    }




    private SceneMngr scene_;
    public SceneMngr scene {
        get {
            if (scene_ == null)
                scene_ = GetComponent<SceneMngr>();

            if (scene_ == null)
                scene_ = GetComponentInParent<SceneMngr>();
            return scene_; }
    }



    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

namespace Dance { 
 public class SnapShot : MonoBehaviour {

	Texture2D texture;
	public bool snap = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Space))
		{
			
			snap =true;
			
		}
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(100, 100, 100, 100),texture );
	}
	void OnPostRender()
	{
		if(snap)
		{
		Vector3 screenPos = Camera.main.WorldToScreenPoint(Player.instance.transform.position );
			snap = false;

			screenPos.y = 0;

			int h = (int)(Screen.height);
			int w = h;
			texture = new Texture2D(w, h);
			texture.ReadPixels(new Rect(screenPos.x - w/2, screenPos.y, w, h), 0, 0, false);
			texture.Apply();
		}
	}
}

}
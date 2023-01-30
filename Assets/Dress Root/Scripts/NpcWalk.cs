using UnityEngine;
using System.Collections;

namespace Dance { 
 public class NpcWalk : MonoBehaviour {


	float timer = 0;
	public float amplitude =0.3f;
	public float frequency =5f;

	public float speed = 24;

	Vector3 offset;

	public bool walk = false;

	public Joint waddle;

	public Rigidbody reparent;
	// Use this for initialization
	void Start () {
	
	}

	public void StartWalk()
	{
		walk = true;
		if(reparent)
		{
			reparent.isKinematic = true;
			reparent.transform.parent = transform;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		if(waddle)
		{
			waddle.enabled = walk;
		}
		if(walk == false )
		{
			return ;
		}

		timer += Time.deltaTime*frequency;

		transform.position -= offset;

		transform.position += Vector3.right*Time.deltaTime*speed;
		offset = Mathf.Abs( Mathf.Sin( timer))*amplitude*Vector3.up;

		transform.position += offset;




	}
}

}
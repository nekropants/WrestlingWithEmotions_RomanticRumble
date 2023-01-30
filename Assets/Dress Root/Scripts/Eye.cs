using UnityEngine;
using System.Collections;

namespace Dance { 
 public class Eye : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 target;

    public bool smileEyes = false;


    public Transform followThis;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

        if(followThis )
        {
            LookAt(followThis.transform.position);
        }

        if (smileEyes)
            target = Vector3.up;

            transform.localPosition -= offset;



	    offset = Vector3.Lerp(offset, target*0.08f, Time.deltaTime*20);

        transform.localPosition += offset;
    }

    public void LookAt( Vector3 pos )
    {
            target = pos - transform.position;
        target.z = 0;
        target.Normalize();
      //  target.x = Mathf.Clamp(target.x, -0.3f, 0.3f);
    }
    public void Clear(  )

{
    target.x = target.y = 0;   
}

}

}
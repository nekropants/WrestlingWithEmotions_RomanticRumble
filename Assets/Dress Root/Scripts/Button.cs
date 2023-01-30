using UnityEngine;
using System.Collections;


namespace Dance { 
[ExecuteInEditMode]
 public class Button : MonoBehaviour
{
    private HingeJoint hinge;
    private Rigidbody rBody;
    public TextMesh text;

    public float forceM = 1;

    public KeyCode key = KeyCode.A;

    public bool flip = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        text = GetComponentInChildren<TextMesh>();
        hinge = GetComponentInParent<HingeJoint>();
        rBody = GetComponentInParent<Rigidbody>();

        Vector3 anchor = hinge.transform.TransformPoint(hinge.anchor);
        transform.position = anchor;
        text.transform.rotation = Quaternion.identity;

        Vector3 limbEnd = hinge.transform.TransformPoint(-hinge.anchor);
        text.text = "" + key;
        hinge.useSpring = true;

        float force = forceM;

        if (flip)
            force = -force;


        if (Input.GetKey(key))
        {
            hinge.useSpring = false;

            rBody.AddForceAtPosition(-transform.right*Time.deltaTime* force, limbEnd);
            Debug.DrawRay(limbEnd, -transform.right * 4);
        }
    }
}

}
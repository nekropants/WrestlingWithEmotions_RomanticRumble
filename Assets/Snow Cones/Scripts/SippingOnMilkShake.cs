using UnityEngine;
using System.Collections;

public class SippingOnMilkShake : MonoBehaviour
{
    public int direction = 1;

    public SpriteSM face;
	// Use this for initialization
	void Start ()
	{
	}

    private Vector3 offset = Vector3.zero;
    private Quaternion rotOffset = Quaternion.identity;

	// Update is called once per frame
	void Update ()
	{

        transform.position -= offset;
	    float loopLenth = suckRate*6;
        float x = Time.time / loopLenth;
        float sin = Mathf.Sin(Mathf.PI * 2 * x);
        offset = Vector3.up * sin * 3;
        
        transform.position += offset;
	    print(sin + "  " + frame);

        transform.rotation *= Quaternion.Inverse(rotOffset);

        rotOffset = Quaternion.AngleAxis(sin * 0.5f * direction, Vector3.forward);
        transform.rotation *= rotOffset;
	    suckTimer += Time.deltaTime;

	    if (suckTimer >= suckRate)
	    {
	        suckTimer -= suckRate;
            frame++;
            frame %= 6;

            face.SetLowerLeftPixel_Y((frame + 1) * face.height);
	    }
	}

    private float suckTimer = 0;
    int frame = 0;
    private float suckRate = 0.13f;

}

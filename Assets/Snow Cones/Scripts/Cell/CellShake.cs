
using UnityEngine;
using System.Collections;

public class CellShake : Singleton<CellShake>
{
    private Vector3 offset = Vector3.zero;


	// Use this for initialization
    public static  void Shake()
	{
        Instance.transform.position -= Instance.offset;
        Instance.offset = Vector3.right * Random.Range(10, 20);
        Instance.transform.position += Instance.offset;
    }

    private void Start()
    {
    }

    // Update is called once per frame
	void Update ()
	{
	    transform.position -= offset;
        offset = Vector3.Lerp(offset, Vector3.zero, Time.deltaTime * 10);
        transform.position += offset;
    }
}

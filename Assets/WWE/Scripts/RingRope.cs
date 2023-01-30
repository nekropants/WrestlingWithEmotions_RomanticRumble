using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRope : MonoBehaviour
{

    public Transform anchor1;
    public Transform anchor2;
    public Transform midl;

    private LineRenderer line;
    public BoxCollider2D area;

    public int direction = -1;
    public Vector3 offset = new Vector3(1, 1, 0);

    // Use this for initialization
    void Start () {
		
	}


    public bool wasPulled = false;
    public float springAmp = 1;
    public float springFreq = 1;
    public int wobbles = 4;
    private float t = 0;
    private Vector3 wobbleOffset;

    private Vector3 actualMid;
    // Update is called once per frame
    void Update ()
	{

	    Vector3 mid = anchor1.transform.position + anchor2.transform.position;
        mid /=2;


        Vector3 pos = ArenaWrestler.player.transform.position + offset;
        pos.z = area.transform.position.z;
        if (area.bounds.Contains(pos))
        {
            print("---");
            mid = pos;
            wasPulled = true;
            ArenaWrestler.player.againstRope = true;
            ArenaWrestler.player.ropeDirection = direction;
            
        }
        else
        {


            if (wasPulled)
            {
                t = 1;
            }
            wasPulled = false;
        }

        midl.transform.position = Vector3.Lerp(midl.transform.position, mid, Time.deltaTime*5);
	    Refresh();


	    midl.transform.position -= wobbleOffset;

        if (t > 0)
	    {
	        t -= Time.deltaTime*springFreq;
	        t = Mathf.Max(0, t);

            wobbleOffset = Vector3.right*Mathf.Sin(t*wobbles*Mathf.PI)*springAmp*t;

        }
        else
        {
            wobbleOffset = Vector3.zero;
        }

        midl.transform.position += wobbleOffset;

    }


    void Refresh()
    {



        if (!line)
            line = GetComponent<LineRenderer>();

        line.positionCount = 3;
        line.SetPositions(new Vector3[3 ] {anchor1.transform.position, midl.transform.position , anchor2.transform.position });
    }
    void OnDrawGizmos()
    {
        Refresh();
    }
}

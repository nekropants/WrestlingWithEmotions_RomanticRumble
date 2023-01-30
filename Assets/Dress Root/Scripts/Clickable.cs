using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

//[RequireComponent(typeof(Collider2D ))]
namespace Dance { 
 public class Clickable : MonoBehaviour
{
    public List<DanceBase> moves;
    public int startingMove = 0;
    public int moveIndex = 0;


    public bool isHips = false;

    public bool allowStartingMove = false;
    private bool over = false;
    private bool flickering = false;
    private SpriteRenderer renderer;

    public bool setMoveOnstart = false;

    public Player player;
    private PolygonCollider2D collider;

    public void AddLameComps()
    {
        allowStartingMove = true;
        startingMove = 0;
        AddJoint(gameObject, 1, 1);
        AddJoint(gameObject, 2, 15);
        if (isHips == false)
            AddJoint(gameObject, 6, 8);
        moves.Reverse();
    }

    public void AddRadComps()
    {
        allowStartingMove = true;
        startingMove = 0;
        AddJoint(gameObject, 1, 1);
        AddJoint(gameObject, 2, 45); 

        if (isHips)
        {
            
            AddJoint(gameObject, 0.5f, 25);
            AddJoint(gameObject, 2, 25);
        }
        else
        {
            AddJoint(gameObject, 1, 90);
            AddSpin(gameObject, 190);

        }
        //   AddSpin(gameObject, -200);

        foreach (DanceBase danceBase in moves)
        {
            danceBase.Randomize(0.1f);
        }
    }

    void AddJoint(GameObject go, float f, float a)
    {
        Joint j = go.AddComponent<Joint>();
        j.frequency = f;
        j.amplitude = a;
        j.randomize = false;
        j.flip = Random.value > 0.5f;
        moves.Add(j);

        SetMove(startingMove);
    }

    void AddSpin(GameObject go, float f)
    {
        Spin j = go.AddComponent<Spin>();
        j.frequency = f;
        j.randomize = false;
        j.flip = Random.value > 0.5f;
        moves.Add(j);
        SetMove(startingMove);
    }


    // Use this for initialization
    void Start ()
	{

        player = GetComponentInParent<Player>();

        if(moves.Count == 0)
	        moves = new List<DanceBase>( GetComponents<DanceBase>() );

	    renderer = GetComponent<SpriteRenderer>();

        moves.Reverse();

        if(setMoveOnstart)
            SetMove(startingMove);
        collider = GetComponent<PolygonCollider2D>();
	}

    void SetMove(int index)
    {

        moveIndex = index%moves.Count;

        for (int i = 0; i < moves.Count; i++)
        {
            if (moves[i])
            moves[i].enabled = i == moveIndex;


        }



    }
    public bool Interactable()
{
       return (player == null || player.isClickable);

}


    // Update is called once per frame
    void Update ()
    {

     //   collider.enabled = Interactable();
        if (flickering == false)
        {
            if (over)
            {
                renderer.color = Color.green;

            }
            else
            {
                renderer.color = Color.white;

            }
        }

        if( Interactable() == false)
        {
                renderer.color = Color.white;
           // SetMove(startingMove);
        }
    }



    public void OnPointerClick()
    {

        if(Interactable() ==false)
        {
            return;
        }
        moveIndex++;
        moveIndex = moveIndex % moves.Count;

        if (moveIndex == startingMove && allowStartingMove == false)
            moveIndex++;

        moveIndex = moveIndex % moves.Count;

        SetMove(moveIndex);

        StartCoroutine(Flicker());

        DanceEvaluator.clicks++;


    }


    IEnumerator Pulse()
    {

        flickering = true;
            renderer.color = Color.green;

            yield return new WaitForSeconds(0.05f);

        flickering = false;
    }

    public void ForcePulse()
    {
        StartCoroutine(Pulse());
    }

    public void ForceFlicker()
    {

        print("ForceFlicker");
        StartCoroutine(Flicker());
    }

    public void OnPointerEnter()
    {
        over = true;
        {

        }
       // throw new System.NotImplementedException();
    }

    public void OnPointerExit()
    {
        over = false;



        // throw new System.NotImplementedException();
    }


    IEnumerator Flicker()
    {

        flickering = true;


        for (int i = 0; i < 3; i++)
        {
            renderer.color = Color.white;

            yield return new WaitForSeconds(0.1f);

            renderer.color = Color.green;
            yield return new WaitForSeconds(0.1f);

        }


        flickering = false;
    }
}

}
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MapMarker : MonoBase
{

    public SpriteRenderer name;
    public bool highlighted = false;
    public bool setHighlighted = false;

    public List<MapMarker> leftMarkers = new List<MapMarker>();
    public List<MapMarker> rightMarkers = new List<MapMarker>();
    public MapMarker upMarker;
    public MapMarker downMarker;

    public Color openColor;
    public Color closedColor;

    public bool open = false;
    public bool visited = false;

    public Transform avatarPos;

    public SceneEnum sceneToOpen = SceneEnum.None;

    public List<MapMarker> toUnlock = new List<MapMarker>();

    public bool isHill = false;

	// Use this for initialization
	void Start ()
	{
        openColor = name.color;
	    avatarPos.gameObject.SetActive(false);

	    if (isHill)
	        MapController.Instance.hill = this;
	}
	
	// Update is called once per frame
    private void Update()
    {

        if (MapController.Instance.openCount > 3 && visited == false)
        {
            MapController.Instance.goToHill = true; 
            open = true;
        }
        if (open)
        {
            name.color = openColor;
        }
        else
        {
            name.color = closedColor;
        }

        if (highlighted)
        {

          
            MapMarker newMarker = null;
            if (LeftDown)
            {
                foreach (MapMarker leftMarker in leftMarkers)
                {
                    if (leftMarker != null && leftMarker.open)
                    {
                        newMarker = leftMarker;
                        break;
                    }
                }

            }

            if (RightDown)
            {
                foreach (MapMarker rightMarker in rightMarkers)
                {
                    if (rightMarker != null && rightMarker.open)
                    {
                        newMarker = rightMarker;
                        break;

                    }
                }
            }
            if (DownDown)
            {
                newMarker = downMarker;
            }
            if (UpDown)
            {
                newMarker = upMarker;
             
            }

            if (MapController.Instance.goToHill)
            {

                if (UpDown || DownDown || RightDown || LeftDown)
                 newMarker = MapController.Instance.hill;

            }

            if (newMarker != null && newMarker.open )
            {
                newMarker.setHighlighted = true;
                highlighted = false;
            }

            if (ActionDown && visited == false && open)
            {
                Load();
            }


            Vector3 newPos = Vector3.MoveTowards(MapConesAvatars.Instance.transform.position, avatarPos.transform.position, Time.deltaTime * 200);
            MapConesAvatars.Instance.transform.SetXY(newPos);


            RunHighlight();
        }

        if (setHighlighted)
        {
         
            setHighlighted = false;

            if (current)
                current.highlighted = false;
            current = this;
            highlighted = true;

        }

    }

    void Load()
    {
        visited = true;
        open = false;

        foreach (MapMarker mapMarker in toUnlock)
        {
            OpenMarker(mapMarker);
        }

        MapController.Instance.openCount++;

        SceneController.ChangeScene(sceneToOpen);
    }

    public void TouchHighlighted()
    {

        if (open)
        {
            setHighlighted = true;

            if(highlighted && visited == false)
            {
                Load();
            }

        }
    }

    private void OpenMarker(MapMarker marker)
    {
        if (marker && marker.visited == false)
        {
            marker.open = true;
        }
    }

    public static MapMarker current;



    public float frequency = 2f;
    public float amplitude = 5;
    private float timer = 0;
    private void RunHighlight()
    {
        timer += Time.deltaTime;

        if (open == false)
            timer = 0;

        float angle = Mathf.Sin(timer*frequency)*amplitude;
        name.transform.eulerAngles = new Vector3(0, 0, angle);
    }

}

using UnityEngine;
using System.Collections;
using WWE;

public class Fisto : MonoBehaviour {


    public static Fisto instance;
    
    public SpriteRenderer impact;
    public GameObject crackPrefab;
    
    public Sprite exitArrow;
    public Sprite fistSprite;
    SpriteRenderer fist;
    
    public bool smashedMirror = false;
	// Use this for initialization
    public AudioClip mirrorSmashSound;


    void Awake()
    {
        instance = this;
    }

    public Camera cam;
    void Start ()
    {

        cam = GetComponentInParent<Camera>();
        
        impact.enabled = false;
        
        crackPrefab.gameObject.SetActive(false);
	    
        fist = GetComponent<SpriteRenderer>();
	}
    
    Vector3 offset;

    private int punchCount = 0;
    
    bool punch = false;
    bool exit = false;
    // Update is called once per frame
    void Update () {
        
        Vector3 world = cam.ScreenToWorldPoint(Input.mousePosition);
        
        world.z = transform.position.z;
	    transform.position = world;
        

       
        Vector3 view = cam.ScreenToViewportPoint(Input.mousePosition);

        fist.transform.localScale = Vector3.one;
        exit = false;
        punch = false;

        float deadSpace = 0.00f;
        float min = 0.2f;
        float max = 0.75f;
        float minY = 0.3f;

        if (view.x > min && view.x < max && punchCount < 3)
        {
            if (view.y > minY)
            {
                punch = true;
            }
        }
        else if (view.x < min - deadSpace || view.x > max + deadSpace)
	    {

	        exit = true;

	        if (view.x > max + deadSpace)
	        {
	            fist.transform.localScale = new Vector3(-1, 1, 1);
	        }
	    }
      

	    transform.position -= offset;
        if(exit)
        {
            fist.enabled = true;
            fist.sprite = exitArrow;
            offset =  transform.right*Mathf.Sin(Time.time*8)*2;
        }
        else if (punch)
        {
            fist.enabled = true;
            fist.sprite = fistSprite;
            offset = Vector3.zero;
        }
        else
        {
            fist.enabled = false;
        }
        
        transform.position += offset;
  
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnMouseDown();
        }
        
        if(punchTimer > 0)
        {
            punchTimer -= Time.deltaTime*10;
            transform.localScale = Vector3.one*( 1 + (1 - punchTimer))/2f;
        }
	}
    
    float punchTimer = 0;
    
    void OnMouseDown()
    {
        
        if(exit)
        {
            DressWrestler.Leave();

            gameObject.SetActive(false);
        }
        else if (punch)
        {
            punchCount++;
            print("On Mouse Down");
            impact.enabled = true;

            DressWrestler.instance.PunchMirror();

            AudioController.Play(mirrorSmashSound, 1, Random.Range(0.9f, 1.1f));
            GameObject crack = Instantiate(crackPrefab);
            crack.gameObject.SetActive(true);
            Vector3 pos = transform.position;
            pos.z = crack.transform.position.z;
            crack.transform.position = pos;

            Vector3 dir = crack.transform.position;

            float angle = Angle360(dir.normalized);

            crack.transform.rotation = Quaternion.Euler(0, 0, angle);

            smashedMirror = true;
            crack.transform.parent = crackPrefab.transform.parent;

            punchTimer = 1;

            Invoke("HideImpact", 0.2f);

        }
    }
    
    void HideImpact()
    {
        impact.enabled = false;
    }
    
    
    float Angle360(Vector3 v2)
{
    
    Vector3 v1 = Vector3.up;
    //  Acute angle [0,180]
    float angle = Vector3.Angle(v1,v2);

    //  -Acute angle [180,-179]
    float sign = Mathf.Sign(Vector3.Dot(Vector3.forward, Vector3.Cross(v1, v2)));
    float signed_angle = angle * sign;

    //  360 angle
    return (signed_angle + 180) % 360;
}
}

using UnityEngine;
using System.Collections;

public class EyeController : MonoBehaviour {

	public Transform arms;
	public Transform freak;
	public Transform dweeb;
	public Transform king;
	public Transform piggy;
	public Transform ray;
	public Transform senor;
	public Transform bear;
	public Transform stoney;
	public Transform sunshine;
	public Transform jake;
	
	// Use this for initialization
	void Start () {
		
		
    }
	
	bool first = true;
	
	// Update is called once per frame
	void Update () {
		
		if(first == false )
		return;
		
		first = false;
		arms.gameObject.SetActive(false);
		freak.gameObject.SetActive(false);
		dweeb.gameObject.SetActive(false);
		king.gameObject.SetActive(false);
		piggy.gameObject.SetActive(false);
		ray.gameObject.SetActive(false);
		senor.gameObject.SetActive(false);
		bear.gameObject.SetActive(false);
			stoney.gameObject.SetActive(false);
		sunshine.gameObject.SetActive(false);
		jake.gameObject.SetActive(false);

        switch (WWE.SceneController.winner)
        {
            case Characters.None:
                break;
            case Characters.Arms:
		arms.gameObject.SetActive(true);
			
                break;
            case Characters.Piggy:
		piggy.gameObject.SetActive(true);
                break;
            case Characters.PrettyGuy:
		king.gameObject.SetActive(true);
                break;
            case Characters.Ray:
		ray.gameObject.SetActive(true);
                break;
            case Characters.FreakShow:
		freak.gameObject.SetActive(true);
                break;
            case Characters.Dweeb:
		dweeb.gameObject.SetActive(true);
                break;
            case Characters.SenorMurder:
		senor.gameObject.SetActive(true);
                break;
            case Characters.Bear:
		bear.gameObject.SetActive(true);
                break;
				 case Characters.Stoney:
		stoney.gameObject.SetActive(true);
                break;
            case Characters.SenorSunshine:
		sunshine.gameObject.SetActive(true);
                break;
            case Characters.JakeTheGerbil:
		jake.gameObject.SetActive(true);
                break;
        }
	}
	
	
	
}

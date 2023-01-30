using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Descriptors : MonoBehaviour {


	public static string [] randomDescriptors = {"Handsome", "Pretty", "Hardcore", "Lame", "Droll", "Beautiful", "Heart pulpitating", "WTF...", "Smells Good", "Shiny",
	"Thoughtful", "Eloquent", "Uhh...","Nice Eyes", "Good Biceps", "Trite", "Funny", "Gentle", "Rough Rider", "XFactor", "Clammy", "Self Assured", "Charming", "Kid Friendly"};
	
	public static int RandomSeed = 0;
	
	public int seed;
	
	public void Refresh()
	{
		Random.seed = seed + RandomSeed;
		
		List<string> qualities = new List<string>(randomDescriptors);
		Text[] texts = GetComponentsInChildren<Text>();
		foreach (var txt in texts)
		{
			int index = Random.Range(0, qualities.Count);
			txt.text = qualities[index];
			qualities.RemoveAt(index);
		}
		
		
		
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

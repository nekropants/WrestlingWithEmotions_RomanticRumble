using UnityEngine;
using System.Collections;

namespace Dance { 
 public class Voice : MonoBehaviour {

	public AudioClip[] audio;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Vocalize(float duration)
	{
		StartCoroutine(VocalizeRoutine(duration));
	}


	IEnumerator VocalizeRoutine(float duration)
	{

		if(audio.Length == 0)
			yield break;
		yield return null;
		AudioSource audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = audio[Random.Range(0, audio.Length)];
		audioSource.pitch = Random.Range(0.9f, 1.1f);
		duration -= audioSource.clip.length;
		audioSource.volume = Random.Range(0.5f, 0.7f);
		//audioSource.loop = true;
		//audioSource.Play();
		yield return new WaitForSeconds(audioSource.clip.length-0.05f);

		if(duration > 0)
		{
		StartCoroutine(VocalizeRoutine(duration));
			
		}

	}
}

}
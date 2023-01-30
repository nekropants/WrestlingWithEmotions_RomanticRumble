//-----------------------------------------------------------------
//	SpriteAnimationPump v1.0 RC4 (11-5-2009)
//  Copyright 2009 Brady Wright and Above and Beyond Software
//	All rights reserved
//-----------------------------------------------------------------


using UnityEngine;
using System.Collections;


/// <remarks>
/// Drives all sprite animation using a coroutine.
/// A SpriteAnimationPump instance will be automatically 
/// created by the first sprite created in the scene.
/// </remarks>
public class SpriteAnimationPump : MonoBehaviour
{
	static SpriteAnimationPump instance = null;

	// Array of sprites that are currently animating:
	protected static ArrayList animList = new ArrayList();

	/// <summary>
	/// Indicates whether the animation pump is
	/// currently running. The pump can also be
	/// turned off by setting this value to false.
	/// </summary>
	public static bool pumpIsRunning = false;

	/// <summary>
	/// The interval between animation coroutine updates.
	/// Defaults to 0.03333f (30 frames per second).
	/// </summary>
	public static float animationPumpInterval = 0.03333f;


	void Awake()
	{
		DontDestroyOnLoad(this);
	}


	/// <summary>
	/// Starts the animation pump coroutine.
	/// </summary>
	public void StartAnimationPump()
	{
		if(!pumpIsRunning)
			StartCoroutine(AnimationPump());
	}

/*
	void Update()
	{
		int i;

		pumpIsRunning = true;

		for (i = 0; i < animList.Count; ++i)
		{
			((SpriteBase)animList[i]).StepAnim(Time.deltaTime);
		}
	}
*/

	// The coroutine that drives animation:
	protected static IEnumerator AnimationPump()
	{
		int i;
		float startTime = Time.realtimeSinceStartup;
		float time;
		float elapsed;

		pumpIsRunning = true;

		while (pumpIsRunning)
		{
			yield return new WaitForSeconds(animationPumpInterval);

			time = Time.realtimeSinceStartup;
			elapsed = time - startTime;
			startTime = time;

			for (i = 0; i < animList.Count; ++i)
			{
				((SpriteBase)animList[i]).StepAnim(elapsed);
			}
		}
	}

	public static SpriteAnimationPump Instance
	{
		get 
		{
			if(instance == null)
			{
				GameObject go = new GameObject("SpriteAnimationPump");
				instance = (SpriteAnimationPump)go.AddComponent(typeof(SpriteAnimationPump));
			}

			return instance;
		}
	}

	/// <summary>
	/// Adds the specified sprite to the animation list.
	/// </summary>
	/// <param name="s">A reference to the sprite to be animated.</param>
	public static void Add(SpriteBase s)
	{
		animList.Add(s);
	}

	/// <summary>
	/// Removes the specified sprite from the animation list,
	/// thereby not receiving animation updates.
	/// </summary>
	/// <param name="s">A reference to the sprite to be removed.</param>
	public static void Remove(SpriteBase s)
	{
		animList.Remove(s);
	}
}
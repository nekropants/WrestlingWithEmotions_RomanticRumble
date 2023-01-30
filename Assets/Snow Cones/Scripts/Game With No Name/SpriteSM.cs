//-----------------------------------------------------------------
//	Sprite v1.0 RC5 (11-9-2009)
//  Copyright 2009 Brady Wright and Above and Beyond Software
//	All rights reserved
//-----------------------------------------------------------------


using UnityEngine;
using System.Collections;


/// <remarks>
/// Implements SpriteBase and adds certain animation functionality
/// specific to this type of sprite.
/// </remarks>
[ExecuteInEditMode]
public class SpriteSM : SpriteBase
{
	/// <summary>
	/// Position of the lower-left pixel of the sprite when
	/// no animation has been played.
	/// </summary>
	public Vector2 lowerLeftPixel;				// Position of the lower-left pixel of the sprite

	/// <summary>
	/// Dimensions, in pixels, of the sprite when no animation
	/// has been played.
	/// </summary>
	public Vector2 pixelDimensions;				// Dimensions, in pixels, of the sprite

 
	// Animation-related vars and types:

	/// <summary>
	/// Array of available animation sequences.
	/// This is typically built in-editor.
	/// </summary>
	public UVAnimation_Multi[] animations;				// Array of available animations
	protected UVAnimation_Multi curAnim = null;			// The current animation


    public bool refreshMeshNow = false;
	// Members related to drawing the sprite in edit mode:
	SpriteMirror mirror = null;


    public bool  lockWidthAndHeight = false;
    public bool round = false;
    public int roundInterval = 1;

	protected override void Awake()
	{
        createNormals = true;

		base.Awake();

		if (animations == null)
			animations = new UVAnimation_Multi[0];

		for (i = 0; i < animations.Length; ++i)
			animations[i].BuildUVAnim(this);

		base.Init();

     
	}


    protected override void Start()
    {
        base.Start();

        // See if we should play a default animation:
        if (playAnimOnStart && defaultAnim < animations.Length)
            if (Application.isPlaying)
                PlayAnim(defaultAnim);

      
    }


	// Resets all sprite values to defaults for reuse:
	public override void Clear()
	{
		base.Clear();

		if (curAnim != null)
		{
			PauseAnim();
			curAnim = null;
		}
	}

    void Update()
    {
        if (round)
        {
            round = false;

            pixelDimensions.x = Snap(pixelDimensions.x);
            pixelDimensions.y = Snap(pixelDimensions.y);
            SetPixelDimensions(pixelDimensions);

            lowerLeftPixel.x = Snap(lowerLeftPixel.x);
            lowerLeftPixel.y = Snap(lowerLeftPixel.y);
            SetLowerLeftPixel(lowerLeftPixel);

            width = pixelDimensions.x;
            height = pixelDimensions.y; 

            SetSize(width, height);
        }


        if (lockWidthAndHeight )
        {
            //lowerLeftPixel.x = Mathf.Round(lowerLeftPixel.x);
            //lowerLeftPixel.y = Mathf.Round(lowerLeftPixel.y);
            //pixelDimensions.x = Mathf.Round(pixelDimensions.x);
            //pixelDimensions.y = Mathf.Round(pixelDimensions.y);
            SetSize(pixelDimensions.x, pixelDimensions.y);
        }

        if (refreshMeshNow)
        {
            refreshMeshNow = false;
            Awake();
        }
    }

    float Snap(float value)
    {
        value /= roundInterval;
        value = Mathf.Round(value);
        value *= roundInterval;

        return value;
    }

	// Copies all the attributes of another sprite:
	public override void Copy(SpriteBase s)
	{
		// Check the type:
		if ( !(s is SpriteSM) )
			return;

		base.Copy(s);

		lowerLeftPixel = ((SpriteSM)s).lowerLeftPixel;
		pixelDimensions = ((SpriteSM)s).pixelDimensions;

		CalcUVs();

		SetBleedCompensation(s.bleedCompensation);

		if (autoResize || pixelPerfect)
			CalcSize();
		else
			SetSize(s.width, s.height);

		if (((SpriteSM)s).animations.Length > 0)
		{
			animations = new UVAnimation_Multi[((SpriteSM)s).animations.Length];
			((SpriteSM)s).animations.CopyTo(animations, 0);
		}

		for (i = 0; i < animations.Length; ++i)
			animations[i].BuildUVAnim(this);
	}


	// Implements UV calculation
	public override void CalcUVs()
	{
		tempUV = PixelCoordToUVCoord(lowerLeftPixel);
		uvRect.x = tempUV.x;
		uvRect.y = tempUV.y;

		tempUV = PixelSpaceToUVSpace(pixelDimensions);
		uvRect.xMax = uvRect.x + tempUV.x;
		uvRect.yMax = uvRect.y + tempUV.y;
	}


	//-----------------------------------------------------------------
	// Animation-related routines:
	//-----------------------------------------------------------------


	/// <summary>
	/// Adds an animation to the sprite for its use.
	/// </summary>
	/// <param name="anim">The animation to add</param>
	public void AddAnimation(UVAnimation_Multi anim)
	{
		UVAnimation_Multi[] temp;
		temp = animations;

		animations = new UVAnimation_Multi[temp.Length + 1];
		temp.CopyTo(animations, 0);

		animations[animations.Length - 1] = anim;
	}

	/*
		// Steps to the next frame of sprite animation
		public bool StepAnim(float time)
		{
			if (curAnim == null)
				return false;

			timeSinceLastFrame += time;

			framesToAdvance = (int)(timeSinceLastFrame / timeBetweenAnimFrames);

			// If there's nothing to do, return:
			if (framesToAdvance < 1)
				return true;

			while (framesToAdvance > 0)
			{
				if (curAnim.GetNextFrame(ref lowerLeftUV))
					--framesToAdvance;
				else
				{
					// We reached the end of our animation
					if (animCompleteDelegate != null)
						animCompleteDelegate();

					// Update mesh UVs:
					UpdateUVs();
					PauseAnim();
					animating = false;
	 
					return false;
				}
			}

			// Update mesh UVs:
			UpdateUVs();

			timeSinceLastFrame = 0;

			return true;
		}
	*/

	// Steps to the next frame of sprite animation
	public override bool StepAnim(float time)
	{
		if (curAnim == null)
			return false;

		timeSinceLastFrame += Mathf.Max(0, time);
		//timeSinceLastFrame += time;

		framesToAdvance = (int)(timeSinceLastFrame / timeBetweenAnimFrames);

		// If there's nothing to do, return:
		if (framesToAdvance < 1)
			return true;

		//timeSinceLastFrame -= timeBetweenAnimFrames * (float)framesToAdvance;

		while (framesToAdvance > 0)
		{
			if (curAnim.GetNextFrame(ref uvRect))
			{
				--framesToAdvance;
				timeSinceLastFrame -= timeBetweenAnimFrames;
			}
			else
			{
				// We reached the end of our animation

				// See if we should revert to a static appearance,
				// default anim, or do nothing:
				if (curAnim.onAnimEnd == UVAnimation.ANIM_END_ACTION.Do_Nothing)
				{
					PauseAnim();

					// Update mesh UVs:
					SetBleedCompensation();

					// Resize if selected:
					if (autoResize || pixelPerfect)
						CalcSize();
				}
				else if (curAnim.onAnimEnd == UVAnimation.ANIM_END_ACTION.Revert_To_Static)
					RevertToStatic();
				else if (curAnim.onAnimEnd == UVAnimation.ANIM_END_ACTION.Play_Default_Anim)
				{
					// Notify the delegate:
					if (animCompleteDelegate != null)
						animCompleteDelegate();

					// Play the default animation:
					PlayAnim(defaultAnim);

					return false;
				}

				// Notify the delegate:
				if (animCompleteDelegate != null)
					animCompleteDelegate();

				// Check to see if we are still animating
				// before setting the curAnim to null.
				// Animating should be turned off if
				// PauseAnim() was called above, or if we
				// reverted to static.  But it could have
				// been turned on again by the 
				// animCompleteDelegate.
				if (!animating)
					curAnim = null;

				return false;
			}
		}


		// Update mesh UVs:
		SetBleedCompensation();

		UpdateUVs();

		// Resize if selected:
		if (autoResize || pixelPerfect)
			CalcSize();

		//timeSinceLastFrame = 0;

		return true;
	}

/*
	// Steps to the next frame of sprite animation
	public override bool StepAnim()
	{
		if (curAnim == null)
			return false;

		if (!curAnim.GetNextFrame(ref uvRect))
		{
			// We reached the end of our animation

			// See if we should revert to a static appearance,
			// default anim, or do nothing:
			if (curAnim.onAnimEnd == UVAnimation.ANIM_END_ACTION.Do_Nothing)
			{
				PauseAnim();

				// Update mesh UVs:
				SetBleedCompensation();

				// Resize if selected:
				if (autoResize || pixelPerfect)
					CalcSize();
			}
			else if (curAnim.onAnimEnd == UVAnimation.ANIM_END_ACTION.Revert_To_Static)
				RevertToStatic();
			else if (curAnim.onAnimEnd == UVAnimation.ANIM_END_ACTION.Play_Default_Anim)
			{
				// Notify the delegate:
				if (animCompleteDelegate != null)
					animCompleteDelegate();

				// Play the default animation:
				PlayAnim(defaultAnim);

				return false;
			}

			// Notify the delegate:
			if (animCompleteDelegate != null)
				animCompleteDelegate();

			// Check to see if we are still animating
			// before setting the curAnim to null.
			// Animating should be turned off if
			// PauseAnim() was called above, or if we
			// reverted to static.  But it could have
			// been turned on again by the 
			// animCompleteDelegate.
			if (!animating)
				curAnim = null;

			return false;
		}

		// Update mesh UVs:
		SetBleedCompensation();

		UpdateUVs();

		// Resize if selected:
		if (autoResize || pixelPerfect)
			CalcSize();

		return true;
	}*/


	/// <summary>
	/// Starts playing the specified animation
	/// Note: this doesn't resume from a pause,
	/// it completely restarts the animation. To
	/// unpause, use <see cref="UnpauseAnim"/>.
	/// </summary>
	/// <param name="anim">A reference to the animation to play.</param>
	public void PlayAnim(UVAnimation_Multi anim)
	{
		curAnim = anim;
		curAnim.Reset();

		// Ensure the framerate is not 0 so we don't
		// divide by zero:
		anim.framerate = Mathf.Max(0.0001f, anim.framerate);

		timeBetweenAnimFrames = 1f / anim.framerate;
		timeSinceLastFrame = timeBetweenAnimFrames;

		// Only add to the animated list if
		// the animation has more than 1 frame:
		if (anim.GetFrameCount() > 1)
		{
			StepAnim(0);
			// Start coroutine
			if (!animating)
			{
				//animating = true;
				AddToAnimatedList();
				//StartCoroutine(AnimationPump());
			}
		}
		else
		{
			// Since this is a single-frame anim,
			// call our delegate before setting
			// the frame so that our behavior is
			// consistent with multi-frame anims:
			if (animCompleteDelegate != null)
				animCompleteDelegate();

			StepAnim(0);
		}
	}

	/// <summary>
	/// Starts playing the specified animation
	/// Note: this doesn't resume from a pause,
	/// it completely restarts the animation. To
	/// unpause, use <see cref="UnpauseAnim"/>.
	/// </summary>
	/// <param name="index">Index of the animation to play.</param>
	public void PlayAnim(int index)
	{
		if (index >= animations.Length)
		{
			Debug.LogError("ERROR: Animation index " + index + " is out of bounds!");
			return;
		}

		PlayAnim(animations[index]);
	}

	/// <summary>
	/// Starts playing the specified animation
	/// Note: this doesn't resume from a pause,
	/// it completely restarts the animation. To
	/// unpause, use <see cref="UnpauseAnim"/>.
	/// </summary>
	/// <param name="name">The name of the animation to play.</param>
	public void PlayAnim(string name)
	{
		for (int i = 0; i < animations.Length; ++i)
		{
			if (animations[i].name == name)
			{
				PlayAnim(animations[i]);
				return;
			}
		}

		Debug.LogError("ERROR: Animation \"" + name + "\" not found!");
	}

	/// <summary>
	/// Like PlayAnim, but plays the animation in reverse.
	/// See <see cref="PlayAnim"/>.
	/// </summary>
	/// <param name="anim">Reference to the animation to play in reverse.</param>
	public void PlayAnimInReverse(UVAnimation_Multi anim)
	{
		curAnim = anim;
		curAnim.Reset();
		curAnim.PlayInReverse();

		// Ensure the framerate is not 0 so we don't
		// divide by zero:
		anim.framerate = Mathf.Max(0.0001f, anim.framerate);

		timeBetweenAnimFrames = 1f / anim.framerate;
		timeSinceLastFrame = timeBetweenAnimFrames;

		// Only add to the animated list if
		// the animation has more than 1 frame:
		if (anim.GetFrameCount() > 1)
		{
			StepAnim(0);
			// Start coroutine
			if (!animating)
			{
				//animating = true;
				AddToAnimatedList();
				//StartCoroutine(AnimationPump());
			}
		}
		else
		{
			// Since this is a single-frame anim,
			// call our delegate before setting
			// the frame so that our behavior is
			// consistent with multi-frame anims:
			if (animCompleteDelegate != null)
				animCompleteDelegate();

			StepAnim(0);
		}
	}

	/// <summary>
	/// Like PlayAnim, but plays the animation in reverse.
	/// See <see cref="PlayAnim"/>.
	/// </summary>
	/// <param name="index">Index of the animation to play in reverse.</param>
	public void PlayAnimInReverse(int index)
	{
		if (index >= animations.Length)
		{
			Debug.LogError("ERROR: Animation index " + index + " is out of bounds!");
			return;
		}

		PlayAnimInReverse(animations[index]);
	}

	/// <summary>
	/// Like PlayAnim, but plays the animation in reverse.
	/// See <see cref="PlayAnim"/>.
	/// </summary>
	/// <param name="name">Name of the animation to play in reverse.</param>
	public void PlayAnimInReverse(string name)
	{
		for (int i = 0; i < animations.Length; ++i)
		{
			if (animations[i].name == name)
			{
				animations[i].PlayInReverse();
				PlayAnimInReverse(animations[i]);
				return;
			}
		}

		Debug.LogError("ERROR: Animation \"" + name + "\" not found!");
	}

	/// <summary>
	/// Stops the current animation from playing
	/// and resets it to the beginning for playing
	/// again.  The sprite then reverts to the static
	/// (non-animating default) image.
	/// </summary>
	public override void StopAnim()
	{
		// Stop coroutine
		//animating = false;
		RemoveFromAnimatedList();

		//StopAllCoroutines();

		// Reset the animation:
		if(curAnim != null)
			curAnim.Reset();

		// Revert to our static appearance:
		RevertToStatic();
	}

	/// <summary>
	/// Resumes an animation from where it left off previously.
	/// </summary>
	public void UnpauseAnim()
	{
		if (curAnim == null) return;

		// Resume coroutine
		//animating = true;
		AddToAnimatedList();
		//StartCoroutine(AnimationPump());
	}


	//--------------------------------------------------------------
	// Accessors:
	//--------------------------------------------------------------
	/// <summary>
	/// Returns a reference to the currently selected animation.
	/// NOTE: This does not mean the animation is currently playing.
	/// To determine whether the animation is playing, use <see cref="IsAnimating"/>
	/// </summary>
	/// <returns>Reference to the currently selected animation.</returns>
	public UVAnimation_Multi GetCurAnim() { return curAnim; }

	/// <summary>
	/// Returns a reference to the animation that matches the
	/// name specified.
	/// </summary>
	/// <param name="name">Name of the animation sought.</param>
	/// <returns>Reference to the animation, if found, null otherwise.</returns>
	public UVAnimation_Multi GetAnim(string name)
	{
		for (int i = 0; i < animations.Length; ++i)
		{
			if (animations[i].name == name)
			{
				return animations[i];
			}
		}

		return null;
	}

	/// <summary>
	/// Sets the lower-left pixel of the sprite.
	/// See <see cref="lowerLeftPixel"/>
	/// </summary>
	/// <param name="lowerLeft">Pixel coordinate of the lower-left corner of the sprite.</param>
	public void SetLowerLeftPixel(Vector2 lowerLeft)
	{
		lowerLeftPixel = lowerLeft;

		// Calculate UV dimensions:
		tempUV = PixelCoordToUVCoord(lowerLeftPixel);
		uvRect.x = tempUV.x;
		uvRect.y = tempUV.y;

		tempUV = PixelSpaceToUVSpace(pixelDimensions);
		uvRect.xMax = uvRect.x + tempUV.x;
		uvRect.yMax = uvRect.y + tempUV.y;

		// Adjust for bleed compensation:
		SetBleedCompensation(bleedCompensation);

		// Now see if we need to resize:
		if (autoResize || pixelPerfect)
			CalcSize();
	}

   
	/// <summary>
	/// Sets the lower-left pixel of the sprite.
	/// See <see cref="lowerLeftPixel"/>
	/// </summary>
	/// <param name="x">X pixel coordinate.</param>
	/// <param name="y">Y pixel coordinate.</param>
    public void SetLowerLeftPixel(float x, float y)
	{
		SetLowerLeftPixel(new Vector2((float)x, (float)y));
	}

	/// <summary>
	/// Sets the pixel dimensions of the sprite.
	/// See <see cref="pixelDimensions"/>
	/// </summary>
	/// <param name="size">Dimensions of the sprite in pixels.</param>
	public void SetPixelDimensions(Vector2 size)
	{
		pixelDimensions = size;

		tempUV = PixelSpaceToUVSpace(pixelDimensions);
		uvRect.xMax = uvRect.x + tempUV.x;
		uvRect.yMax = uvRect.y + tempUV.y;

		// Adjust for bleed compensation
		// NOTE: We can't call SetBleedCompensation()
		// here because we've only changed the right-hand
		// side, so we have to calculate it ourselves:
		uvRect.xMax -= bleedCompensationUV.x * 2f;
		uvRect.yMax -= bleedCompensationUV.y * 2f;

		// Now see if we need to resize:
		if (autoResize || pixelPerfect)
			CalcSize();
	}

	/// <summary>
	/// Sets the pixel dimensions of the sprite.
	/// See <see cref="pixelDimensions"/>
	/// </summary>
	/// <param name="x">X size in pixels.</param>
	/// <param name="y">Y size in pixels.</param>
	public void SetPixelDimensions(int x, int y)
	{
		SetPixelDimensions(new Vector2((float)x, (float)y));
	}
	



	// Ensures that the sprite is updated in the scene view
	// while editing:
	public void OnDrawGizmos()
	{
		// Only run if we're not playing:
		if (Application.isPlaying)
			return;

		if (mirror == null)
		{
			mirror = new SpriteMirror();
			mirror.Mirror(this);
		}

		mirror.Validate(this);

		// Compare our mirrored settings to the current settings
		// to see if something was changed:
		if (mirror.DidChange(this))
		{
			Init();
			mirror.Mirror(this);	// Update the mirror
		}
	}
}


// Mirrors the editable settings of a sprite that affect
// how the sprite is drawn in the scene view
public class SpriteMirror : SpriteBaseMirror
{
	public Vector2 lowerLeftPixel, pixelDimensions;

	// Mirrors the specified sprite's settings
	public override void Mirror(SpriteBase s)
	{
		base.Mirror(s);

		lowerLeftPixel = ((SpriteSM)s).lowerLeftPixel;
		pixelDimensions = ((SpriteSM)s).pixelDimensions;
	}

	// Returns true if any of the settings do not match:
	public override bool DidChange(SpriteBase s)
	{
		if (base.DidChange(s))
			return true;
		if (((SpriteSM)s).lowerLeftPixel != lowerLeftPixel)
			return true;
		if (((SpriteSM)s).pixelDimensions != pixelDimensions)
			return true;

		return false;
	}
}
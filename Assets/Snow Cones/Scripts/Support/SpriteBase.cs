//-----------------------------------------------------------------
//	SpriteBase v1.0 RC5 (11-9-2009)
//  Copyright 2009 Brady Wright and Above and Beyond Software
//	All rights reserved
//-----------------------------------------------------------------


using UnityEngine;
using System.Collections;


/// <remarks>
/// Describes a UV animation by holding all the information necessary
/// to move the UVs of a sprite across a texture atlas.
/// </remarks>
[System.Serializable]
public class UVAnimation
{
    /// <remarks>
    /// The action to take when an animation ends.
    /// The options are, Do Nothing, Revert To Static, or Play Default Anim.
    /// </remarks>
    public enum ANIM_END_ACTION
    {
        Do_Nothing,			// Do nothing when the animation ends
        Revert_To_Static,	// Revert to the static image when the animation ends
        Play_Default_Anim	// Play the default animation when the animation ends
    };


    protected Rect[] frames;						// Array of UV coordinates (for quads) defining the frames of an animation

    // Animation state vars:
    protected int curFrame = -1;					// The current frame
    protected int stepDir = 1;						// The direction we're currently playing the animation (1=forwards (default), -1=backwards)
    protected int numLoops = 0;						// Number of times we've looped since last animation
    protected bool playInReverse = false;			// Indicates that we've been instructed to play in reverse, as opposed to reversing merely as a result of loopReverse.

    protected float length;							// Length of the clip, in seconds (not taking into account looping, loop reversing, etc)

    /// <summary>
    /// The name of the animation.
    /// </summary>
    public string name;								// The name of the 

    /// <summary>
    /// How many times to loop the animation IN ADDITION to the initial play-through.  
    /// -1 indicates to loop infinitely.  
    /// 0 indicates to place the animation once then stop.  
    /// 1 indicates to repeat the animation once before 
    /// stopping, and so on.
    /// </summary>
    public int loopCycles = 0;						// How many times to loop the animation (-1 loop infinitely)

    /// <summary>
    /// Reverse the play direction when the end of the 
    /// animation is reached? (Ping-pong)
    /// If true, a loop iteration isn't counted until 
    /// the animation returns to the beginning.
    /// </summary>
    public bool loopReverse = false;				// Reverse the play direction when the end of the animation is reached? (if true, a loop iteration isn't counted until we return to the beginning)

    /// <summary>
    /// The rate in frames per second at which to play 
    /// the animation
    /// </summary>
    [HideInInspector]
    public float framerate = 15f;					// The rate in frames per second at which to play the animation

    /// <summary>
    /// What the sprite should do when the animation is done playing.
    /// The options are to: 1) Do nothing, 2) return to the static image,
    /// 3) play the default animation.
    /// </summary>
    [HideInInspector]
    public ANIM_END_ACTION onAnimEnd = ANIM_END_ACTION.Do_Nothing;


    public UVAnimation()
    {
        frames = new Rect[0];
    }

    /// <summary>
    /// Resets all the animation state vars to ready the object
    /// for playing anew.
    /// </summary>
    public void Reset()
    {
        curFrame = -1;
        stepDir = 1;
        numLoops = 0;
        playInReverse = false;
    }

    // Sets the stepDir to -1 and sets the current frame to the end
    // so that the animation plays in reverse
    public void PlayInReverse()
    {
        stepDir = -1;
        curFrame = frames.Length;
        numLoops = 0;
        playInReverse = true;
    }

    public void SetStepDir(int dir)
    {
        if (dir < 0)
        {
            stepDir = -1;
            playInReverse = true;
        }
        else
            stepDir = 1;
    }

    // Stores the UV of the next frame in 'uv', returns false if
    // we've reached the end of the animation (this will never
    // happen if it is set to loop infinitely)
    public bool GetNextFrame(ref Rect uv)
    {
        if (frames.Length < 1)
            return false;

        // See if we can advance to the next frame:
        if ((curFrame + stepDir) >= frames.Length || (curFrame + stepDir) < 0)
        {
            // See if we need to loop (if we're reversing, we don't loop until we get back to the beginning):
            if (stepDir > 0 && loopReverse)
            {
                stepDir = -1;	// Reverse playback direction
                curFrame += stepDir;

                curFrame = Mathf.Clamp(curFrame, 0, frames.Length - 1);

                uv = frames[curFrame];
            }
            else
            {
                // See if we can loop:
                if (numLoops + 1 > loopCycles && loopCycles != -1)
                    return false;
                else
                {	// Loop the animation:
                    ++numLoops;

                    if (loopReverse)
                    {
                        stepDir *= -1;
                        curFrame += stepDir;
                        curFrame = Mathf.Clamp(curFrame, 0, frames.Length - 1);
                    }
                    else
                    {
                        if (playInReverse)
                            curFrame = frames.Length - 1;
                        else
                            curFrame = 0;
                    }

                    uv = frames[curFrame];
                }
            }
        }
        else
        {
            curFrame += stepDir;
            uv = frames[curFrame];
        }

        return true;
    }


    /// <summary>
    /// Constructs an array of UV coordinates based upon the info
    /// supplied.  NOTE: When the edge of the texture is reached,
    /// this algorithm will "wrap" to the next row down, starting
    /// directly below the position of the first animation cell
    /// on the row above.
    /// </summary>
    /// <param name="start">The UV of the lower-left corner of the first cell</param>
    /// <param name="cellSize">width and height, in UV space, of each cell</param>
    /// <param name="cols">Number of columns in the grid</param>
    /// <param name="rows">Number of rows in the grid</param>
    /// <param name="totalCells">Total number of cells in the grid (left-to-right, top-to-bottom ordering is assumed, just like reading English).</param>
    /// <returns>Arrau of Rect objects that contain the UVs of each frame of animation.</returns>
    public Rect[] BuildUVAnim(Vector2 start, Vector2 cellSize, int cols, int rows, int totalCells)
    {
        int cellCount = 0;

        frames = new Rect[totalCells];

        frames[0].x = start.x;
        frames[0].y = start.y;
        frames[0].xMax = start.x + cellSize.x;
        frames[0].yMax = start.y + cellSize.y;

        for (int row = 0; row < rows; ++row)
        {
            for (int col = 0; col < cols && cellCount < totalCells; ++col)
            {
                frames[cellCount].x = start.x + cellSize.x * ((float)col);
                frames[cellCount].y = start.y - cellSize.y * ((float)row);
                frames[cellCount].xMax = frames[cellCount].x + cellSize.x;
                frames[cellCount].yMax = frames[cellCount].y + cellSize.y;

                ++cellCount;
            }
        }

        CalcLength();

        return frames;
    }

    /// <summary>
    /// Assigns the specified array of UV coordinates to the
    /// animation, replacing its current contents.
    /// </summary>
    /// <param name="anim">Array of Rect objects which hold the UV
    /// coordinates defining the animation.</param>
    public void SetAnim(Rect[] anim)
    {
        frames = anim;
        CalcLength();
    }

    /// <summary>
    /// Appends the specified array of UV coordinates to the
    /// existing animation.
    /// </summary>
    /// <param name="anim">Array of Rect objects which hold the UV
    /// coordinates defining the animation.</param>
    public void AppendAnim(Rect[] anim)
    {
        Rect[] tempFrames = frames;

        frames = new Rect[frames.Length + anim.Length];
        tempFrames.CopyTo(frames, 0);
        anim.CopyTo(frames, tempFrames.Length);

        CalcLength();
    }

    /// <summary>
    /// Sets the current frame of animation.
    /// </summary>
    /// <param name="f">The number of the frame.</param>
    public void SetCurrentFrame(int f)
    {
        if (f < 0 || f >= frames.Length)
            return;

        curFrame = f;
    }

    /// <summary>
    /// Sets the current frame based on a 0-1 value indicating
    /// the desired position in the animation.  For example, a
    /// position of 0.5 would specify a frame half-way into the
    /// animation.  A position of 0 would specify the starting frame.
    /// A position of 1 would specify the last frame in the animation.
    /// NOTE: Loop cycles and loop reverse are taken into account.
    /// To set the frame without regard to loop cycles etc, use
    /// SetClipPosition().
    /// </summary>
    /// <param name="pos">Percentage of the way through the animation (0-1).</param>
    public void SetPosition(float pos)
    {
        pos = Mathf.Clamp01(pos);

        // If this is an infinitely looping animation,
        // or a single-play animation, just set the
        // position within the clip:
        if (loopCycles < 1)
        {
            SetClipPosition(pos);
            return;
        }

        // The percentage of total animation that is
        // accounted for by a single loop iteration:
        float iterationPct = 1f / loopCycles;

        // The loop iteration containing the desired
        // frame:
        numLoops = Mathf.FloorToInt(pos / iterationPct);

        // Portion of our "pos" that is unaccounted for
        // merely by counting loop iterations:
        float remainder = pos - (((float)numLoops) * iterationPct);

        // Position within the "clip" (the "clip" being 
        // the series of frames between the first frame 
        // and last frame, without regard to the loop cycles)
        float clipPos = remainder / iterationPct;

        if (loopReverse)
        {
            if (clipPos < 0.5f)
            {
                curFrame = (int)(((float)frames.Length - 1) * (clipPos / 0.5f));
                // We're stepping forward from here:
                stepDir = 1;
            }
            else
            {
                curFrame = (frames.Length - 1) - (int)(((float)frames.Length - 1) * ((clipPos - 0.5f) / 0.5f));
                // We're stepping backwards from here:
                stepDir = -1;
            }
        }
        else
        {
            curFrame = (int)(((float)frames.Length - 1) * clipPos);
        }
    }

    /// <summary>
    /// Sets the current frame based on a 0-1 value indicating
    /// the desired position in the animation.  For example, a
    /// position of 0.5 would specify a frame half-way into the
    /// animation.  A position of 0 would specify the starting frame.
    /// A position of 1 would specify the last frame in the animation.
    /// NOTE: Loop cycles and loop reverse are NOT taken into account.
    /// Rather, this method sets the desired frame within the clip,
    /// the clip being the series of frames 0-n without regard to
    /// loop cycles or loop reversing.
    /// To set the frame with regard to loop cycles etc, use
    /// SetPosition().
    /// </summary>
    /// <param name="pos">Percentage of the way through the animation (0-1).</param>
    public void SetClipPosition(float pos)
    {
        curFrame = (int)(((float)frames.Length - 1) * pos);
    }

    // Calculates the length of the animation clip (not accounting for looping, loop reversing, etc)
    protected void CalcLength()
    {
        length = (1f / framerate) * frames.Length;
    }

    /// <summary>
    /// Returns the length, in seconds, of the animation.
    /// NOTE: This does not take into account looping or reversing.
    /// It simply returns the length, in seconds, of the animation, when
    /// played once from beginning to end.
    /// To get the duration of the animation including looping and reversing,
    /// use GetDuration().
    /// </summary>
    /// <returns>The length of the animation in seconds.</returns>
    public float GetLength()
    {
        return length;
    }

    /// <summary>
    /// Returns the duration, in seconds, of the animation.
    /// NOTE: This takes into account loop cycles and loop reverse.
    /// Ex: If an animation has a framerate of 30fps, consists of 60
    /// frames, and is set to loop once, the duration will be 4 seconds.
    /// To retrieve the length of the animation without regard to the loop
    /// cycles and loop reverse settings, use GetLength().
    /// </summary>
    /// <returns>The duration of the animation in seconds.  -1 if the animation loops infinitely.</returns>
    public float GetDuration()
    {
        // If this loops infinitely, return -1:
        if (loopCycles < 0)
            return -1f;

        float length = GetLength();

        if (loopReverse)
            length *= 2f;

        return length + (loopCycles * length);
    }

    /// <summary>
    /// Returns the number of frames in the animation.
    /// </summary>
    /// <returns>The number of frames in the animation.</returns>
    public int GetFrameCount()
    {
        return frames.Length;
    }

    /// <summary>
    /// Returns the number of frames displayed by the
    /// animation according to its current loop cycles
    /// and loop reverse settings.
    /// </summary>
    /// <returns>The number of frames displayed, -1 if set to loop infinitely.</returns>
    public int GetFramesDisplayed()
    {
        if (loopCycles == -1)
            return -1;

        int count = frames.Length + (frames.Length * loopCycles);

        if (loopReverse)
            count *= 2;

        return count;
    }
}


/// <remarks>
/// This derived class allows you to specify parameters in-editor
/// that will build an animation for you.
/// </remarks>
[System.Serializable]
public class UVAnimation_Auto : UVAnimation
{
    /// <summary>
    /// The pixel coordinates of the lower-left corner of the first
    /// frame in the animation sequence.
    /// </summary>
    public Vector2 start;

    /// <summary>
    /// The number of pixels from the left edge of one sprite frame
    /// to the left edge of the next one, and the number of pixels
    /// from the top of one sprite frame to the top of the one in
    /// the next row.  You may also want to think of this as the
    /// size (width and height) of each animation cell.
    /// </summary>
    public Vector2 pixelsToNextColumnAndRow;

    /// <summary>
    /// The number of columns in the animation.
    /// </summary>
    public int cols;

    /// <summary>
    /// The number of rows in the animation.
    /// </summary>
    public int rows;

    /// <summary>
    /// The total number of frames (cells) of animation.
    /// </summary>
    public int totalCells;

    /// <summary>
    /// Uses the information stored in this class to build
    /// a UV animation for the specified sprite.
    /// </summary>
    /// <param name="s">The sprite for which the animation will be built.</param>
    /// <returns>An array of UV coordinates that define the animation.</returns>
    public Rect[] BuildUVAnim(SpriteSM s)
    {
        if (totalCells < 1)
            return null;

        return this.BuildUVAnim(s.PixelCoordToUVCoord(start), s.PixelSpaceToUVSpace(pixelsToNextColumnAndRow), cols, rows, totalCells);
    }
}


/// <remarks>
/// This class allows you to specify multiple 
/// animation "clips" that will play sequentially
/// to the end of the list of clips.
/// </remarks>
[System.Serializable]
public class UVAnimation_Multi
{
    /// <summary>
    /// The name of the animation sequence
    /// </summary>
    public string name;					// The name of the animation sequence

    /// <summary>
    /// How many times to loop the animation IN ADDITION TO the initial play-through. (-1 to loop infinitely, 0 not to loop at all, 1 to repeat once before stopping, etc.)
    /// </summary>
    public int loopCycles = 0;			// How many times to loop the animation (-1 loop infinitely)

    /// <summary>
    /// Reverse the play direction when the end of the animation is reached? If true, a loop iteration isn't counted until the animation returns to the beginning.
    /// </summary>
    public bool loopReverse = false;	// Reverse the play direction when the end of the animation is reached? (if true, a loop iteration isn't counted until we return to the beginning)

    /// <summary>
    /// The rate in frames per second at which to play the animation.
    /// </summary>
    public float framerate = 15f;		// The rate in frames per second at which to play the animation

    /// <summary>
    /// What the sprite should do when the animation is done playing.
    /// The options are to: 1) Do nothing, 2) return to the static image,
    /// 3) play the default animation.
    /// </summary>
    public UVAnimation.ANIM_END_ACTION onAnimEnd = UVAnimation.ANIM_END_ACTION.Do_Nothing;

    /// <summary>
    /// The actual sprite animation clips that make up the animation sequence.
    /// </summary>
    public UVAnimation_Auto[] clips;	// The actual sprite animation clips that make up this animation sequence

    protected int curClip;				// Index of the currently-playing clip
    protected int stepDir = 1;			// The direction to step through our clips (1 == forwards, -1 == backwards)
    protected int numLoops = 0;			// Number of times we've looped since last animation

    protected float duration;			// The duration of the animation, accounting for looping and loop reversing.

    // Working vars:
    protected bool ret;
    protected int i;


    public UVAnimation_Multi()
    {
        if (clips == null)
            clips = new UVAnimation_Auto[0];
    }

    /// <summary>
    /// Gets a reference to the currently-playing clip.
    /// </summary>
    /// <returns>Reference to the currently-playing clip.</returns>
    public UVAnimation_Auto GetCurrentClip()
    {
        return clips[curClip];
    }

    /// <summary>
    /// Builds the UV animations for all animation clips
    /// that are a part of this animation sequence.
    /// </summary>
    /// <param name="s">The sprite for which to buidl the animation.</param>
    /// <returns>Array of animation clips that constitute the animation sequence.</returns>
    public UVAnimation_Auto[] BuildUVAnim(SpriteSM s)
    {
        for (i = 0; i < clips.Length; ++i)
        {
            clips[i].BuildUVAnim(s);
        }

        CalcDuration();

        return clips;
    }

    public bool GetNextFrame(ref Rect uv)
    {
        if (clips.Length < 1)
            return false;

        ret = clips[curClip].GetNextFrame(ref uv);

        if (!ret)
        {
            // See if we have another clip in the queue:
            if ((curClip + stepDir) >= clips.Length || (curClip + stepDir) < 0)
            {
                // See if we need to loop (if we're reversing, we don't loop until we get back to the beginning):
                if (stepDir > 0 && loopReverse)
                {
                    stepDir = -1;	// Reverse playback direction
                    curClip += stepDir;

                    curClip = Mathf.Clamp(curClip, 0, clips.Length - 1);

                    // Make the newly selected clip ready for playing:
                    clips[curClip].Reset();
                    clips[curClip].PlayInReverse();
                }
                else
                {
                    // See if we can loop:
                    if (numLoops + 1 > loopCycles && loopCycles != -1)
                        return false;	// We've reached the end of the last clip
                    else
                    {	// Loop the animation:
                        ++numLoops;

                        if (loopReverse)
                        {
                            stepDir *= -1;
                            curClip += stepDir;

                            curClip = Mathf.Clamp(curClip, 0, clips.Length - 1);

                            // Make the newly selected clip ready for playing:
                            clips[curClip].Reset();

                            if (stepDir < 0)
                                clips[curClip].PlayInReverse();
                        }
                        else
                        {
                            curClip = 0;

                            // Make the newly selected clip ready for playing:
                            clips[curClip].Reset();
                        }
                    }
                }
            }
            else
            {
                curClip += stepDir;

                // Make the newly selected clip ready for playing:
                clips[curClip].Reset();

                if (stepDir < 0)
                    clips[curClip].PlayInReverse();
            }

            return true;	// Keep playing
        }

        /*
                // Simpler, non-looping logic:
                if (curClip < clips.Length - 1)
                {
                    // Go to the next clip:
                    ++curClip;
                    return true;	// Keep playing
                }
                else
                    return false;	// We've reached the end of the last clip
        */

        return true;
    }

    /// <summary>
    /// Appends UV animation to the clip specified by index.
    /// </summary>
    /// <param name="index">The animation clip to append to.</param>
    /// <param name="anim">Array of UV coordinates that define the animation to be appended.</param>
    public void AppendAnim(int index, Rect[] anim)
    {
        if (index >= clips.Length)
            return;

        clips[index].AppendAnim(anim);

        CalcDuration();
    }

    /// <summary>
    /// Appends UV animation clip to the end of the animation sequence.
    /// </summary>
    /// <param name="clip">Animation clip to append.</param>
    public void AppendClip(UVAnimation clip)
    {
        UVAnimation[] temp;
        temp = clips;

        clips = new UVAnimation_Auto[clips.Length + 1];
        temp.CopyTo(clips, 0);

        clips[clips.Length - 1] = (UVAnimation_Auto)clip;

        CalcDuration();
    }

    public void PlayInReverse()
    {
        for (i = 0; i < clips.Length; ++i)
        {
            clips[i].PlayInReverse();
        }

        stepDir = -1;
        curClip = clips.Length - 1;
    }

    /// <summary>
    /// Replaces the contents of the specified clip.
    /// </summary>
    /// <param name="index">Index of the clip the contents of which you wish to replace.</param>
    /// <param name="frames">Array of UV coordinates that define the content of an animation clip.</param>
    public void SetAnim(int index, Rect[] frames)
    {
        if (index >= clips.Length)
            return;

        clips[index].SetAnim(frames);

        CalcDuration();
    }

    /// <summary>
    /// Resets the animation sequence for playing anew.
    /// </summary>
    public void Reset()
    {
        curClip = 0;
        stepDir = 1;
        numLoops = 0;

        for (i = 0; i < clips.Length; ++i)
        {
            clips[i].Reset();
        }
    }

    /// <summary>
    /// Sets the current playing position of the animation.
    /// NOTE: This method takes loop cycles and loop reversing
    /// into account.  To set the position without regard to
    /// loop cycles or loop reversing, use SetAnimPosition().
    /// </summary>
    /// <param name="pos">Desired position in the animation (0-1).</param>
    public void SetPosition(float pos)
    {
        pos = Mathf.Clamp01(pos);

        // If this is an infinitely looping animation,
        // or if it is a single-play animation, just
        // set the position:
        if (loopCycles < 1)
        {
            SetAnimPosition(pos);
            return;
        }

        // The percentage of total animation that is
        // accounted for by a single loop iteration:
        float iterationPct = 1f / loopCycles;

        // Find the loop iteration of the desired position:
        numLoops = Mathf.FloorToInt(pos / iterationPct);

        // Portion of our "pos" that is unaccounted for
        // merely by counting loop iterations:
        float remainder = pos - (((float)numLoops) * iterationPct);

        SetAnimPosition(remainder / iterationPct);
    }

    /// <summary>
    /// Sets the current playing position of the animation.
    /// NOTE: This method does NOT take loop cycles and loop 
    /// reversing into account.  To set the position taking 
    /// loop cycles and loop reversing into account, use 
    /// SetAnimPosition().
    /// </summary>
    /// <param name="pos">Desired position in the animation (0-1).</param>
    public void SetAnimPosition(float pos)
    {
        int totalFrames = 0;
        float pct;
        float remaining = pos;

        // Get the total number of frames:
        for (int n = 0; n < clips.Length; ++n)
        {
            totalFrames += clips[n].GetFramesDisplayed();
        }

        // Find which clip our desired position is in:
        if (loopReverse)
        {
            if (pos < 0.5f)
            {
                // We will step forward from here:
                stepDir = 1;

                // Adjust to account for the fact that a value
                // of .5 in this context means 100% of the way
                // from the first frame to the last frame:
                remaining *= 2f;

                for (int n = 0; n < clips.Length; ++n)
                {
                    // Get the percentage of our animation
                    // that is accounted for by this clip:
                    pct = clips[n].GetFramesDisplayed() / totalFrames;

                    // If the distance we have left to go into
                    // our animation is less than the distance
                    // accounted for by this clip, this is the
                    // clip we're looking for!:
                    if (remaining <= pct)
                    {
                        curClip = n;
                        clips[curClip].SetPosition(remaining / pct);
                        return;
                    }
                    else
                        remaining -= pct;
                }
            }
            else
            {
                // We will step backward from here:
                stepDir = -1;

                // Adjust for the fact that in this context, 
                // a value of .5 means 0% of the way from the
                // last frame to the first frame:
                remaining = (remaining - 0.5f) / 0.5f;

                for (int n = clips.Length - 1; n >= 0; --n)
                {
                    // Get the percentage of our animation
                    // that is accounted for by this clip:
                    pct = clips[n].GetFramesDisplayed() / totalFrames;

                    // If the distance we have left to go into
                    // our animation is less than the distance
                    // accounted for by this clip, this is the
                    // clip we're looking for!:
                    if (remaining <= pct)
                    {
                        curClip = n;
                        clips[curClip].SetPosition(1f - (remaining / pct));
                        clips[curClip].SetStepDir(-1);
                        return;
                    }
                    else
                        remaining -= pct;
                }
            }
        }
        else
        {
            for (int n = 0; n < clips.Length; ++n)
            {
                // Get the percentage of our animation
                // that is accounted for by this clip:
                pct = clips[n].GetFramesDisplayed() / totalFrames;

                // If the distance we have left to go into
                // our animation is less than the distance
                // accounted for by this clip, this is the
                // clip we're looking for!:
                if (remaining <= pct)
                {
                    curClip = n;
                    clips[curClip].SetPosition(remaining / pct);
                    return;
                }
                else
                    remaining -= pct;
            }
        }
    }

    // Calculates the duration of the animation:
    protected void CalcDuration()
    {
        // If this loops infinitely, set duration to -1:
        if (loopCycles < 0)
        {
            duration = -1f;
            return;
        }

        duration = 0;

        for (int n = 0; n < clips.Length; ++n)
        {
            duration += clips[n].GetDuration();
        }

        if (loopReverse)
            duration *= 2f;

        duration += (loopCycles * duration);
    }

    /// <summary>
    /// Returns the duration, in seconds, of the animation.
    /// NOTE: This takes into account loop cycles and loop reverse.
    /// Ex: If an animation has a framerate of 30fps, consists of 60
    /// frames, and is set to loop once, the duration will be 4 seconds.
    /// </summary>
    /// <returns>The duration of the animation in seconds. -1 if the animation loops infinitely.</returns>
    public float GetDuration()
    {
        return duration;
    }

    /// <summary>
    /// Returns the total number of frames displayed by
    /// this animation.
    /// </summary>
    /// <returns>Number of frames to be displayed.</returns>
    public int GetFrameCount()
    {
        int totalFrames = 0;

        // Get the total number of frames:
        for (int n = 0; n < clips.Length; ++n)
        {
            totalFrames += clips[n].GetFramesDisplayed();
        }

        return totalFrames;
    }
}



/// <remarks>
/// Serves as the base for defining a sprite.
/// This class should not actually be used despite
/// the fact that Unity will allow you to attach it
/// to a GameObject.
/// </remarks>
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class SpriteBase : MonoBehaviour
{
    /// <remarks>
    /// The plane in which a sprite should be drawn.
    /// </remarks>
    public enum SPRITE_PLANE
    {
        XY,
        XZ,
        YZ
    };

    /// <remarks>
    /// The anchoring scheme of a sprite.  The anchor point is the
    /// point on the sprite that will remain stationary when the
    /// sprite's size changes.
    /// <example>For a health bar that "grows" to the right while
    /// its left edge remains stationary, you would use UPPER_LEFT,
    /// MIDDLE_LEFT, or BOTTOM_LEFT.</example>
    /// <example>For a health bar that "grows" upward while the
    /// bottom edge remains stationary, you would use BOTTOM_LEFT,
    /// BOTTOM_CENTER, or BOTTOM_RIGHT.</example>
    /// </remarks>
    public enum ANCHOR_METHOD
    {
        UPPER_LEFT,
        UPPER_CENTER,
        UPPER_RIGHT,
        MIDDLE_LEFT,
        MIDDLE_CENTER,
        MIDDLE_RIGHT,
        BOTTOM_LEFT,
        BOTTOM_CENTER,
        BOTTOM_RIGHT
    }

    /// <remarks>
    /// Defines which way the polygons of a sprite
    /// should be wound.  The two options are
    /// clock-wise (CW) and counter clock-wise (CCW).
    /// These determine the direction the sprite will "face".
    /// </remarks>
    public enum WINDING_ORDER
    {
        CCW,		// Counter-clockwise
        CW			// Clockwise
    };


    /// <summary>
    /// The plane in which the sprite will be drawn.
    /// </summary>
    public SPRITE_PLANE plane = SPRITE_PLANE.XY;// The plane in which the sprite will be drawn

    /// <summary>
    /// The winding order of the sprite's polygons - determines
    /// the direction the sprite will "face".
    /// </summary>
    public WINDING_ORDER winding = WINDING_ORDER.CW;

    /// <summary>
    /// Width of the sprite in world space.
    /// </summary>
    public float width;							// Width and Height of the sprite in worldspace units

    /// <summary>
    /// Height of the sprite in world space.
    /// </summary>
    public float height;

    /// <summary>
    /// Will contract the UV edges of the sprite 
    /// by the specified amount to prevent "bleeding" 
    /// from neighboring pixels, especially when mipmapping.
    /// </summary>
    public Vector2 bleedCompensation;			// Will contract the UV edges of the sprite to prevent "bleeding" from neighboring pixels, especially when mipmapping

    /// <summary>
    /// Anchor method to use. <seealso cref="ANCHOR_METHOD"/>
    /// </summary>
    public ANCHOR_METHOD anchor = ANCHOR_METHOD.MIDDLE_CENTER;
    public float shearAmount = 0;

    /// <summary>
    /// Automatically sizes the sprite so that it will 
    /// display pixel-perfect on-screen.
    /// NOTE: Only use this when you are using an orthographic 
    /// projection.  Also note that if you change the
    /// orthographic size of the camera to achieve a zooming
    /// effect, sprites set to render pixel-perfect will
    /// not appear to change size on-screen as they will be
    /// scaled to always draw the same visible size. This
    /// can be useful for things such as UI elements.
    /// However, if you want automatic resizing functionality
    /// without being pixel-perfect and therefore allowing
    /// zooming in and out, use <see cref="autoResize"/> instead.
    /// </summary>
    public bool pixelPerfect = false;			// Automatically sizes the sprite so that it will display pixel-perfect on-screen (NOTE: Only use this when you are using an orthographic projection)

    /// <summary>
    /// Automatically resizes the sprite based on its new 
    /// UV dimensions compared to its previous dimensions.
    /// Setting this to true allows you to use non-uniform
    /// sized sprites for animation without causing the
    /// sprite to appear "squashed" while animating.
    /// </summary>
    public bool autoResize = false;				// Automatically resizes the sprite based on its new UV dimensions compared to its previous dimensions

    protected Vector2 bleedCompensationUV;		// UV-space version of bleedCompensation
    protected Rect uvRect;						// UV coordinates defining the rect of our sprite
    /*
        protected Vector2 lowerLeftUV;				// UV coordinate for the upper-left corner of the sprite
        protected Vector2 uvDimensions;				// Distance from the upper-left UV to place the other UVs
    */
    protected Vector2 topLeft;					// The adjustment needed for the current anchoring scheme
    protected Vector2 bottomRight;				// The adjustment needed for the current anchoring scheme
    [HideInInspector]
    public bool billboarded = false;			// Is the sprite to be billboarded? (not currently supported)

    /// <summary>
    /// Offsets the sprite, in world space, from the center of its
    /// GameObject.
    /// </summary>
    public Vector3 offset = new Vector3();		// Offset of sprite from center of client GameObject

    /// <summary>
    /// The color to be used by all four of the sprite's
    /// vertices.  This can be used to color, highlight,
    /// or fade the sprite. Be sure to use a vertex-colored
    /// shader for this to have an effect.
    /// </summary>
    public Color color = Color.white;			// The color to be used by all four vertices

    protected MeshFilter meshFilter;
    protected MeshRenderer meshRenderer;
    protected Mesh mesh;						// Reference to our mesh
    protected Texture texture;
    protected Vector3[] vertices = new Vector3[4];
    protected Color[] colors = new Color[4];
    protected Vector2[] uvs = new Vector2[4];
    protected Vector3[] normals;
    protected int[] faces = new int[6];
    //
    public bool createNormals = false;
    //
    // Vars that make pixel-perfect sizing and
    // automatic sizing work:
    protected static Vector2 screenSize;		// The size of the screen in pixels
    protected Camera curCamera;
    protected Rect prevUVRect;					// The previous UV rect
    protected Vector2 pixelsPerUV;				// The number of pixels in both axes per UV unit
    protected float worldUnitsPerScreenPixel;	// The number of world units in both axes per screen pixel when using orthographic projections



    // Animation-related vars and types:

    /// <remarks>
    /// Defines a delegate that can be called upon animation completion.
    /// Use this if you want something to happen as soon as an animation
    /// reaches the end.
    /// </remarks>
    public delegate void AnimCompleteDelegate();		// Definition of delegate to be called upon animation completion

    /// <remarks>
    /// Defines a delegate that can be called upon resizing of the sprite.
    /// Use this if you want to adjust colliders, etc, when the sprites
    /// dimensions are resized.
    /// </remarks>
    public delegate void SpriteResizedDelegate(float newWidth, float newHeight, SpriteBase sprite);

    /// <summary>
    /// When set to true, the sprite will play the default
    /// animation (see <see cref="defaultAnim"/>) when the sprite
    /// is instantiated.
    /// </summary>
    public bool playAnimOnStart = false;				// When set to true, will start playing the default animation on start

    /// <summary>
    /// Index of the animation to play by default.
    /// </summary>
    public int defaultAnim = 0;							// Index of the default animation

    protected AnimCompleteDelegate animCompleteDelegate = null;	// Delegate to be called upon animation completion
    protected SpriteResizedDelegate resizedDelegate = null; // Delegate to be called upon sprite resizing.
    protected float timeSinceLastFrame = 0;				// The total time since our last animation frame change
    protected float timeBetweenAnimFrames;				// The amount of time we want to pass before moving to the next frame of animation
    protected int framesToAdvance;						// (working) The number of animation frames to advance given the time elapsed
    protected bool animating = false;					// True when an animation is playing


    // Working vars:
    protected int i;
    protected Vector2 tempUV;


    protected virtual void Awake()
    {
        meshFilter = (MeshFilter)gameObject.GetComponent(typeof(MeshFilter));
        meshRenderer = (MeshRenderer)gameObject.GetComponent(typeof(MeshRenderer));
        //mesh = meshFilter.mesh;

        //mesh = meshFilter.sharedMesh;
        meshFilter.sharedMesh = null;

        if (meshRenderer.sharedMaterial != null)
            texture = meshRenderer.sharedMaterial.GetTexture("_MainTex");
        else
            Debug.LogWarning("Sprite on GameObject \"" + name + "\" has not been assigned a material.");

        // Start the shared animation coroutine if it is not running already:
        if (!SpriteAnimationPump.pumpIsRunning && Application.isPlaying)
            SpriteAnimationPump.Instance.StartAnimationPump();

        //
        if (createNormals)
        {
            normals = new Vector3[4];
            for (int i = 0; i < 4; i++)
            {
                normals[i] = Vector3.back;
            }
        }
    }


    protected virtual void Start()
    {
        prevUVRect = uvRect;

        if (texture != null)
        {
            pixelsPerUV.x = texture.width;
            pixelsPerUV.y = texture.height;
        }

        SetCamera(Camera.main);
    }


    protected virtual void Init()
    {
        if (mesh == null)
        {
            meshFilter.sharedMesh = new Mesh();
            mesh = meshFilter.sharedMesh;
        }

        // Assign to mesh object:
        mesh.Clear();
        mesh.vertices = vertices;
        if (createNormals)
        {
            mesh.normals = normals;
        }
        mesh.uv = uvs;
        mesh.colors = colors;
        mesh.triangles = faces;

        SetWindingOrder(winding);

        // Calculate UV dimensions:
        CalcUVs();

        SetBleedCompensation(bleedCompensation);

        // Build vertices:
        if (pixelPerfect)
        {
            if (texture == null)
            {
                if (meshRenderer.sharedMaterial != null)
                    texture = meshRenderer.sharedMaterial.GetTexture("_MainTex");
            }

            if (texture != null)
            {
                pixelsPerUV.x = texture.width;
                pixelsPerUV.y = texture.height;
            }

            SetCamera(Camera.main);
        }
        else
            SetSize(width, height);

        // Set colors:
        SetColor(color);
        //
    }


    /// <summary>
    /// Resets important sprite values to defaults for reuse.
    /// </summary>
    public virtual void Clear()
    {
        billboarded = false;
        SetColor(Color.white);
        offset = Vector3.zero;

        //animations.Clear();
        animCompleteDelegate = null;
    }


    // Called when the GO is disabled or destroyed
    public void OnDisable()
    {
        if (animating)
        {
            // Remove ourselves from the animating list.
            RemoveFromAnimatedList();

            // Leave "animating" set to true so that when
            // we re-enable, we can pick up animating again:
            animating = true;
        }
    }

    // Called when the GO is enabled or created:
    public void OnEnable()
    {
        // If this is being called in edit mode,
        // disregard:
        if (!Application.isPlaying)
            return;

        // If we were previously animating,
        // resume animation:
        if (animating)
        {
            // Set to false so AddToAnimatingList()
            // won't bail out:
            animating = false;
            AddToAnimatedList();
        }
    }


    /*
        /// <summary>
        /// Allows you to setup some of the main features of a sprite in a single method call.
        /// </summary>
        /// <param name="width">Width of the sprite in world space.</param>
        /// <param name="height">Height of the sprite in world space.</param>
        /// <param name="lowerleftPixel">Lower-left pixel of the sprite when no animation has been played.</param>
        /// <param name="pixeldimensions">Pixel dimeinsions of the sprite when no animation has been played.</param>
        public virtual void Setup(float width, float height, Vector2 lowerleftPixel, Vector2 pixeldimensions)
        {
            SetSize(width, height);
            lowerLeftPixel = lowerleftPixel;
            pixelDimensions = pixeldimensions;

            tempUV = PixelCoordToUVCoord(lowerLeftPixel);
            uvRect.x = tempUV.x;
            uvRect.y = tempUV.y;

            tempUV = PixelSpaceToUVSpace(pixelDimensions);
            uvRect.xMax = uvRect.x + tempUV.x;
            uvRect.yMax = uvRect.y + tempUV.y;

            SetBleedCompensation(bleedCompensation);
        }
    */

    /// <summary>
    /// Copies all the vital attributes of another sprite.
    /// </summary>
    /// <param name="s">Source sprite to be copied.</param>
    public virtual void Copy(SpriteBase s)
    {
        // Copy the material:
        GetComponent<Renderer>().sharedMaterial = s.GetComponent<Renderer>().sharedMaterial;
        texture = GetComponent<Renderer>().sharedMaterial.mainTexture;

        if (texture != null)
        {
            pixelsPerUV.x = texture.width;
            pixelsPerUV.y = texture.height;
        }

        plane = s.plane;
        winding = s.winding;
        offset = s.offset;
        anchor = s.anchor;
        autoResize = s.autoResize;
        pixelPerfect = s.pixelPerfect;

        SetColor(s.color);
    }

    // Pure virtual intended to be overridden with code
    // that calculates UVs in a manner appropriate to
    // derived class.
    public virtual void CalcUVs()
    {

    }
    public virtual void RecalcTexture()
    {
        if (GetComponent<Renderer>() != null)
        {
            texture = GetComponent<Renderer>().sharedMaterial.mainTexture;
            if (texture != null)
            {
                pixelsPerUV.x = texture.width;
                pixelsPerUV.y = texture.height;
            }
        }
    }
    // Sets the edge positions needed to properly
    // orient our sprite according to our anchoring
    // method:
    public void CalcEdges()
    {
        switch (anchor)
        {
            case ANCHOR_METHOD.UPPER_LEFT:
                topLeft.x = 0;
                topLeft.y = 0;
                bottomRight.x = width;
                bottomRight.y = -height;
                break;
            case ANCHOR_METHOD.UPPER_CENTER:
                topLeft.x = width * -0.5f;
                topLeft.y = 0;
                bottomRight.x = width * 0.5f;
                bottomRight.y = -height;
                break;
            case ANCHOR_METHOD.UPPER_RIGHT:
                topLeft.x = -width;
                topLeft.y = 0;
                bottomRight.x = 0;
                bottomRight.y = -height;
                break;
            case ANCHOR_METHOD.MIDDLE_LEFT:
                topLeft.x = 0;
                topLeft.y = height * 0.5f;
                bottomRight.x = width;
                bottomRight.y = height * -0.5f;
                break;
            case ANCHOR_METHOD.MIDDLE_CENTER:
                topLeft.x = width * -0.5f;
                topLeft.y = height * 0.5f;
                bottomRight.x = width * 0.5f;
                bottomRight.y = height * -0.5f;
                break;
            case ANCHOR_METHOD.MIDDLE_RIGHT:
                topLeft.x = -width;
                topLeft.y = height * 0.5f;
                bottomRight.x = 0;
                bottomRight.y = height * -0.5f;
                break;
            case ANCHOR_METHOD.BOTTOM_LEFT:
                topLeft.x = 0;
                topLeft.y = height;
                bottomRight.x = width;
                bottomRight.y = 0;
                break;
            case ANCHOR_METHOD.BOTTOM_CENTER:
                topLeft.x = width * -0.5f;
                topLeft.y = height;
                bottomRight.x = width * 0.5f;
                bottomRight.y = 0;
                break;
            case ANCHOR_METHOD.BOTTOM_RIGHT:
                topLeft.x = -width;
                topLeft.y = height;
                bottomRight.x = 0;
                bottomRight.y = 0;
                break;
        }
    }

    // Sets the width and height of the sprite based upon
    // the change in its UV dimensions
    /// <summary>
    /// Recalculates the width and height of the sprite
    /// based upon the change in its UV dimensions (autoResize) or
    /// on the current orthographic size (pixelPerfect).
    /// </summary>
    public void CalcSize()
    {
        if (pixelPerfect)
        {
            // Calculate the size based on the orthographic size:
            worldUnitsPerScreenPixel = (curCamera.orthographicSize * 2f) / screenSize.y;
            width = worldUnitsPerScreenPixel * uvRect.width * pixelsPerUV.x;
            height = worldUnitsPerScreenPixel * uvRect.height * pixelsPerUV.y;
        }
        else if (autoResize) // Else calculate the size based on the change in UV dimensions:
        {
            // Prevent divide by zero:
            if (prevUVRect.width != 0 && prevUVRect.height != 0)
            {
                // Find the percentage change in the UV:
                tempUV.x = uvRect.width / prevUVRect.width;
                tempUV.y = uvRect.height / prevUVRect.height;

                // Change the width and height accordingly:
                width *= tempUV.x;
                height *= tempUV.y;
            }
        }

        // Now that we've calculated the change,
        // save the current UV rect so that if
        // this method is called again without 
        // there being a UV change, we don't
        // continue to resize the sprite:
        prevUVRect = uvRect;

        SetSize(width, height);
    }

    /// <summary>
    /// Sets the physical dimensions of the sprite in the 
    /// plane selected
    /// </summary>
    /// <param name="width">Width of the sprite in world space.</param>
    /// <param name="height">Height of the sprite in world space.</param>
    public void SetSize(float width, float height)
    {
        switch (plane)
        {
            case SPRITE_PLANE.XY:
                SetSizeXY(width, height);
                break;
            case SPRITE_PLANE.XZ:
                SetSizeXZ(width, height);
                break;
            case SPRITE_PLANE.YZ:
                SetSizeYZ(width, height);
                break;
        }

        if (resizedDelegate != null)
            resizedDelegate(width, height, this);
    }

    // Sets the physical dimensions of the sprite in the XY plane:
    protected void SetSizeXY(float w, float h)
    {
        if (mesh == null)
        {
            Debug.LogError("Awake has not been called yet");
        }
        width = w;
        height = h;

        CalcEdges();

        // Upper-left
        vertices[0].x = offset.x + topLeft.x;
        vertices[0].y = offset.y + topLeft.y;
        vertices[0].z = offset.z;

        // Lower-left
        vertices[1].x = offset.x + topLeft.x;
        vertices[1].y = offset.y + bottomRight.y;
        vertices[1].z = offset.z;

        // Lower-right
        vertices[2].x = offset.x + bottomRight.x;
        vertices[2].y = offset.y + bottomRight.y;
        vertices[2].z = offset.z;

        // Upper-right
        vertices[3].x = offset.x + bottomRight.x;
        vertices[3].y = offset.y + topLeft.y;
        vertices[3].z = offset.z;

        mesh.vertices = vertices;

        mesh.RecalculateBounds();
    }


    public void RefreshVertices()
    {
        CalcEdges();

        // Upper-left
        vertices[0].x = offset.x + topLeft.x + shearAmount*width;
        vertices[0].y = offset.y + topLeft.y;
        vertices[0].z = offset.z;

        // Lower-left
        vertices[1].x = offset.x + topLeft.x;
        vertices[1].y = offset.y + bottomRight.y;
        vertices[1].z = offset.z;

        // Lower-right
        vertices[2].x = offset.x + bottomRight.x;
        vertices[2].y = offset.y + bottomRight.y;
        vertices[2].z = offset.z;

        // Upper-right
        vertices[3].x = offset.x + bottomRight.x + shearAmount*width;
        vertices[3].y = offset.y + topLeft.y;
        vertices[3].z = offset.z;

        mesh.vertices = vertices;

        mesh.RecalculateBounds();
    }

    // Sets the physical dimensions of the sprite in the XZ plane:
    protected void SetSizeXZ(float w, float h)
    {
        width = w;
        height = h;

        CalcEdges();

        // Upper-left
        vertices[0].x = offset.x + topLeft.x;
        vertices[0].y = offset.y;
        vertices[0].z = offset.z + topLeft.y;

        // Lower-left
        vertices[1].x = offset.x + topLeft.x;
        vertices[1].y = offset.y;
        vertices[1].z = offset.z + bottomRight.y;

        // Lower-right
        vertices[2].x = offset.x + bottomRight.x;
        vertices[2].y = offset.y;
        vertices[2].z = offset.z + bottomRight.y;

        // Upper-right
        vertices[3].x = offset.x + bottomRight.x;
        vertices[3].y = offset.y;
        vertices[3].z = offset.z + topLeft.y;

        mesh.vertices = vertices;

        mesh.RecalculateBounds();
    }

    // Sets the physical dimensions of the sprite in the YZ plane:
    protected void SetSizeYZ(float w, float h)
    {
        width = w;
        height = h;

        CalcEdges();

        // Upper-left
        vertices[0].x = offset.x;
        vertices[0].y = offset.y + topLeft.y;
        vertices[0].z = offset.z + topLeft.x;

        // Lower-left
        vertices[1].x = offset.x;
        vertices[1].y = offset.y + bottomRight.y;
        vertices[1].z = offset.z + topLeft.x;

        // Lower-right
        vertices[2].x = offset.x;
        vertices[2].y = offset.y + bottomRight.y;
        vertices[2].z = offset.z + bottomRight.x;

        // Upper-right
        vertices[3].x = offset.x;
        vertices[3].y = offset.y + topLeft.y;
        vertices[3].z = offset.z + bottomRight.x;

        mesh.vertices = vertices;

        mesh.RecalculateBounds();
    }

    public void UpdateUVs()
    {
        if (winding == WINDING_ORDER.CW)
        {
            uvs[0].x = uvRect.x; uvs[0].y = uvRect.yMax;
            uvs[1].x = uvRect.x; uvs[1].y = uvRect.y;
            uvs[2].x = uvRect.xMax; uvs[2].y = uvRect.y;
            uvs[3].x = uvRect.xMax; uvs[3].y = uvRect.yMax;
        }
        else
        {
            uvs[3].x = uvRect.x; uvs[3].y = uvRect.yMax;
            uvs[2].x = uvRect.x; uvs[2].y = uvRect.y;
            uvs[1].x = uvRect.xMax; uvs[1].y = uvRect.y;
            uvs[0].x = uvRect.xMax; uvs[0].y = uvRect.yMax;
        }


        mesh.uv = uvs;
    }

    // Applies the transform of the client GameObject and stores
    // the results in the associated vertices of the overall mesh:
    public void TransformBillboarded(Transform t)
    {	//Todo
        /*
                Vector3 pos = clientTransform.position;

                meshVerts[mv1] = pos + t.InverseTransformDirection(v1);
                meshVerts[mv2] = pos + t.InverseTransformDirection(v2);
                meshVerts[mv3] = pos + t.InverseTransformDirection(v3);
                meshVerts[mv4] = pos + t.InverseTransformDirection(v4);

                m_manager.UpdatePositions();
         */
    }

    /// <summary>
    /// Sets the sprite's color to the specified color.
    /// </summary>
    /// <param name="c">Color to shade the sprite.</param>
    public void SetColor(Color c)
    {
        color = c;

        // Update vertex colors:
        colors[0] = color;
        colors[1] = color;
        colors[2] = color;
        colors[3] = color;

        mesh.colors = colors;
    }

    // Sets the camera to use when calculating 
    // pixel-perfect sprite size:
    /// <summary>
    /// Sets the camera to use when calculating
    /// a pixel-perfect sprite size. Be sure
    /// that this camera is set to use an orthographic
    /// projection.
    /// </summary>
    /// <param name="c"></param>
    public void SetCamera(Camera c)
    {
        if (c == null)
            return;

        screenSize.x = c.pixelWidth;
        screenSize.y = c.pixelHeight;
        curCamera = c;
        CalcSize();
    }

    //-----------------------------------------------------------------
    // Animation-related routines:
    //-----------------------------------------------------------------

    /// <summary>
    /// Sets the delegate to be called upon animation completion.
    /// </summary>
    /// <param name="del">The delegate to be called when an animation finishes playing.</param>
    public void SetAnimCompleteDelegate(AnimCompleteDelegate del)
    {
        animCompleteDelegate = del;
    }

    /// <summary>
    /// Sets the delegate to be called when the sprite is resized.
    /// </summary>
    /// <param name="del">The delegate to be called when the sprite is resized.</param>
    public void SetSpriteResizedDelegate(SpriteResizedDelegate del)
    {
        resizedDelegate = del;
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
    public virtual bool StepAnim(float time) { return false; }


    /// <summary>
    /// Pauses the currently-playing animation.
    /// </summary>
    public void PauseAnim()
    {
        RemoveFromAnimatedList();
        // Stop coroutine
        //animating = false;
        //StopCoroutine("AnimationPump");
        //StopAllCoroutines();
    }


    /// <summary>
    /// Stops the current animation from playing
    /// and resets it to the beginning for playing
    /// again.  The sprite then reverts to the static
    /// image.
    /// </summary>
    public virtual void StopAnim() { }


    /// <summary>
    /// Reverts the sprite to its static (non-animating) default appearance.
    /// </summary>
    public void RevertToStatic()
    {
        if (animating)
            StopAnim();

        CalcUVs();
        SetBleedCompensation();

        if (autoResize || pixelPerfect)
            CalcSize();
    }

    // Adds the sprite to the list of currently
    // animating sprites:
    protected void AddToAnimatedList()
    {
        // Check to see if the coroutine is running,
        // and if not, start it:
        if (!SpriteAnimationPump.pumpIsRunning)
            SpriteAnimationPump.Instance.StartAnimationPump();

        // If we're already animating, then we're
        // already in the list, no need to add again:
        if (animating)
            return;

        animating = true;
        SpriteAnimationPump.Add(this);
    }

    // Removes the sprite from the list of currently
    // animating sprites:
    protected void RemoveFromAnimatedList()
    {
        SpriteAnimationPump.Remove(this);
        animating = false;
    }


    //--------------------------------------------------------------
    // Accessors:
    //--------------------------------------------------------------
    /// <summary>
    /// Returns whether the sprite is currently animating.
    /// </summary>
    /// <returns>True if the sprite is currently animating, false otherwise.</returns>
    public bool IsAnimating() { return animating; }

    public void SetBleedCompensation() { SetBleedCompensation(bleedCompensation); }

    /// <summary>
    /// Sets the bleed compensation to use (see <see cref="bleedCompensation"/>).
    /// </summary>
    public void SetBleedCompensation(float x, float y) { SetBleedCompensation(new Vector2(x, y)); }

    /// <summary>
    /// Sets the bleed compensation to use (see <see cref="bleedCompensation"/>).
    /// </summary>
    public void SetBleedCompensation(Vector2 xy)
    {
        bleedCompensation = xy;
        bleedCompensationUV = PixelSpaceToUVSpace(bleedCompensation);

        uvRect.x += bleedCompensationUV.x;
        uvRect.y += bleedCompensationUV.y;
        uvRect.xMax -= bleedCompensationUV.x * 2f;
        uvRect.yMax -= bleedCompensationUV.y * 2f;

        UpdateUVs();
    }

    /// <summary>
    /// Sets the plane in which the sprite is to be drawn. See: <see cref="SPRITE_PLANE"/>
    /// </summary>
    /// <param name="p">The plane in which the sprite should be drawn.</param>
    public void SetPlane(SPRITE_PLANE p)
    {
        plane = p;
        SetSize(width, height);
    }

    /// <summary>
    /// Sets the winding order to use. See <see cref="WINDING_ORDER"/>.
    /// </summary>
    /// <param name="order">The winding order to use.</param>
    public void SetWindingOrder(WINDING_ORDER order)
    {
        winding = order;

        if (winding == WINDING_ORDER.CCW)
        {
            // Counter-clockwise:
            faces[0] = 0;	//	0_ 2			0 ___ 3
            faces[1] = 1;	//  | /		Verts:	 |	/|
            faces[2] = 3;	// 1|/				1|/__|2

            faces[3] = 3;	//	  3
            faces[4] = 1;	//   /|
            faces[5] = 2;	// 4/_|5
        }
        else
        {
            // Clock-wise:
            faces[0] = 0;	//	0_ 1			0 ___ 3
            faces[1] = 3;	//  | /		Verts:	 |	/|
            faces[2] = 1;	// 2|/				1|/__|2

            faces[3] = 3;	//	  3
            faces[4] = 2;	//   /|
            faces[5] = 1;	// 5/_|4
        }

        if (mesh != null)
            mesh.triangles = faces;
    }

    /// <summary>
    /// Sets the sprite's UVs to the specified values.
    /// </summary>
    /// <param name="uv">A Rect containing the new UV coordinates.</param>
    public void SetUVs(Rect uv)
    {
        uvRect = uv;

        SetBleedCompensation();

        if (autoResize || pixelPerfect)
            CalcSize();
    }

    /// <summary>
    /// Sets the sprite's UVs from pixel coordinates.
    /// </summary>
    /// <param name="pxCoords">A rect containing the pixel coordinates.</param>
    public void SetUVsFromPixelCoords(Rect pxCoords)
    {
        tempUV = PixelCoordToUVCoord((int)pxCoords.x, (int)pxCoords.y);
        uvRect.x = tempUV.x;
        uvRect.y = tempUV.y;

        tempUV = PixelCoordToUVCoord((int)pxCoords.xMax, (int)pxCoords.yMax);
        uvRect.xMax = tempUV.x;
        uvRect.yMax = tempUV.y;

        SetBleedCompensation();

        if (autoResize || pixelPerfect)
            CalcSize();
    }

    /// <summary>
    /// Sets the anchor method to use.
    /// See <see cref="ANCHOR_METHOD"/>
    /// </summary>
    /// <param name="a">The anchor method to use.</param>
    public void SetAnchor(ANCHOR_METHOD a)
    {
        anchor = a;

        SetSize(width, height);
    }

    /// <summary>
    /// Sets the offset of the sprite from its
    /// GameObject.
    /// See <see cref="offset"/>
    /// </summary>
    /// <param name="o">The offset to use.</param>
    public void SetOffset(Vector3 o)
    {

        offset = o;
        SetSize(width, height);
    }


    //--------------------------------------------------------------
    // Utility methods:
    //--------------------------------------------------------------

    /// <summary>
    /// Converts pixel-space values to UV-space scalar values
    /// according to the currently assigned material.
    /// NOTE: This is for converting widths and heights-not
    /// coordinates (which have reversed Y-coordinates).
    /// For coordinates, use <see cref="PixelCoordToUVCoord"/>()!
    /// </summary>
    /// <param name="xy">The values to convert.</param>
    /// <returns>The values converted to UV space.</returns>
    public Vector2 PixelSpaceToUVSpace(Vector2 xy)
    {
        if (texture == null)
            return Vector2.zero;

        return new Vector2(xy.x / ((float)texture.width), xy.y / ((float)texture.height));
    }

    /// <summary>
    /// Converts pixel-space values to UV-space scalar values
    /// according to the currently assigned material.
    /// NOTE: This is for converting widths and heights-not
    /// coordinates (which have reversed Y-coordinates).
    /// For coordinates, use <see cref="PixelCoordToUVCoord"/>()!
    /// </summary>
    /// <param name="x">The X-value to convert.</param>
    /// <param name="y">The Y-value to convert.</param>
    /// <returns>The values converted to UV space.</returns>
    public Vector2 PixelSpaceToUVSpace(int x, int y)
    {
        return PixelSpaceToUVSpace(new Vector2((float)x, (float)y));
    }

    /// <summary>
    /// Converts pixel coordinates to UV coordinates according to
    /// the currently assigned material.
    /// NOTE: This is for converting coordinates and will reverse
    /// the Y component accordingly.  For converting widths and
    /// heights, use <see cref="PixelSpaceToUVSpace"/>()!
    /// </summary>
    /// <param name="xy">The coordinates to convert.</param>
    /// <returns>The coordinates converted to UV coordinates.</returns>
    public Vector2 PixelCoordToUVCoord(Vector2 xy)
    {
        Vector2 p = PixelSpaceToUVSpace(xy);
        p.y = 1.0f - p.y;
        return p;
    }

    /// <summary>
    /// Converts pixel coordinates to UV coordinates according to
    /// the currently assigned material.
    /// NOTE: This is for converting coordinates and will reverse
    /// the Y component accordingly.  For converting widths and
    /// heights, use <see cref="PixelSpaceToUVSpace"/>()!
    /// </summary>
    /// <param name="x">The x-coordinate to convert.</param>
    /// <param name="y">The y-coordinate to convert.</param>
    /// <returns>The coordinates converted to UV coordinates.</returns>
    public Vector2 PixelCoordToUVCoord(int x, int y)
    {
        return PixelCoordToUVCoord(new Vector2((float)x, (float)y));
    }
}



// Mirrors the editable settings of a sprite that affect
// how the sprite is drawn in the scene view
public class SpriteBaseMirror
{
    public SpriteBase.SPRITE_PLANE plane;
    public SpriteBase.WINDING_ORDER winding;
    public float width, height;
    public Vector2 bleedCompensation;
    public SpriteBase.ANCHOR_METHOD anchor;
    public Vector3 offset;
    public Color color;
    public bool pixelPerfect;
    public bool autoResize;

    // Mirrors the specified sprite's settings
    public virtual void Mirror(SpriteBase s)
    {
        plane = s.plane;
        winding = s.winding;
        width = s.width;
        height = s.height;
        bleedCompensation = s.bleedCompensation;
        anchor = s.anchor;
        offset = s.offset;
        color = s.color;
        pixelPerfect = s.pixelPerfect;
        autoResize = s.autoResize;
    }

    // Validates certain settings:
    public virtual bool Validate(SpriteBase s)
    {
        if (s.pixelPerfect)
        {
            s.autoResize = true;
        }

        return true;
    }

    // Returns true if any of the settings do not match:
    public virtual bool DidChange(SpriteBase s)
    {
        if (s.plane != plane)
            return true;
        if (s.winding != winding)
            return true;
        if (s.width != width)
            return true;
        if (s.height != height)
            return true;
        if (s.bleedCompensation != bleedCompensation)
            return true;
        if (s.anchor != anchor)
            return true;
        if (s.offset != offset)
            return true;
        if (s.color.r != color.r ||
            s.color.g != color.g ||
            s.color.b != color.b ||
            s.color.a != color.a)
            return true;
        if (s.pixelPerfect != pixelPerfect)
            return true;
        if (s.autoResize != autoResize)
            return true;

        return false;
    }
}
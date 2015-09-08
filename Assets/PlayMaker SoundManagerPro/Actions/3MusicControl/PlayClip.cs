using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Plays the song (this is not a SFX) by crossing out what's currently playing regardless of a SoundConnection.\nCan call an event when the clip is done. You can use that event to resume a SoundConnection if you want.\nCan either use a variable or an AudioClip file.")]
public class PlayClip : FsmStateAction
{
	[ActionSection("Clip Load (Choose One Method)")]
	[UIHint(UIHint.Variable)]
	[Title("Either, Clip Variable*")]
	[HutongGames.PlayMaker.Tooltip("You can either load a clip from a variable...")]
	public FsmObject clip;
	
	[Title("Or, Clip Object*")]
	[HutongGames.PlayMaker.Tooltip("...or load a clip from your project. One method is required.")]
	[ObjectType(typeof(AudioClip))]
	public FsmObject clipObj;
	[ActionSection("Other Parameters")]
	
	[UIHint(UIHint.FsmBool)]
	[HutongGames.PlayMaker.Tooltip("Optionally set whether the clip should loop (as ContinousPlayThrough or OncePlayThrough).")]
	public FsmBool loop;
	
	[HutongGames.PlayMaker.Tooltip("Optionally set an Event to send when the AudioClip finished playing.")]
	public FsmEvent finishedEvent;
	
	
	public override void Reset()
	{
		clip = null;
		clipObj = null;
		loop = null;
		finishedEvent = null;
	}

	public override void OnEnter()
	{
		bool guaranteedLoop;
		if(loop.IsNone)
			guaranteedLoop = false;
		else
			guaranteedLoop = loop.Value;
		
		if(finishedEvent != null)
		{
			if(!clip.IsNone && clip.Value != null)
				SoundManager.Play(clip.Value as AudioClip, guaranteedLoop, SongEnd);
			else if(!clipObj.IsNone && clipObj.Value != null)
				SoundManager.Play(clipObj.Value as AudioClip, guaranteedLoop, SongEnd);
		}
		else
		{
			if(!clip.IsNone && clip.Value != null)
				SoundManager.Play(clip.Value as AudioClip, guaranteedLoop);
			else if(!clipObj.IsNone && clipObj.Value != null)
				SoundManager.Play(clipObj.Value as AudioClip, guaranteedLoop);
			
			Finish();
		}
	}

	public override string ErrorCheck()
	{
		if((clip.IsNone) && (clipObj.IsNone || clipObj.Value == null))
			return "You must specify a clip! Either by Variable or by AudioClip file.";
		
		return null;
	}
	
	void SongEnd()
	{
		Fsm.Event(finishedEvent);
		Finish();
	}
}

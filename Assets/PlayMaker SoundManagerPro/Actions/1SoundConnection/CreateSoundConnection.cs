using UnityEngine;
using System.Collections.Generic;
using HutongGames.PlayMaker;


[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Create a SoundConnection and store it in a Variable.  Use before any function that uses a SoundConnection.  Can store up to 10 songs.")]
public class CreateSoundconnection : FsmStateAction
{
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("The name of the scene for this SoundConnection.")]
	public FsmString levelName;
	
	[RequiredField]
	[UIHint(UIHint.Variable)]
	[Title("SoundConnection")]
	[HutongGames.PlayMaker.Tooltip("Variable(Object) to store the created SoundConnection in.")]
	public FsmObject soundConnection;
	
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Play Method type.")]
	public SoundManager.PlayMethod playMethod;
	
	[Title("AudioClip 1")]
	[ObjectType(typeof(AudioClip))]
	public FsmObject clip1;
	
	[Title("AudioClip 2")]
	[ObjectType(typeof(AudioClip))]
	public FsmObject clip2;
	
	[Title("AudioClip 3")]
	[ObjectType(typeof(AudioClip))]
	public FsmObject clip3;
	
	[Title("AudioClip 4")]
	[ObjectType(typeof(AudioClip))]
	public FsmObject clip4;
	
	[Title("AudioClip 5")]
	[ObjectType(typeof(AudioClip))]
	public FsmObject clip5;
	
	[Title("AudioClip 6")]
	[ObjectType(typeof(AudioClip))]
	public FsmObject clip6;
	
	[Title("AudioClip 7")]
	[ObjectType(typeof(AudioClip))]
	public FsmObject clip7;
	
	[Title("AudioClip 8")]
	[ObjectType(typeof(AudioClip))]
	public FsmObject clip8;
	
	[Title("AudioClip 9")]
	[ObjectType(typeof(AudioClip))]
	public FsmObject clip9;
	
	[Title("AudioClip 10")]
	[ObjectType(typeof(AudioClip))]
	public FsmObject clip10;
	
	public FsmFloat delay;
	public FsmFloat minDelay;
	public FsmFloat maxDelay;
	
	public override void Reset()
	{
		levelName = new FsmString { Value = Application.loadedLevelName };
		soundConnection = null;
		playMethod = SoundManager.PlayMethod.ContinuousPlayThrough;
		clip1 = clip2 = clip3 = clip4 = clip5 = clip6 = clip7 = clip8 = clip9 = clip10 = null;
		
		delay = new FsmFloat { UseVariable = true };
		minDelay = new FsmFloat { UseVariable = true };
		maxDelay = new FsmFloat { UseVariable = true };
	}

	public override void OnEnter()
	{
		List<AudioClip> clips = new List<AudioClip>();
		
		if(!clip1.IsNone && clip1.Value != null)
			clips.Add(clip1.Value as AudioClip);
		if(!clip2.IsNone && clip2.Value != null)
			clips.Add(clip2.Value as AudioClip);
		if(!clip3.IsNone && clip3.Value != null)
			clips.Add(clip3.Value as AudioClip);
		if(!clip4.IsNone && clip4.Value != null)
			clips.Add(clip4.Value as AudioClip);
		if(!clip5.IsNone && clip5.Value != null)
			clips.Add(clip5.Value as AudioClip);
		if(!clip6.IsNone && clip6.Value != null)
			clips.Add(clip6.Value as AudioClip);
		if(!clip7.IsNone && clip7.Value != null)
			clips.Add(clip7.Value as AudioClip);
		if(!clip8.IsNone && clip8.Value != null)
			clips.Add(clip8.Value as AudioClip);
		if(!clip9.IsNone && clip9.Value != null)
			clips.Add(clip9.Value as AudioClip);
		if(!clip10.IsNone && clip10.Value != null)
			clips.Add(clip10.Value as AudioClip);
		
		SoundConnection sC;
		switch(playMethod)
		{
		case SoundManager.PlayMethod.ContinuousPlayThrough:
		case SoundManager.PlayMethod.OncePlayThrough:
		case SoundManager.PlayMethod.ShufflePlayThrough:
			sC = SoundManager.CreateSoundConnection(levelName.Value, playMethod, clips.ToArray());
			break;
		case SoundManager.PlayMethod.ContinuousPlayThroughWithDelay:
		case SoundManager.PlayMethod.OncePlayThroughWithDelay:
		case SoundManager.PlayMethod.ShufflePlayThroughWithDelay:
			sC = SoundManager.CreateSoundConnection(levelName.Value, playMethod, delay.Value, clips.ToArray());
			break;
		case SoundManager.PlayMethod.ContinuousPlayThroughWithRandomDelayInRange:
		case SoundManager.PlayMethod.OncePlayThroughWithRandomDelayInRange:
		case SoundManager.PlayMethod.ShufflePlayThroughWithRandomDelayInRange:
			sC = SoundManager.CreateSoundConnection(levelName.Value, playMethod, minDelay.Value, maxDelay.Value, clips.ToArray());
			break;
		default:
			sC = SoundManager.CreateSoundConnection(levelName.Value, SoundManager.PlayMethod.ContinuousPlayThrough, clips.ToArray());
			break;
		}
		
		SoundConnectionWrapper wrapper = ScriptableObject.CreateInstance<SoundConnectionWrapper>();
		wrapper.Init(sC);
		soundConnection.Value = wrapper;
		
		Finish();
	}
	
	public override string ErrorCheck ()
	{
		if(levelName.IsNone || string.IsNullOrEmpty(levelName.Value))
			return "You must specify a level name!";
		
		switch(playMethod)
		{
		case SoundManager.PlayMethod.ContinuousPlayThrough:
		case SoundManager.PlayMethod.OncePlayThrough:
		case SoundManager.PlayMethod.ShufflePlayThrough:
			break;
		case SoundManager.PlayMethod.ContinuousPlayThroughWithDelay:
		case SoundManager.PlayMethod.OncePlayThroughWithDelay:
		case SoundManager.PlayMethod.ShufflePlayThroughWithDelay:
			if(delay.IsNone)
				return "You must specify the delay parameter if you are going to use this PlayMethod.";
			break;
		case SoundManager.PlayMethod.ContinuousPlayThroughWithRandomDelayInRange:
		case SoundManager.PlayMethod.OncePlayThroughWithRandomDelayInRange:
		case SoundManager.PlayMethod.ShufflePlayThroughWithRandomDelayInRange:
			if(minDelay.IsNone)
				return "You must specify the Min Delay parameter if you are going to use this PlayMethod.";
			if(maxDelay.IsNone)
				return "You must specify the Max Delay parameter if you are going to use this PlayMethod.";
			if(maxDelay.Value < minDelay.Value)
				return "Max Delay must be greater than or equal to the Min Delay";
			break;
		}
		return null;
	}
}

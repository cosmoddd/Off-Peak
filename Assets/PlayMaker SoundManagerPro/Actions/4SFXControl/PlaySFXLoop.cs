using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Plays a SFX in a loop on another GameObject of your choice.  This function is catered more towards customizing the loop.\nYou can set the loop to end when the object dies or a maximum duration, whichever comes first.")]
public class PlaySfxLoop : FsmStateAction
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
	
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Object the SFX will be played on.  If it doesn't have an AudioSource, one will be added automatically.")]
	public FsmOwnerDefault gameObject;
	
	[HutongGames.PlayMaker.Tooltip("Optionally set whether the object will loop until it is destroyed, OR to respect the maxDuration--whichever comes first.")]
	public FsmBool tillDestroy;
	
	[HutongGames.PlayMaker.Tooltip("Optionally set the max duration of the loop.  If it is reached, the loop will stop.")]
	public FsmFloat maxDuration;
	
	[HasFloatSlider(0f, 1f)]
	[HutongGames.PlayMaker.Tooltip("Optionally, set the volume the SFX is played at.  If not specified, it will use the current SFX volume set.")]
	public FsmFloat volume;
	
	[HutongGames.PlayMaker.Tooltip("Optionally, set the pitch the SFX is played at.  If not specified, it will use the current SFX pitch set.")]
	public FsmFloat pitch;
	
	GameObject obj;
	
	
	public override void Reset()
	{
		clip = null;
		clipObj = null;
		gameObject = null;
		tillDestroy = new FsmBool { UseVariable = true };
		maxDuration = new FsmFloat { UseVariable = true };
		volume = new FsmFloat { UseVariable = true };
		pitch = new FsmFloat { UseVariable = true };
	}

	public override void OnEnter()
	{
		obj = Fsm.GetOwnerDefaultTarget(gameObject);
		
		bool guaranteedTillDestroy;
		float guaranteedPitch, guaranteedVolume, guaranteedMaxDuration;
		
		if(tillDestroy.IsNone)
			guaranteedTillDestroy = true;
		else
			guaranteedTillDestroy = tillDestroy.Value;
		
		if(maxDuration.IsNone || float.IsNaN(maxDuration.Value))
			guaranteedMaxDuration = 0f;
		else
			guaranteedMaxDuration = maxDuration.Value;
		
		if(pitch.IsNone || float.IsNaN(pitch.Value))
			guaranteedPitch = SoundManager.Instance.pitchSFX;
		else
			guaranteedPitch = pitch.Value;
		
		if(volume.IsNone || float.IsNaN(volume.Value))
			guaranteedVolume = SoundManager.Instance.volumeSFX;
		else
			guaranteedVolume = volume.Value;
			
		if(!clip.IsNone && clip.Value != null)
			SoundManager.PlaySFXLoop(obj, clip.Value as AudioClip, guaranteedTillDestroy, guaranteedVolume, guaranteedPitch, guaranteedMaxDuration);
		else if(!clipObj.IsNone && clipObj.Value != null)
			SoundManager.PlaySFXLoop(obj, clipObj.Value as AudioClip, guaranteedTillDestroy, guaranteedVolume, guaranteedPitch, guaranteedMaxDuration);
		
		Finish();
	}

	public override string ErrorCheck()
	{
		if((clip.IsNone) && (clipObj.IsNone || clipObj.Value == null))
			return "You must specify a clip! Either by Variable or by AudioClip file.";
		
		return null;
	}
}

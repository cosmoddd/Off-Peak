using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Plays a SFX on another GameObject of your choice.")]
public class PlaySfxOnGameObject : FsmStateAction
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
	
	[UIHint(UIHint.FsmBool)]
	[HutongGames.PlayMaker.Tooltip("Optionally set whether the SFX will loop.  This will default to false.  It is a simpler version of PlaySFXLoop, for more customization use that.")]
	public FsmBool loop;
	
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
		loop = null;
		volume = new FsmFloat { UseVariable = true };
		pitch = new FsmFloat { UseVariable = true };
	}

	public override void OnEnter()
	{
		obj = Fsm.GetOwnerDefaultTarget(gameObject);
		
		float guaranteedPitch, guaranteedVolume;
		bool guaranteedLoop;
		
		if(pitch.IsNone || float.IsNaN(pitch.Value))
			guaranteedPitch = SoundManager.Instance.pitchSFX;
		else
			guaranteedPitch = pitch.Value;
		
		if(volume.IsNone || float.IsNaN(volume.Value))
			guaranteedVolume = SoundManager.Instance.volumeSFX;
		else
			guaranteedVolume = volume.Value;
		
		if(loop.IsNone)
			guaranteedLoop = false;
		else
			guaranteedLoop = loop.Value;
			
		if(!clip.IsNone && clip.Value != null)
			SoundManager.PlaySFX(obj, clip.Value as AudioClip, guaranteedLoop, guaranteedVolume, guaranteedPitch);
		else if(!clipObj.IsNone && clipObj.Value != null)
			SoundManager.PlaySFX(obj, clipObj.Value as AudioClip, guaranteedLoop, guaranteedVolume, guaranteedPitch);
		
		Finish();
	}

	public override string ErrorCheck()
	{
		if((clip.IsNone) && (clipObj.IsNone || clipObj.Value == null))
			return "You must specify a clip! Either by Variable or by AudioClip file.";
		
		return null;
	}
}

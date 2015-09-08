using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Plays a SFX on an owned object.")]
public class PlayPooledSfx : FsmStateAction
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
	
	[HasFloatSlider(0f, 1f)]
	[HutongGames.PlayMaker.Tooltip("Optionally, set the volume the SFX is played at.  If not specified, it will use the current SFX volume set.")]
	public FsmFloat volume;
	
	[HutongGames.PlayMaker.Tooltip("Optionally, set the pitch the SFX is played at.  If not specified, it will use the current SFX pitch set.")]
	public FsmFloat pitch;
	
	[HutongGames.PlayMaker.Tooltip("Optionally if a 3D sound, set the location the SFX is played at.  If not specified, it will use Vector3.zero.")]
	public FsmVector3 location;
	
	
	public override void Reset()
	{
		clip = null;
		clipObj = null;
		volume = new FsmFloat { UseVariable = true };
		pitch = new FsmFloat { UseVariable = true };
		location = new FsmVector3 { UseVariable = true };
	}

	public override void OnEnter()
	{
		Vector3 guaranteedLocation;
		float guaranteedPitch, guaranteedVolume;
		
		if(location.IsNone)
			guaranteedLocation = Vector3.zero;
		else
			guaranteedLocation = location.Value;
		
		if(pitch.IsNone || float.IsNaN(pitch.Value))
			guaranteedPitch = SoundManager.Instance.pitchSFX;
		else
			guaranteedPitch = pitch.Value;
		
		if(volume.IsNone || float.IsNaN(volume.Value))
			guaranteedVolume = SoundManager.Instance.volumeSFX;
		else
			guaranteedVolume = volume.Value;
			
		if(!clip.IsNone && clip.Value != null)
			SoundManager.PlaySFX(clip.Value as AudioClip, false, 0f, guaranteedVolume, guaranteedPitch, guaranteedLocation);
		else if(!clipObj.IsNone && clipObj.Value != null)
			SoundManager.PlaySFX(clipObj.Value as AudioClip, false, 0f, guaranteedVolume, guaranteedPitch, guaranteedLocation);
		
		Finish();
	}

	public override string ErrorCheck()
	{
		if((clip.IsNone) && (clipObj.IsNone || clipObj.Value == null))
			return "You must specify a clip! Either by Variable or by AudioClip file.";
		
		return null;
	}
}

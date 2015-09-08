using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Sets pitch of sfx.")]
public class SetSfxPitch : FsmStateAction
{
	[RequiredField]
	[UIHint(UIHint.FsmFloat)]
	[HutongGames.PlayMaker.Tooltip("The pitch value to be set.")]
	public FsmFloat pitch;
	
	public override void Reset()
	{
		pitch = null;
	}

	public override void OnEnter()
	{
		SoundManager.SetPitchSFX(pitch.Value);
		
		Finish();
	}
}


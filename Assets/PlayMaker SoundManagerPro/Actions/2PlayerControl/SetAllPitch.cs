using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Sets pitch of all audio, sfx and music")]
public class SetAllPitch : FsmStateAction
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
		SoundManager.SetPitch(pitch.Value);
		
		Finish();
	}
}


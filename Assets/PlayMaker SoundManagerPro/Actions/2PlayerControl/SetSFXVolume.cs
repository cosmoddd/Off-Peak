using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Sets volume of sfx.")]
public class SetSfxVolume : FsmStateAction
{
	[RequiredField]
	[UIHint(UIHint.FsmFloat)]
	[HutongGames.PlayMaker.Tooltip("The volume value to be set.")]
	public FsmFloat volume;
	
	public override void Reset()
	{
		volume = null;
	}

	public override void OnEnter()
	{
		SoundManager.SetVolumeSFX(volume.Value);
		
		Finish();
	}
}


using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Sets volume of all audio, sfx and music")]
public class SetAllVolume : FsmStateAction
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
		SoundManager.SetVolume(volume.Value);
		
		Finish();
	}
}


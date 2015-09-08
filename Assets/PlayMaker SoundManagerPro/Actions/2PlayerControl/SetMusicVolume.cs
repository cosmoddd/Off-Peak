using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Sets volume of music.")]
public class SetMusicVolume : FsmStateAction
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
		SoundManager.SetVolumeMusic(volume.Value);
		
		Finish();
	}
}


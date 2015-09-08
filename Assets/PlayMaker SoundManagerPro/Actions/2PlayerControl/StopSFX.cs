using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Stop all sfx.")]
public class StopSfx : FsmStateAction
{
	public override void OnEnter()
	{
		SoundManager.StopSFX();
		
		Finish();
	}
}


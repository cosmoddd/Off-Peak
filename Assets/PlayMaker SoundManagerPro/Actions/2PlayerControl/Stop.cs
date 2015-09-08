using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Stop all audio, sfx and music, immediately.")]
public class Stop : FsmStateAction
{
	public override void OnEnter()
	{
		SoundManager.Stop();
		
		Finish();
	}
}


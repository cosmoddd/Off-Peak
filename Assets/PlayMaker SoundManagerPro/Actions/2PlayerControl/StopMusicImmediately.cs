using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Stops all music immediately.")]
public class StopMusicImmediately : FsmStateAction
{
	public override void OnEnter()
	{
		SoundManager.StopMusicImmediately();
		
		Finish();
	}
}


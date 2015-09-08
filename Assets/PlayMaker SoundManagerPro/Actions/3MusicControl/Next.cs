using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Go to the next song in the current SoundConnection playlist.")]
public class Next : FsmStateAction
{
	public override void OnEnter()
	{
		SoundManager.Next();
		
		Finish();
	}
}


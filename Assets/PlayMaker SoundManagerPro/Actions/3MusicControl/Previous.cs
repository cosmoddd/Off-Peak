using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Go to the previous song in the current SoundConnection playlist.")]
public class Previous : FsmStateAction
{
	public override void OnEnter()
	{
		SoundManager.Prev();
		
		Finish();
	}
}
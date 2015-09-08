using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Crosses out all music.")]
public class StopMusic : FsmStateAction
{
	public override void OnEnter()
	{
		SoundManager.StopMusic();
		
		Finish();
	}
}


using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Pauses/Resumes all sounds. Can optionally force the pause state.")]
public class Pause : FsmStateAction
{
	[Title("Force Pause?")]
	[HutongGames.PlayMaker.Tooltip("If set, will force pause to true or false.  If not set, will just toggle pause status.")]
	public FsmBool toggle;
	
	public override void Reset()
	{
		toggle = new FsmBool { UseVariable = true };
	}

	public override void OnEnter()
	{
		if(toggle.IsNone)
		{
			if(SoundManager.IsPaused())
				SoundManager.UnPause();
			else
				SoundManager.Pause();
		}
		else
			SoundManager.PauseToggle();
		
		Finish();
	}
}

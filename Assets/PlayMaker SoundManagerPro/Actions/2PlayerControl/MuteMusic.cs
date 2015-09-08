using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Mutes/Unmutes all music.")]
public class MuteMusic : FsmStateAction
{
	[Title("Force Mute?")]
	[HutongGames.PlayMaker.Tooltip("If set, will force mute to true or false.  If not set, will just toggle mute status.")]
	public FsmBool toggle;
	
	public override void Reset()
	{
		toggle = new FsmBool { UseVariable = true };
	}

	public override void OnEnter()
	{
		if(toggle.IsNone)
			SoundManager.MuteMusic();
		else
			SoundManager.MuteMusic(toggle.Value);
		
		Finish();
	}
}

using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Replaces the SoundConnection at the level specified in the SoundConnection.\nIf a SoundConnection doesn't exist, it will just add it.\nMust have a SoundConnection stored as a variable (use CreateSoundConnection first)")]
public class ReplaceSoundConnection : FsmStateAction
{
	[RequiredField]
	[UIHint(UIHint.Variable)]
	[HutongGames.PlayMaker.Tooltip("SoundConnection to replace.  Store it in a Variable(Object) with CreateSoundConnection.")]
	public FsmObject soundConnection;
	
	public override void Reset()
	{
		soundConnection = null;
	}

	public override void OnEnter()
	{
		SoundManager.ReplaceSoundConnection((soundConnection.Value as SoundConnectionWrapper).soundConnection);
		
		Finish();
	}
}


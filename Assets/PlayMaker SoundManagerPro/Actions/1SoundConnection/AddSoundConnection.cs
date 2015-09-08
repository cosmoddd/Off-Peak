using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Add a SoundConnection to SoundManagerPro.  Must have a SoundConnection stored as a variable (use CreateSoundConnection first)")]
public class AddSoundconnection : FsmStateAction
{
	[RequiredField]
	[UIHint(UIHint.Variable)]
	[Title("SoundConnection")]
	[HutongGames.PlayMaker.Tooltip("SoundConnection to add.  Store it in a Variable(Object) with CreateSoundConnection.")]
	public FsmObject soundConnection;
	
	public override void Reset()
	{
		soundConnection = null;
	}

	public override void OnEnter()
	{
		SoundManager.AddSoundConnection((soundConnection.Value as SoundConnectionWrapper).soundConnection);
		
		Finish();
	}
}


using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Plays the SoundConnection right then, regardless of what you put at the level parameter of the SoundConnection.\nMust have a SoundConnection stored as a variable (use CreateSoundConnection first), or reference it by level name on the SoundManager.")]
public class PlaySoundconnection : FsmStateAction
{
	[UIHint(UIHint.Variable)]
	[Title("Either, Existing Variable*")]
	[HutongGames.PlayMaker.Tooltip("You can either load a SoundConnection from a variable to play(Store it in a Variable[Object] with CreateSoundConnection)...")]
	public FsmObject soundConnection;
	
	[UIHint(UIHint.FsmString)]
	[Title("Or, Existing Level Name*")]
	[HutongGames.PlayMaker.Tooltip("...or load a SoundConnection by level name on the SoundManager. One method is required.")]
	public FsmString levelName;
	
	public override void Reset()
	{
		soundConnection = null;
		levelName = null;
	}

	public override void OnEnter()
	{
		if(!soundConnection.IsNone && soundConnection.Value != null)
			SoundManager.PlayConnection((soundConnection.Value as SoundConnectionWrapper).soundConnection);
		else if(!levelName.IsNone && !string.IsNullOrEmpty(levelName.Value))
			SoundManager.PlayConnection(levelName.Value);
		
		Finish();
	}
}


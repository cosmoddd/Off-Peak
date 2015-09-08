using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Remove a SoundConnection for a level.")]
public class RemoveSoundconnection : FsmStateAction
{
	[RequiredField]
	[UIHint(UIHint.Variable)]
	[Title("Level To Remove")]
	[HutongGames.PlayMaker.Tooltip("Will remove the SoundConnection at this level.")]
	public FsmString levelName;
	
	public override void Reset()
	{
		levelName = null;
	}

	public override void OnEnter()
	{
		SoundManager.RemoveSoundConnectionForLevel(levelName.Value);
		
		Finish();
	}
}


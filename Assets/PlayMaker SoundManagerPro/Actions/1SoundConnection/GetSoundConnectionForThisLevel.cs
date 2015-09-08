using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Gets the SoundConnection for a specified level.")]
public class GetSoundconnectionForThisLevel : FsmStateAction
{
	[RequiredField]
	[UIHint(UIHint.Variable)]
	[Title("SoundConnection")]
	[HutongGames.PlayMaker.Tooltip("Variable(Object) where the SoundConnection will be loaded into.")]
	public FsmObject soundConnection;
	
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Name of the level you want the SoundConnection from.")]
	public FsmString levelName;
	
	public override void Reset()
	{
		soundConnection = null;
		levelName = null;
	}

	public override void OnEnter()
	{
		SoundConnectionWrapper wrapper = ScriptableObject.CreateInstance<SoundConnectionWrapper>();
		wrapper.Init(SoundManager.GetSoundConnectionForThisLevel(levelName.Value));
		soundConnection.Value = wrapper;
		
		Finish();
	}
}
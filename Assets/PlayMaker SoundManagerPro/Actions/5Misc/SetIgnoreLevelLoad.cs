using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Activates/Deactivates SoundManaging AI to respond to level load.")]
public class SetIgnoreLevelLoad : FsmStateAction
{
	[RequiredField]
	[UIHint(UIHint.FsmBool)]
	[HutongGames.PlayMaker.Tooltip("The ignore value to be set.")]
	public FsmBool ignore;
	
	public override void Reset()
	{
		ignore = null;
	}

	public override void OnEnter()
	{
		SoundManager.SetIgnoreLevelLoad(ignore.Value);
		
		Finish();
	}
}


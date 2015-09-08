using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Loads a random clip from a SFX Group into a Variable(Object) using SoundManager.LoadClipFromGroup. Run this before Actions that use it.")]
public class LoadRandomClipFromGroup : FsmStateAction
{
	[RequiredField]
	[UIHint(UIHint.Variable)]
	[Title("AudioClip Variable")]
	[HutongGames.PlayMaker.Tooltip("Variable(Object) where the AudioClip will be loaded into.")]
	public FsmObject clip;
	
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Name of the Group")]
	public FsmString groupName;
	
	public override void Reset()
	{
		clip = null;
	}

	public override void OnEnter()
	{
		
		clip.Value = SoundManager.LoadFromGroup(groupName.Value);
		
		Finish();
	}
}

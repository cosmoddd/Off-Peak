using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Load a clip into a Variable(Object) using SoundManager.Load. Run this before Actions that use it.\nIf Custom Path is not null nor empty, it wil try to load from the Custom Path.\nThen, it will check the Stored SFX on SoundManager.\nIf that fails, it will lastly check the SoundManager default RESOURCES_PATH.")]
public class LoadClip : FsmStateAction
{
	[RequiredField]
	[UIHint(UIHint.Variable)]
	[Title("AudioClip Variable")]
	[HutongGames.PlayMaker.Tooltip("Variable(Object) where the AudioClip will be loaded into.")]
	public FsmObject clip;
	
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Name of the AudioClip")]
	public FsmString clipName;
	
	[HutongGames.PlayMaker.Tooltip("Optionally, set a custom path for it to look from the Resources folder.")]
	public FsmString customPath;
	
	public override void Reset()
	{
		clip = null;
	}

	public override void OnEnter()
	{
		if(customPath.IsNone)
			clip.Value = SoundManager.Load(clipName.Value);
		else
			clip.Value = SoundManager.Load(clipName.Value, customPath.Value);
		
		Finish();
	}
}

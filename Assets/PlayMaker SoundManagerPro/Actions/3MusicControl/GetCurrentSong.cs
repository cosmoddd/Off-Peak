using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Gets the current song playing in SoundManager.")]
public class GetCurrentSong : FsmStateAction
{
	[RequiredField]
	[UIHint(UIHint.Variable)]
	[Title("AudioClip Variable")]
	[HutongGames.PlayMaker.Tooltip("Variable(Object) where the AudioClip will be loaded into.")]
	public FsmObject clip;
	
	public override void Reset()
	{
		clip = null;
	}

	public override void OnEnter()
	{
		clip.Value = SoundManager.GetCurrentSong();
		
		Finish();
	}
}

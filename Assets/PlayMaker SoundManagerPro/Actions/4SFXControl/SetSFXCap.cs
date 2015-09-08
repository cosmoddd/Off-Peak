using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Sets SFX cap, used when PlayCappedSFX is called.")]
public class SetSfxCap : FsmStateAction
{
	[RequiredField]
	[UIHint(UIHint.FsmInt)]
	[HutongGames.PlayMaker.Tooltip("The cap value to be set.")]
	public FsmInt cap;
	
	public override void Reset()
	{
		cap = null;
	}

	public override void OnEnter()
	{
		SoundManager.SetSFXCap(cap.Value);
		
		Finish();
	}
	
	public override string ErrorCheck ()
	{
		if(cap.Value < 0)
			return "You need a positive cap amount.";
		return null;
	}
}


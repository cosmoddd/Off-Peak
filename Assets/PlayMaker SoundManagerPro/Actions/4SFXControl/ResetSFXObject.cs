using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Reset a SFX object to default settings.  Not much use outside of SoundManager internal code.")]
public class ResetSfxObject : FsmStateAction
{
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("The SFX Pooled Object")]
	FsmOwnerDefault gameObject;
	
	GameObject obj;
	
	public override void Reset()
	{
		gameObject = null;
	}

	public override void OnEnter()
	{
		obj = Fsm.GetOwnerDefaultTarget(gameObject);
		
		SoundManager.ResetSFXObject(obj);
		
		Finish();
	}
}

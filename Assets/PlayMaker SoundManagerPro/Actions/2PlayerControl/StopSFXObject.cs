using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Stops SFX playing on a GameObject.  Mainly used in response to PlaySFXLoop.")]
public class StopSfxObject : FsmStateAction
{
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("The SFX Object you wish to stop.")]
	public FsmOwnerDefault gameObject;
	
	GameObject obj;
	
	public override void Reset ()
	{
		gameObject = null;
	}
	
	public override void OnEnter()
	{
		obj = Fsm.GetOwnerDefaultTarget(gameObject);
		
		SoundManager.StopSFXObject(obj);
		
		Finish();
	}
}


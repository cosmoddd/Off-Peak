using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Stores a SFX in SoundManager for easy, quick and efficient access.  It is wiser to store them beforehand on SoundManager, or add them in your beginning scene.")]
public class StoreSfx : FsmStateAction
{
	[RequiredField]
	[ObjectType(typeof(AudioClip))]
	[HutongGames.PlayMaker.Tooltip("The clip to store in SoundManager.")]
	public FsmObject clipObj;
	
	public override void Reset ()
	{
		clipObj = null;
	}
	
	public override void OnEnter()
	{		
		SoundManager.SaveSFX(clipObj.Value as AudioClip);
		
		Finish();
	}
}


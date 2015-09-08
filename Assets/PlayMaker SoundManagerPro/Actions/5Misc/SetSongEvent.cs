using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("SoundManagerPro")]
[HutongGames.PlayMaker.Tooltip("Add a SoundConnection to SoundManagerPro.  Must have a SoundConnection stored as a variable (use CreateSoundConnection first)")]
public class SetSongEvent : FsmStateAction
{
	public enum SongEvent
	{
		OnSongEnd,
		OnSongBegin,
		OnCrossIn,
		OnCrossOut
	}
	
	[RequiredField]
	public SongEvent songEvent;
	
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Target to receive event.")]
	public FsmEventTarget eventTarget;
	
	[RequiredField]
	[UIHint(UIHint.FsmEvent)]
	[Title("Event To Send")]
	[HutongGames.PlayMaker.Tooltip("Event to send.")]
	public FsmEvent evt;
	
	public override void Reset()
	{
		songEvent = SongEvent.OnSongEnd;
		evt = null;
	}

	public override void OnEnter()
	{
		switch(songEvent)
		{
		case SongEvent.OnSongEnd:
			SoundManager.Instance.OnSongEnd += TriggerEvent;
			return;
		case SongEvent.OnSongBegin:
			SoundManager.Instance.OnSongBegin += TriggerEvent;
			return;
		case SongEvent.OnCrossIn:
			SoundManager.Instance.OnCrossInBegin += TriggerEvent;
			return;
		case SongEvent.OnCrossOut:
			SoundManager.Instance.OnCrossOutBegin += TriggerEvent;
			return;
		}
		Finish();
	}
	
	void TriggerEvent()
	{
		Fsm.Event(eventTarget, evt);
		Finish();
	}
}


using UnityEngine;
using System.Collections;

public class SoundConnectionWrapper : ScriptableObject {

	public SoundConnection soundConnection;
	
	public void Init(SoundConnection sC)
	{
		soundConnection = sC;
	}
}

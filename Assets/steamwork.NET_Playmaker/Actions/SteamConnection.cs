// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;
using System.Collections;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("True if the client has a live connection to the Steam servers.\r\nFalse means there is no active connection due to either a networking issue on the local machine, or the Steam server is down/busy.")]
	public class SteamConnection : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmBool Connect;
		
		public override void Reset()
		{
			Connect = false;
		}
		
		public override void OnEnter()
		{
			Connect.Value = SteamUser.BLoggedOn();
		}
	}
}
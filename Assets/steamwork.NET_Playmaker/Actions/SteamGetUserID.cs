// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;
using System.Collections;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Get´s steam user ID.")]
	public class SteamGetUserID : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString userID;
		
		public override void Reset()
		{
			userID = null;
		}
		
		public override void OnEnter()
		{
			CSteamID ID = SteamUser.GetSteamID();
			userID.Value = ID.ToString();
		}
	}
}
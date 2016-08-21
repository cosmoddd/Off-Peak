// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;
using System.Collections;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Get´s steam user name.")]
	public class SteamGetUserName : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString userName;
		
		public override void Reset()
		{
			userName = null;
		}
		
		public override void OnEnter()
		{
			userName.Value = SteamFriends.GetPersonaName();
		}
	}
}
// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;
using System.Collections;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Get´s Friend ID by an int.")]
	public class SteamGetFriendIDByIndex : FsmStateAction
	{
		[RequiredField]
		public FsmInt friendNumber;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString friendName;

		public override void Reset()
		{
			friendNumber = null;
			friendName = null;
		}
		
		public override void OnEnter()
		{
			CSteamID friend = SteamFriends.GetFriendByIndex(friendNumber.Value,EFriendFlags.k_EFriendFlagAll);
			friendName.Value = friend.ToString();
		}
	}
}
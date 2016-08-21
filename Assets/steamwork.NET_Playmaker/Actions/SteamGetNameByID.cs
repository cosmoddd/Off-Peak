// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;
using System.Collections;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Get´s Friend Name By His ID.")]
	public class SteamGetNameByID : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString UserID;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString friendName;

		public override void Reset()
		{
			UserID = null;
			friendName = null;
		}
		
		public override void OnEnter()
		{
			ulong ID = ulong.Parse(UserID.Value);
			CSteamID FriendID = SteamUser.GetSteamID(); //not perfect but works!
			FriendID.m_SteamID = ID;
			friendName.Value = SteamFriends.GetFriendPersonaName(FriendID);
		}
	}
}
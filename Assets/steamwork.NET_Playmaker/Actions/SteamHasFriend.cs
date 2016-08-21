// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;
using System.Collections;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Check if a user is friend.")]
	public class SteamHasFriend : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString UserID;

		[UIHint(UIHint.Variable)]
		public FsmBool isFriend;

		public override void Reset()
		{
			UserID = null;
			isFriend = false;
		}
		
		public override void OnEnter()
		{
			ulong ID = ulong.Parse(UserID.Value);
			CSteamID FriendID = SteamUser.GetSteamID(); //not perfect but works!
			FriendID.m_SteamID = ID;

			isFriend.Value = SteamFriends.HasFriend(FriendID, EFriendFlags.k_EFriendFlagAll);
		}
	}
}
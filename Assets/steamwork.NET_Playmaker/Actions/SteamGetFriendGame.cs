// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;
using System.Collections;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Get´s steam user actual activity.\r\nGet game app ID, 0 equal playing nothing.")]
	public class SteamGetFriendGame : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString UserID;

		[UIHint(UIHint.Variable)]
		public FsmString game;

		[UIHint(UIHint.Variable)]
		public FsmBool play;

		public override void Reset()
		{
			UserID = null;
			game = null;
			play = false;
		}
		
		public override void OnEnter()
		{
			FriendGameInfo_t activity;

			ulong ID = ulong.Parse(UserID.Value);
			CSteamID FriendID = SteamUser.GetSteamID(); //not perfect but works!
			FriendID.m_SteamID = ID;

			play = SteamFriends.GetFriendGamePlayed(FriendID, out activity);
			game.Value = activity.m_gameID.ToString();
		}
	}
}
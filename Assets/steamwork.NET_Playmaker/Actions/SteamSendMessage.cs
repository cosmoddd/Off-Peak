// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;
using System.Collections;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Send´s message to friend by ID.")]
	public class SteamSendMessage : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString friendID; //friend ID as string

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Message text as string.")]
		public FsmString message;

		[Tooltip("Return true if sended, return false if not possible.")]
		public FsmBool send;

		public override void Reset()
		{
			friendID = null;
			message = null;
		}
		
		public override void OnEnter()
		{
			ulong ID = ulong.Parse(friendID.Value);
			CSteamID FriendID = SteamUser.GetSteamID(); //not perfect but works!
			FriendID.m_SteamID = ID;
			send.Value = SteamFriends.ReplyToFriendMessage(FriendID,message.Value);

		}
	}
}
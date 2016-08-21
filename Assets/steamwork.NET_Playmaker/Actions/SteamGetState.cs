// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;
using System.Collections;
using Steamworks;
using System.Runtime.InteropServices;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Get´s steam user state.\r\nWill return [Online;Away;Busy;LookingToPlay;LookingToTrade;Offline].")]
	public class SteamGetState : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString UserID;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString userState;

		public override void Reset()
		{
			UserID = null;
			userState = null;
		}

		public override void OnEnter()
		{
			ulong ID = ulong.Parse(UserID.Value);
			CSteamID User = SteamUser.GetSteamID(); //not perfect but works!
			User.m_SteamID = ID;
			Steamworks.EPersonaState enumState = SteamFriends.GetFriendPersonaState(User);
			string state = enumState.ToString();

			switch(enumState)
			{
				case EPersonaState.k_EPersonaStateOnline: // friend is logged on
					userState.Value = "Online";
					break;

				case EPersonaState.k_EPersonaStateOffline: // friend is not currently logged on
					userState.Value = "Offline";
					break;

				case EPersonaState.k_EPersonaStateAway: // auto-away feature
					userState.Value = "Away";
					break;
			
				case EPersonaState.k_EPersonaStateSnooze: // auto-away for a long time
					userState.Value = "Away";
					break;

				case EPersonaState.k_EPersonaStateBusy: // user is on, but busy
					userState.Value = "Busy";
					break;

				case EPersonaState.k_EPersonaStateLookingToPlay: // Online, wanting to play
					userState.Value = "LookingToPlay";
					break;

				case EPersonaState.k_EPersonaStateLookingToTrade: // Online, trading
					userState.Value = "LookingToTrade";
					break;
			}
		}
	}
}
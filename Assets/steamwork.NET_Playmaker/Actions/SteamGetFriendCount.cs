// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;
using System.Collections;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Get´s number of friends on Steam.")]
	public class SteamGetFriendCount : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt friendCount;
		
		public override void Reset()
		{
			friendCount = null;
		}
		
		public override void OnEnter()
		{
			friendCount.Value = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagAll);
		}
	}
}
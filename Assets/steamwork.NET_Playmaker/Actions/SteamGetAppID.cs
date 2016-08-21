// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;
using System.Collections;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Get´s steam app ID.")]
	public class SteamGetAppID : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString appID;
		
		public override void Reset()
		{
			appID = null;
		}
		
		public override void OnEnter()
		{
			AppId_t ID = SteamUtils.GetAppID();
			appID.Value = ID.ToString();
		}
	}
}
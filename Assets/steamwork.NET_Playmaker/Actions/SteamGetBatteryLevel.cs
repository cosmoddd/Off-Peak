// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;
using System.Collections;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Get´s battery level.\r\nInt [0;100]. Will return 255 if laptop.")]
	public class SteamGetBatteryLevel : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt batteryLevel;
		
		public override void Reset()
		{
			batteryLevel = null;
		}
		
		public override void OnEnter()
		{
			batteryLevel.Value = SteamUtils.GetCurrentBatteryPower();
		}
	}
}
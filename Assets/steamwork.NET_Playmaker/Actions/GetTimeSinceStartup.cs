// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;
using System.Collections;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Get´s time since startup.")]
	public class SteamGetTimeSinceStartup : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt time;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			time = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			GetTime();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			GetTime();
		}

		public void GetTime()
		{
			uint time1 = SteamUtils.GetSecondsSinceAppActive();
			string time2 = time1.ToString();
			time.Value = int.Parse(time2);
		}
	}
}
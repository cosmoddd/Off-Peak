// Created by Riley Labrecque for Digital Devolver and Terri Vellmann
// (c) 2014
using UnityEngine;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Unlocks an Achievement by name.")]
	public class SteamAchievementUnlock : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Achievement name.")]
		public FsmString achievementId;

		[UIHint(UIHint.Variable)]
		[Tooltip("Returns true on success, false on failure.")]
		public FsmBool success;

		public override void Reset()
		{
			achievementId = null;
			success = null;
		}

		public override void OnEnter()
		{
			success.Value = SteamManager.StatsAndAchievements.UnlockAchievement(achievementId.Value);	
			SteamUserStats.StoreStats();
		}
	}
}

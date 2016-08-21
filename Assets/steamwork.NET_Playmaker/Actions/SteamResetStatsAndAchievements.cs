// Created by Riley Labrecque for Digital Devolver and Terri Vellmann
// (c) 2014
using UnityEngine;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Resets all Stats and Achievements.")]
	public class SteamResetStatsAndAchievements : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Returns true on success, false on failure.")]
		public FsmBool success;

		public override void Reset()
		{
			success = null;
		}

		public override void OnEnter()
		{
			if (SteamManager.Initialized)
			{

				success.Value = SteamManager.StatsAndAchievements.ResetStatsAndAchievements();

				if (!success.Value) {
					Debug.LogError("Steamworks.NET - ResetStatsAndAchievements() returned false.");
				}
			}

			Finish();
		}
	}
}

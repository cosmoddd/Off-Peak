// Created by Riley Labrecque for Digital Devolver and Terri Vellmann
// (c) 2014
using UnityEngine;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Sets up and Initializes the Steam Achievements.")]
	public class SteamAchievementsSetup : FsmStateAction
	{
		[RequiredField]
		[Tooltip("List of Achievement Ids.")]
		public FsmString[] AchievementIds;
		
		public override void Reset() {
			AchievementIds = null;
		}
		
		public override void OnEnter() {
			if (SteamManager.Initialized) {
				string[] achievementIds = new string[AchievementIds.Length];
				//Debug.Log(variables.Length);
				for (int i = 0; i < AchievementIds.Length; ++i) {
					//Debug.Log(variables[i]);
					achievementIds[i] = AchievementIds[i].Value;
				}
				SteamManager.StatsAndAchievements.InitAchievements(achievementIds);
			}
			
			Finish();
		}
	}
}

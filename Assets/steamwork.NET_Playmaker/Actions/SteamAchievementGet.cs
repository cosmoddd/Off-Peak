// Created by Riley Labrecque for Digital Devolver and Terri Vellmann
// (c) 2014
using UnityEngine;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Gets the display name, description, and status of an achievement.")]
	public class SteamAchievementGet : FsmStateAction
	{
		[RequiredField]
		[Tooltip("This is the 'API Name' in the Steam Partner backend")]
		public FsmString achievementId;

		[UIHint(UIHint.Variable)]
		[Tooltip("Returns the Achievement Name.")]
		public FsmString achievementName;

		[UIHint(UIHint.Variable)]
		[Tooltip("Returns the Achievement Description.")]
		public FsmString achievementDescription;

		[UIHint(UIHint.Variable)]
		[Tooltip("True if the Achievement is unlocked, False if it is locked.")]
		public FsmBool achievementUnlocked;

		public string achievementHiddenString;
		public bool achievementUnlockedbool;

		public override void Reset()
		{
			achievementId = null;
			achievementName = null;
			achievementDescription = null;
		//	achievementUnlocked = null;
		}

		public override void OnEnter() 
		{
			Achievement_t ach = SteamManager.StatsAndAchievements.GetAchievement(achievementId.Value);


			if (ach != null)
			{

				achievementName.Value = SteamUserStats.GetAchievementDisplayAttribute(achievementId.Value, "name"); 					// achievement name
				achievementDescription.Value = SteamUserStats.GetAchievementDisplayAttribute(achievementId.Value, "desc"); 	// achievement description
				//achievementUnlockedbool = SteamUserStats.GetAchievement(achievementId.Value, out achievementUnlockedbool); 			// achievement status

			}

			Finish();
		}
	}
}

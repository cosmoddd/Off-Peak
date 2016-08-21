// Created by Riley Labrecque for Digital Devolver and Terri Vellmann
// (c) 2014
using UnityEngine;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Sets the value of a stat.")]
	public class SteamStatSet : FsmStateAction
	{
		[RequiredField]
		[Tooltip("This is the 'API Name' in the Steam Partner backend")]
		public FsmString statName;

		[RequiredField]
		[Tooltip("Returns the Stats value (int or float only!).")]
		public FsmVar statValue;


		public override void Reset()
		{
			statName = null;
			statValue = null;
		}

		public override void OnEnter()
		{
			if (statValue.Type == VariableType.Float)
			{
				SteamManager.StatsAndAchievements.SetStat(statName.Value, statValue.floatValue);
			}
			else if (statValue.Type == VariableType.Int)
			{
				SteamManager.StatsAndAchievements.SetStat(statName.Value, statValue.intValue);
				Debug.Log("SetStat: " + statValue.intValue);
			}
			else
			{
				throw new System.Exception("GetStat requires an Int or Float for the return value.");
			}

			Finish();
		}
	}
}

// Created by Riley Labrecque for Digital Devolver and Terri Vellmann
// (c) 2014
using UnityEngine;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Sets up and Initializes the Steam Stats.")]
	public class SteamStatsSetup : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("List of Stats.")]
		public FsmVar[] stats;

		public override void Reset()
		{
			stats = null;
		}

		public override void OnEnter() {
			if (SteamManager.Initialized) {
				Stat_t[] test = new Stat_t[stats.Length];
				
				for (int i = 0; i < stats.Length; ++i) {
					Debug.Log(stats[i].variableName + " " + stats[i]);
					
					if (stats[i].IsNone) {
						throw new System.Exception("Steam stats must have a variable attached.");
					}
					
					if (stats[i].Type != VariableType.Float && stats[i].Type != VariableType.Int) {
						throw new System.Exception("Steam stats may only be floats and ints: " + stats[i].variableName);
					}
					
					test[i] = new Stat_t(stats[i].variableName, stats[i].RealType);
				}
				
				SteamManager.StatsAndAchievements.InitStats(test);
			}
			
			Finish();
		}
	}
}

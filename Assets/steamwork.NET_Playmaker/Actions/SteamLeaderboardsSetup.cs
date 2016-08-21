// Created by Riley Labrecque for Digital Devolver and Terri Vellmann
// (c) 2014
using UnityEngine;
using Steamworks;

namespace HutongGames.PlayMaker.Actions {
	[ActionCategory("steamworks.NET")]
	[Tooltip("Sets up and Initializes the Steam Achievements.")]
	public class SteamLeaderboardsSetup : FsmStateAction {
		[RequiredField]
		[Tooltip("List of Leaderboard names.")]
		public FsmString[] Leaderboards;

		public override void Reset() {
			Leaderboards = null;
		}

		public override void OnEnter() {
			if (SteamManager.Initialized) {
				string[] LeaderboardNames = new string[Leaderboards.Length];
				//Debug.Log(variables.Length);
				for (int i = 0; i < Leaderboards.Length; ++i) {
					//Debug.Log(variables[i]);
					LeaderboardNames[i] = Leaderboards[i].Value;
				}
				SteamManager.Leaderboards.InitLeaderboards(LeaderboardNames);
			}

			Finish();
		}
	}
}

// Created by Riley Labrecque for Digital Devolver and Terri Vellmann
// (c) 2014
using UnityEngine;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("steamworks.NET")]
	[Tooltip("Gets the value of a stat.")]
	public class SteamStatGet : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Stat name of steam.")]
		public FsmString statName;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Returns the stat value.[int;float only!]")]
		public FsmVar statOutput;

		public override void Reset()
		{
			statName = null;
			statOutput = null;
		}

		public override void OnEnter() {
			if (SteamManager.Initialized) {
				if (statOutput.IsNone) {
					throw new System.Exception("GetStat must have an output variable attached.");
				}
				
				if (statOutput.Type != VariableType.Float && statOutput.Type != VariableType.Int) {
					throw new System.Exception("GetStat requires an Int or Float for the return value.");
				}
				
				Stat_t stat = SteamManager.StatsAndAchievements.GetStat(statName.Value);
				if (stat != null) {
					if (statOutput.RealType == stat.m_StatType) {
						if (stat.m_StatType == typeof(float)) {
							statOutput.SetValue(stat.m_fValue);
						}
						else {
							statOutput.SetValue(stat.m_nValue);
						}
					}
					else {
						throw new System.Exception("GetStat type mismatch for stat: " + statName.Value + " - Expected: " + stat.m_StatType + " but got: " + statOutput.RealType);
					}
				}
			}
			
			Finish();
		}
	}
}

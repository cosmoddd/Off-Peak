using UnityEngine;
using System.Collections;
using Steamworks;
using System.Runtime.InteropServices;

// Steamworks.NET SimpleScripts - SteamStatsAndAchievements
// This is an simple implementation of how to properly use Steam Stats and Achievements.
// Please view the included Readme for more details.

[AddComponentMenu("Scripts/Steam Simple Scripts/Stats and Achievements")]
public class SteamStatsAndAchievements : MonoBehaviour {
	private Achievement_t[] m_Achievements;

	private Stat_t[] m_Stats;
	// Stats:
	// Follow this template for each stat that you wish to implement.
	/*private int m_nTotalNumWins;
	public int TotalNumWins {
		get {
			return m_nTotalNumWins;
		}
		set {
			m_nTotalNumWins = value;
			m_bStoreStats = true;
		}
	}
	
	private float m_flTotalFeetTraveled;
	public float TotalFeetTraveled {
		get {
			return m_flTotalFeetTraveled;
		}
		set {
			m_flTotalFeetTraveled = value;
			m_bStoreStats = true;
		}
	}

	// Gets called from OnUserStatsReceived() below which gets called asyncronously when SteamUserStats.RequestCurrentStats() returns.
	private void GetStats() {
		SteamUserStats.GetStat("NumWins", out m_nTotalNumWins);
		SteamUserStats.GetStat("FeetTraveled", out m_flTotalFeetTraveled);
	}

	// This gets called when m_bStoreStats is set to true. That happens when ever an Achievement Unlocks or a stat gets updated.
	private void SetStats() {
		SteamUserStats.SetStat("NumWins", m_nTotalNumWins);
		SteamUserStats.SetStat("FeetTraveled", m_flTotalFeetTraveled);
	}*/

	//=======================================================


	// Our GameID
	private CGameID m_GameID;

	// Did we get the stats from Steam?
	private bool m_bRequestedStats;
	private bool m_bStatsValid;

	// Should we store stats this frame?
	private bool m_bStoreStats;

	protected Callback<UserStatsReceived_t> m_UserStatsReceived;
	protected Callback<UserStatsStored_t> m_UserStatsStored;

	// This should only ever be called by SteamManager in OnEnable()!
	// The reasoning behind this is that SteamManager Initializes Steam which needs to happen before callbacks are registered.
	public void Init() {
		if (!SteamManager.Initialized)
			return;

		m_GameID = new CGameID(SteamUtils.GetAppID());

		// Register for steam callbacks
		m_UserStatsReceived = Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
		m_UserStatsStored = Callback<UserStatsStored_t>.Create(OnUserStatsStored);

		// These need to be reset to get the stats upon an Assembly reload in the Editor.
		m_bRequestedStats = false;
		m_bStatsValid = false;
	}

	public void InitAchievements(string[] ach) {
		if (m_Achievements != null) {
			Debug.LogWarning("Attempted to InitAchievements twice.");
			return;
		}

		m_Achievements = new Achievement_t[ach.Length];
		for (int i = 0; i < ach.Length; ++i) {
			m_Achievements[i] = new Achievement_t(ach[i]);
		}
	}

	public void InitStats(Stat_t[] stats) {
		if (m_Stats != null) {
			Debug.LogWarning("Attempted to InitStats twice.");
			return;
		}

		m_Stats = stats;
	}

	public bool UnlockAchievement(string achievementId) {
		if (m_Achievements == null) {
			return false; // Called before InitAchievements();
		}

		bool ret = false;
		foreach (Achievement_t ach in m_Achievements) { //Todo: Using an ordered dictionary would be great.
			if (ach.m_strAchievementID == achievementId) {
				if (!ach.m_bAchieved) {
					ach.m_bAchieved = true;

					ret = SteamUserStats.SetAchievement(ach.m_strAchievementID);

					// Store stats end of frame
					m_bStoreStats = true;
				}
				else {
					ret = true;
				}
				break;
			}
		}
		return ret;
	}

	public Achievement_t GetAchievement(string achievementId) {
		if (m_Achievements == null) {
			return null; // Called before InitAchievements();
		}

		foreach (Achievement_t ach in m_Achievements) { //Todo: Using an ordered dictionary would be great.
			if (ach.m_strAchievementID == achievementId) {
				return ach;
			}
		}

		Debug.LogError("GetAchievement could not find achievement: " + achievementId);
		return null;
	}

	public Stat_t GetStat(string statname) {
		if (m_Achievements == null) {
			return null; // Called before InitStats();
		}

		foreach (Stat_t stat in m_Stats) { //Todo: Using an ordered dictionary would be great.
			if (stat.m_strStatName == statname) {
				return stat;
			}
		}

		Debug.LogError("GetStat could not find stat: " + statname);
		return null;
	}

	public void SetStat(string statname, float value) {
		if (m_Achievements == null) {
			return; // Called before InitAchievements();
		}

		foreach (Stat_t stat in m_Stats) { //Todo: Using an ordered dictionary would be great.
			if (stat.m_strStatName == statname) {
				if (stat.m_StatType != typeof(float)) {
					throw new System.Exception("Trying to assign an int to a float stat: " + statname);
				}

				stat.m_fValue = value;
				m_bStoreStats = true;
				return;
			}
		}

		Debug.LogError("SetStat could not find stat: " + statname);
	}

	public void SetStat(string statname, int value) {
		if (m_Achievements == null) {
			return; // Called before InitAchievements();
		}

		foreach (Stat_t stat in m_Stats) { //Todo: Using an ordered dictionary would be great.
			if (stat.m_strStatName == statname) {
				if (stat.m_StatType != typeof(int)) {
					throw new System.Exception("Trying to assign an float to a int stat: " + statname);
				}

				stat.m_nValue = value;
				m_bStoreStats = true;
				return;
			}
		}

		Debug.LogError("GetStat could not find stat: " + statname);
	}

	public bool ResetStatsAndAchievements() {
		m_bRequestedStats = false;
		m_bStatsValid = false;
		return SteamUserStats.ResetAllStats(true);
	}

	private void Update() {
		if (!SteamManager.Initialized)
			return;

		if (!m_bRequestedStats) {
			// This function should only return false if we weren't logged in, and we already checked that.
			// But handle it being false again anyway, just ask again later.
			if (m_Achievements != null && m_Stats != null) { // Wait until InitAchievements() has been called.
				m_bRequestedStats = SteamUserStats.RequestCurrentStats();
			}
		}

		// Wait for our stats to come down from steam.
		if (!m_bStatsValid)
			return;
		
		if (m_bStoreStats) {
			if (m_Stats != null) {
				for (int i = 0; i < m_Stats.Length; ++i) {
					bool ret;
					if (m_Stats[i].m_StatType == typeof(float)) {
						ret = SteamUserStats.SetStat(m_Stats[i].m_strStatName, m_Stats[i].m_fValue);
					}
					else {
						ret = SteamUserStats.SetStat(m_Stats[i].m_strStatName, m_Stats[i].m_nValue);
					}

					if (!ret) {
						Debug.LogWarning(" SteamUserStats.SetStat failed for Stat " + m_Stats[i].m_strStatName + "\nIs it registered in the Steam Partner site?");
					}
				}
			}
			

			// If this failed, we never sent anything to the server, try again later.
			m_bStoreStats = !SteamUserStats.StoreStats();
		}
	}
	
	private void OnUserStatsReceived(UserStatsReceived_t pCallback) {
		// We may get callbacks for other games' stats arriving, ignore them
		if ((ulong)m_GameID != pCallback.m_nGameID)
			return;

		if (pCallback.m_eResult != EResult.k_EResultOK) {
			Debug.Log("RequestStats - failed, " + pCallback.m_eResult);
			return;
		}

		// Load achievements
		foreach (Achievement_t ach in m_Achievements) {
			bool ret = SteamUserStats.GetAchievement(ach.m_strAchievementID, out ach.m_bAchieved);
			if (ret) {
				ach.m_strName = SteamUserStats.GetAchievementDisplayAttribute(ach.m_strAchievementID, "name");
				ach.m_strDescription = SteamUserStats.GetAchievementDisplayAttribute(ach.m_strAchievementID, "desc");
			}
			else {
				Debug.LogWarning("SteamUserStats.GetAchievement failed for Achievement " + ach.m_strAchievementID + "\nIs it registered in the Steam Partner site?");
			}
		}

		// load stats

		if (m_Stats != null) {
			for (int i = 0; i < m_Stats.Length; ++i) {
				bool ret;
				if (m_Stats[i].m_StatType == typeof(float)) {
					ret = SteamUserStats.GetStat(m_Stats[i].m_strStatName, out m_Stats[i].m_fValue);
				}
				else {
					ret = SteamUserStats.GetStat(m_Stats[i].m_strStatName, out m_Stats[i].m_nValue);
				}

				if (!ret) {
					Debug.LogWarning(" SteamUserStats.GetStat failed for Stat " + m_Stats[i].m_strStatName + "\nIs it registered in the Steam Partner site?");
				}
			}
		}

		//Debug.Log("UserStats Received");
		m_bStatsValid = true;
	}

	private void OnUserStatsStored(UserStatsStored_t pCallback) {
		// We may get callbacks for other games' stats arriving, ignore them
		if ((ulong)m_GameID != pCallback.m_nGameID)
			return;
		
		if (EResult.k_EResultOK != pCallback.m_eResult) {
			if (EResult.k_EResultInvalidParam == pCallback.m_eResult) {
				// One or more stats we set broke a constraint. They've been reverted,
				// and we should re-iterate the values now to keep in sync.
				Debug.LogWarning("StoreStats - some failed to validate");

				// Fake up a callback here so that we re-load the values.
				UserStatsReceived_t callback = new UserStatsReceived_t();
				callback.m_eResult = EResult.k_EResultOK;
				callback.m_nGameID = (ulong)m_GameID;
				OnUserStatsReceived(callback);
			}
			else {
				Debug.LogWarning("StoreStats - failed, " + pCallback.m_eResult);
			}
		}

		//Debug.Log("UserStats Stored");
	}
}

public class Achievement_t {
	public string m_strAchievementID;
	public string m_strName;
	public string m_strDescription;
	public bool m_bAchieved;
	public Achievement_t(string achievementID) {
		m_strAchievementID = achievementID;
		m_strName = "";
		m_strDescription = "";
		m_bAchieved = false;
	}
}

public class Stat_t {
	public string m_strStatName;
	public System.Type m_StatType;
	public float m_fValue;
	public int m_nValue;

	public Stat_t(string name, System.Type type) {
		m_strStatName = name;
		m_StatType = type;
	}
}

using UnityEngine;
using System.Collections;
using Steamworks;


public class SteamScriptCMD : MonoBehaviour {

	public string[] achievementName;
	public bool Checkkkk;
	public string theKey;

	protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;

	void Start () {

		if(SteamManager.Initialized){
			string name = SteamFriends.GetPersonaName();
			Debug.Log(name);
			Debug.Log("steam has been initialized");

	}

	}
	private void OnEnable(){
		if (SteamManager.Initialized){
			m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
		}
	}

	private void OnGameOverlayActivated(GameOverlayActivated_t pCallback) {
		if (pCallback.m_bActive != 0)
		{
			Debug.Log("Steam Overlay has been mf'n actiVATEd!");
		}
		else {
			Debug.Log("Steam oerlay has been crushed, closed and phased out like a donuz.");
		}
	}

	public void CheckAchievement(){
	
		Debug.Log ("Achieve this: " + SteamUserStats.GetAchievementDisplayAttribute(achievementName[0], "name"));
		Debug.Log ("Achieve this: " + SteamUserStats.GetAchievementDisplayAttribute(achievementName[0], "desc"));
		Debug.Log ("Achieve this: " + SteamUserStats.GetAchievementDisplayAttribute(achievementName[0], "hidden"));
	//	Debug.Log ("achieved?" + Steam

	//	Debug.Log ("Achieve this: " + SteamManager.StatsAndAchievements.GetAchievement(achievementName[0]).m_strName);

	//	Debug.Log ("Achieve this: " + SteamManager.StatsAndAchievements.GetAchievement(achievementName[0]).m_strDescription);
			
	}

	


	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown(KeyCode.B))
		{
			CheckAchievement();
		}

	}
}

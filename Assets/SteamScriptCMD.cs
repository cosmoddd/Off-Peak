using UnityEngine;
using System.Collections;
using Steamworks;


public class SteamScriptCMD : MonoBehaviour {

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


	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using Steamworks;

// Steamworks.NET SimpleScripts - SteamLeaderboards
// This is an simple implementation of how to properly use Steam Leaderboards.
// Please view the included Readme for more details.

public class Leaderboard_t {
	public string m_strName;
	public SteamLeaderboard_t m_hHandle;

	public Leaderboard_t(string name) {
		m_strName = name;
	}
}

public class SteamLeaderboards : MonoBehaviour {
	private Leaderboard_t[] m_Leaderboards;

	//=======================================================
#if DISABLED
	// Max number of Leaderboard Entries to download at once.
	public const int k_nMaxLeaderboardEntries = 10;
#endif

	// Called when SteamUserStats.FindLeaderboard() returns asynchronously
	private CallResult<LeaderboardFindResult_t> m_callResultFindLeaderboard;
#if DISABLED
	// Called when SteamUserStats.DownloadLeaderboardEntries() returns asynchronously
	private CallResult<LeaderboardScoresDownloaded_t> m_callResultDownloadedEntries;
#endif
	// Called when SteamUserStats.UploadLeaderboardScore() returns asynchronously
	private CallResult<LeaderboardScoreUploaded_t> m_callResultUploadScore;

	// True if we are currently looking up a leaderboard handle
	private bool m_bLoading;

#if DISABLED
	// True if we are currently downloading leaderboard entries
	private bool m_bDownloading;

	// True if there was an issue connecting to Steam
	private bool m_bIOFailure;

	// These two callbacks provide the GUI with callbacks.
	// These are registered via SetCallbacks()
	public delegate void LeaderboardDownloadedDelegate(LeaderboardEntry_t[] t);
	private event LeaderboardDownloadedDelegate m_LeaderboardDownloadedCallback;

	public delegate void ScoreUploadedDelegate();
	private event ScoreUploadedDelegate m_ScoreUploadedCallback;
#endif

	// This should only ever be called by SteamManager in OnEnable()!
	public void Init() {
		m_callResultFindLeaderboard = new CallResult<LeaderboardFindResult_t>(OnFindLeaderboard);
		m_callResultUploadScore = new CallResult<LeaderboardScoreUploaded_t>(OnScoreUploaded);
#if DISABLED
		m_callResultDownloadedEntries = new CallResult<LeaderboardScoresDownloaded_t>(OnLeaderboardDownloadedEntries);
#endif

	}

	public void InitLeaderboards(string[] names) {
		if (m_Leaderboards != null) {
			Debug.LogWarning("Attempted to InitLeaderboards twice.");
			return;
		}

		if (names == null || names.Length == 0) {
			Debug.LogWarning("Empty list of Leaderboard names passed into InitLeaderboards.");
			return;
		}

		m_Leaderboards = new Leaderboard_t[names.Length];
		for (int i = 0; i < names.Length; ++i) {
			m_Leaderboards[i] = new Leaderboard_t(names[i]);
		}

		FindLeaderboards(0);
	}

#if DISABLED
	// Call this from your GUI class to recieve Steam callbacks to it.
	public void SetCallbacks(LeaderboardDownloadedDelegate OnLeaderboardDownloaded = null, ScoreUploadedDelegate OnScoreUploaded = null) {
		m_LeaderboardDownloadedCallback = OnLeaderboardDownloaded;
		m_ScoreUploadedCallback = OnScoreUploaded;
	}

	// Call this each render frame to know what you should render.
	// k_EResultFail warns the user that steam is not initialized.
	// k_EResultBusy indicates loading either through text ("Loading...") or a spinning loading icon.
	// k_EResultIOFailure should alert the user to an IOFailure.
	// k_EResultOK should draw the leaderboard.
	public EResult GetCurrentState() {
		if (!SteamManager.Initialized) {
			return EResult.k_EResultFail;
		}

		if (m_bLoading || m_bDownloading) {
			return EResult.k_EResultBusy;
		}

		if (m_bIOFailure) {
			return EResult.k_EResultIOFailure;
		}

		return EResult.k_EResultOK;
	}

	// Call this once when ever your GUI switches state to download the updated leaderboard entries.
	// If you wish to have multiple pages you could pass in the offset from your GUI.
	public bool GetLeaderboard(Leaderboard board, ELeaderboardDataRequest requesttype) {
		if (!SteamManager.Initialized) {
			return false;
		}

		if (m_bLoading) {
			// If we're still loading the Leaderboards then we'll defer this until we've finished
			StartCoroutine(RetryGetLeaderboard(board, requesttype));
			return false;
		}

		int offset = 0;
		if (requesttype == ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser) {
			offset = -5;
		}

		SteamAPICall_t hSteamAPICall = SteamUserStats.DownloadLeaderboardEntries(m_LeaderboardHandles[(int)board], requesttype, offset, offset + k_nMaxLeaderboardEntries);
		// Register for the async callback
		m_callResultDownloadedEntries.Set(hSteamAPICall);

		m_bDownloading = true;
		return true;
	}
#endif

	// Call this to upload your score to the leaderboard. By default it will only keep the best score that the user has submitted.
	public void UploadScore(string name, int score) {
		if (!SteamManager.Initialized) {
			return;
		}

		if(m_Leaderboards == null) {
			return; // Called before InitLeaderboards();
		}

		if (m_bLoading) {
			Debug.LogError("Tried to upload score but Leaderboards haven't finished loading yet.");
			return;
		}


		foreach (Leaderboard_t lb in m_Leaderboards) { //Todo: Using an ordered dictionary would be great.
			if (lb.m_strName == name) {
				SteamAPICall_t hSteamAPICall = SteamUserStats.UploadLeaderboardScore(lb.m_hHandle, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest, score, null, 0);
				Debug.Log("New Score of " + score + " uploaded to leaderboard: " + name);
				m_callResultUploadScore.Set(hSteamAPICall);
				return;
			}
		}

		Debug.LogError("UploadScore could not find leaderboard named: " + name);
	}

#if DISABLED
	// If we're still loading the Leaderboards when GetLeaderboard() is called we'll wait until they are loaded and try calling GetLeaderboard() again.
	private IEnumerator RetryGetLeaderboard(Leaderboard board, ELeaderboardDataRequest requesttype) {
		while (m_bLoading) {
			yield return null;
		}
		GetLeaderboard(board, requesttype);
	}
#endif

	private void FindLeaderboards(int index) {
		if (index > m_Leaderboards.Length) {
			Debug.LogWarning("SteamLeaderboards Script Internal Error - index > m_Leaderboards.Length in FindLeaderboards.");
			return;
		}

		SteamAPICall_t hSteamAPICall = SteamUserStats.FindLeaderboard(m_Leaderboards[index].m_strName);
		m_callResultFindLeaderboard.Set(hSteamAPICall);
		m_bLoading = true;
	}

	private void OnFindLeaderboard(LeaderboardFindResult_t pFindLeaderboardResult, bool bIOFailure) {
		// See if we encountered an error during the call
		if (bIOFailure) {
			return;
		}

		if(pFindLeaderboardResult.m_bLeaderboardFound == 0) {
			Debug.LogWarning("Could not find one of the Leaderboards, was it named correctly?");
			return;
		}

		// check to see which leaderboard handle we just retrieved
		string pchName = SteamUserStats.GetLeaderboardName(pFindLeaderboardResult.m_hSteamLeaderboard);
		int i = 0;
		for (; i < m_Leaderboards.Length; ++i) {
			if (pchName == m_Leaderboards[i].m_strName) {
				m_Leaderboards[i].m_hHandle = pFindLeaderboardResult.m_hSteamLeaderboard;
				break;
			}
		}

		// look up any other leaderboards
		if (i + 1 < m_Leaderboards.Length) {
			FindLeaderboards(i + 1);
		}
		else {
			m_bLoading = false;
		}
	}

#if DISABLED
	private void OnLeaderboardDownloadedEntries(LeaderboardScoresDownloaded_t pLeaderboardScoresDownloaded, bool bIOFailure) {
		m_bDownloading = false;
		m_bIOFailure = bIOFailure;

		// Leaderboard entries handle will be invalid once we return from this function. Copy all data now.
		int nLeaderboardEntries = Mathf.Min(pLeaderboardScoresDownloaded.m_cEntryCount, k_nMaxLeaderboardEntries);
		LeaderboardEntry_t[] leaderboardEntries = new LeaderboardEntry_t[nLeaderboardEntries];
		for (int i = 0; i < nLeaderboardEntries; i++) {
			SteamUserStats.GetDownloadedLeaderboardEntry(pLeaderboardScoresDownloaded.m_hSteamLeaderboardEntries, i, out leaderboardEntries[i], null, 0);
		}

		if (m_LeaderboardDownloadedCallback != null) {
			m_LeaderboardDownloadedCallback(leaderboardEntries);
		}
	}
#endif

	private void OnScoreUploaded(LeaderboardScoreUploaded_t pScoreUploadedResult, bool bIOFailure) {
		Debug.Log("OnScoreUploaded " + pScoreUploadedResult.m_bSuccess + " - " + pScoreUploadedResult.m_bScoreChanged);
		//if (pScoreUploadedResult.m_bSuccess == 1 && pScoreUploadedResult.m_bScoreChanged == 1) {
		//
		//}
	}
}

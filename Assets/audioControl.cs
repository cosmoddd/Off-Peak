using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class audioControl : MonoBehaviour {


	public AudioMixer mastaMixer;

	public Slider slider;

//	public bool sfxMute = false;
//	public bool musicMute = false;

	public bool valuezmin = false;
	public bool valuezmin2 = false;

	
	public float sfxOld = 0f;
	public float sfxNew = 0f;
	public float time = 10f;

	public float musicOld = 0f;
	public float musicNew = 0f;
	public float musicCurrent = 0f;
	public float stored;

	

	// Use this for initialization
	void Start () {

	bool sfxMute = false;
	
	}
	
	// Update is called once per frame
	void Update () {

		SetSliderValue ();
		SetSfxLevel(sfxOld);


		SFXV ();
		MV ();

	
//		if (Input.GetKeyDown ("n")) {
//			print ("n was pressed");
//			CheckSFXMute();
//
//			}
//
//
//		if (Input.GetKeyDown ("m")) {
//			print ("m was fuckin pressed");
//			CheckMusicMute();
//			
//		}

		}

	void SetSliderValue(){
		//musicCurrent = slider.value;
	}


		void SFXV() {
			if (Input.GetKeyDown ("n")) {
			if (valuezmin == false) {
				sfxNew = -80f;
				valuezmin = true;
			} else if (valuezmin == true) {
				sfxNew = 0f;
				valuezmin = false;
			}
		}


		sfxOld = Mathf.Lerp (sfxOld, sfxNew, time);

	}

	void MV() {
		if (Input.GetKeyDown ("m"))
		{
			if (valuezmin2 == false) {
				stored = slider.value;
				slider.value = -80f;
				valuezmin2 = true;
			} else if (valuezmin2 == true) {
				slider.value = stored;
				valuezmin2 = false;
			}

		//slider.value = Mathf.Lerp (musicCurrent, musicNew, time* Time.deltaTime);
		}
	}
		




		


//
//	void CheckSFXMute()
//			{
//		Debug.Log ("initializing Checks");
//
//			if (sfxMute == false) {
//				SetSfxLevel (-80);
//				sfxMute = true;
//				Debug.Log (sfxMute);
//		}
//			else if (sfxMute = true) {
//				SetSfxLevel (0);
//				sfxMute = false;
//				Debug.Log (sfxMute);
//		}
//
//		
//	}
//
//	void CheckMusicMute()
//			{
//		if (musicMute == false) {
//			SetMusicLvl (-80);
//			musicMute = true;
//			Debug.Log (musicMute);
//		} else if (musicMute = true) {
//			SetMusicLvl (0);
//			musicMute = false;
//			Debug.Log (musicMute);
//		}
//	}
//



	public void SetSfxLevel(float sfxLvl)
	{
		mastaMixer.SetFloat ("SFXVol", sfxOld);
	}
	
	public void SetMusicLvl (float musicCurrent)
	
	{
		mastaMixer.SetFloat ("MusicVol", slider.value);
	}



}

#pragma strict

	// Moves the cuttoutFrequency from 10 to 22000 following a Sinus function
	// Attach this to an audio source with a LowPassFilter to listen it working.
	@script RequireComponent(AudioSource)
	@script RequireComponent(AudioLowPassFilter)
	
	
	private var myFilter : int;
//	var (AudioLowPassFilter)cutoffFrequency : int;
	
function Start () {


}

	function Update() {
		
	//	GetComponent(AudioLowPassFilter).cutoffFrequency = 650;
	
	
	
	myFilter = 250;
	
	GetComponent(AudioLowPassFilter).cutoffFrequency = myFilter;
	
	
	Debug.Log(myFilter);
	
	
	
	}

using UnityEngine;
using System.Collections;

public class MouseLock : MonoBehaviour {

	// Use this for initialization

	public CursorLockMode wantedMode;



	void Start () {

		Cursor.lockState = wantedMode = CursorLockMode.Locked;
	
	}

	/*void SetCursorState ()
	{
		Cursor.lockState = wantedMode;
		// Hide cursor when locking
		Cursor.visible = (CursorLockMode.Locked != wantedMode);
	}
*/
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown("escape"))
			print("escape key was hit");
			Cursor.lockState = wantedMode = CursorLockMode.Locked;

		//Cursor.lockState = wantedMode = CursorLockMode.Locked;



	}
}

using UnityEngine;
using System.Collections;

public class lock_cursor : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Screen.lockCursor = true;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown("escape"))
			Screen.lockCursor = true;

		}
}

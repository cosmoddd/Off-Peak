using UnityEngine;
using System.Collections;

public class secretdoor : MonoBehaviour {


	public Vector3 doorPosition;
	public int clicked;



	// Use this for initialization
	void Start () {

		//doorPosition = new Vector3(-223f, -48.5307f, -222.0323f);
		doorPosition = transform.position;
		clicked = 0;

			
	}
	
	// Update is called once per frame
	void Update () {

		PositionChanging ();

	}


	void PositionChanging()

	{
		Vector3 startPosition = new Vector3(-223f, -48.5307f, -222.0323f);
		Vector3 newPosition = new Vector3(-223f, -42.86f, -222.0323f);

		
		//Vector3 startPosition = new Vector3(-223f, -48.5307f, -222.0323f);
		//Vector3 newPosition = new Vector3(-223f, -54.15415f, -222.0323f);

	    if(clicked == 1)
			doorPosition = newPosition;

		transform.position = Vector3.Lerp(transform.position, doorPosition, Time.deltaTime);
	}


/*void OnMouseDown()		
	
	{
		clicked = 1;
	}
*/

void SetClicked()

	{
		clicked = 1;
	}

}




using UnityEngine;
using System.Collections;

public class CameraPath : MonoBehaviour {

	[HideInInspector]
	float cameraSpeed;

	[HideInInspector]
	int currentHotspot;

	[HideInInspector]
	GameObject[] path;

	[HideInInspector]
	GameObject nextHotspot, start;

	[HideInInspector]
	Vector2 cameraDirection;

	// initialization
	void Start () {
		cameraSpeed = GameManager.instance.cameraSpeed / 10.0f;
		currentHotspot = 0;
		path = GameObject.FindGameObjectsWithTag ("Hotspot");
		start = GameObject.Find ("Start");
		getNextHotspot ();
		getDirection ();
	}
	
	// Update is called once per frame
	void Update () {

		if (atDestination ()) {
			currentHotspot = nextHotspot.GetComponent<Hotspots>().order;
			getNextHotspot ();

			// Find direction for camera to move
			getDirection ();
		}
		// Check if level reset, if it did reset current hotspot to start, and make sure camera speed is proper.
		if ((this.transform.position == start.transform.position) && (currentHotspot != 0)) {
			currentHotspot = 0;
			cameraSpeed = GameManager.instance.cameraSpeed / 10.0f;
		}

		// Move Camera
		this.transform.Translate (cameraDirection * cameraSpeed * Time.deltaTime);
		  
	}

	// Used to find the next location that the camera will move toward
	void getNextHotspot()
	{
		bool didChange = false;

		foreach (GameObject hs in path) {
			if (hs.GetComponent<Hotspots> ().order == (currentHotspot + 1)) {
				nextHotspot = hs;
				didChange = true;
			}
		}

		// If we're at the last hotspot, we have a boss battle and camera should not move
		if (!didChange && (cameraSpeed != 0.0f))
			cameraSpeed = 0.0f;
	}

	// Check to see if we've reached our hotspot destination
	bool atDestination()
	{
		if (((nextHotspot.transform.position.x - this.transform.position.x) < 0.1f) &&
		    ((nextHotspot.transform.position.x - this.transform.position.x) > -0.1f)) {
			if (((nextHotspot.transform.position.y - this.transform.position.y) < 0.1f) &&
			    ((nextHotspot.transform.position.y - this.transform.position.y) > -0.1f)){
				return true;
			}
		}
		return false;
	}

	// Used to find the direction for the camera to move
	void getDirection()
	{
		Vector2 camPos = this.transform.position;
		Vector2 hsPos = nextHotspot.transform.position;

		// Logically, we'll always want to subtract camera position from hotspot position
		// to determine the direction we're heading
		cameraDirection.x = hsPos.x - camPos.x;
		cameraDirection.y = hsPos.y - camPos.y;

		// Keep it normal, no super speeds!
		cameraDirection = cameraDirection.normalized;
	}

}

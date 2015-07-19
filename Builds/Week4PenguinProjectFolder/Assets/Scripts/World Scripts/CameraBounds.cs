using UnityEngine;
using System.Collections;

public class CameraBounds : MonoBehaviour {

	[HideInInspector]
	GameObject player;

	[HideInInspector]
	float playerY;

	[HideInInspector]
	float playerX;

	[HideInInspector]
	float camHeight;

	[HideInInspector]
	float camWidth;

	[HideInInspector]
	Vector2 direction;
	

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		camHeight = Camera.main.orthographicSize;
		camWidth = camHeight * Camera.main.aspect ;
	}
	
	// Update is called once per frame
	void Update () {
		movePlayer ();
	}


	//Check to see if the player would go out of bounds from the camera
	//If they would, move them so that they don't (Secret speed buff!?)
	//  Currently uses 'magic numbers', until I can figure out how to get
	//   the player's box collider values.
	void movePlayer()
	{
		playerX = player.transform.position.x;
		playerY = player.transform.position.y;

		if ((playerX - 1.0f) < (this.transform.position.x - camWidth)) {
			// Uses a fraction of the speed, so that the player can't outrun it
			// Not very smooth, can try to work out the kinks in week 4 or 5
			direction.x = 0.0005f * GameManager.instance.playerMoveSpeed;
			direction.y = 0.0f;
			player.transform.Translate (direction);
		} 
		else if ((playerX + 1.0f) > (this.transform.position.x + camWidth)) {
			direction.x = -0.0005f * GameManager.instance.playerMoveSpeed;
			direction.y = 0.0f;
			player.transform.Translate (direction);
		}
		if ((playerY + 0.5f) > (this.transform.position.y + camHeight)) {
			direction.x = 0.0f;
			direction.y = -0.0005f * GameManager.instance.playerMoveSpeed;
			player.transform.Translate (direction);
		}
		else if ((playerY - 0.5f) < (this.transform.position.y - camHeight)) {
			direction.x = 0.0f;
			direction.y = 0.0005f * GameManager.instance.playerMoveSpeed;
			player.transform.Translate (direction);
		}
	}
}

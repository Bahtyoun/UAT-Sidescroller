using UnityEngine;
using System.Collections;

public class MenuControls : MonoBehaviour {

	public Texture NewGame, HighScores, Credits;
	private Texture currentTexture;
	private float waitTime;
	// Use this for initialization
	void Start () {
		currentTexture = NewGame;
		waitTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		waitTime += Time.deltaTime;
		if (waitTime >= 0.2) {

			if (Input.GetKey (KeyCode.W)) {
				waitTime = 0.0f;
				if (currentTexture == NewGame) {
					currentTexture = Credits;
				} else if (currentTexture == HighScores) {
					currentTexture = NewGame;
				} else { // it's on Credits
					currentTexture = HighScores;
				}
			}

			if (Input.GetKey (KeyCode.S)) {
				waitTime = 0.0f;
				if (currentTexture == NewGame) {
					currentTexture = HighScores;
				} else if (currentTexture == HighScores) {
					currentTexture = Credits;
				} else { // it's on Credits
					currentTexture = NewGame;
				}
			}

			this.GetComponent<GUITexture> ().texture = currentTexture;

			if (Input.GetKey (KeyCode.Space)) {
				if (currentTexture == NewGame) {
					// Load level 1
					Application.LoadLevel (1);
				}
			}

		}

	}
}

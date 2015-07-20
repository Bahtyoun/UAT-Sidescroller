using UnityEngine;
using System.Collections;

public class MenuControls : MonoBehaviour {

	public Texture NewGame, HighScores, Credits, CreditsScreen;
	private Texture currentTexture;
	private float waitTime;
	// Use this for initialization
	void Start () {
		currentTexture = NewGame;
		waitTime = 0.0f;
		Screen.SetResolution (1024, 768, false);
	}
	
	// Update is called once per frame
	void Update () {
		waitTime += Time.deltaTime;
		if (waitTime >= 0.2) {

			if ((Input.GetKey (KeyCode.W)) || Input.GetKey (KeyCode.UpArrow)) {
				waitTime = 0.0f;
				if (currentTexture == NewGame) {
					currentTexture = Credits;
				} else if (currentTexture == HighScores) {
					currentTexture = NewGame;
				} else if (currentTexture == Credits){ // it's on Credits
					currentTexture = HighScores;
				}
			}

			if ((Input.GetKey (KeyCode.S)) || Input.GetKey(KeyCode.DownArrow)) {
				waitTime = 0.0f;
				if (currentTexture == NewGame) {
					currentTexture = HighScores;
				} else if (currentTexture == HighScores) {
					currentTexture = Credits;
				} else if (currentTexture == Credits){
					currentTexture = NewGame;
				}
			}

			this.GetComponent<GUITexture> ().texture = currentTexture;

			if ((Input.GetKey (KeyCode.Space)) || Input.GetKey (KeyCode.Return)) {
				waitTime = 0.0f;
				if (currentTexture == NewGame) {
					// Load level 1
					Application.LoadLevel (1);
				}
				// Credits screen toggle
				else if (currentTexture == Credits){
					currentTexture = CreditsScreen;
				}
				else if(currentTexture == CreditsScreen){
					currentTexture = Credits;
				}
			}

		}

	}
}

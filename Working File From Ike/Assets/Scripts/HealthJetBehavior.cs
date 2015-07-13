using UnityEngine;
using System.Collections;

public class HealthJetBehavior : MonoBehaviour {

	[HideInInspector]
	float timer;

	public GameObject HPRestoreBubblePrefab;

	private GameObject bubble;

	// Use this for initialization
	void Start () {
		timer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time - timer > 1.0f) {
			bubble = (GameObject)Instantiate(HPRestoreBubblePrefab, 
			                                 this.transform.position, 
			                                 Quaternion.identity);

			timer = Time.time; // reset timer
		}
	
	}
}

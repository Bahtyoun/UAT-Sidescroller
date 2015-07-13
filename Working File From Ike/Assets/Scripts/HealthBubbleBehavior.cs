using UnityEngine;
using System.Collections;

public class HealthBubbleBehavior : MonoBehaviour {

	[HideInInspector]
	float bubbleSpeed;

	[HideInInspector]
	Vector2 bubbleDirection;

	// Use this for initialization
	void Start () {
		bubbleSpeed = 1.0f;
		bubbleDirection.x = 0.0f;
		bubbleDirection.y = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		// Quickly thrown together, needs to be improved upon!
		this.transform.Translate (bubbleDirection * bubbleSpeed * Time.deltaTime);
		if (this.transform.position.y > 10.0f)
			Destroy (gameObject);
	}
}

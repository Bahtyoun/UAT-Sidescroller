using UnityEngine;
using System.Collections;

public class JellyShipEngine : EnemyEngine {

	private Transform tf;
	private Vector2 newPosition;
	private JellyShipData data;

	void Start () 
	{
		tf = GetComponent<Transform>();
		data = GetComponent<JellyShipData>();
	}

	void Update () 
	{
		// From Allan: No idea why this hasn't been changed. This ship was using the same
		// prototype movement function that I implemented for the prototype in
		// week 1. That's not going to fly anymore. These will be stationary until
		// you get a hold of Aykut's AI scripts. I just needed them to test
		// my scoring functionality
	}
	




}

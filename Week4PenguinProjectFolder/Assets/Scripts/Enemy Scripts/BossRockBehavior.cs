using UnityEngine;
using System.Collections;

public class BossRockBehavior : MonoBehaviour {

	private Transform tf;
	private Vector2 newPos;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		newPos = new Vector2(tf.position.x - (10 * Time.deltaTime),
		                     tf.position.y);
		tf.position  = newPos;

		// If the rock goes off the screen, destroy it
		if (tf.position.x < (Camera.main.orthographicSize * Camera.main.aspect))
			Destroy (gameObject);
	}
}

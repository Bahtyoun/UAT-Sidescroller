using UnityEngine;
using System.Collections;

public class BossRockBehavior : MonoBehaviour {

	private Transform tf;
	private Vector2 direction;
	private float speed, camWidth;
	GameObject player;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		tf = GetComponent<Transform>();
		speed = 4.5f;

		// Aims rock at player location at spawn time
		direction.x = player.transform.position.x - tf.position.x;
		direction.y = player.transform.position.y - tf.position.y; 
		direction.Normalize();

		camWidth = Camera.main.orthographicSize * Camera.main.aspect;
	}
	
	// Update is called once per frame
	void Update () {
		
		tf.Translate (direction * speed * Time.deltaTime);
		
		// If the rock goes off the screen, destroy it 
		if (tf.position.x < (Camera.main.transform.position.x - camWidth))
			Destroy (gameObject);
	}
}

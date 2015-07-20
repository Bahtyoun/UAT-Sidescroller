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

	// Threw a quick movement scheme on these because I'm tired
	// of looking at their sorry stationary butts
	void Update () 
	{
		Vector2 newPos = new Vector2(tf.position.x - (2 * Time.deltaTime),
		                             tf.position.y);
		tf.position  = newPos;
	}
	




}

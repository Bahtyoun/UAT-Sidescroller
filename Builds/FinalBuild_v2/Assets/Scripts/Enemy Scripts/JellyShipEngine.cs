using UnityEngine;
using System.Collections;

public class JellyShipEngine : EnemyEngine {

	private Transform tf;
	private Vector2 newPosition;
	private JellyShipData data;

	public int maxDistance; // If player is closer than this, attack them
	private GameObject target;
	private Vector2 direction;
	public float checkFrequency;
	private float checkTimer;

	void Start () 
	{
		tf = GetComponent<Transform>();
		data = GetComponent<JellyShipData>();
		//default direction
		direction.x = direction.y = 0.0f;
		target = GameObject.FindGameObjectWithTag ("Player");
		checkTimer = 0.0f;
	}

	// Threw a quick movement scheme on these because I'm tired
	// of looking at their sorry stationary butts
	void Update () 
	{
		//Vector2 newPos = new Vector2(tf.position.x - (2 * Time.deltaTime),
		//                             tf.position.y);
		//tf.position  = newPos;
		checkTimer += Time.deltaTime;
		if ((checkFrequency <= checkTimer) 
		    && (Vector2.Distance (target.transform.position, tf.position) < maxDistance)) {
			getDirection ();
			checkTimer = 0.0f;
		}
		tf.Translate (direction * data.moveSpeed * Time.deltaTime);
	}

	void getDirection()
	{
		Vector2 currPos = tf.position;
		Vector2 targetPos = target.transform.position;
		
		// Logically, we'll always want to subtract camera position from hotspot position
		// to determine the direction we're heading
		direction.x = targetPos.x - currPos.x;
		direction.y = targetPos.y - currPos.y;
		
		// Keep it normal, no super speeds!
		direction = direction.normalized;
	}
	




}

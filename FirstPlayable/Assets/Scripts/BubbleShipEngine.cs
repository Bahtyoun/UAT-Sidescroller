using UnityEngine;
using System.Collections;

public class BubbleShipEngine : EnemyEngine {

	private Transform tf;
	private Vector2 newPosition;
	private BubbleShipData data;
	// True if moving left, false if moving back to the right
	private bool movingLeft;
	private float startingx;

	void Start () 
	{
		tf = GetComponent<Transform>();
		data = GetComponent<BubbleShipData>();
		movingLeft = true;
		startingx = this.transform.position.x;
	}

	void Update () 
	{
		Move();
	}
	
	// All this ship does is move left and right at the moment. 
	// AYKUT: I'm leaving this part to you. Feel free to give it whatever
	// kind of behavior you wish! I was thinking our enemy ships
	// could move along complicated curves but I'm not trying to implement
	// that this week!
	void Move()
	{
		if(movingLeft)
		{
			newPosition = new Vector2(tf.position.x - (data.moveSpeed * Time.deltaTime), tf.position.y);
			tf.position = newPosition;
			if(tf.position.x <= (GameManager.instance.xMin + startingx))
				movingLeft = false;
		}
		else
		{
			newPosition = new Vector2(tf.position.x + (data.moveSpeed * Time.deltaTime), tf.position.y);
			tf.position = newPosition;
			if(tf.position.x >= (GameManager.instance.xMax + startingx))
				movingLeft = true;
		}
	}



}

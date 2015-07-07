using UnityEngine;
using System.Collections;

public class ShipEngine : MonoBehaviour 
{
	// Stores the ship's transform component so only one call to 
	// GetComponent() is made
	public Transform tf;
	// ShipDate component
	public ShipData data;
	public bool isInvincible;
	float invincibleCooldown;
	
	
	void Start () 
	{
		tf = GetComponent<Transform> ();
		data = GetComponent<ShipData>();
		isInvincible = false;
		invincibleCooldown = 0.0f;
	}

	void Update()
	{
		// If we're in invincible mode, call invincibleMode() to handle
		// the cooldown and changing of sprite color
		if(isInvincible)
			invincibleMode();
	}
	
	// Move the player up or down,
	// depending on the positive/negative value of speed, respectively
	public void moveVertical(float speed)
	{
		Vector2 newPosition = new Vector2(tf.position.x, tf.position.y + (speed * Time.deltaTime));
		tf.position = newPosition;
	}

	// Move the player right or left,
	// depending on the positive/negative value of speed, respectively
	public void moveHorizontal(float speed)
	{
		Vector2 newPosition = new Vector2(tf.position.x + (speed * Time.deltaTime), tf.position.y);
		tf.position = newPosition;
	}
	
	// Damages the player's ship by amount parameter
	public void Damage(int amount)
	{
		// Adjust HP by amount and check to see if we're less than 0,
		// meaning the ship is destroyed and the player must respawn.
		if((data.HP -= amount) <= 0)
		{
			Respawn();
		}
	}

	// Handles obstacle and enemy collisions
	// After colliding, the player takes 5 damage (or 1/3 of total health)
	// and is invlunerable for invlunTimeLength seconds, a value
	// included in and adjustable from the GameManager
	void OnCollisionEnter2D(Collision2D col)
	{
		// If we're in invincible mode, ignore collisions
		if(!isInvincible)
		{
			// If we hit the floor, bounce up a bit 
			// and activate invincibility
			if(col.gameObject.tag == "FloorCol")
			{
				Vector2 bounceUp = new Vector2(tf.position.x,
				                               tf.position.y + GameManager.instance.bounceAmount);
				tf.position = bounceUp;

			}
			// Same thing with the ceiling, except bounce down
			if(col.gameObject.tag == "CeilingCol")
			{
				Vector2 bounceDown = new Vector2(tf.position.x,
				                               tf.position.y - GameManager.instance.bounceAmount);
				tf.position = bounceDown;
				invincibleMode();
			}
		}
	}


	void invincibleMode()
	{
		// This line indicates that invincibleMode has just been called
		// from a collision detection. Changes the sprite color to red
		// and sets invincible mode to true
		if(!isInvincible)
		{
			GetComponent<SpriteRenderer>().material.color = Color.red;
			isInvincible = true;
		}
		// if we're in invincible mode, add to the cooldown. If we're finished
		// cooling down, change the sprite color back to white and reset the
		// cooldown and boolean value
		if(isInvincible)
		{
			invincibleCooldown += Time.deltaTime;
			if(invincibleCooldown >= GameManager.instance.invincibleTimeLength)
			{
				isInvincible = false;
				invincibleCooldown = 0.0f;
				GetComponent<SpriteRenderer>().material.color = Color.white;
			}
		}	
	}
	// To be implemented later
	public void Respawn()
	{
		return; 
	}
	
	// Also to be implemented later
	public void GameOver()
	{
		return;
	}	                                 
	
	
}
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
		// Should fix the rotation glitch
		tf.rotation = Quaternion.identity;
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
	
	// Alter's the player's HP by amount and then updates
	// the GUI health bar
	public void changeHP(int amount)
	{
		// Adjust HP by amount and check to see if we're less than 0,
		// meaning the ship is destroyed and the player must respawn.
		data.HP += amount;
		if(data.HP <= 0)
		{
			data.numLives--;
			if(data.numLives == 0)
				GameOver();
			else Respawn();
		}
		// Don't add more than the max!
		else if(data.HP > data.maxHP)
			data.HP = data.maxHP;
		// ShipData handles the updating of the GUI
		data.updateHP();
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

	// Sends the player flying in a random direction
	void hitCyclone()
	{
		int xMove = Random.Range(1,4);
		int yMove = Random.Range(1,4);
		Vector2 newPos = new Vector2(tf.position.x + xMove,
		                             tf.position.y + yMove);
		tf.position = newPos;
	}

	// To be implemented later
	public void Respawn()
	{
		// Creates an "explosion" by using the explosion prefab
		// and then destroying the explosion object shortly after
		GameObject explosion = (GameObject)Instantiate(GameManager.instance.explosion1, 
		                                               GetComponent<Transform>().position,
		                                               Quaternion.identity);
		// I slowed this down a bit because it was playing too fast
		// and didn't give the sound enough time to play!
		explosion.GetComponent<Animator>().speed = 0.75f;
		Destroy(explosion, 1.0f);
		// Now, wait for two seconds and then
		// adjust the ship's data to reflect a respawn
		// and then move the ship back to the respawn location
		StartCoroutine(Wait(2.0f));
		data.updateScore(-500);
		data.HP = data.maxHP;
		data.updateHP();
		Camera.main.transform.position = new Vector3(0,0,-10);
		tf.position = GameManager.instance.respawnLocation;
		if(data.numLives == 2)
			GameManager.instance.GUI.Life3.enabled = false;
		else if(data.numLives == 1)
			GameManager.instance.GUI.Life2.enabled = false;
	}
	
	// Also to be implemented later
	public void GameOver()
	{
		// Just reset the game for now
		Application.LoadLevel(0);
	}

	// Waits for the given time before proceeding to the next line
	IEnumerator Wait(float duration)
	{
		yield return new WaitForSeconds(duration);
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
				invincibleMode();
				changeHP(-3);
				// No need to check for other collisions
				return;
			}
			// Same thing with the ceiling, except bounce down
			else if(col.gameObject.tag == "CeilCol")
			{
				Vector2 bounceDown = new Vector2(tf.position.x,
				                                 tf.position.y - GameManager.instance.bounceAmount);
				tf.position = bounceDown;
				invincibleMode();
				changeHP(-3);
				return;
			}
			// Certain spots on the floor and ceiling are insta-kill because
			// the player will have to crash into them at a critical point
			// on the ship, due to their location
			else if(col.gameObject.tag == "DeathCol")
				changeHP(-15);
		}
	}
	
	// Enemies and Bubbles are Triggers, not solid objects!
	// So are Cyclones!
	void OnTriggerEnter2D(Collider2D col)
	{
		// Hitting a bubble adds 3 to the player's HP
		// HP is actually "Air", but we'll call it HP
		// to avoid confusion in the code
		if(col.gameObject.tag == "Bubble")
		{
			changeHP(3);
			// Can only get health from the bubble once!
			Destroy(col.gameObject);
			return;
		}
		// When colliding with an enemy, damage the player
		// by 1/3 of its health and then destroy the enemy
		else if(col.gameObject.tag == "Enemy")
		{
			changeHP(-(int)(data.maxHP / 3));
			col.gameObject.GetComponent<EnemyEngine>().DestroyMe();
		}
		// Hitting a cyclone sends the player away in a random direction!
		else if(col.gameObject.tag == "Cyclone")
		{
			changeHP(-4);
			hitCyclone();
		}
	}
	
	
}
using UnityEngine;
using System.Collections;

public class ShipEngine : MonoBehaviour 
{
	// Stores the ship's transform component so only one call to 
	// GetComponent() is made
	public Transform tf;
	// ShipDate component
	public ShipData data;
	public bool isInvincible, waitingToSpawn;
	public float spawnTime, currentSpawnTimer;
	float invincibleCooldown;
	
	
	void Start () 
	{
		tf = GetComponent<Transform> ();
		data = GetComponent<ShipData>();
		isInvincible = false;
		invincibleCooldown = 0.0f;
		spawnTime = 3.0f;
		currentSpawnTimer = 0.0f;
		waitingToSpawn = false;
	}

	void Update()
	{
		// If we're in invincible mode, call invincibleMode() to handle
		// the cooldown and changing of sprite color
		if(isInvincible)
			invincibleMode();
		if(waitingToSpawn)
		{
			Debug.Log("Waiting to spawn. Timer is " + currentSpawnTimer);
			currentSpawnTimer += Time.deltaTime;
			if(currentSpawnTimer >= spawnTime)
			{
				Debug.Log("AAAAAASpawn timer is" + currentSpawnTimer);
				waitingToSpawn = false;
				currentSpawnTimer = 0.0f;
				Respawn();
			}
		}
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
			else waitingToSpawn = true;
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
		int xMove = Random.Range(1,3);
		int yMove = Random.Range(1,3);
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
		                                               tf.position,
		                                               Quaternion.identity);
		Destroy(explosion, 3.0f);
		data.updateScore(-500);
		data.HP = data.maxHP;
		data.updateHP();
		// Move camera to start point (Using -10 position on z axis)
		Camera.main.transform.position = GameObject.Find("Start").transform.position;

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
			// Fixes the rotation issue by reverting to 0,0,0 before the player would see anything
			if(tf.rotation.z != 0.0f) tf.rotation = Quaternion.identity;
		}
	}
	
	// Enemies and Bubbles are Triggers, not solid objects!
	// So are Cyclones!
	void OnTriggerEnter2D(Collider2D col)
	{
		// Hitting a bubble adds 3 to the player's HP
		// HP is actually "Air", but we'll call it HP
		// to avoid confusion in the code
		if (col.gameObject.tag == "Bubble") {
			changeHP (3);
			// Can only get health from the bubble once!
			Destroy (col.gameObject);
			return;
		}
		// When colliding with an enemy, damage the player
		// by 1/3 of its health and then destroy the enemy
		else if (col.gameObject.tag == "Enemy") {
			changeHP (-(int)(data.maxHP / 3));
			col.gameObject.GetComponent<EnemyEngine> ().DestroyMe ();
		}
		// Hitting a cyclone sends the player away in a random direction!
		else if (col.gameObject.tag == "Cyclone") {
			changeHP (-4);
			hitCyclone ();
		} 
		// Colliding with boss's rock attack will deal damage equal to game manager setting for damage
		else if (col.gameObject.tag == "BossRock") {
			changeHP (-GameManager.instance.bossAttackDamage);
			Destroy (col.gameObject);
		}
		// Colliding with the boss will damage the player greatly (half their max health)
		// It will do some damage to the boss as well
		else if (col.gameObject.tag == "Boss") {
			changeHP (-(int)(data.maxHP / 2));
			col.gameObject.GetComponent<BossEngine>().takeDamage(5);
		}
	}
	
	
}
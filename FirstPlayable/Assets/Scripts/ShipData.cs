using UnityEngine;
using System.Collections;

// Contains all data pertaining to the player's ship
public class ShipData : MonoBehaviour 
{

	// All variables hidden from inspector so that necessary changes
	// are made in the GameManager and not here.

	// The ship's base move speed
	[HideInInspector]
	public float moveSpeed;	

	// The base move speed of the ship's primary weapon
	[HideInInspector]
	public float bulletSpeed;

	// The amount of time needed to pass before the next
	// bullet can be fired
	[HideInInspector]
	public float bulletCooldown;

	// The damage of the primary weapon
	[HideInInspector]
	public int bulletDamage;

	// The ship's current and max HP, respectively
	[HideInInspector]	
	public float HP, maxHP;	

	// The player's current score
	[HideInInspector]
	public int score;

	// How far from the front of the ship the bullets will spawn
	[HideInInspector]
	public Vector2 bulletOffset;

	// Number of lives remaining for the player.
	// When this is 0, it's game over man. 
	[HideInInspector]
	public int numLives;
	
	
	// The All-Powerful GameManger will be used to initialize all of these values
	void Start () 
	{
		// Bullet speed and Move speed have been scaled down 
		// so that inspector-edited values don't have to be decimals
		moveSpeed = GameManager.instance.playerMoveSpeed / 100.0f;
		bulletSpeed = GameManager.instance.playerBulletSpeed / 100.0f;
		bulletCooldown = GameManager.instance.playerBulletCooldown;
		bulletDamage = GameManager.instance.playerBulletDamage;
		bulletOffset = GameManager.instance.playerBulletOffset;
		numLives = GameManager.instance.numberOfLives;
		maxHP = GameManager.instance.playerMaxHP;
		HP = maxHP;
	}		
	
}
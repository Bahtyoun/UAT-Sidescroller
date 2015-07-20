using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Responsible for holding all data editable by the design team
// as well as managing the state of the game
public class GameManager : MonoBehaviour 
{

	// By creating a static reference to "itself", this script and all data
	// in it can be accessed without any calls to GetComponent() or without
	// a need for creating an instance of an object
	public static GameManager instance;

	// The On-Screen GUI for the primary gameplay mode
	public GUIManager GUI;

	// The player's ship
	public GameObject playerShip;
	// The respawn location of the player's ship
	// for when the player is destroyed
	public Vector3 respawnLocation;

	// Data pertaining to the player's ship. 
	public int playerMaxHP;
	public float playerMoveSpeed;
	public float playerBulletSpeed;
	public float playerBulletCooldown;
	public int playerBulletDamage;
	public float playerBulletAutoKillTime;
	public Vector2 playerBulletOffset;
	public int numberOfLives;
	// The length of time that the player is invulnerable
	// after colliding with an obstacle or an enemy ship
	public float invincibleTimeLength;
	// The distance the player bounces when hitting the floor/ceiling
	public float bounceAmount;

	// The first enemy, the BubbleShip
	public int jellyShipHP;
	public int jellyShipMoveSpeed;

	// Boss data (depressed Alien)
	public int bossHP;
	public float bossSpeed;
	public float bossAttackCooldown;
	public int bossAttackDamage;

	// The prefab containing the first explosion animation
	public GameObject explosion1, explosion2;
	public GameObject playerExplosion;


	// An array containing all of the enemies currently on the screen
	public GameObject[] enemies;

	// The Highest score the player has achieved, which will be saved upon
	// application exit and loaded upon game start (Persists between sessions!)
	public int highScore;
	
	// Menu Options and related variables
	public float SFXVolume;
	public float MusicVolume;

	// The main camera. Not sure if we'll need this yet but I'll 
	// keep it in for now
	public Camera mainCamera;
	public float cameraSpeed;
	
	// Awake runs before start, so ensure that no other instance of
	// the static reference to this game object exists. Having more than
	// one reference to the GameManager could cause issues!
	void Awake()
	{
		// Peforms a check to be sure that no other instances of this
		// exist
		if (instance == null)
			instance = this;
		else 
		{
			Debug.LogError ("Only one Game Manager may exist!");
			Destroy (gameObject);
		}
		// Load previously stored high score and volume options data
		loadData();
	}
	
	// No functions for update in this manager yet
	void Update () 
	{

	}
	
	// Saves High Score and Volume Options data on exit 
	public void saveData()
	{
		PlayerPrefs.SetInt("highscore", highScore);
		PlayerPrefs.SetFloat("fx", SFXVolume);
		PlayerPrefs.SetFloat("music", MusicVolume);
	}
	
	// Loads High Score and Volume Options data 
	public void loadData()
	{
		highScore = PlayerPrefs.GetInt("highscore");
		SFXVolume = PlayerPrefs.GetFloat("fx");
		MusicVolume = PlayerPrefs.GetFloat("music");
	}
	
	void OnApplicationQuit()
	{
		saveData();
	}
}

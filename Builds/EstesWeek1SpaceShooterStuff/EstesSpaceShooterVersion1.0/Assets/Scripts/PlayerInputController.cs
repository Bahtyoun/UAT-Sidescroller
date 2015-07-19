using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour {


	ShipData data;
	ShipEngine engine;
	ShipShooter shooter;
	
	void Start () 
	{
		// Get the components for the Motor and Data
		data = GetComponent<ShipData>();
		engine = GetComponent<ShipEngine>();
		shooter = GetComponent<ShipShooter>();
	}
	
	
	void Update () 
	{
		// Moves in the desired vertical or horizontal direction based on
		// the ship's moveSpeed float. Uses GetKey so that the movement
		// key can be held down
		if (Input.GetKey(KeyCode.W))
			engine.moveVertical(data.moveSpeed);
		if (Input.GetKey(KeyCode.S))
			engine.moveVertical(-data.moveSpeed);
		if (Input.GetKey(KeyCode.D))
			engine.moveHorizontal(data.moveSpeed);
		if (Input.GetKey(KeyCode.A))
			engine.moveHorizontal(-data.moveSpeed);
		// Also uses GetKey so that the shoot key can be held down
		// for rapid fire. The shoote componenet takes care of the 
		// firing interval
		if(Input.GetKey(KeyCode.Space))
			shooter.Shoot();		
		// Hit Escape to pause the game.
		if(Input.GetKeyDown(KeyCode.Escape))
			pauseGame();
	}
	
	// Currently does nothing but TimeScale to 0, but will later
	// enter the pause menu
	void pauseGame()
	{
		Time.timeScale = 0;
	}
	
	
}
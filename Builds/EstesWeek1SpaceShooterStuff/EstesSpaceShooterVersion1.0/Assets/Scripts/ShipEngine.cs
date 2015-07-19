using UnityEngine;
using System.Collections;

public class ShipEngine : MonoBehaviour 
{
	// Stores the ship's transform component so only one call to 
	// GetComponent() is made
	public Transform tf;
	// ShipDate component
	public ShipData data;
	
	
	void Start () 
	{
		tf = GetComponent<Transform> ();
		data = GetComponent<ShipData>();
	}
	
	// Move the player up or down,
	// depending on the positive/negative value of speed, respectively
	public void moveVertical(float speed)
	{
		Vector2 newPosition = new Vector2(tf.position.x, tf.position.y + speed);
		tf.position = newPosition;
	}

	// Move the player right or left,
	// depending on the positive/negative value of speed, respectively
	public void moveHorizontal(float speed)
	{
		Vector2 newPosition = new Vector2(tf.position.x + speed, tf.position.y);
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
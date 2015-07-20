using UnityEngine;
using System.Collections;

public class EnemyEngine : MonoBehaviour 
{
	EnemyData data;
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	// Does damage to this ship in the amount of the float parameter. 
	// If no HP remaining, calls the DestroyMe() method
	public void Damage(int amount)
	{
		// Combine the decrement and "still alive" check into one statement
		if( (GetComponent<EnemyData>().HP -= amount) <= 0)
			DestroyMe();
	}
	
	// Instead of just destroying the game object, I'm going to first replace
	// it with a prefab "explosion" object, which will take care of the explosion
	// animation and sounds. Then, this object has my permission to die. 
	public void DestroyMe()
	{
		// Update's the player's score
		GameManager.instance.playerShip.GetComponent<ShipData>().updateScore(100);
		GameObject explosion;
		// Creates an "explosion" by using the explosion prefab
		// and then destroying the explosion object shortly after
		if (this.name == "JellyShip") {
			explosion = (GameObject)Instantiate (GameManager.instance.explosion1, 
		                                   GetComponent<Transform> ().position,
		                                               Quaternion.identity);
		}
		else /*if (this.name == "Pincher")*/ {
			explosion = (GameObject)Instantiate (GameManager.instance.explosion2, 
			                                                GetComponent<Transform> ().position,
			                                                Quaternion.identity);
		}
		// I slowed this down a bit because it was playing too fast
		// and didn't give the sound enough time to play!
		explosion.GetComponent<Animator>().speed = 0.75f;
		Destroy(explosion, 1.0f);
		Destroy(gameObject);
	}
}

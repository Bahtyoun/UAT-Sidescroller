using UnityEngine;
using System.Collections;

public class ShipShooter : MonoBehaviour 
{
	// The prefab for the ship's primary bullet weapon
	public GameObject bulletPrefab;

	// The current bullet being created
	private GameObject bullet;

	// The ShipData component
	public ShipData data;

	// The Ship's transform component
	public Transform tf;

	// The current cooldown that the bullet is on
	private float currentCooldown;

	
	void Start () 
	{
		data = GetComponent<ShipData>();
		tf = GetComponent<Transform>();
		// Start out ready to fire!
		currentCooldown = data.bulletCooldown;
	}
	
	void Update () 
	{
		// If we're in cooldown mode, add Time.deltaTime
		// until we've reached the cooldown timer
		if(currentCooldown < data.bulletCooldown)
			currentCooldown += Time.deltaTime;
	}

	// Fires the primary ship weapon, a small bullet. Fires in intervals
	// of data.bulletCooldown, which will likely be a small number less than
	// one second
	public void Shoot()
	{
		// Do nothing if we're in cooldown mode
		if(currentCooldown < data.bulletCooldown) return;
		else
		{
			// Instantiate a bullet at the given offset position and rotation and then
			// add force to it to make it move
			bullet = (GameObject)Instantiate(bulletPrefab, 
			                                 (Vector2)tf.position + (Vector2)tf.forward + data.bulletOffset, 
			                                 Quaternion.identity);
			bullet.GetComponent<BulletController>().parentData = data;
			currentCooldown = 0;
		}
	}
}

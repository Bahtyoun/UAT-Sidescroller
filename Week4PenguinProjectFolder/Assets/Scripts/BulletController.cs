using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour 
{
	// This bullet will auto destroy itself after AutoKill seconds
	private float AutoKill;

	// The bullet's transform component
	private Transform tf;

	// The bullet's new position
	private Vector2 newPosition;

	// The Ship to which this bullet belongs
	public ShipData parentData;
	// Use this for initialization
	void Start () 
	{
		AutoKill = GameManager.instance.playerBulletAutoKillTime;
		// Automatically destroy bullet after AutoKill seconds
		Destroy(gameObject, AutoKill);
		// Note that parent data is set in the ShipShooter component
		tf = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		newPosition = new Vector2(tf.position.x + (parentData.bulletSpeed * Time.deltaTime), 
		                          tf.position.y);
		tf.position = newPosition;
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		// If an enemy is hit, damage it. Then, self-destruct.
		if(col.gameObject.tag == "Enemy")
		{
			// TODO: Not ready to implement yet
			col.gameObject.GetComponent<EnemyEngine>().Damage(GameManager.instance.playerBulletDamage);
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "CeilCol"
		   || col.gameObject.tag == "FloorCol"
		   || col.gameObject.tag == "DeathCol")
			Destroy(gameObject);
	}
}

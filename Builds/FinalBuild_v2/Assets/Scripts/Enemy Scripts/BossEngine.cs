using UnityEngine;
using System.Collections;

public class BossEngine : MonoBehaviour {

	private int BossHP;
	private float moveSpeed, currentCooldown, camWidth;
	private GameObject rock;
	private Vector2 attackOffset;
	public GameObject RockPrefab;
	
	// Use this for initialization
	void Start () {
		BossHP = GameManager.instance.bossHP;
		moveSpeed = GameManager.instance.bossSpeed / 100.0f;
		currentCooldown = 0.0f;
		attackOffset = new Vector2(transform.position.x - 1.0f , transform.position.y);
		camWidth = Camera.main.orthographicSize * Camera.main.aspect;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentCooldown < GameManager.instance.bossAttackCooldown)
			currentCooldown += Time.deltaTime;
		
		else if (currentCooldown >= GameManager.instance.bossAttackCooldown) {
			// If the boss is coming up, start throwing rocks!
			if(transform.position.x <= (Camera.main.transform.position.x + (2.0f * camWidth))){
				rock = (GameObject)Instantiate(RockPrefab, 
				                               attackOffset, 
				                               Quaternion.identity);
			}
			currentCooldown = 0.0f;
		}
	}
	
	public void takeDamage(int damage)
	{
		BossHP -= damage;
		
		if (BossHP <= 0) {
			// Update's the player's score
			GameManager.instance.playerShip.GetComponent<ShipData>().updateScore(1000);
			
			// Destroy the boss
			Destroy(gameObject);

			// Go to main menu, since we don't have High score list.
			Application.LoadLevel(0);
		}
	}
}
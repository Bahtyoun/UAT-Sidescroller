using UnityEngine;
using System.Collections;

public class BossEngine : MonoBehaviour {

	[HideInInspector]
	int BossHP;
	[HideInInspector]
	float moveSpeed;

	// Use this for initialization
	void Start () {
		BossHP = GameManager.instance.bossHP;
		moveSpeed = GameManager.instance.bossSpeed / 100.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void takeDamage(int damage)
	{
	}
}

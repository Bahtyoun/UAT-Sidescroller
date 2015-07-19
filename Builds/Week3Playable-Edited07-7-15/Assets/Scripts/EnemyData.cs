using UnityEngine;
using System.Collections;

public class EnemyData : MonoBehaviour {

	// At present time, all ships have two things in common: movement
	// speed and HP
	[HideInInspector]
	public int HP;
	[HideInInspector]
	public float moveSpeed;

}

using UnityEngine;
using System.Collections;

public class JellyShipData : EnemyData 
{
	// No instance data at present time because moveSpeed and HP are 
	// inherited from EnemyData

	void Start()
	{
		// Scaled down so large values can be entered into the inspector
		moveSpeed = GameManager.instance.jellyShipMoveSpeed / 100.0f;
		HP = GameManager.instance.jellyShipHP;
	}
}

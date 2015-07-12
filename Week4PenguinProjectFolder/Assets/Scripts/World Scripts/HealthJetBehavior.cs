using UnityEngine;
using System.Collections;

public class HealthJetBehavior : MonoBehaviour 
{
	public GameObject BubblePrefab;
	float timer, currentTime;
	GameObject bubble;
	Vector2 offset;

	// Use this for initialization
	void Start () 
	{
		Transform tf = GetComponent<Transform>();
		offset = new Vector2(tf.position.x, tf.position.y + 1.5f);
		timer = 3.0f;
		currentTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentTime += Time.deltaTime;
		if (currentTime >= timer) 
		{
			bubble = (GameObject)Instantiate(BubblePrefab, 
			                                 offset, 
			                                 Quaternion.identity);

			currentTime = 0.0f;
		}
	
	}
}

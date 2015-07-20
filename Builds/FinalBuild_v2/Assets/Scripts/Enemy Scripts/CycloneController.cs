using UnityEngine;
using System.Collections;

public class CycloneController : MonoBehaviour 
{

	Transform tf;
	float xLeft, xRight, currentLerp;
	Vector3 originalPos, xRightPos;
	bool movingRight;



	// Use this for initialization
	void Start () 
	{
		tf = GetComponent<Transform>();
		originalPos = tf.position;
		xRightPos = new Vector3(tf.position.x + 5.0f, tf.position.y, 0);
		currentLerp = 0;
		movingRight = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(currentLerp < 1 && movingRight)
		{
			currentLerp += Time.deltaTime;
			if(currentLerp >= 1)
				movingRight = false;
		}
		else if(currentLerp > 0)
		{
			currentLerp -= Time.deltaTime;
			if(currentLerp <= 0)
				movingRight = true;
		}

		tf.position = Vector3.Lerp(originalPos, xRightPos, currentLerp);
	}
}

using UnityEngine;
using System.Collections;

public class PincherTriggered : MonoBehaviour {


	public Transform sightStart, sightEnd;
	public bool inPerimeter = false;
	public GameObject Animation;


	void Update()
	{
		Raycasting ();
		Behaviours ();
	
	}



	// Use this for initialization
	void Raycasting () 
	
	{
		Debug.DrawLine(sightStart.position, sightEnd.position, Color.magenta);
		inPerimeter = Physics2D.Linecast (sightStart.position, sightEnd.position);

	}
	
	// Update is called once per frame
	void Behaviours()
	{
		if (inPerimeter = true) 
		{
			Animation.SetActive (true);

		} 
		else		
		{
			Animation.SetActive(false);
					
		}

	}
}

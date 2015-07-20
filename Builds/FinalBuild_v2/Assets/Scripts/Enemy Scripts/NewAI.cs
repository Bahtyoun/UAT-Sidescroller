using UnityEngine;
using System.Collections;

public class NewAI : MonoBehaviour {
	public GameObject target;
	private float moveSpeed;
	public int rotationSpeed;
	public int maxdistance;
	
	private Transform myTransform;
	
	void Awake(){
		myTransform = transform;
	}
	
	
	void Start () {
		moveSpeed = GetComponent<JellyShipData> ().moveSpeed;
		//GameObject go = GameObject.FindGameObjectWithTag("PlayerShipPrefab");
		
		//target = go.transform;
		
		//maxdistance = 2;
	}
	
	
	void Update () {
		Debug.DrawLine(target.transform.position, myTransform.position, Color.red); 
		
		
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, 
		                                        Quaternion.LookRotation(target.transform.position - myTransform.position), 
		                                        rotationSpeed * Time.deltaTime);
		
		if(Vector3.Distance(target.transform.position, myTransform.position) < maxdistance){
			//Move towards target
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
			transform.rotation = Quaternion.Euler(myTransform.rotation.eulerAngles.x, 0, 0);
			
		}
	}
	
}
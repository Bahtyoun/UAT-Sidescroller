using UnityEngine;
using System.Collections;

public class PincherScript : MonoBehaviour 
{
	private Vector3 pointB;
		
		IEnumerator Start()
		{
			var pointA = transform.position;
		    pointB.x = pointA.x;
		    pointB.y = pointA.y + 0.75f;
			while(true)
			{
				yield return StartCoroutine(MoveObject(transform, pointA, pointB, 2.0f));
				yield return StartCoroutine(MoveObject(transform, pointB, pointA, 2.0f));
			}
		}
		
		IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
		{
			var i= 0.0f;
			var rate= 1.0f/time;
			while(i < 1.0f)
			{
				i += Time.deltaTime * rate;
				thisTransform.position = Vector3.Lerp(startPos, endPos, i);
				yield return null;
			}
		}
	}

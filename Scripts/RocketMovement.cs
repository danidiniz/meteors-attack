using UnityEngine;
using System.Collections;

public class RocketMovement : MonoBehaviour 
{
	void Update () 
	{
		transform.Translate (Vector3.left * 3 * Time.deltaTime);
	}
}

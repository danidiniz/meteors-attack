using UnityEngine;
using System.Collections;

public class MeteorsMovement : MonoBehaviour {

	public Vector3 target;
	public float speedMovement;

	void FixedUpdate()
	{
		transform.position = Vector3.MoveTowards (transform.position, target, speedMovement * Time.deltaTime);
	}
}

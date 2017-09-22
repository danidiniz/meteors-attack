using UnityEngine;
using System.Collections;

public class SpawnerRotation : MonoBehaviour {

	// Rotation speed of spawner
	public static float rotationSpeed;

	void Update () 
	{
		transform.Rotate (0.0f, 0.0f, Time.deltaTime * rotationSpeed);
	}
}

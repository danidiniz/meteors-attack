using UnityEngine;
using System.Collections;

public class AlienRotation : MonoBehaviour {

	private float speedRotation;
	
	void Start()
	{
		if (LevelMode.levelMode.Equals ("Easy"))
			speedRotation = 30.0f;
		else if (LevelMode.levelMode.Equals ("Medium"))
			speedRotation = 40.0f;
		else if (LevelMode.levelMode.Equals ("Hard"))
			speedRotation = 50.0f;
	}

	void Update () 
	{
		transform.Rotate(0, 0, speedRotation * Time.deltaTime, Space.World);
	}
}

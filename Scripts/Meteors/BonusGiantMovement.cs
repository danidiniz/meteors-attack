using UnityEngine;
using System.Collections;

public class BonusGiantMovement : MonoBehaviour 
{
	
	public float speedMovement;
	
	public string direction;
	
	void Update () 
	{
		if (direction.Equals ("left"))
			transform.Translate ( Vector3.left * speedMovement );
		else
			transform.Translate ( Vector3.right * speedMovement );
	}
}
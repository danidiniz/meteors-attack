using UnityEngine;

public class PlayerRotation : MonoBehaviour {
	
	public float speedRotation;

    void Awake()
    {
        speedRotation = PlayerPrefs.GetFloat("Rotation speed");
    }
	
	void FixedUpdate()
	{
		if(Arrow.isArrow)
		{
			if(ArrowOnClick.clickLeft)
				transform.Rotate(0, 0, speedRotation * Time.deltaTime, Space.World); // anti horario
			
			else if(ArrowOnClick.clickRight)
				transform.Rotate(0, 0, -speedRotation * Time.deltaTime, Space.World); // horario
		}
	}
	
}

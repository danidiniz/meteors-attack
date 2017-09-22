using UnityEngine;

public class ArrowOnClick : MonoBehaviour {
	
	public static bool clickLeft, clickRight;
	
	public void OnMouseDown()
	{
		if (this.name.Equals ("leftArrow")) 
		{
			clickLeft = true;
			clickRight = false;
        }
		else if(this.name.Equals("rightArrow"))
		{
			clickLeft = false;
			clickRight = true;
		}
	}

	public void OnMouseUp()
	{
		clickLeft = false;
		clickRight = false;
	}
}

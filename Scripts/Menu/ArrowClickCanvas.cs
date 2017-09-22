using UnityEngine;
using System.Collections;

public class ArrowClickCanvas : MonoBehaviour {

    public static bool clickLeft, clickRight;

    public void OnMouseDown()
    {
        if (this.name.Equals("Left arrow"))
        {
            clickLeft = true;
            clickRight = false;
        }
        else if (this.name.Equals("Right arrow"))
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

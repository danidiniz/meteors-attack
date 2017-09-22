using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerRotationCanvas : MonoBehaviour
{

    public static float speedRotation;

    RectTransform rectPlayer;

    void Awake()
    {
        rectPlayer = this.GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if (ArrowClickCanvas.clickLeft)
            rectPlayer.transform.Rotate(0, 0, speedRotation * Time.deltaTime, Space.World); // anti horario

        else if (ArrowClickCanvas.clickRight)
            rectPlayer.transform.Rotate(0, 0, -speedRotation * Time.deltaTime, Space.World); // horario
    }
}

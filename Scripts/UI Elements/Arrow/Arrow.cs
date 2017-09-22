using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	Vector3 upperCorner, target;
	public static float arrowBounds, maxWidth, maxHeigth;
	public GameObject arrowRenderer, leftArrow, rightArrow;

	public static bool isArrow;

	void Awake()
	{
		isArrow = true;

		upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f); // upperCorner = canto superior
		target = Camera.main.ScreenToWorldPoint (upperCorner);
		arrowBounds = arrowRenderer.GetComponent<Renderer>().bounds.extents.x;
		maxWidth = target.x;
		maxHeigth = target.y;
		
		leftArrow.transform.position = new Vector3 (-maxWidth + arrowBounds, -maxHeigth + arrowBounds, 0.0f);
		rightArrow.transform.position = new Vector3 (maxWidth - arrowBounds, -maxHeigth + arrowBounds, 0.0f);
	}
}

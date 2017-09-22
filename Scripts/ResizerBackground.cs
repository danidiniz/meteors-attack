using UnityEngine;
using System.Collections;

public class ResizerBackground : MonoBehaviour
{

	void Awake()
	{

		SpriteRenderer rend = this.GetComponent<SpriteRenderer>();

		if (rend == null)
			return;

		float width = rend.sprite.bounds.size.x;
		float height = rend.sprite.bounds.size.y;

		float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
		float worldScreenWidth = worldScreenHeight / (float)Screen.height * (float)Screen.width;

		transform.localScale = new Vector3 (worldScreenWidth / width, worldScreenHeight / height, transform.localScale.z);
	}


}

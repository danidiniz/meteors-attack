using UnityEngine;
using System.Collections;

public class TapHere : MonoBehaviour 
{


	void Start()
	{
		StartCoroutine ( scaling() );
	}

	private IEnumerator scaling()
	{
		for (float i = .9f; i > 0.7f; i -= 0.025f) 
		{
			this.transform.localScale = new Vector3(i, i, 0.0f );
			yield return new WaitForSeconds( 0.05f );
		}

		for (float i = 0.7f; i < 0.9f; i += 0.025f) 
		{
			this.transform.localScale = new Vector3(i, i, 0.0f );
			yield return new WaitForSeconds( 0.05f );
		}

		StartCoroutine ( scaling() );
	}

}

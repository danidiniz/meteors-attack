using UnityEngine;
using System.Collections;

public class ColorAnimationCollider : MonoBehaviour {
	
	void Start()
	{
		if (!this.name.Contains(LevelMode.levelMode))
			this.gameObject.SetActive( false );
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals ("Meteor"))
		{
			MeteorsInfo meteorInfo = other.GetComponent<MeteorsInfo> ();
			
			// Verificando se ele eh um color meteor ou invisible meteor
			// Soh esses meteors tem animacao
			if (meteorInfo.meteorType.Equals ("Color") || meteorInfo.meteorType.Equals ("Invisible")) {
				other.GetComponent<MeteorsAnimation> ().colidiu = true;
			}
		}

		else if(other.gameObject.GetComponent<MeteorsInfo>().meteorType.Equals("Bonus Color"))
		{
				other.GetComponent<MeteorsAnimation> ().colidiu = true;
		}
	}
}

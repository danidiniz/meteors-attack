using UnityEngine;
using System.Collections;

public class MeteorsCollider : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		//if (other.gameObject.tag.Equals ("Astronaut shot")) {
		//	Destroy (this.gameObject);
		//	Destroy (other.gameObject);
		//}
		
		// Colisao de meteor com meteor
		if(other.gameObject.tag.Equals("Meteor") && !this.gameObject.tag.Equals("Enemy meteor"))
		{
			// Primeiro verifica se esse nao eh um black meteor e o que ele colidiu for um black meteor
			if (!this.GetComponent<MeteorsInfo> ().meteorType.Equals ("Neutral") &&
			    other.GetComponent<MeteorsInfo> ().meteorType.Equals ("Neutral")) 
			{
				// Destroi o black meteor
				Destroy (other.gameObject);
			}
			// Se o meteor que ele colidiu for um Giant
			else if (!this.GetComponent<MeteorsInfo> ().meteorType.Equals ("Neutral") &&
			         other.GetComponent<MeteorsInfo>().meteorType.Equals("Giant"))
			{
				other.GetComponent<MeteorOnClick>().OnMouseDown();
				Destroy( this.gameObject );
			}
		}

		if(other.gameObject.tag.Equals("Bonus meteor"))
		{
			// Se estiver no bonus e um giant colidir com giant
			if(this.GetComponent<MeteorsInfo>().meteorType.Equals("Bonus Giant") && 
			   other.gameObject.GetComponent<MeteorsInfo>().meteorType.Equals("Bonus Giant"))
			{
				other.gameObject.GetComponent<MeteorOnClick>().OnMouseDown();
			}
		}

        // Verificando se um giant meteor colidiu com uma das arrows
        // Fiz isso porque o player não conseguia girar quando um giant meteor estava sob uma das arrows
        if (this.GetComponent<MeteorsInfo>().meteorType.Equals("Giant") &&
            (other.gameObject.name.Equals("leftArrow") || other.gameObject.name.Equals("rightArrow")))
            Destroy(this.gameObject);

	}
}

using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour 
{
	
	public string playerColor;
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag.Equals("Meteor") || other.gameObject.tag.Equals("Enemy meteor"))
		{
			// MeteorInfo Component pra facilitar
			MeteorsInfo meteorInfo = other.GetComponent<MeteorsInfo>();
			
			// Antes de tudo, verifica black meteor
			if(meteorInfo.meteorColor.Equals("Black"))
			{
				Destroy(other.gameObject);
			}
			
			// Errou
			else if(!meteorInfo.meteorColor.Equals(playerColor))
			{
				// Dano do meteor retira 'x' vidas
				PlayerInfo.playerLife -= meteorInfo.meteorDamage;
				if(PlayerInfo.playerLife < 0) PlayerInfo.playerLife = 0;
				
				// Divide pela metade o player count
				PlayerInfo.playerCount /= 2;
				
				// Animacao de erro
				other.gameObject.GetComponent<Explosions>().explosionMiss( other.transform );
				
				// Som de erro
				
				//MatchInfo.errou(other.GetComponent<MeteorsInfo>()); // Para estatisticas do jogo
				
				//Debug.Log("Errou um " + meteorInfo.meteorType +" "+ meteorInfo.meteorColor);
			}
			
			// Acertou
			else if(meteorInfo.meteorColor.Equals(playerColor))
			{
                // Acrescenta o score
                PlayerInfo.playerScore += meteorInfo.meteorScore;
				
				// Acrescenta o count
				PlayerInfo.playerCount += 1;
				
				// Animcao de acerto
				other.gameObject.GetComponent<Explosions>().explosionPoints( meteorInfo, other.transform );

				// Som de acerto
				
				MatchInfo.acertou(other.GetComponent<MeteorsInfo>()); // Para estatisticas
				
				//Debug.Log("Acertou um " + meteorInfo.meteorType +" "+ meteorInfo.meteorColor + " e recebeu " + meteorInfo.meteorScore + " pontos");
			}
			
			Destroy (other.gameObject);
		}
		
		// Bonus meteor
		if(other.gameObject.tag.Equals("Bonus meteor"))
		{
			// MeteorInfo Component pra facilitar
			MeteorsInfo meteorInfo = other.GetComponent<MeteorsInfo>();
			
			// Verifica se errou
			if(!meteorInfo.meteorColor.Equals(playerColor))
			{
				
				// Animacao de erro
				other.gameObject.GetComponent<Explosions>().explosionMiss( other.transform );

				// Som de erro
				
				//Debug.Log("Errou um " + meteorInfo.meteorType +" "+ meteorInfo.meteorColor);
			}
			
			// Acertou
			else if(meteorInfo.meteorColor.Equals(playerColor))
			{
				// Acrescenta o score
				PlayerInfo.playerScore += meteorInfo.meteorScore;
				
				// Acrescenta o count
				PlayerInfo.playerCount += 1;
				
				// Animacao de acerto
				other.gameObject.GetComponent<Explosions>().explosionPoints( meteorInfo, other.transform );

				// Som de acerto
				
				//Debug.Log("Acertou um " + meteorInfo.meteorType +" "+ meteorInfo.meteorColor + " e recebeu " + meteorInfo.meteorScore + " pontos");
			}
			
			Destroy( other.gameObject );
		}
	}


























}
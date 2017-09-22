using UnityEngine;
using System.Collections;

public class MeteorOnClick : MonoBehaviour 
{
	
	public void OnMouseDown()
	{
		MeteorsInfo meteorInfo = this.GetComponent<MeteorsInfo>();
		
		// Clicou na giant meteor ou bonus giant
		if(meteorInfo.meteorType.Equals("Giant") || meteorInfo.meteorType.Equals("Bonus Giant"))
		{
            // Diminui tamanho do meteor
            //this.transform.localScale = new Vector3(this.transform.localScale.x - 1.0f, this.transform.localScale.y - 1.0f, 1.0f);

            // Diminui damage do meteor
            //meteorInfo.meteorDamage -= 1;

            // Diminui o score recebido
            //if(meteorInfo.meteorScore > 10)
            //	meteorInfo.meteorScore -= 10;

            // Acrescenta o score
            PlayerInfo.playerScore += meteorInfo.meteorScore;

            // Acrescenta o count
            PlayerInfo.playerCount += 1;

            // Para estatisticas
            MatchInfo.acertou(meteorInfo);

            // Explode animaçao da pontuacao
            this.GetComponent<Explosions>().explosionPoints(meteorInfo, this.transform);

            Destroy(this.gameObject);

            //Debug.Log("Destruiu um giant meteor e recebeu " + meteorInfo.meteorScore + " pontos");
        }
	}
	
}
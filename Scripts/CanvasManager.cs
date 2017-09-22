using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour 
{
	
	// Text do numero de vidas do player
	public Text playerLifeText;
	
	// Text da pontuacao do player
	public Text playerScoreText;
	
	// Text da maior pontuacao do player
	public Text playerHighScoreText;

	// Text do count
	public Text countText;

    // Text com info dos bônus (deletar depois, talvez)
    public Text bonuses;
	
	void Update()
	{
		// Atualizando numero de vidas
		playerLifeText.text = PlayerInfo.playerLife.ToString();
		
		// Atualizando score
		playerScoreText.text = "Score " + PlayerInfo.playerScore.ToString ();
		
		// Inicializando text do high score
		playerHighScoreText.text = "High score ";

		// Atualizando count
		countText.text = PlayerInfo.playerCount.ToString();
		
		// Atualizando high score 
		// High score no easy
		if(LevelMode.levelMode.Equals("Easy"))
			playerHighScoreText.text += PlayerInfo.playerHighScoreEasy.ToString ();
		
		// High score no medium
		if(LevelMode.levelMode.Equals("Medium"))
			playerHighScoreText.text += PlayerInfo.playerHighScoreMedium.ToString ();
		
		// High score no hard
		if(LevelMode.levelMode.Equals("Hard"))
			playerHighScoreText.text += PlayerInfo.playerHighScoreHard.ToString ();


        //bonuses.text = "Meteors normal: " + MatchInfo.bonusNormal.ToString() + "\n" +
        //   "Meteors color: " + MatchInfo.bonusColor.ToString() + "\n" +
        //    "Meteors invisible: " + MatchInfo.bonusInvisible.ToString() + "\n" +
        //    "Meteors giant: " + MatchInfo.bonusGiant.ToString() + "\n" + 
        //   "Spawn harder: " + MatchInfo.spawnHarderCount.ToString();
	}
}
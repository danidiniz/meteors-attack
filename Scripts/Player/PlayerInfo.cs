using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour 
{
	// Numero de vidas
	public static int playerLife;
	
	// Pontuacao na partida
	public static int playerScore;
	
	// Maxima pontuacao atingida;
	public static int playerHighScoreEasy, playerHighScoreMedium, playerHighScoreHard;
	
	// Contador de acertos na partida
	public static int playerCount; 
	
	// Numero de estrelas (comprar itens)
	public static int playerStars;
	
	// Numero de Stop Time
	public static int playerStopTime;
	
	void Awake()
	{
		playerLife = 3 + PlayerPrefs.GetInt ("playerLife");
		
		playerScore = 0;
		
		playerHighScoreEasy = PlayerPrefs.GetInt ("playerHighScoreEasy");
		playerHighScoreMedium = PlayerPrefs.GetInt ("playerHighScoreMedium");
		playerHighScoreHard = PlayerPrefs.GetInt ("playerHighScoreHard");
		
		playerCount = 0;
		
		playerStars = PlayerPrefs.GetInt ("playerStars");
		
		playerStopTime = PlayerPrefs.GetInt ("playerStopTime");
	}
	
	void Update()
	{
        // Atualizando High Scores, caso o player ultrapasse
        if (LevelMode.levelMode.Equals("Easy"))
            if (playerScore > playerHighScoreEasy)
            {
                playerHighScoreEasy = playerScore;
                PlayerPrefs.SetInt("playerHighScoreEasy", playerHighScoreEasy);
            }

        if (LevelMode.levelMode.Equals("Medium"))
            if (playerScore > playerHighScoreMedium)
            {
                playerHighScoreMedium = playerScore;
                PlayerPrefs.SetInt("playerHighScoreMedium", playerHighScoreMedium);
            }

        if (LevelMode.levelMode.Equals("Hard"))
            if (playerScore > playerHighScoreHard)
            {
                playerHighScoreHard = playerScore;
                PlayerPrefs.SetInt("playerHighScoreHard", playerHighScoreHard);
            }
		//	
	}

	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}

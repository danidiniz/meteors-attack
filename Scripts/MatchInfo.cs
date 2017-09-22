using UnityEngine;
using System.Collections;

public class MatchInfo : MonoBehaviour 
{

	// Numero de meteoros destruidos (para estatísticas)
	public static int meteorNormal, meteorColor, meteorInvisible, meteorGiant;

    // Número de meteoros pra liberar o bônus
    public static int bonusNormal, bonusColor, bonusInvisible, bonusGiant;

    // Número de meteoros pra spawnmar tipo 4 ou alien
    public static int hitsForSpawnHarder, spawnHarderCount;

    private static int hitsForBonus;

    void Awake()
	{
		// Inicializando as variaveis
		meteorNormal = 0;
		meteorColor = 0;
		meteorInvisible = 0;
		meteorGiant = 0;

        bonusNormal = 0;
        bonusColor = 0;
        bonusInvisible = 0;
        bonusGiant = 0;

        spawnHarderCount = 0;
    }

    void Start()
    {
        if (LevelMode.levelMode.Equals("Easy")) hitsForBonus = 15;
        else if (LevelMode.levelMode.Equals("Medium")) hitsForBonus = 25;
        else if (LevelMode.levelMode.Equals("Hard")) hitsForBonus = 50;

        if (LevelMode.levelMode.Equals("Easy")) hitsForSpawnHarder = 25;
        else if (LevelMode.levelMode.Equals("Medium")) hitsForSpawnHarder = 20;
        else if (LevelMode.levelMode.Equals("Hard")) hitsForSpawnHarder = 10;
    }

		public static void acertou(MeteorsInfo meteor)
	{
        if (meteor.meteorType.Equals("Normal"))
        {
            meteorNormal++;
            bonusNormal++;

            if (bonusNormal >= hitsForBonus)
            {
                bonusNormal = 0;
                MatchControl.addBonusToList( "spawnBonusNormal" );
            }
        }
        else if (meteor.meteorType.Equals("Color"))
        {
            meteorColor++;
            bonusColor++;

            if (bonusColor >= hitsForBonus)
            {
                bonusColor = 0;
                MatchControl.addBonusToList("spawnBonusColor");
            }
        }
        else if (meteor.meteorType.Equals("Invisible"))
        {
            meteorInvisible++;
            bonusInvisible++;

            if (bonusInvisible >= hitsForBonus)
            {
                bonusInvisible = 0;
                MatchControl.addBonusToList("spawnBonusInvisible");
            }
        }
        else if (meteor.meteorType.Equals("Giant"))
        {
            meteorGiant++;
            bonusGiant++;

            if (bonusGiant >= hitsForBonus)
            {
                bonusGiant = 0;
                MatchControl.addBonusToList("spawnBonusGiant");
            }
        }

        // Verifica o número de acertos pra dificultar (adiciona spawnType4 e aliens)

        spawnHarderCount++;

        if (spawnHarderCount >= hitsForSpawnHarder)
        {
            spawnHarderCount = 0;

            int i = Random.Range(0, 3);

            switch (i)
            {
                case 0:
                    MatchControl.hardSpawn("spawnType4");
                    break;
                case 1:
                    MatchControl.hardSpawn("spawnAlien");
                    break;
                case 2:
                    MatchControl.hardSpawn("spawnAlien4");
                    break;
            }
        }
	}

	// Salvando os meteors que errou nas estatisticas
	//public static void errou(MeteorsInfo meteor)
	//{
	//	if (meteor.meteorType.Equals ("Normal"))
	//		PlayerPrefs.SetInt ("Errou meteor normal", PlayerPrefs.GetInt("Errou meteor normal") + 1);
	//	else if(meteor.meteorType.Equals("Color"))
	//		PlayerPrefs.SetInt ("Errou meteor color", PlayerPrefs.GetInt("Errou meteor color") + 1);
	//	else if(meteor.meteorType.Equals("Invisible"))
	//		PlayerPrefs.SetInt ("Errou meteor invisible", PlayerPrefs.GetInt("Errou meteor invisible") + 1);
	//	else if(meteor.meteorType.Equals("Giant"))
	//		PlayerPrefs.SetInt ("Errou meteor giant", PlayerPrefs.GetInt("Errou meteor giant") + 1);
	//}


}

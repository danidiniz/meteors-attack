using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MatchControl : MatchInfo
{

    private Text textAnimations;
    private Animator textAnimator;
    private RectTransform textTransform;

    private List<GameObject> meteorsInScene = new List<GameObject>();

    public static bool gameOverInitialized;

    // Panel das estatísticas
    public GameObject statisticsPanel;
    // Texts pra mostrar as pontuações
    public Text scoreText, normalText, colorText, invisibleText, giantText, starsEarnedText;

    // Player
    private GameObject playerParent, blueChild, yellowChild, greenChild, redChild;

    // Buttons
    public GameObject restartButton, menuButton;

    GoogleMobileAdsDemoScript adInstance;
    public GameObject AdMob;

    // Instancia do MusicHandler pra parar a musica no game over
    MusicHandler musicInstance;

    void Awake()
    {
        textAnimations = GameObject.Find("Text game over animation").GetComponent<Text>();
        textAnimator = GameObject.Find("Text game over animation").GetComponent<Animator>();
        textTransform = GameObject.Find("Text game over animation").GetComponent<RectTransform>();

        gameOverInitialized = false;

        adInstance = GameObject.Find("Main Camera").GetComponent<GoogleMobileAdsDemoScript>();

        // Player
        playerParent = GameObject.Find("Player");
        blueChild = GameObject.Find("Blue Edge Collider");
        yellowChild = GameObject.Find("Yellow Edge Collider");
        greenChild = GameObject.Find("Green Edge Collider");
        redChild = GameObject.Find("Red Edge Collider");

        // Music
        musicInstance = GameObject.Find("Music handler").GetComponent<MusicHandler>();
    }

    void Update()
    {
        if (PlayerInfo.playerLife <= 0 && !gameOverInitialized)
        {
            StartCoroutine(onGameOver());
        }
    }


    public static void addBonusToList(string bonus)
    {
        Spawner.bonusList.Add(bonus);
    }

    // Método pra startar o spawnType4 ou enemies
    public static void hardSpawn(string spawn)
    {
        if (!Spawner.spawnList.Contains(spawn))
        {
            Debug.Log("Adicionou hard: " + spawn);
            Spawner.spawnList.Add(spawn);
        }
    }


    public IEnumerator onGameOver()
    {
        gameOverInitialized = true;

        // Ativa a publicidade
        AdMob.SetActive(true);

        // Fade na música
        StartCoroutine( musicInstance.fadeMusic() );

        // Stop a música
        //musicInstance.stopAudios();

        // Para todos spawns
        StartCoroutine(GameObject.Find("Spawner").GetComponent<Spawner>().stopSpawns());

        // Guarda todos os meteors em cena em um array
        StartCoroutine(stopMeteorsInScene());

        // Animação de texto do Game Over, uma coroutine
        StartCoroutine(gameOverTextAnimation());

        // Destrói o player
        StartCoroutine(destroyPlayer());

        // Pega todos meteors em cena, para o movimento deles e destrói
        StartCoroutine(destroyMeteorsInScene());

        yield return new WaitForSeconds(3.0f);

        // Desativa a animação
        textAnimator.SetBool("game over", false);

        // Carrega estatísticas e pontuação
        StartCoroutine(statistics());

        yield return new WaitForSeconds(2.5f);
        // Mostra os buttons de restart e main menu
        restartButton.SetActive(true);
        menuButton.SetActive(true);

        yield return new WaitForSeconds(1.0f);
    }

    // Pega os meteors que estão na cena e guarda em um array
    public IEnumerator stopMeteorsInScene()
    {
        GameObject[] meteors = GameObject.FindGameObjectsWithTag("Meteor");
        GameObject[] enemyMeteors = GameObject.FindGameObjectsWithTag("Enemy meteor");
        GameObject[] bonusMeteors = GameObject.FindGameObjectsWithTag("Bonus meteor");

        for (int i = 0; i < meteors.Length; i++)
        {
            meteorsInScene.Add(meteors[i]);

            // Destruindo component de click do giant meteor, pro player não clicar caso esteja no game over
            if (meteors[i].GetComponent<MeteorsInfo>().meteorType.Equals("Giant"))
                Destroy(meteors[i].GetComponent<MeteorOnClick>());
        }

        for (int i = 0; i < enemyMeteors.Length; i++)
        {
            meteorsInScene.Add(enemyMeteors[i]);
        }

        for (int i = 0; i < bonusMeteors.Length; i++)
        {
            meteorsInScene.Add(bonusMeteors[i]);
        }

        for (int i = 0; i < meteorsInScene.Count; i++)
        {
            meteorsInScene[i].GetComponent<MeteorsMovement>().speedMovement = 0;
        }

        yield break;
    }

    public IEnumerator gameOverTextAnimation()
    {
        // Ativa a animação
        textAnimator.SetBool("game over", true);

        // Muda o texto
        textAnimations.text = "Game over";

        // Espera animação terminar
        // yield return new WaitForSeconds(3.0f);

        // Muda o texto
        //textAnimations.text = "";

        // Desativa a animação
        // textAnimator.SetBool("game over", false);

        yield break;
    }

    public IEnumerator destroyMeteorsInScene()
    {
        //Debug.Log("Vai destruir os meteors em: 3");
        //yield return new WaitForSeconds(1.0f);
        //Debug.Log("2");
        //yield return new WaitForSeconds(1.0f);
        //Debug.Log("1");
        //yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < meteorsInScene.Count; i++)
        {
            // Pega o component da explosão
            Explosions explosion = meteorsInScene[i].GetComponent<Explosions>();
            // Explode
            explosion.explosionMiss(meteorsInScene[i].transform);
            // Destrói o game object
            Destroy(meteorsInScene[i]);

            // Tempo pra explodir em sequência
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator statistics()
    {
        // Ativa o panel com as estatísticas
        statisticsPanel.SetActive(true);

        yield return new WaitForSeconds(3.0f);

        // Score points
        scoreText.text = PlayerInfo.playerScore.ToString();
        yield return new WaitForSeconds(0.5f);

        // Normal
        normalText.text = meteorNormal.ToString();
        yield return new WaitForSeconds(0.5f);

        // Color
        colorText.text = meteorColor.ToString();
        yield return new WaitForSeconds(0.5f);

        // Invisible
        invisibleText.text = meteorInvisible.ToString();
        yield return new WaitForSeconds(0.5f);

        // Giant
        giantText.text = meteorGiant.ToString();
        yield return new WaitForSeconds(0.5f);

        // Total
        int total = meteorNormal + meteorColor + meteorInvisible + meteorGiant + (int)(5 * PlayerInfo.playerScore / 100);
        starsEarnedText.text = total.ToString();
        PlayerPrefs.SetInt("playerStars", PlayerPrefs.GetInt("playerStars") + total);

        yield break;
    }

    public IEnumerator destroyPlayer()
    {
        yield return new WaitForSeconds(3.5f);

        blueChild.GetComponent<Explosions>().explosionMiss(blueChild.transform);

        yield return new WaitForSeconds(0.5f);

        yellowChild.GetComponent<Explosions>().explosionMiss(yellowChild.transform);

        yield return new WaitForSeconds(0.5f);

        greenChild.GetComponent<Explosions>().explosionMiss(greenChild.transform);

        yield return new WaitForSeconds(0.5f);

        redChild.GetComponent<Explosions>().explosionMiss(redChild.transform);

        yield return new WaitForSeconds(1.0f);

        playerParent.GetComponent<Explosions>().explosionMiss(playerParent.transform);

        Destroy(playerParent);
    }

}
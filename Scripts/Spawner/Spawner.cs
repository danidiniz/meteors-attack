using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

// 7 tipos de Spawn

// 3 modos de meteoros:
// - Color meteor
// - Invisible meteor
// - Giant meteor

public class Spawner : MonoBehaviour 
{
	
	// Array com prefabs dos meteors
	public static GameObject[] meteorsNormal, meteorsColor, meteorsInvisible, meteorsGiant, meteorsALL;
	public static GameObject meteorNeutral;
	
	// Intervalo entre spawn
	public static float spawnTime;
	
	// Lista contendo todos tipos de spawns
	public static List<string> spawnList;
	
	[HideInInspector]
	public static GameObject lastMeteorSpawned;
	
	// Lista pra salvar os meteors que vai ser usado
	private List<GameObject[]> uniaoMeteors;

    // Lista pra salvar os bônus que estão na fila pra serem executados
    public static List<string> bonusList;
	
	// Spawn 3 variaveis
	// Numero de meteors que vai spawnmar
	private int numberOfMeteorsSpawn3;
	private bool dontStop;
	
	// Spawn 4 variaveis
	public GameObject[] spawnerQuadra;
	private int quantidadeSpawn;
	
	// Alien prefab
	public GameObject alienPrefab;
	public GameObject alien4Prefab;
	
	// Variaveis pra setar Spawners de acordo com a tela
	public GameObject ballRendererPrefab;
	private float maxWidth, maxHeigth, ballExtents;

    // Variavel pra não repetir bonus
    private int lastSpawnBonus;

    // Text Animator (para bônus)
    private Animator textAnimator;

    // Bonus Text
    private GameObject bonusText;
    private float animationTime;
	
	void Awake()
	{
        // Inicializando listas
        uniaoMeteors = new List<GameObject[]>();
        spawnList = new List<string>();
        bonusList = new List<string>();
        
        // Carregando meteors
        meteorsNormal = Resources.LoadAll<GameObject> ("Prefabs/Meteors/Normal meteors");
		meteorsColor = Resources.LoadAll<GameObject> ("Prefabs/Meteors/Color meteors");
		meteorsGiant = Resources.LoadAll<GameObject> ("Prefabs/Meteors/Giant meteors");
		meteorsInvisible = Resources.LoadAll<GameObject> ("Prefabs/Meteors/Invisible meteors");
		meteorNeutral = Resources.Load<GameObject>("Prefabs/Meteors/Neutral black meteor");
		meteorsALL = Resources.LoadAll<GameObject> ("Prefabs/Meteors");
		
		// Setando posicao do spawner
		maxWidth = Camera.main.ScreenToWorldPoint ( new Vector3 (Screen.width, Screen.height, 0.0f) ).x;
		maxHeigth = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0.0f)).y;
		ballExtents = ballRendererPrefab.GetComponent<Renderer>().bounds.extents.x;
		transform.position = new Vector3 (maxWidth + ballExtents*3, 0.0f, 0.0f);
		
		// Setando a posicao dos spawners quadra
		spawnerQuadra[1].transform.position = new Vector3 (-maxWidth/2, -maxHeigth-ballExtents*3, 0.0f);
		spawnerQuadra[2].transform.position = new Vector3 (maxWidth/2, -maxHeigth-ballExtents*2, 0.0f);
		
		spawnerQuadra[0].transform.position = new Vector3 (-maxWidth/2, maxHeigth+ballExtents*2, 0.0f);
		spawnerQuadra[3].transform.position = new Vector3 (maxWidth/2, maxHeigth+ballExtents*2, 0.0f);

        // Bonus text
        bonusText = GameObject.Find("Text bonus animations");
        // Bonus animator
        textAnimator = bonusText.GetComponent<Animator>();
        // Tempo de espera até a animação terminar
        animationTime = 3.0f;

        // Adicionando spawns na lista
        spawnList.Add ( "spawnType1" );
        spawnList.Add ( "spawnType2" );
        spawnList.Add ( "spawnType3" );
    }

    void OnEnable()
	{
        // Spawnmando
        StartCoroutine(spawnList[Random.Range(0, spawnList.Count)]);

        // Fiz isso pra nao aparecer o alien 2 vezes seguidas
        //		spawnList.Add ( "spawnAlien" );
        //		spawnList.Add ( "spawnAlien4" );

        //StartCoroutine ( spawnAlien4() );
    }
	
	
	// Normal spawn (aleatorio)
	public IEnumerator spawnType1()
	{	
		// Setando o spawn
		spawnSettings ("spawnType1");

        // Instanciando
        // Randomizando um array de meteors
        for (int i = 0; i < 10; i++)
        {
            GameObject[] arrayRandom = uniaoMeteors[Random.Range(0, uniaoMeteors.Count)];
            lastMeteorSpawned = Instantiate(arrayRandom[Random.Range(0, arrayRandom.Length)], transform.position, Quaternion.identity) as GameObject;

            // Intervalo de spawn
            yield return new WaitForSeconds(spawnTime);
        }

        // Spawnmando de novo
        nextSpawn();
	}
	
	
	// Alternando em 180 graus
	public IEnumerator spawnType2()
	{
		// Setando o spawn
		spawnSettings ("spawnType2");
		
		for(int i = 0; i < 6; i++)
		{
			// Randomizando um array de meteors
			GameObject[] arrayRandom = uniaoMeteors [Random.Range (0, uniaoMeteors.Count)];
			
			// Instanciando
			lastMeteorSpawned = Instantiate (arrayRandom[ Random.Range(0, arrayRandom.Length) ], transform.position, Quaternion.identity) as GameObject;
			
			// Intervalo de spawn
			yield return new WaitForSeconds ( spawnTime );
		}

        // Spawnmando de novo
        nextSpawn();
	}
	
	
	// Varios black meteors e alguns meteors verdadeiros pelo meio!
	public IEnumerator spawnType3()
	{
		// Setando o spawn
		spawnSettings ("spawnType3");
		
		// Starta o spawn de meteors
		StartCoroutine ( spawnType3Variation() );
		
		while(dontStop)
		{
			// Starta o spawn de meteors distracao (black meteors)
			Instantiate( meteorNeutral, transform.position, Quaternion.identity );
			yield return new WaitForSeconds( 0.05f );
		}

        // Spawnmando de novo
        nextSpawn();
	}
	
	// Continuacao do SpawnType 3
	public IEnumerator spawnType3Variation()
	{	
		
		for(int i = 0; i < numberOfMeteorsSpawn3; i++)
		{
			// Randomizando um array de meteors
			GameObject[] arrayRandom = uniaoMeteors [Random.Range (0, uniaoMeteors.Count)];
			
			// Instanciando
			lastMeteorSpawned = Instantiate (arrayRandom[ Random.Range(0, arrayRandom.Length) ], transform.position, Quaternion.identity) as GameObject;
			
			// Intervalo de spawn
			yield return new WaitForSeconds ( spawnTime );
		}
		
		// Parando o while dentro de SpawnType3
		dontStop = false;
	}


    // Como esse spawn é muito difícil de pegar o jeito, resolvi só spawnmar ele de vez em quando, usando as informações do MatchInfo e MatchControl.
    // spawnList.Remove("spawnType4");
    public IEnumerator spawnType4()
	{
        // Remove ele da lista pra não repetir
        spawnList.Remove("spawnType4");

        // Espera o ultimo meteor ser destruido pra comecar esse spawn
        while (lastMeteorSpawned != null)
		{
			yield return new WaitForSeconds( 0.1f );
		}
		
		// Setando o spawn
		spawnSettings ("spawnType4"); 
		
		// Randomizando um array de meteors (nessa uniaoMeteors, tem as listas de meteors normal, color e/ou invisible, randomizo uma delas)
		GameObject[] arrayRandom = uniaoMeteors [Random.Range (0, uniaoMeteors.Count)];
//
//		// Colocando o array em ordem de id
		for (int i = 0; i < arrayRandom.Length; i++) 
		{
			GameObject curr = arrayRandom[i];
			if(curr.GetComponent<MeteorsInfo>().meteorId != i)
			{
				GameObject aux = arrayRandom[ curr.GetComponent<MeteorsInfo>().meteorId ];
				arrayRandom[i] = aux;
				arrayRandom[ curr.GetComponent<MeteorsInfo>().meteorId ] = curr;
			}
		}
		
		// Instanciando
		for(int i = 0; i < quantidadeSpawn; i++)
		{
			// Spawnma 1 meteor antes e os proximos 3 serao de acordo com esse primeiro. Isso foi feito pra manter a ordem correta

			// Randomizando um meteor no array
			int id = Random.Range(0, meteorsNormal.Length);

			// Instanciando
			Instantiate (arrayRandom[ id ], spawnerQuadra[0].transform.position, Quaternion.identity);
			id++;
			if(id > 3) id = 0;
			Instantiate (arrayRandom[ id ], spawnerQuadra[1].transform.position, Quaternion.identity);
			id++;
			if(id > 3) id = 0;
			Instantiate (arrayRandom[ id ], spawnerQuadra[2].transform.position, Quaternion.identity);
			id++;
			if(id > 3) id = 0;
			Instantiate (arrayRandom[ id ], spawnerQuadra[3].transform.position, Quaternion.identity);
			
//			// Proximos 3 meteors na ordem correta
//			for(int j = 1; j < spawnerQuadra.Length; j++)
//			{
//				id++;
//				if(id > 3) id = 0;
//
//				Instantiate( arrayRandom[ id ], spawnerQuadra[j].transform.position, Quaternion.identity );
//			}
			
			// Intervalo de spawn
			yield return new WaitForSeconds ( spawnTime );
		}

        // Spawnmando de novo
        nextSpawn();
	}
	
	// Metodo pra selecionar na ordem correta os proximos 3 meteors do Spawn Type 4
	// Recebe o id do meteor e o array que foi randomizado
	private GameObject nextMeteor(int meteorId, GameObject[] arrayRandomizado)
	{
		int nextId;
		
		// Caso ele esteja no id maximo (3, que eh o meteor red), ele pula pro id 0 (blue)
		if (meteorId == 3)
			nextId = 0;
		else
			nextId = meteorId + 1;
		
		GameObject meteor = arrayRandomizado[0];
		int i = 0;
		// Procurando o proximo meteor correto
		while(meteor.GetComponent<MeteorsInfo>().meteorId != nextId)
		{
			meteor = arrayRandomizado[i];
			i++;
		}
		
		// Retorna
		return meteor;
	}
	
	
	// Bonus em forma de elipsoide apenas com Meteors Colors
	public IEnumerator spawnBonusColor()
	{
        yield return new WaitForSeconds(2.5f);

        // Espera o ultimo meteor ser destruido pra comecar esse spawn
        while (lastMeteorSpawned != null)
        {
            yield return new WaitForSeconds(0.1f);
        }

        // Ativa a animação
        textAnimator.SetBool("color", true);
        // Muda o text
        bonusText.GetComponent<Text>().text = "Color Bonus!";

        yield return new WaitForSeconds ( animationTime );

        // Muda o text
        bonusText.GetComponent<Text>().text = "";
        // Desativa a animação
        textAnimator.SetBool("color", false);

        // Randomiza um tipo de Bonus
        spawnSettings("spawnBonus");
		
		int meteorRandom = Random.Range (0, meteorsColor.Length);
		
		for(float i = 0; i < 10; i += 0.1f)
		{
			// De metade em metade, randomiza um novo meteor e mantem a cor
			if((i > 2.4f && i < 2.5f) || (i > 4.9f && i < 5.0f) || (i > 7.4f && i < 7.5f) || (i > 9.9f && i < 10.0f))
			{
				meteorRandom = Random.Range (0, meteorsColor.Length);
			}
			
			GameObject bonusMeteor = Instantiate( meteorsColor[ meteorRandom ], transform.position, Quaternion.identity ) as GameObject;
			bonusMeteor.tag = "Bonus meteor";
			bonusMeteor.GetComponent<MeteorsInfo>().meteorType = "Bonus Color";
			yield return new WaitForSeconds( spawnTime );
		}

        // Ao terminar o bônus, remove da lista porque já foi executado
        bonusList.Remove("spawnBonusColor");

        yield return new WaitForSeconds(2.5f);

        // Próximo spawn
        nextSpawn();
    }
	
	
	public IEnumerator spawnBonusNormal()
	{
        yield return new WaitForSeconds(2.5f);

        // Espera o ultimo meteor ser destruido pra comecar esse spawn
        while (lastMeteorSpawned != null)
        {
            yield return new WaitForSeconds(0.1f);
        }
        
        // Muda o text
        bonusText.GetComponent<Text>().text = "Normal Bonus!";
        // Ativa a animação
        textAnimator.SetBool("normal", true);
		
		yield return new WaitForSeconds ( animationTime );

        // Muda o text
        bonusText.GetComponent<Text>().text = "";
        // Desativa a animação
        textAnimator.SetBool("normal", false);

        SpawnerRotation.rotationSpeed = 360.0f;

        for (int i = 0; i < 150; i++)
		{
			SpawnerRotation.rotationSpeed = Random.Range(360,1440);
			GameObject bonusMeteor = Instantiate( meteorsNormal[ Random.Range(0, meteorsNormal.Length) ], transform.position, Quaternion.identity ) as GameObject;
			bonusMeteor.tag = "Bonus meteor";
			bonusMeteor.GetComponent<MeteorsInfo>().meteorType = "Bonus Normal";
			yield return new WaitForSeconds( 0.05f );
		}

        // Ao terminar o bônus, remove da lista porque já foi executado
        bonusList.Remove("spawnBonusNormal");

        yield return new WaitForSeconds(2.5f);

        // Próximo spawn
        nextSpawn();
    }
	
	
	private bool stopFade;
	// Variavel pra fazer o fade
	private bool fade = true;
	// Variavel pra mudar o fade
	private int contador = 0;
	public IEnumerator bonusInvisibleFade()
	{
		SpriteRenderer player = GameObject.Find ("Player").GetComponent<SpriteRenderer>();
		
		if (stopFade) 
		{
			contador = 0;
			while(contador <= 100)
			{
				player.color = Color.Lerp(player.color, Color.white, 0.025f);
				yield return new WaitForSeconds (.005f);
				contador++;
			}
			yield break;
		}
		
		if(fade)
			player.color = Color.Lerp(player.color, Color.black, 0.05f);
		else
			player.color = Color.Lerp(player.color, Color.white, 0.025f);
		
		contador++;
		
		if (contador == 70) 
		{
			fade = !fade;
			contador = 0;
		}
		
		yield return new WaitForSeconds (.005f);
		
		StartCoroutine ( bonusInvisibleFade() );
	}
	
	public IEnumerator spawnBonusInvisible()
	{
        yield return new WaitForSeconds(2.5f);

        // Espera o ultimo meteor ser destruido pra comecar esse spawn
        while (lastMeteorSpawned != null)
        {
            yield return new WaitForSeconds(0.1f);
        }
        
        // Muda o text
        bonusText.GetComponent<Text>().text = "Invisible Bonus!";
        // Ativa a animação
        textAnimator.SetBool("invisible", true);

        yield return new WaitForSeconds ( animationTime );

        // Muda o text
        bonusText.GetComponent<Text>().text = "";
        // Desativa a animação
        textAnimator.SetBool("invisible", false);

        stopFade = false;
		
		StartCoroutine ( bonusInvisibleFade() );

        SpawnerRotation.rotationSpeed = Random.Range(360, 1440);

        for (int i = 0; i < 150; i++)
		{
			GameObject bonusMeteor = Instantiate( meteorsInvisible[ Random.Range(0, meteorsInvisible.Length) ], transform.position, Quaternion.identity ) as GameObject;
			bonusMeteor.tag = "Bonus meteor";
			bonusMeteor.GetComponent<MeteorsInfo>().meteorType = "Bonus Invisible";
			yield return new WaitForSeconds( 0.05f );
		}
		
		stopFade = true;

        // Ao terminar o bônus, remove da lista porque já foi executado
        bonusList.Remove("spawnBonusInvisible");

        yield return new WaitForSeconds(2.5f);

        // Próximo spawn
        nextSpawn();
    }
	
	public IEnumerator spawnBonusGiant()
	{
        yield return new WaitForSeconds(2.5f);
        
        // Espera o ultimo meteor ser destruido pra comecar esse spawn
        while (lastMeteorSpawned != null)
        {
            yield return new WaitForSeconds(0.1f);
        }
        
        // Muda o text
        bonusText.GetComponent<Text>().text = "Giant Bonus!";
        // Ativa a animação
        textAnimator.SetBool("giant", true);

        yield return new WaitForSeconds ( animationTime );

        // Muda o text
        bonusText.GetComponent<Text>().text = "";
        // Desativa a animação
        textAnimator.SetBool("giant", false);

        SpawnerRotation.rotationSpeed = 360.0f;
		
		for(int i = 0; i < 150; i++)
		{
			SpawnerRotation.rotationSpeed = Random.Range(360,1440);
			GameObject bonusMeteor = Instantiate( meteorsGiant[ Random.Range(0, meteorsGiant.Length) ], transform.position, Quaternion.identity ) as GameObject;
			bonusMeteor.tag = "Bonus meteor";
			bonusMeteor.GetComponent<MeteorsInfo>().meteorType = "Bonus Giant";
			bonusMeteor.GetComponent<MeteorsMovement>().speedMovement = 2.0f;
			yield return new WaitForSeconds( 0.05f );
		}

        // Ao terminar o bônus, remove da lista porque já foi executado
        bonusList.Remove("spawnBonusGiant");

        yield return new WaitForSeconds(2.5f);

        // Próximo spawn
        nextSpawn();
    }
	
	// Alien spawn
	private IEnumerator spawnAlien()
	{
		// Fiz isso pra nao aparecer alien 2 vezes seguidas
		spawnList.Remove("spawnAlien");
		
		// Extents do alien, pra setar corretamente na tela
		float alienExtents = alienPrefab.GetComponent<Renderer> ().bounds.extents.y;
		
		// Instanciando o alien
		GameObject alien = Instantiate (alienPrefab, 
		                                new Vector3(0.0f, Arrow.maxHeigth - alienExtents, 0.0f),
		                                Quaternion.identity) as GameObject;

        // Som dos aliens
        this.GetComponent<AudioSource>().PlayOneShot(audiotalking);
		
		// Seta o parent do alien
		alien.transform.SetParent ( GameObject.Find("Center of Alien").transform );
		
		// Setando nome do alien
		alien.name = "Alien";
		
		// Comeca com o script desativado ate a animacao dele nascendo acabar
		alien.GetComponent<Alien>().enabled = false;
		
		// Animacao do alien 'nascendo'
		for (float i = 0; i <= 0.6; i += 0.025f) 
		{
			alien.transform.localScale = new Vector3(i, i, 0.0f);
			yield return new WaitForSeconds(0.1f);
		}
		
		// Ativa o alien pra poder atirar
		alien.GetComponent<Alien>().enabled = true;
		
		yield break;
	}

    public AudioClip audiotalking;
	// Alien 4 spawn
	private IEnumerator spawnAlien4()
	{
		// Fiz isso pra nao aparecer alien 2 vezes seguidas
		spawnList.Remove("spawnAlien4");
		
		// Extents do alien, pra setar corretamente na tela
		float alienExtents = alien4Prefab.GetComponent<Renderer> ().bounds.extents.y;
		
		// Instanciando o alien
		GameObject alien0 = Instantiate (alien4Prefab, 
		                                 new Vector3(0.0f, maxHeigth - alienExtents, 0.0f),
		                                 Quaternion.identity) as GameObject;
		GameObject alien1 = Instantiate (alien4Prefab, 
		                                 new Vector3(0.0f, -maxHeigth + alienExtents, 0.0f),
		                                 Quaternion.identity) as GameObject;
		GameObject alien2 = Instantiate (alien4Prefab, 
		                                 new Vector3(maxHeigth - alienExtents, 0.0f, 0.0f),
		                                 Quaternion.identity) as GameObject;
		GameObject alien3 = Instantiate (alien4Prefab, 
		                                 new Vector3(-maxHeigth + alienExtents, 0.0f, 0.0f),
		                                 Quaternion.identity) as GameObject;

        // Som dos aliens
        this.GetComponent<AudioSource>().PlayOneShot(audiotalking);

        // Rotacionando aliens pra posicao correta
        alien1.transform.Rotate (0.0f, 0.0f, 180.0f, Space.World);
		alien2.transform.Rotate (0.0f, 0.0f, 270.0f, Space.World);
		alien3.transform.Rotate (0.0f, 0.0f, 90.0f, Space.World);
		
		//
		alien0.transform.SetParent ( GameObject.Find("Center of Alien").transform );
		alien1.transform.SetParent ( GameObject.Find("Center of Alien").transform );
		alien2.transform.SetParent ( GameObject.Find("Center of Alien").transform );
		alien3.transform.SetParent ( GameObject.Find("Center of Alien").transform );
		
		// Setando nome do alien
		alien0.name = "Alien";
		alien1.name = "Alien 1";
		alien2.name = "Alien 2";
		alien3.name = "Alien 3";
		
		// Comeca com o script desativado ate a animacao dele nascendo acabar
		alien0.GetComponent<Alien4>().enabled = false;
		alien1.GetComponent<Alien4>().enabled = false;
		alien2.GetComponent<Alien4>().enabled = false;
		alien3.GetComponent<Alien4>().enabled = false;
		
		// Animacao do alien 'nascendo'
		for (float i = 0; i <= 0.6; i += 0.025f) 
		{
			alien0.transform.localScale = new Vector3(i, i, 0.0f);
			alien1.transform.localScale = new Vector3(i, i, 0.0f);
			alien2.transform.localScale = new Vector3(i, i, 0.0f);
			alien3.transform.localScale = new Vector3(i, i, 0.0f);
			yield return new WaitForSeconds(0.1f);
		}
		
		// Ativa o alien pra poder atirar
		alien0.GetComponent<Alien4>().enabled = true;
		alien1.GetComponent<Alien4>().enabled = true;
		alien2.GetComponent<Alien4>().enabled = true;
		alien3.GetComponent<Alien4>().enabled = true;
		
		// Decide qual alien vai atirar o meteor verdadeiro!
		int k = Random.Range (0, 4);
		switch ( k )
		{
		case 0:
			alien0.GetComponent<Alien4>().TheChosenOne = true;
			break;
		case 1:
			alien1.GetComponent<Alien4>().TheChosenOne = true;
			break;
		case 2:
			alien2.GetComponent<Alien4>().TheChosenOne = true;
			break;
		case 3:
			alien3.GetComponent<Alien4>().TheChosenOne = true;
			break;
		}
	}
	
	private void spawnSettings(string spawnChosen)
	{
		// ANTES DE TUDO, limpa lista que vai conter os arrays de meteors
		uniaoMeteors.Clear ();

        if (spawnChosen.Equals("spawnType1"))
        {
            SpawnerRotation.rotationSpeed = Random.Range(360, 1440);

            // Spawn meteor normal, color, invisible, giant
            uniaoMeteors.Add(meteorsALL);

            if (LevelMode.levelMode.Equals("Easy"))
                spawnTime = 1.5f;
            else if (LevelMode.levelMode.Equals("Medium"))
                spawnTime = 1.0f;
            else if (LevelMode.levelMode.Equals("Hard"))
                spawnTime = 0.85f;
        }

        else if (spawnChosen.Equals("spawnType2"))
        {
            // Spawn meteor normal, color, invisible, giant
            uniaoMeteors.Add(meteorsALL);

            if (LevelMode.levelMode.Equals("Easy"))
            {
                spawnTime = 1.5f;
                SpawnerRotation.rotationSpeed = 122.0f;
            }
            else if (LevelMode.levelMode.Equals("Medium"))
            {
                spawnTime = 1.4f;
                SpawnerRotation.rotationSpeed = 126.0f;
            }
            else if (LevelMode.levelMode.Equals("Hard"))
            {
                spawnTime = 1.0f;
                SpawnerRotation.rotationSpeed = 180.0f;
            }
        }

        else if (spawnChosen.Equals("spawnType3"))
        {
            SpawnerRotation.rotationSpeed = 360;

            if (LevelMode.levelMode.Equals("Easy"))
            {
                numberOfMeteorsSpawn3 = 5;
                spawnTime = 1.5f;
                // Spawn meteor normal
                uniaoMeteors.Add(meteorsNormal);
            }
            else if (LevelMode.levelMode.Equals("Medium"))
            {
                numberOfMeteorsSpawn3 = 10;
                spawnTime = 1.0f;
                // Spawn meteor normal, color, invisible
                uniaoMeteors.Add(meteorsNormal);
                uniaoMeteors.Add(meteorsColor);
                uniaoMeteors.Add(meteorsInvisible);
            }
            else if (LevelMode.levelMode.Equals("Hard"))
            {
                numberOfMeteorsSpawn3 = 15;
                spawnTime = 0.85f;
                // Spawn meteor normal, color, invisible, giant
                uniaoMeteors.Add(meteorsALL);
            }

            // Ativando o while dentro de SpawnType3
            dontStop = true;
        }

        else if (spawnChosen.Equals("spawnType4"))
        {
            if (LevelMode.levelMode.Equals("Easy"))
            {
                quantidadeSpawn = 3;
                spawnTime = 1.5f;
                // Spawn meteor normal
                uniaoMeteors.Add(meteorsNormal);
            }
            else if (LevelMode.levelMode.Equals("Medium"))
            {
                quantidadeSpawn = 5;
                spawnTime = 1.0f;
                // Spawn meteor normal
                uniaoMeteors.Add(meteorsNormal);
            }
            else if (LevelMode.levelMode.Equals("Hard"))
            {
                quantidadeSpawn = 5;
                spawnTime = 0.88f;
                // Spawn meteor normal, color, invisible
                uniaoMeteors.Add(meteorsNormal);
                uniaoMeteors.Add(meteorsColor);
                uniaoMeteors.Add(meteorsInvisible);
            }
        }

        else if (spawnChosen.Equals("spawnBonus"))
        {
            // Randomiza um spawn
            int newSpawnBonus = Random.Range(0,2);

            // Caso o randomizado seja igual o bonus anterior (repetiu), randomiza um novo até ser diferente
            while (lastSpawnBonus == newSpawnBonus) { newSpawnBonus = Random.Range(0, 2); }

            // Atualiza o last spawn
            lastSpawnBonus = newSpawnBonus;

            switch (newSpawnBonus)
            {
                case 0:
                    SpawnerRotation.rotationSpeed = 360.0f;
                    spawnTime = 0.025f;
                    break;
                case 1:
                    SpawnerRotation.rotationSpeed = 720.0f;
                    spawnTime = 0.025f;
                    break;
            }
        }
		
	}
	
	
	// Se retornar true, pode jogar
	// Se retornar false, nao pode jogar
	private void nextSpawn ()
	{
        // Verificar:

        // - Score (bonus)
        // - Player's lives
        // - Enemy

        //if(bonusList.Count > 0)
        //Debug.Log("Próximo bônus: " + bonusList[0]);

        // Verifica a lista de espera de bônus a serem executados
        if (bonusList.Count > 0)
        {
            // Executa o primeiro bônus da fila
            StartCoroutine( bonusList[0] );
            // Retorna false pra não iniciar nenhum spawn enquanto o bônus roda
        }

        else StartCoroutine(spawnList[Random.Range(0, spawnList.Count)]);
	}

    public IEnumerator stopSpawns()
    { 
        StopAllCoroutines();

        yield break;
    }	
}

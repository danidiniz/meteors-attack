using UnityEngine;
using System.Collections;

public class Alien4 : MonoBehaviour
{


    // Array com sprites do alien
    private Sprite[] alienAnimationSprites;

    // Component sprite desse game object
    SpriteRenderer spriteComponent;

    public static bool isAlienReady;

    GameObject alienMeteor, alienMeteorNeutral;

    // Variavel pra decidir se este Alien eh o ESCOLHIDO rs
    public bool TheChosenOne;

    public AudioClip lazerAudio;

    void Awake()
    {
        TheChosenOne = false;

        // Carregando sprites
        alienAnimationSprites = new Sprite[14];
        for (int i = 0; i < 14; i++)
        {
            alienAnimationSprites[i] = Resources.Load<Sprite>("Alien animation sprites/" + i.ToString());
        }

        // Pegando component sprite pra facilitar
        spriteComponent = this.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Disable Spawner script
        GameObject.Find("Spawner").GetComponent<Spawner>().enabled = false;

        // Starta o tiro
        if (TheChosenOne)
            StartCoroutine(alienChosenShot());
        else
            StartCoroutine(alienNotChosenShot());
    }


    public IEnumerator alienSpawning()
    {
        // Animacao do alien se preparando para atirar
        for (int i = 0; i < alienAnimationSprites.Length; i++)
        {
            spriteComponent.sprite = alienAnimationSprites[i];
            yield return new WaitForSeconds(0.07f);
        }
    }

    // Método pro alien com o meteor verdadeiro atirar
    public IEnumerator alienChosenShot()
    {
        // Starta e aguarda o alien spawnmar
        yield return StartCoroutine(alienSpawning());

        // Meteor sendo instanciado
        alienMeteor = Instantiate(Spawner.meteorsNormal[Random.Range(0, Spawner.meteorsNormal.Length)],
                                       transform.position,
                                       Quaternion.identity
                                       ) as GameObject;
        // Seta como parent, caso eu queria os 4 alien girando
        alienMeteor.transform.SetParent(GameObject.Find("Center of Alien").transform);

        // Movimento lento do meteor saindo da nave
        alienMeteor.GetComponent<MeteorsMovement>().speedMovement = 0.5f;
        // Tempo pra esperar o meteor sair da nave
        yield return new WaitForSeconds(0.7f);

        // Ao terminar de sair da nave, para o movimento do meteor
        alienMeteor.GetComponent<MeteorsMovement>().enabled = false;

        // Alien ready para atirar. (variavel pra parar de spawnmar em Spawner)
        isAlienReady = true;

        // Espera o ultimo meteor lancado ser destruido
        while (Spawner.lastMeteorSpawned != null)
        {
            yield return new WaitForSeconds(0.1f);
        }

        // Inicia contagem para lancar o meteor
        yield return new WaitForSeconds(1.0f);
        Debug.Log("1");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("2");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("3");
        Debug.Log("Lançou!");

        // Audio do tiro
        this.GetComponent<AudioSource>().PlayOneShot(lazerAudio);

        // Ativa o movimento do meteor
        alienMeteor.GetComponent<MeteorsMovement>().enabled = true;
        // Velocidade normal do meteor
        alienMeteor.GetComponent<MeteorsMovement>().speedMovement = 5.0f;

        // Animação do alien desativando
        StartCoroutine(alienDesactivate());
    }

    // Método pro alien com o meteor falso (black meteor) atirar
    public IEnumerator alienNotChosenShot()
    {
        // Starta e aguarda o alien spawnmar
        yield return StartCoroutine(alienSpawning());

        // Meteor sendo instanciado
        alienMeteorNeutral = Instantiate(Spawner.meteorNeutral,
                                              transform.position,
                                              Quaternion.identity
                                              ) as GameObject;
        alienMeteorNeutral.transform.SetParent(GameObject.Find("Center of Alien").transform);
        // Seta como parent, caso eu queria os 4 alien girando
        alienMeteorNeutral.transform.SetParent(GameObject.Find("Center of Alien").transform);

        // Movimento lento do meteor saindo da nave
        alienMeteorNeutral.GetComponent<MeteorsMovement>().speedMovement = 0.5f;
        // Tempo pra esperar o meteor sair da nave
        yield return new WaitForSeconds(0.7f);

        // Ao terminar de sair da nave, para o movimento do meteor
        alienMeteorNeutral.GetComponent<MeteorsMovement>().enabled = false;

        // Alien ready para atirar. (variavel pra parar de spawnmar em Spawner)
        isAlienReady = true;

        // Espera o ultimo meteor lancado ser destruido
        while (Spawner.lastMeteorSpawned != null)
        {
            yield return new WaitForSeconds(0.1f);
        }

        // Inicia contagem para lancar o meteor
        yield return new WaitForSeconds(1.0f);
        Debug.Log("1");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("2");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("3");
        Debug.Log("Lançou!");

        // Ativa o movimento do meteor
        alienMeteorNeutral.GetComponent<MeteorsMovement>().enabled = true;
        // Velocidade normal do meteor
        alienMeteorNeutral.GetComponent<MeteorsMovement>().speedMovement = 5.0f;

        // Animação do alien desativando
        StartCoroutine(alienDesactivate());
    }

    public IEnumerator alienDesactivate()
    {
        // Alien desativado
        isAlienReady = false;

        // False pra poder randomizar de novo
        TheChosenOne = false;

        // Animacao do alien desativando
        for (int i = alienAnimationSprites.Length - 1; i >= 0; i--)
        {
            spriteComponent.sprite = alienAnimationSprites[i];
            yield return new WaitForSeconds(0.07f);
        }

        // Se não for game over, starta novamente o Spawner
        if (!MatchControl.gameOverInitialized)
            GameObject.Find("Spawner").GetComponent<Spawner>().enabled = true;

        // Animacao do alien indo embora
        for (float i = 0.6f; i >= 0; i -= 0.025f)
        {
            this.transform.localScale = new Vector3(i, i, 0.0f);
            yield return new WaitForSeconds(0.1f);
        }

        // Destroi o prefab do alien
        Destroy(this.gameObject);
    }

    private IEnumerator alienMeteorRotation()
    {
        while (alienMeteor != null && alienMeteorNeutral != null)
        {
            if (TheChosenOne)
                alienMeteor.transform.Rotate(0, 0, -1000.0f * Time.deltaTime, Space.Self);
            else
                alienMeteorNeutral.transform.Rotate(0, 0, -1000.0f * Time.deltaTime, Space.Self);
            yield return new WaitForSeconds(0.1f);
        }
    }


}

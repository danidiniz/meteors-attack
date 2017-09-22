using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour
{

    // Array com sprites do alien
    private Sprite[] alienAnimationSprites;

    // Component sprite desse game object
    SpriteRenderer spriteComponent;

    public static bool isAlienReady;

    GameObject alienMeteor;

    public AudioClip lazerAudio;

    void Awake()
    {
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
        StartCoroutine(alienShotAnimation());
    }


    public IEnumerator alienShotAnimation()
    {
        yield return new WaitForSeconds(1.0f);

        // Disable Spawner script
        GameObject.Find("Spawner").GetComponent<Spawner>().enabled = false;

        // Animacao do alien se preparando para atirar
        for (int i = 0; i < alienAnimationSprites.Length; i++)
        {
            spriteComponent.sprite = alienAnimationSprites[i];
            yield return new WaitForSeconds(0.07f);
        }

        // Animacao do meteor saindo da nave
        // Instanciando o meteor
        alienMeteor = Instantiate(Spawner.meteorsNormal[Random.Range(0, Spawner.meteorsNormal.Length)],
                                   transform.position,
                                   Quaternion.identity
                                   ) as GameObject;
        // Seta tag do meteor, pra nao colidir com nenhum outro
        alienMeteor.tag = "Enemy meteor";

        // Ativa rotacao do alienMeteor (soh pra ficar bonitinho rs)
        StartCoroutine(alienMeteorRotation());
        // Setando o meteor do alien como child
        alienMeteor.transform.SetParent(GameObject.Find("Center of Alien").transform);
        // Meteor se movimenta lentamente ate a saida da nave
        alienMeteor.GetComponent<MeteorsMovement>().speedMovement = 0.5f;
        // Tempo pra esperar ele se movimentar
        yield return new WaitForSeconds(0.7f);
        // Para o meteor
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
        // Alien desativado
        isAlienReady = false;

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
        while (alienMeteor != null)
        {
            alienMeteor.transform.Rotate(0, 0, -1000.0f * Time.deltaTime, Space.Self);
            yield return new WaitForSeconds(0.1f);
        }
    }
}

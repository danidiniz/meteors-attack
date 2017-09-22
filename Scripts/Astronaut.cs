using UnityEngine;
using System.Collections;

public class Astronaut : MonoBehaviour 
{
	// Game object do astronaut
	public GameObject astronautGO;

	// Meteor escolhido para destruir
	public static GameObject meteorChosen;

	// Velocidade de rotacao do astronaut
	public float astronautSpeed;

	// Variavel pra decidir se pode rotacionar
	private bool rotate;

	// Testando (marca o meteor pra saber qual foi escolhido)
	public GameObject teste;

	// Prefab do tiro
	public GameObject tiro;

	// Posicao do tiro
	public GameObject shotPosition;

	void Awake()
	{
		// Iniciando meteor escolhido como null
		meteorChosen = null;

		// Inicia rotacionando
		rotate = true;
	}

	void FixedUpdate()
	{
		// Se nenhum meteor foi escolhido, escolhe um
		if (meteorChosen == null) 
		{
			// Ativa a rotacao
			rotate = true;

			// Esclhe o meteor
			meteorChosen = Spawner.lastMeteorSpawned;

			// Testando (marca o meteor pra saber qual foi escolhido)
			GameObject teste2 = Instantiate(teste, meteorChosen.transform.position, Quaternion.identity) as GameObject;
			teste2.transform.SetParent( meteorChosen.transform );
		}

		// Se o meteor ja foi escolhido, rotaciona ate ele
		else if (meteorChosen != null)
		{
			// Se puder rotacionar, rotaciona ate o meteor
			if(rotate)
				this.transform.Rotate(0, 0, astronautSpeed * Time.deltaTime, Space.World);
		}
	}

	// Quando o collider do astronaut atingir o meteor escolhido, para de rotacionar e destroi o meteor
	void OnTriggerEnter2D(Collider2D other)
	{
		// Se o meteor escolhido ainda existir
		if(meteorChosen != null)
		{
			// Se o collider bateu no meteor certo (o escolhido)
			if(other.gameObject.Equals(meteorChosen))
			{
				// Para rotacao
				rotate = false;

				// Animacao de atirar no meteor
				GameObject testeShot = Instantiate(tiro, shotPosition.transform.position, Quaternion.identity) as GameObject;

			}
		}
	}
	
	
	
}

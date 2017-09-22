using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeteorsAnimation : MonoBehaviour
{
	// Array pra carregar sprites das cores
	private Sprite[] meteorSprites;
	
	// Variavel pra salvar onde esta a cor ORIGINAL do meteor
	private int posicao;
	
	// Lista pra guardar somente os sprites de cores diferentes da cor deste meteor
	private List<Sprite> meteorSpritesList = new List<Sprite>();
	
	// Variavel para nao repetir cores
	int lastColor = -1;
	
	// Variavel para parar a coroutine
	public bool colidiu = false;
	
	void Start()
	{
		// MeteorInfo Component
		MeteorsInfo meteorInfo = this.GetComponent<MeteorsInfo>();
		
		// Verificando qual tipo de meteor pra decidir qual animacao rodar
		if (meteorInfo.meteorType.Equals ("Color") || meteorInfo.meteorType.Equals ("Bonus Color")) 
		{
			meteorSprites = Resources.LoadAll<Sprite>("Meteors/Normal");
			
			string meteorColor = this.GetComponent<MeteorsInfo>().meteorColor;
			
			for (int i = 0; i < meteorSprites.Length; i++) 
			{
				if(meteorSprites[i].name != meteorColor)
					meteorSpritesList.Add(meteorSprites[i]);
				else
					posicao = i; // salva a posicao em que a cor original esta
			}

			StartCoroutine (colorAnimaton ());
		}
		
		else if(meteorInfo.meteorType.Equals("Invisible") || meteorInfo.meteorType.Equals("Bonus Invisible"))
			StartCoroutine ( invisibleAnimation() );
	}
	
	private IEnumerator colorAnimaton()
	{
		// Randomiza uma nova cor
		int newColor = Random.Range (0, meteorSpritesList.Count);
		
		// Verifica se repetiu cor
		while(lastColor == newColor)
		{
			newColor = Random.Range (0, meteorSpritesList.Count);
		}
		
		// Muda a cor
		this.GetComponent<SpriteRenderer> ().sprite = meteorSpritesList [ newColor ];
		
		// Atualiza o lastColor
		lastColor = newColor;
		
		// Verifica se colidiu ou se pode continuar mudando de cor
		if (colidiu) 
		{
			this.GetComponent<SpriteRenderer> ().sprite = meteorSprites[ posicao ];
			yield break;
		}
		else
		{
			yield return new WaitForSeconds (.5f);
			StartCoroutine ( colorAnimaton() );
		}
	}
	
	
	// Variavel pra fazer o fade
	private bool fade = true;
	// Variavel pra mudar o fade
	private int contador = 0;
	
	private IEnumerator invisibleAnimation()
	{
		if(fade)
			this.GetComponent<SpriteRenderer>().color = Color.Lerp(this.GetComponent<SpriteRenderer>().color, Color.black, 0.05f);
		else
			this.GetComponent<SpriteRenderer>().color = Color.Lerp(this.GetComponent<SpriteRenderer>().color, Color.white, 0.05f);
		contador++;
		
		if (contador == 70) 
		{
			fade = !fade;
			contador = 0;
		}
		
		yield return new WaitForSeconds (.005f);
		
		StartCoroutine ( invisibleAnimation() );
	}	
}

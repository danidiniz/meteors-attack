using UnityEngine;
using System.Collections;


// Script para executar varios metodos ao mesmo tempo

// No caso do jogo, posso usar isso para, por exemplo, quando o meteor colidir com o player, executar varios metodos ao mesmo tempo:
// - Explosao
// - Adicionar os pontos do player (no script do PlayerInfo)
// - Sons

// Posso fazer tudo isso com cada um em seu script !!!

// Preciso apenas em cada script criar o OnEnable e OnDisable e adicionar os metodos que quero no event que vou criar aqui. :)



public class EventManager : MonoBehaviour 
{

	public delegate void OnDestroyMeteor(GameObject meteor);
	public static event OnDestroyMeteor OnDestroyMeteorEvent;


	// Obs.: sempre que for usar o OnDestroyEvent, verificar antes
	//if(OnDestroyMeteorEvent != null){}

}
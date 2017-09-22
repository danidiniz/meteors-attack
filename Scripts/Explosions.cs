using UnityEngine;
using System.Collections;

public class Explosions : MonoBehaviour 
{
	
	private GameObject points10, points15, points20, points25, points30, points50, points100;
	private GameObject stars;
	private GameObject missExplosion;
	
	void Awake()
	{
		points10 = Resources.Load<GameObject>("Prefabs/Points/10 points explosion");
		points15 = Resources.Load<GameObject>("Prefabs/Points/15 points explosion");
		points20 = Resources.Load<GameObject>("Prefabs/Points/20 points explosion");
		points25 = Resources.Load<GameObject>("Prefabs/Points/25 points explosion");
		points30 = Resources.Load<GameObject>("Prefabs/Points/30 points explosion");
		points50 = Resources.Load<GameObject>("Prefabs/Points/50 points explosion");
		points100 = Resources.Load<GameObject>("Prefabs/Points/100 points explosion");
		
		stars = Resources.Load<GameObject>("Prefabs/Points/Star Explosion");

		missExplosion = Resources.Load<GameObject>("Prefabs/Miss Explosion");
	}

	public void explosionPoints(MeteorsInfo meteorInfoScore, Transform meteorTransform)
	{
		GameObject star, point = null;
		
		star = Instantiate ( stars, meteorTransform.position, Quaternion.identity ) as GameObject;
		
		switch( meteorInfoScore.meteorScore )
		{
		case 10:
			point	= Instantiate ( points10, meteorTransform.position, Quaternion.identity ) as GameObject;
			break;
		case 15:
			point = Instantiate ( points15, meteorTransform.position, Quaternion.identity ) as GameObject;
			break;
		case 20:
			point = Instantiate ( points20, meteorTransform.position, Quaternion.identity ) as GameObject;
			break;
		case 25:
			point = Instantiate ( points25, meteorTransform.position, Quaternion.identity ) as GameObject;
			break;
		case 30:
			point = Instantiate ( points30, meteorTransform.position, Quaternion.identity ) as GameObject;
			break;
		case 50:
			point = Instantiate ( points50, meteorTransform.position, Quaternion.identity ) as GameObject;
			break;
		case 100:
			point = Instantiate ( points100, meteorTransform.position, Quaternion.identity ) as GameObject;
			break;
		}
		
		Destroy ( star, 2.0f );
		if(point != null)
			Destroy ( point, 2.0f );
	}

	public void explosionMiss(Transform meteorTransform)
	{
		GameObject miss = Instantiate ( missExplosion, meteorTransform.position, Quaternion.identity ) as GameObject;

		Destroy ( miss, 2.0f );
	}
	
}
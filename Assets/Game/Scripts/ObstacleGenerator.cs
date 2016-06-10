using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour
{


	public GameObject[]  obstaclesGroup;

	public int index = 0, ObjCount = 0;
	GameObject Obscaleobj, Obscaleobj1;
	public float DistanceBetweenObstacles, FirstDistance;
	public static ObstacleGenerator Static;

	float ObstacleDistance ;
	void Start ()
	{
		Static = this;
		//obstaclesGroup = Resources.LoadAll<GameObject> ("Groups");
    }
	public void  resetObstacles ()
	{

		ObjCount = 0;

	}

	public void CreateNewObstacle ()
	{
		if (PlayerController.isPlayerDead || GameController.Static.isGamePaused)
			return;
		if (ObstacleDistance - PlayerController.thisPosition.z > 300)
			return;

		if (ObjCount == 0) {
				
			ObstacleDistance = PlayerController.thisPosition.z + FirstDistance;
				
		} else {

			ObstacleDistance += DistanceBetweenObstacles;
		}

				
		switch (UnityEngine.Random.Range (1, 5)) {
		case 1:

			Obscaleobj = Instantiate (obstaclesGroup [UnityEngine.Random.Range (0, obstaclesGroup.Length)])as GameObject;
			Obscaleobj.transform.position = new Vector3 (0.0f, 0.0f, ObstacleDistance);
						 
			break;

		case 2:

			Obscaleobj = Instantiate (obstaclesGroup [UnityEngine.Random.Range (0, obstaclesGroup.Length)])as GameObject;
			Obscaleobj.transform.position = new Vector3 (0.0f, 0.0f, ObstacleDistance);
						 
			break;
		
		case 3:
			Obscaleobj1 = Instantiate (obstaclesGroup [UnityEngine.Random.Range (0, obstaclesGroup.Length)])as GameObject;
			Obscaleobj1.transform.position = new Vector3 (0.0f, 0.0f, ObstacleDistance);
			 
			break;
		case 4:
			Obscaleobj1 = Instantiate (obstaclesGroup [UnityEngine.Random.Range (0, obstaclesGroup.Length)])as GameObject;
			Obscaleobj1.transform.position = new Vector3 (0.0f, 0.0f, ObstacleDistance);
			 
			break;
		}

				 
		ObjCount++;
		
	}
	


}

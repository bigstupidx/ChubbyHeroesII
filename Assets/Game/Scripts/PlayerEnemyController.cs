using UnityEngine;
using System.Collections;

public class PlayerEnemyController : MonoBehaviour
{


	public  Transform enemy;
	public Animator enemyAnims;
	public static PlayerEnemyController Static;
	public float CamZPosition = -8;
	public enum PlayerEnemyStates
	{
		Idle,
		chasing,
		attack,
		movingBack,
		none,
		//Screenin, out 
	};

	public PlayerEnemyStates currentEnemyState;

	Vector3 startPosition;

	void Start ()
	{

		Static = this;
		 
		currentEnemyState = PlayerEnemyStates.none;
		startPosition = transform.position;
		enemyTargetposition = new Vector3 (0, 0, -10.0f);
	}
	float enemyZ;
	Vector3 enemyTargetposition;
	float enemySpeed = 0.1f ;
	void Update ()
	{


		switch (currentEnemyState) {

		case PlayerEnemyStates.attack:
			Debug.Log ("play attack animation here");
			enemyTargetposition = new Vector3 (0, 0, -2.0f);
			CancelInvoke ("CamZPositionReset");
			CamZPosition = -10;
			enemyAnims.SetTrigger ("attack");
			currentEnemyState = PlayerEnemyStates.none;
			break;


		case PlayerEnemyStates.chasing:
			CamZPosition = -10;
			enemyTargetposition = new Vector3 (0, 0, -5.0f);
			Invoke ("moveBackToCamera", 5f);
			currentEnemyState = PlayerEnemyStates.none;
			break;
		}

		enemy.localPosition = Vector3.MoveTowards (enemy.localPosition, enemyTargetposition, enemySpeed);
	
	}
	public void ResetToChase ()
	{
		currentEnemyState = PlayerEnemyStates.none;
		enemyAnims.SetTrigger ("Run");
		moveBackToCamera ();
	}
	public void moveBackToCamera ()
	{
		CamZPosition = -8;
		enemyTargetposition = new Vector3 (0, 0, -10.0f);
		 
	}
	public void QuickHideEnemy ()
	{
		CamZPosition = -8;
		enemyTargetposition = enemy.localPosition = new Vector3 (0, 0, -70.0f);
	}

}

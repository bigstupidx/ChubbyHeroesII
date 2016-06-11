using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum Worlds
{
	world1,
	world2
}

public class GameController : MonoBehaviour
{

	// Use this for initialization
	public Worlds currentWorld;

	public static GameController Static ;
	public GameObject[] powerUps, coins, coins_FlyMode, obstacles_Tutorials;
	public GameObject PlayerPosition, shuriken, brokenBarrel, brokenPot, StartingWorldGroup;
	public static event GAMESTATE onGameStateChange ;	
	public static event EventHandler ShowADD;
	public float[] lanePositions ;
	public delegate void GAMESTATE ();
	public float NewWayDistance;
	public bool isStopCreateNewWay = true;
	public bool isGamePaused = false ;
	public int CoinMultipler ;
	Transform playerTransform ;
	float lastPlayerPosition ;
	public Transform mainCameraTrans;
	public List<GameObject> World1 = new List<GameObject> ();
	public List<GameObject> World2 = new List<GameObject> ();
	public List<GameObject> upCoins = new List<GameObject> ();
    public List<GameObject> pooledGrounds = new List<GameObject>();

    void OnEnable ()
	{
		//ON_GAME_Start(); // this methos should me called after the tutorials 
		Static = this;
		CoinMultipler = PlayerPrefs.GetInt ("2XMultiplier", 0);
		mainCameraTrans = Camera.main.transform;
		Physics.IgnoreLayerCollision (0, 2);//to ignore collision between broken objects layer and player
				
		PlayerController.DestroyUpCoins += onDestoryUPCoinsCall;
		//InvokeRepeating ("ChangeWorld", 25, 15);
		Invoke ("DestroyStartingWorld", 30);
	}

    void ChangeWorld()
    {

        if (currentWorld == Worlds.world1)
            currentWorld = Worlds.world2;
        else
            currentWorld = Worlds.world1;
    }

    void OnDisable ()
	{
		CancelInvoke ();
		PlayerController.DestroyUpCoins -= onDestoryUPCoinsCall;
	}
	void DestroyStartingWorld ()
	{

		Destroy (StartingWorldGroup);
	}
	public bool stopObsticalIns = false; //to create or stop the instatiation of new obstacles
	//if player is on ground ,we will create new obstacles,if he is flying or dead ,we will stop creating new ones.


	public void ON_GAME_Start ()
	{
		InvokeRepeating ("GenerateObstacles", 2f, 1.0f);// for obstacles
		InvokeRepeating ("GeneratePowerUps", 20, 50);// for PowerUps
		InvokeRepeating ("GenerateCoins", 2, 10f);// for coins
		isStopCreateNewWay = true;
		stopObsticalIns = false;

		//Ace_IngameUiControl.Static.ShowHighestIndicatorAnim();
	}

	public void ON_GAME_END ()
	{
		CancelInvoke ("DestroyStartingWorld");
		stopObsticalIns = true;
		//to show an ad at the end of the Game
		if (ShowADD != null)
			ShowADD (null, null);
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<curverSetter> ().enabled = false; //add pause state in curve setter!
	}
	
	void Start ()
	{
		playerTransform = PlayerPosition.transform;
		lastPlayerPosition = playerTransform.position.z;
	}

	public	 float flyCointTicks = 51;
	void Update ()
	{
		// to create new way depending on player z value...............
		if (playerTransform.position.z - lastPlayerPosition > 295) {
			CreateNewWay ();
			lastPlayerPosition = playerTransform.position.z;
		}

				
	}

	// to create New Obsticals....................................

	void GenerateObstacles ()
	{
		if (!stopObsticalIns) {
			//Debug.Log("Obstacles Insta");
			ObstacleGenerator.Static.CreateNewObstacle ();
			//Debug.Log("Obstacle generated here");
		}
	}

	//.............................................

	// to create shuriken.............................
	int countShuriken;
	public void GenerateWeapon ()
	{
		if (Time.timeScale != 1)
			return;
		GameObject Obj = Instantiate (shuriken, PlayerController.thisPosition + new Vector3 (0, 0.07f, 2), Quaternion.identity)as GameObject;
		Obj.name = "shuriken" + countShuriken;
		countShuriken++;
	}
		

	//...................................

	int newPowerUp = 1;

	// to create PowerUPs ....................
	public void GeneratePowerUps ()
	{
	
		GameObject Obj = Instantiate (powerUps [UnityEngine.Random.Range (0, powerUps.Length)], new Vector3 (0.0f, 2.0f, PlayerController.thisPosition.z + 100 * newPowerUp), Quaternion.identity)as GameObject;//lanePositions[UnityEngine.Random.Range (0,coins.Length)]
		Obj.name = "powerUp" + newPowerUp;
		newPowerUp++;
	}

	//..........................................

	// To created broken barrenl here........................

	Component[] barrel_ChildObj, pot_ChildObj;
	GameObject barrel;

	public void GenerateBrokenBarrel ()
	{
		barrel = Instantiate (brokenBarrel)as GameObject;
		if (PlayerController.isBarrelBroken) {
			barrel.transform.position = PlayerController.thisPosition + new Vector3 (0, 0, 2.0f);//PlayerController.barrelPosition;
			PlayerController.isBarrelBroken = false;
		} else {
			barrel.transform.position = ShurikenController.brokenBarrrel;
		}
		Destroy (barrel, 1.0f);
	}

	//.............................................................


	// To creat broken pot here..................................

	GameObject pot;

	public void GenerateBrokenPots ()
	{
		pot = Instantiate (brokenPot) as GameObject;
		if (PlayerController.isPotBroken) {
			pot.transform.position = PlayerController.thisPosition + new Vector3 (0, 0, 2.0f);
			// PlayerController.potPosition;
			PlayerController.isPotBroken = false;
		} else {
			//from shuriken poistion here
			pot.transform.position = ShurikenController.brokenPot;
			//}
		}
		Destroy (pot, 1.0f);
	}
	
	//...........................................................

	int coin_Index = 0;
	int newCoins = 1;

	// To create Coins.................................
	
	public void GenerateCoins ()
	{
		if (coin_Index >= coins.Length)
			coin_Index = 0;
		GameObject coin = Instantiate (coins [UnityEngine.Random.Range (0, coins.Length)],
		                            new Vector3 (0, 1.5f, PlayerController.thisPosition.z + 80 * newCoins),
		                            Quaternion.identity)as GameObject;
		coin.name = "Coin " + coin_Index;
		coin_Index++;
		newCoins++;
	}

	//................................................



		 
	
	// To create Coins at fly mode.................................
		
	public void GenerateCoins_FlyMode ()
	{
		//Debug.Log ("Generate Coins At fly Mode");
		for (int i = 1; i < 12 + PlayerPrefs.GetInt ("FlyPower_Ingame", 0); i++) {
			GameObject coinGroupUP = Instantiate (coins_FlyMode [UnityEngine.Random.Range (0, coins_FlyMode.Length - 1)],
		                               new Vector3 (0, 21.5f, PlayerController.thisPosition.z + (80 * i)),
		                               Quaternion.identity)as GameObject;//
			upCoins.Add (coinGroupUP);
		}
		 
	}
			
	void onDestoryUPCoinsCall (System.Object obj, EventArgs args)
	{
			 
		foreach (GameObject Coinobj in upCoins) {

			Destroy (Coinobj);
		}
	}
	//................................................

	// To create new way.............................

	int newWay_Index = 2;
	int selectedGroundIndex;
	GameObject nextObj;
    Vector3 restingPos = new Vector3(0, 0, -1000);
    public bool usePooling;
	public void CreateNewWay ()
	{
		//if no block available add it to the bringit from usedGroundLists

		if (World1.Count != 0) {

						 
			//if (currentWorld == Worlds.world1) {
                if (usePooling) {
				    nextObj = GetNextPoolObject();
				    nextObj.transform.position = new Vector3(0, 0, NewWayDistance * newWay_Index);
                }
                //else {
                //    selectedGroundIndex = UnityEngine.Random.Range(0, World1.Count - 1);   //selecting random block from World1
                //    nextObj = GameObject.Instantiate(World1[selectedGroundIndex]) as GameObject;
                //    nextObj.transform.position = new Vector3(0, 0, NewWayDistance * newWay_Index);
                //}
                //}

                else
                {
                    if (usePooling)
                    {
				        nextObj = GetNextPoolObject();
				        nextObj.transform.position = new Vector3(0, 0, NewWayDistance * newWay_Index);
                    }
                //else {
                //    selectedGroundIndex = UnityEngine.Random.Range(0, World2.Count - 1);   //selecting random block from World1
                //    nextObj = GameObject.Instantiate(World2[selectedGroundIndex]) as GameObject;
                //    nextObj.transform.position = new Vector3(0, 0, NewWayDistance * newWay_Index);
                //}
            }

			// i think this is pointless
			//nextObj.GetComponent<groundDestroyer> ().canBeDestroyed = true;	
            					 
			newWay_Index++; 
		}
	}

    GameObject GetNextPoolObject()
    {     
            GameObject go = pooledGrounds[0];
            pooledGrounds.RemoveAt(0);
            return go;
    }

    public void ReturnPoolObject(GameObject go)
    {
            go.transform.position = restingPos;
            pooledGrounds.Add(go);
    }

    //............................................

    public void ONGameEnd () {
		SoundController.Static.bgSound.enabled = false;
		SoundController.Static.playSoundFromName ("GameOver");
		//avoid all cancel
		CancelInvoke ("GenerateObstacles");// for obstacles
		CancelInvoke ("GeneratePowerUps");// for PowerUps
		CancelInvoke ("GenerateCoins");// for coins
		isStopCreateNewWay = false;
	}
}

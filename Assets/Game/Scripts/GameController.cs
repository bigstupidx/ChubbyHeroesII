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

    public static GameController Static;
    public GameObject[] powerUps, coins, coins_FlyMode, obstacles_Tutorials, enemies;
    public GameObject PlayerPosition, brokenBarrel, brokenPot, StartingWorldGroup;
    public static event GAMESTATE onGameStateChange;
    public static event EventHandler ShowADD;
    public float[] lanePositions;
    public delegate void GAMESTATE();
    public float NewWayDistance;
    public bool isStopCreateNewWay = true;
    public bool isGamePaused = false;
    public int CoinMultipler;
    Transform playerTransform;
    float lastPlayerPosition;
    public Transform mainCameraTrans;
    //public List<GameObject> World1 = new List<GameObject> ();
    //public List<GameObject> World2 = new List<GameObject> ();
    public List<GameObject> upCoins = new List<GameObject>();
    public List<GameObject> pooledGrounds = new List<GameObject>();
    Vector3 playerStartPos = new Vector3(0, 0, 0);


    void OnEnable()
    {
        //ON_GAME_Start(); // this methos should me called after the tutorials 
        Static = this;
        CoinMultipler = PlayerPrefs.GetInt("2XMultiplier", 0);
        mainCameraTrans = Camera.main.transform;
        Physics.IgnoreLayerCollision(0, 2);//to ignore collision between broken objects layer and player

        PlayerController.DestroyUpCoins += onDestoryUPCoinsCall;
        //InvokeRepeating ("ChangeWorld", 25, 15);
        //Invoke ("DestroyStartingWorld", 30);        
    }

    void ChangeWorld()
    {

        if (currentWorld == Worlds.world1)
            currentWorld = Worlds.world2;
        else
            currentWorld = Worlds.world1;
    }

    void OnDisable()
    {
        CancelInvoke();
        PlayerController.DestroyUpCoins -= onDestoryUPCoinsCall;
    }
    //void DestroyStartingWorld ()
    //{

    //	Destroy (StartingWorldGroup);
    //}
    public bool stopObsticalIns = false; //to create or stop the instatiation of new obstacles
                                         //if player is on ground ,we will create new obstacles,if he is flying or dead ,we will stop creating new ones.


    public void ON_GAME_Start()
    {
        InvokeRepeating("GenerateObstacles", 0.1f, 1.0f);// for obstacles
        InvokeRepeating("GeneratePowerUps", 20, 30f);// for PowerUps
        InvokeRepeating("GenerateCoins", 0.1F, 1.5f);// for coins
        InvokeRepeating("GenerateEnemies", 0.1f, 4f); // for enemies
        isStopCreateNewWay = true;
        stopObsticalIns = false;

        //IngameUiControlls.Static.ShowHighestIndicatorAnim();
    }

    //public void ON_GAME_END()
    //{
    //    CancelInvoke("DestroyStartingWorld");
    //    stopObsticalIns = true;
    //    to show an ad at the end of the Game

    //    if (ShowADD != null)
    //        ShowADD(null, null);
    //    GameObject.FindGameObjectWithTag("GameController").GetComponent<curverSetter>().enabled = false; //add pause state in curve setter!
    //}

    void Start()
    {
        playerTransform = PlayerPosition.transform;

        lastPlayerPosition = playerTransform.position.z;
    }

    public float flyCointTicks = 51;
    void Update()
    {
        // to create new way depending on player z value...............
        if (playerTransform.position.z - lastPlayerPosition > 295) {
            CreateNewWay();
            lastPlayerPosition = playerTransform.position.z;
        }


    }

    // to create New Obsticals....................................

    void GenerateObstacles()
    {
        if (!stopObsticalIns) {
            //Debug.Log("Obstacles Insta");
            ObstacleGenerator.Static.CreateNewObstacle();
            //Debug.Log("Obstacle generated here");
        }
    }
    //...................................

    // to create PowerUPs ....................
    int newPowerUp = 1;
    Vector3 powerupOffset = new Vector3(0, 1f, 0);

    public void GeneratePowerUps()
    {

        //get random lane position on X axis
        float laneOffset = lanePositions[UnityEngine.Random.Range(0, lanePositions.Length)];

        //raycast to that pposition down, see what hits
        Vector3 origin = new Vector3(laneOffset, 40.0f, PlayerController.thisPosition.z + 100); // * newPowerup
        RaycastHit hit;
        if (Physics.Raycast(origin, Vector3.down, out hit, 50f))
        {

            if (hit.transform.name == "Col") //change to BUS
            {
                Invoke("GeneratePowerUps", 2f);
                return;
            }

            else if (hit.transform.tag == "Coin")
            {
                Invoke("GeneratePowerUps", 2f);
                return;
            }
            else
            {
                GameObject Obj = Instantiate(powerUps[UnityEngine.Random.Range(0, powerUps.Length)], hit.point + powerupOffset, Quaternion.identity) as GameObject;
            }

        }
        newPowerUp++;
    }
    //..........................................
    // To generate enemies ..........................
    Vector3 enemiesOffset = new Vector3(0, 15f, 0);

    public void GenerateEnemies()
    {
        // stop generating if player dead
        if (PlayerController.isPlayerDead || GameController.Static.isGamePaused)
        return;
        //get random lane position on X axis
        //float laneOffset = lanePositions[UnityEngine.Random.Range(0, lanePositions.Length)];

        //raycast to that pposition down, see what hits
        Vector3 origin = new Vector3(0, 40.0f, PlayerController.thisPosition.z + 150); // * newPowerup
        RaycastHit hit;
        if (Physics.Raycast(origin, Vector3.down, out hit, 50f))
        {
            GameObject Obj = Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Length)], hit.point + enemiesOffset, Quaternion.identity) as GameObject;
        }

    }


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
		                            new Vector3 (0, 1f, PlayerController.thisPosition.z + 100), // * NEWCOINS
		                            Quaternion.identity)as GameObject;
		//coin.name = "Coin " + coin_Index;
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
		//if (World1.Count != 0)
  //      {
            //if (currentWorld == Worlds.world1)
            //{
                //if (usePooling)
                //{
				    nextObj = GetNextPoolObject();
				    nextObj.transform.position = new Vector3(0, 0, NewWayDistance * newWay_Index);
                //}
                //else
                //{
                //    selectedGroundIndex = UnityEngine.Random.Range(0, World1.Count - 1);   //selecting random block from World1
                //    nextObj = Instantiate(World1[selectedGroundIndex]) as GameObject;
                //    nextObj.transform.position = new Vector3(0, 0, NewWayDistance * newWay_Index);
                //}
        //}
        //else
        //{
    //        if (usePooling)
    //        {
				//nextObj = GetNextPoolObject();
				//nextObj.transform.position = new Vector3(0, 0, NewWayDistance * newWay_Index);
    //        }
            //else
            //{
            //    selectedGroundIndex = UnityEngine.Random.Range(0, World2.Count - 1);   //selecting random block from World1
            //    nextObj = Instantiate(World2[selectedGroundIndex]) as GameObject;
            //    nextObj.transform.position = new Vector3(0, 0, NewWayDistance * newWay_Index);
            //}
        //}

            // i think this is pointless
            //nextObj.GetComponent<groundDestroyer>().canBeDestroyed = true;

            newWay_Index++; 
		//}
	}

    GameObject GetNextPoolObject()
    {     
            GameObject go = pooledGrounds[0];
            pooledGrounds.RemoveAt(0);
            go.GetComponent<groundDestroyer>().isInPool = false;
            return go;
    }

    public void ReturnPoolObject(GameObject go)
    {
            go.transform.position = restingPos;
            pooledGrounds.Add(go);
            go.GetComponent<groundDestroyer>().isInPool = true;
    }

    //............................................

    public void ONGameEnd () {
		SoundController.Static.bgSound.enabled = false;
		SoundController.Static.playSoundFromName ("GameOver");
        InGameUIController.isGameEnd = true;
        //InputController.Static.takeInput = false;
        //avoid all cancel
        CancelInvoke("GenerateObstacles");// for obstacles
		CancelInvoke ("GeneratePowerUps");// for PowerUps
		CancelInvoke ("GenerateCoins");// for coins
        CancelInvoke("GenerateEnemies");// for enemies
		isStopCreateNewWay = false;
	}
}

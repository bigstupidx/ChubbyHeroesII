using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;



public class GameController : MonoBehaviour
{
    public enum GameState
    {
        mainMenu,
        gameplay
    }
    public GameState currentGameState;
    PlayerController playerController;
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
    public List<GameObject> upCoins = new List<GameObject>();
    public List<GameObject> pooledGrounds = new List<GameObject>();
    Vector3 playerStartPos = new Vector3(0, 0, 0);



    void Awake()
    {
        Static = this;
        currentGameState = GameState.mainMenu;
    }

    void OnEnable()
    {
        CoinMultipler = PlayerPrefs.GetInt("2XMultiplier", 0);
        mainCameraTrans = Camera.main.transform;
        Physics.IgnoreLayerCollision(0, 2); //to ignore collision between broken objects layer and player

        PlayerController.DestroyUpCoins += onDestoryUPCoinsCall;       
    }

    void OnDisable()
    {
        CancelInvoke();
        PlayerController.DestroyUpCoins -= onDestoryUPCoinsCall;
    }

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        playerTransform = PlayerPosition.transform;

        lastPlayerPosition = playerTransform.position.z;

        if (FindObjectOfType<MenuHelper>().restartFromGameplay == 1)
        {   
            FindObjectOfType<MainMenu>().OnButtonClick("Play");
            GetComponent<curverSetter>().enabled = true;
        }
        else
        {
            GetComponent<curverSetter>().enabled = false;
        }
        
    }

    void Update()
    {
        if (currentGameState == GameState.mainMenu)
        {
            playerController.CurrentState = PlayerStates.Idle;
        }

        else if (currentGameState == GameState.gameplay)
        {
            if (playerTransform.position.z - lastPlayerPosition > 295)
            {
                CreateNewWay();
                lastPlayerPosition = playerTransform.position.z;
            }
        }
    }


    //to create or stop the instatiation of new obstacles
    //if player is on ground ,we will create new obstacles,if he is flying or dead ,we will stop creating new ones.
    public bool stopObsticalIns = false;

    public void OnGameStart()
    {
        GetComponent<curverSetter>().enabled = true;
        playerController.CurrentState = PlayerStates.PlayerAlive;
        InvokeRepeating("GenerateObstacles", 0.1f, 1.0f); // for obstacles
        InvokeRepeating("GeneratePowerUps", 20, 30f); // for PowerUps
        InvokeRepeating("GenerateCoins", 0.1F, 1.5f); // for coins
        InvokeRepeating("GenerateEnemies", 0.1f, 4f); // for enemies
        isStopCreateNewWay = true;
        stopObsticalIns = false;
    }

    public void OnGameEnd()
    {
        SoundController.Static.bgSound.enabled = false;
        SoundController.Static.playSoundFromName("GameOver");
        GameUIController.isGameEnd = true;

        CancelInvoke("GenerateObstacles"); // for obstacles
        CancelInvoke("GeneratePowerUps"); // for PowerUps
        CancelInvoke("GenerateCoins"); // for coins
        CancelInvoke("GenerateEnemies"); // for enemies
        isStopCreateNewWay = false;
    }

    // to create New Obstacles....................................
    void GenerateObstacles()
    {
        if (!stopObsticalIns) {
            ObstacleGenerator.Static.CreateNewObstacle();
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
            if (hit.transform.name == "Col") {//change to BUS
                Invoke("GeneratePowerUps", 2f);
                return;
            }
            else if (hit.transform.tag == "Coin") { }
                Invoke("GeneratePowerUps", 2f);
                return;
            }
            else { 
                GameObject Obj = Instantiate(powerUps[UnityEngine.Random.Range(0, powerUps.Length)], hit.point + powerupOffset, Quaternion.identity) as GameObject;
            }
        newPowerUp++;
    }
    //..........................................


    // To generate enemies ..........................
    Vector3 enemiesOffset = new Vector3(0, 12f, 0);
    int enemyIndex = 1;
    public void GenerateEnemies()
    {
        // stop generating if player dead
        if (PlayerController.isPlayerDead || isGamePaused)
            return;

        //raycast to that pposition down, see what hits
        Vector3 origin = new Vector3(0, 40.0f, PlayerController.thisPosition.z + 150); // * newPowerup
        RaycastHit hit;
        if (Physics.Raycast(origin, Vector3.down, out hit, 50f))
        {
            GameObject Obj = Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Length)], hit.point + enemiesOffset, Quaternion.identity) as GameObject;
            Obj.name = "Enemy " + enemyIndex++;
        }

    }
    // ........................................


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


    // To create Coins.................................
    int coin_Index = 0;
	int newCoins = 1;

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
		foreach (GameObject Coinobj in upCoins)
        {
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
        nextObj = GetNextPoolObject();
        nextObj.transform.position = new Vector3(0, 0, NewWayDistance * newWay_Index);
        newWay_Index++; 
	}

    GameObject GetNextPoolObject()
    {
        int indexul = UnityEngine.Random.Range(0, pooledGrounds.Count);
        GameObject go = pooledGrounds[indexul];
        pooledGrounds.RemoveAt(indexul);
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
}

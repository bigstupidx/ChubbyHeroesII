using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

// player States.......... 
public enum PlayerStates
{
	
	PlayerRunning,
	PlayerDead,
	fly,
	powerJump,
	empty,
	Tutorial,
	Idle,
};


//..
public class PlayerController : MonoBehaviour
{
    public PlayerStates CurrentState;

    public enum TurnColliders
    {
        SOUTH = 1,
        EAST,
        NORTH,
        WEST
    }

    public TurnColliders _currentTurnCollider;

    public enum IntersectionType
    {
        x, t, y
    }

    public IntersectionType intersectionType;

    public static event EventHandler
        switchOnMagnetPower,
        switchOFFMagnetPower,
        gameEnded,
        DestroyUpCoins;

    CharacterController controller;

    public Animator playerAnimator;
    public GameObject[] playerPrefabs;
    public float 
        speed = 10f, 
        TopSpeed = 10f,
        jumpSpeed = 16.0F,
        gravity = 20.0F,
        increaseSpeedTime,
        StandingHeight,
        DownHeight;

    private Vector3 moveDirection = Vector3.zero;

    public Vector3 
        StandingPosition, 
        DownPosistion;

    public static Vector3 thisPosition;

    int Coin = 0;

    public GameObject 
        powerObj_JetPack, 
        powerObj_Magnet, 
        shoe1, 
        shoe2;// this can be used in when player picup any powers

    public ParticleEmitter coinParticle;

    public static int PlayerHealth
    {
        get { return playerHealt; }
    }

    static int playerHealt;// hitCount changes accoding to player index value THIS IS A STAT SPECIFFIC TO EACH PLAYER
    int runState = Animator.StringToHash("Base Layer.Run");
    int downStateValue2 = Animator.StringToHash("Base Layer.Roll");
    int downStateValue1 = Animator.StringToHash("Base Layer.Slide");
    int downStateValue3 = Animator.StringToHash("Base Layer.Down");
    int JumpStateValue = Animator.StringToHash("Base Layer.jump");
    int hitSide1 = Animator.StringToHash("Base Layer.LookBackSideleft");
    int hitSide2 = Animator.StringToHash("Base Layer.LookBackSideRight");//Double_Jump
    int Double_Jump = Animator.StringToHash("Base Layer.Double_Jump");
    public int leftTurn = Animator.StringToHash("Base Layer.Left_Turn");
    public int rightTurn = Animator.StringToHash("Base Layer.Right_Turn");
    Transform 
        thisTranfrom,
        turnTarget;
    public PlayerObstacleCheck ObstacleCheck;

    //public bool canTurn;
    public bool isTurning;
    //public bool canRegisterTurn;
    //public bool canChangeLane = true;

    private float 
        presentSpeed,
        originalSpeed;

    public float tilt;
    float lastTimeColliderChange;
    float PlayerHorizontalMovement;
    public float HorizontalLerpTarget;
    public float horizontalLerpSpeed;
    public float flyHeight, flySpeed;

    GameObject[] destroy_Obsticals_Respwan;

    public static bool doubleJump = false, normalJump = false;
    public bool isMagnetIndicator = false, isMultiplierIndicator = false, isFlyModeIndicator = false, isJumpModeIndicator = false;
    public static Vector3 barrelPosition, potPosition;
    public static bool isBarrelBroken = false, isPotBroken = false;
    int barrelPotTouche_Count = 0;
    int score = 0;
    int playeHurtCount = 0;
    public float magnetPowerTime, multiplierPowerTime, lastTriggerJumpTime;
    CollisionChecker collisionCheckerScript;
    Rigidbody rb;
    public int hitTurnsInThisIntersection = 0;

    [SerializeField]
    GameObject p;

    void Awake()
    {
 

    }

    void Start()
    {
        InstantiateSelectedPlayer();
        collisionCheckerScript = GetComponentInChildren<CollisionChecker>();
        rb = GetComponent<Rigidbody>();
        //controller = GetComponent<CharacterController>();
        moveDir = transform.forward;
    }

    public void InstantiateSelectedPlayer ()
    {
        collisionCheckerScript = GetComponent<CollisionChecker>();

        if (p != null)
            Destroy(p);

        if (PlayerPrefs.GetInt("SelectedPlayer", 0) == 0)
        { // for Player 1
            // instantiate Correct player prefab
            p = Instantiate(playerPrefabs[0], transform.position, Quaternion.identity) as GameObject;
            p.transform.SetParent(transform);
            p.transform.position = transform.position;
            // set it's stats
            playerHealt = 1;
        }
        else if (PlayerPrefs.GetInt("SelectedPlayer", 0) == 1)
        {//  for Player 2
            // instantiate Correct player prefab
            p = Instantiate(playerPrefabs[1], transform.position, Quaternion.identity) as GameObject;
            p.transform.SetParent(transform);
            p.transform.position = transform.position;
            // set it's stats
            playerHealt = 2;
        }
        else if (PlayerPrefs.GetInt("SelectedPlayer", 0) == 2)
        { // for Player 3
            // instantiate Correct player prefab
            p = Instantiate(playerPrefabs[2], transform.position, Quaternion.identity) as GameObject;
            p.transform.SetParent(transform);
            p.transform.position = transform.position;
            // set it's stats
            playerHealt = 3;
        }
        else if (PlayerPrefs.GetInt("SelectedPlayer", 0) == 3)
        { // for Player 4
            // instantiate Correct player prefab
            p = Instantiate(playerPrefabs[3], transform.position, Quaternion.identity) as GameObject;
            p.transform.SetParent(transform);
            p.transform.position = transform.position;
            // set it's stats
            playerHealt = 4;
        }
        else if (PlayerPrefs.GetInt("SelectedPlayer", 0) == 4)
        { // for Player 4
            // instantiate Correct player prefab
            p = Instantiate(playerPrefabs[3], transform.position, Quaternion.identity) as GameObject;
            p.transform.SetParent(transform);
            p.transform.position = transform.position;
            // set it's stats
            playerHealt = 5;
        }

        //Debug.Log("Instantiated player with " + playerHealt + " health!");
        CurrentState = PlayerStates.empty;
        //.......................................
        playerAnimator = p.GetComponent<Animator>();
        //powerObj_JetPack.SetActive(false); dezactivate pana testez platforma
        //powerObj_Magnet.SetActive(false);
        isPlayerDead = false;
        //shoe1.SetActive(false);
        //shoe2.SetActive(false);
        controller = GetComponent<CharacterController>();
        thisTranfrom = transform;
        originalSpeed = speed;

        // playerHealt = 3; // lifes? per player?

        CurrentState = PlayerStates.Idle;
    }

    Vector3 moveDir;
    bool handledLaneChange;
    bool Grounded;
    void FixedUpdate()
    {

        thisPosition = thisTranfrom.position;//  this variable copy the present Player Position

        if (GameController.Static.isGamePaused)
            return;

        

        switch (CurrentState)
        {
            // player in normal mode....................................................
            case PlayerStates.PlayerRunning:
                InputController.Static.takeInput = true;
                isPlayerDead = false;
                speed = Mathf.Lerp(speed, TopSpeed, increaseSpeedTime);
                presentSpeed = speed;
                isPlayerOnGround();

                RotatePlayer();
                PlayerLaneChanging();

                if (controller.isGrounded)
                {
                    GameController.Static.stopCreatingObstacles = false;
                    //Debug.Log("GROUNDED");
                    if (doubleJump || (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == runState && InputController.Static.isJump))
                    {
                        if (normalJump)
                        { // shoe
                            playerAnimator.SetTrigger("JumpHigh");
                            //jumpSpeed = Mathf.Clamp(speed, 10, 18);
                            jumpSpeed = 5f;
                            InputController.Static.isJump = false;
                            doubleJump = false;
                        }
                        else if (doubleJump)
                        {	// booster
                            doubleJump = false;
                            //jumpSpeed = Mathf.Clamp(speed * 1.5f, 20, 22);
                            jumpSpeed = 7f;
                            InputController.Static.isJump = false;
                            if (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash != Double_Jump)
                                playerAnimator.SetTrigger("JumpHigh");
                        }
                        else
                        {  
                            playerAnimator.SetTrigger("Jump");
                            PlayJumpSound();
                            PlayerPrefs.SetInt("MissionJumpCount", PlayerPrefs.GetInt("MissionJumpCount", 0) - 1);
                            jumpSpeed = 3f;
                            InputController.Static.isJump = false;
                        }
                        moveDir.y = jumpSpeed;

                    }

                }

                if(moveDir.y > -2f)
                {
                    moveDir.y -= (gravity * Time.deltaTime);
                }

                //Debug.Log("Movedir Y is " + moveDir.y);
                controller.Move(moveDir * speed * Time.deltaTime);
                //transform.Translate(moveDir * speed * Time.deltaTime);


                if (!isTurning)
                {
                    switch (CardinalDir)
                    {
                        case cardinalDir.north:
                            transform.position = Vector3.Lerp(transform.position, new Vector3(nextStreetTarget.position.x + targetLanePosition, transform.position.y, transform.position.z), 0.3f);
                            break;
                        case cardinalDir.east:
                            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y,  nextStreetTarget.position.z - targetLanePosition), 0.3f);

                            break;
                        case cardinalDir.south:
                            transform.position = Vector3.Lerp(transform.position, new Vector3( nextStreetTarget.position.x - targetLanePosition, transform.position.y, transform.position.z), 0.3f);

                            break;
                        case cardinalDir.west:
                            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, nextStreetTarget.position.z + targetLanePosition), 0.3f);

                            break;
                        default:
                            break;
                    }
                }




                //if (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == downStateValue1 || playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == downStateValue2)
                //{
                //    controller.center = DownPosistion;
                //    controller.height = DownHeight;
                //}
                //else
                //{
                //    if (Time.timeSinceLevelLoad - lastTimeColliderChange > 0.2f)
                //    {
                //        controller.center = StandingPosition;
                //        controller.height = StandingHeight;
                //        lastTimeColliderChange = Time.timeSinceLevelLoad;
                //    }
                //}
                break;

            case PlayerStates.fly: // under construction after I get the stupid thing running normally
                //PlayerCamera.Static.currentCam = PlayerCamera.Cam.flyModeCam;
                RotatePlayer();
                PlayerLaneChanging();
                speed = 25;
                flyHeight = Mathf.Lerp(flyHeight, 15, flySpeed);
                if (flyHeight > 5)
                {
                    playerAnimator.SetTrigger("JetPackFly");
                }


                controller.Move(moveDir * speed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, flyHeight, transform.position.z);


                if (!isTurning)
                {
                    switch (CardinalDir)
                    {
                        case cardinalDir.north:
                            transform.position = Vector3.Lerp(transform.position, new Vector3(nextStreetTarget.position.x + targetLanePosition, transform.position.y, transform.position.z), 0.3f);
                            break;
                        case cardinalDir.east:
                            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y , nextStreetTarget.position.z - targetLanePosition), 0.3f);

                            break;
                        case cardinalDir.south:
                            transform.position = Vector3.Lerp(transform.position, new Vector3(nextStreetTarget.position.x - targetLanePosition, transform.position.y , transform.position.z), 0.3f);

                            break;
                        case cardinalDir.west:
                            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, nextStreetTarget.position.z + targetLanePosition), 0.3f);

                            break;
                        default:
                            break;
                    }
                }
                break;

            case PlayerStates.PlayerDead:
                CurrentState = PlayerStates.empty;
                InputController.Static.takeInput = false;
                isPlayerDead = true;
                break;

            case PlayerStates.Idle:
                playerAnimator.SetTrigger("Slide");
                //play idle anim
                break;
        }
    }


    public void Slide_Roll()
    {
        playerAnimator.SetTrigger("Roll");
        SoundController.Static.playSoundFromName("roll");
    }

    public void PowerJumpReset()
    {
        normalJump = false;
        GameUIController.Static.playerInJumpIndicator.SetActive(false);
        GameUIController.Static.progressBarScript.jumpModeProgressbar.fillAmount = 1;
        isJumpModeIndicator = false;
        shoe1.SetActive(false);
        shoe2.SetActive(false);
    }

    public void JetPackPowerReset()
    {
        //PlayerCamera.Static.currentCam = PlayerCamera.Cam.NormalCam;
        GameUIController.Static.playerInFlyIndicator.SetActive(false);
        GameUIController.Static.progressBarScript.flyModeProgressBar.fillAmount = 1;// to reset fill amount here
        isFlyModeIndicator = false;
        playerAnimator.SetTrigger("JetPackLand");
        powerObj_JetPack.SetActive(false);
        SoundController.Static.jetPackSound.volume = 0f;
        CurrentState = PlayerStates.PlayerRunning;
        speed = presentSpeed;
        ObstacleGenerator.Static.resetObstacles();
        if (DestroyUpCoins != null)
            DestroyUpCoins(null, null);
    }


    #region player lanePosition

    public float[] LanesPositions;
    public float
        laneShiftSpeed,
        targetLanePosition, 
        lanePosition;

    public enum PlayerLane
    {
        one,
        two,
        three,
        four
    };

    public PlayerLane currentLane;

    Vector3 laneVector;
    // Player lane changing here this method is called at fixed update Player state alive 
    void PlayerLaneChanging() // called from Fixed Update if case PlayerStates.PlayerAlive
    {
        switch (currentLane)
        {
            case PlayerLane.one:
                targetLanePosition = LanesPositions[0]; // -4
                break;
            case PlayerLane.two:
                targetLanePosition = LanesPositions[1]; // 0
                break;
            case PlayerLane.three:
                targetLanePosition = LanesPositions[2]; // 4
                break;
            case PlayerLane.four:
                targetLanePosition = LanesPositions[3]; // 8
                break;
        }
        lanePosition = Mathf.Lerp(lanePosition, targetLanePosition, 1f);
        laneVector = new Vector3(lanePosition, 0, 0);
    }

    #endregion

    // when play Again button clicked this Method is called
    public void RespwanPlayer()
    {
        destroy_Obsticals_Respwan = GameObject.FindGameObjectsWithTag("Destroy");
        for (int i = 0; i < destroy_Obsticals_Respwan.Length; i++)
        {
            Destroy(destroy_Obsticals_Respwan[i]);
        }
        PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins", 0) - Mathf.RoundToInt(GameUIController.Static.continueCoins));
        isPlayerDead = false;
        Invoke("latePlayerAliveOnRespwan", 1.0f);
    }

    void latePlayerAliveOnRespwan()
    {
        speed = 10;
        SoundController.Static.bgSound.enabled = true;
        ResetPlayerHurtCount();
        GameUIController.Static.UpdateHearts(playeHurtCount);
        //PlayerEnemyController.Static.ResetToChase ();
        playerAnimator.SetTrigger("Run");
        CurrentState = PlayerStates.PlayerRunning;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<curverSetter>().enabled = true;
        GameController.Static.OnGameStart();
        GameUIController.isGameEnd = false;
    }

    #region player Trigger Enter with

    void OnTriggerEnter(Collider incoming)
    {
        string incomingTag = incoming.tag;
        GameObject incomingObj = incoming.gameObject;

        // player Trigger with bullet...............
        if (incomingTag.Contains("Projectile"))
        {
            Destroy(incoming.gameObject);

            if (!GameController.Static.isGamePaused || !isPlayerDead)
            {
                playeHurtCount++;
                Debug.Log("playeHurtCount" + playeHurtCount);
                GameUIController.Static.UpdateHearts(playeHurtCount);
                if (playeHurtCount == playerHealt)
                {
                    KillPlayer();

                }
            }
        }

        if (incomingTag.Contains("Coin"))
        {

            coinControl coinScript = incomingObj.GetComponent<coinControl>();
            coinScript.MoveToPlayer();
            if (isFlyModeIndicator)
                coinScript.moveToCoinTarget = true;
            else
                coinScript.moveToPlayer = true;
            SoundController.Static.playCoinSound();
            PlayerPrefs.SetInt("MissionCoinsCount", PlayerPrefs.GetInt("MissionCoinsCount") - 1);
            GameUIController.Static.inGameCoinCount = Coin;
            coinParticle.emit = true;
            Coin++;

        }

        else if (incomingTag.Contains("Magnet"))
        {
            GameUIController.Static.ShowPowerIndicatorAnim();
            GameUIController.Static.powerUpIndicatorText.text = "Magnet Power";
            powerObj_Magnet.SetActive(true);
            SoundController.Static.playSoundFromName("PickUp");
            PlayerPrefs.SetInt("MissionMagnetPowerCount", PlayerPrefs.GetInt("MissionMagnetPowerCount") - 1);
            GameUIController.Static.magnetIndicator.SetActive(true);
            isMagnetIndicator = true;
            if (switchOnMagnetPower != null)
                switchOnMagnetPower(null, null);
            coinControl.isONMagetPower = true;
            incomingTag = "Name Changed";
            Destroy(incomingObj);
            //Invoke ("switchOffMagnet", 10);
        }

        else if (incomingTag.Contains("Multiplier"))
        {
            GameUIController.Static.ShowPowerIndicatorAnim();
            GameUIController.Static.powerUpIndicatorText.text = " Score Multiplier";
            SoundController.Static.playSoundFromName("PickUp");
            PlayerPrefs.SetInt("Mission2XPowerCount", PlayerPrefs.GetInt("Mission2XPowerCount") - 1);

            GameUIController.Static.multiplierIndicator.SetActive(true);
            GameUIController.Static.multiplierValue *= 2;
            isMultiplierIndicator = true;
            Destroy(incomingObj);
            //Invoke ("switchOffMultiplier", 10);
        }

        else if (incomingTag.Contains("JumpMode"))
        {
            GameUIController.Static.ShowPowerIndicatorAnim();
            GameUIController.Static.powerUpIndicatorText.text = "Jump Shoe";
            SoundController.Static.playSoundFromName("PickUp");
            normalJump = true;
            PlayerPrefs.SetInt("MissionJumpPowerCount", PlayerPrefs.GetInt("MissionJumpPowerCount") - 1);
            GameUIController.Static.playerInJumpIndicator.SetActive(true);
            isJumpModeIndicator = true;
            //shoe1.SetActive(true); tre sa le fac reenable si sa le pun direct pe player....astea se vad dupa ce ia powerupu
            //shoe2.SetActive(true);

            //PlayerEnemyController.Static.QuickHideEnemy ();
            //Invoke ("PowerJumpReset", 10);
            Destroy(incomingObj);

        }

        else if (incomingTag.Contains("FlyMode"))
        {
            GameUIController.Static.ShowPowerIndicatorAnim();
            GameUIController.Static.powerUpIndicatorText.text = " JetPack Power";
            CurrentState = PlayerStates.fly;
            SoundController.Static.playSoundFromName("PickUp");
            SoundController.Static.jetPackSound.volume = 0.5f;
            powerObj_JetPack.SetActive(true);
            PlayerPrefs.SetInt("MissionFlyPowerCount", PlayerPrefs.GetInt("MissionFlyPowerCount") - 1);
            GameObject[] Obj = GameObject.FindGameObjectsWithTag("Destroy");
            //for (int i = 0; i <= Obj.Length - 1; i++)
            //{
            //    Destroy(Obj[i], 1.0f);
            //}
            GameUIController.Static.playerInFlyIndicator.SetActive(true);
            isFlyModeIndicator = true;
            playerAnimator.SetTrigger("JetPackJump");
            GameController.Static.GenerateCoins_FlyMode();
            GameController.Static.stopCreatingObstacles = true;
            ObstacleGenerator.Static.index = 0;
            //PlayerEnemyController.Static.QuickHideEnemy ();
            Destroy(incomingObj);
        }

        else if (incomingTag.Contains("Barrel"))
        {
            PlayerPrefs.SetInt("MissionDestroyBarrelCount", PlayerPrefs.GetInt("MissionDestroyBarrelCount", 0) - 1);
            //PlayerEnemyController.Static.currentEnemyState = PlayerEnemyController.PlayerEnemyStates.chasing;
            isBarrelBroken = true;
            barrelPosition = incomingObj.transform.position;
            //GameController.Static.GenerateBrokenBarrel();
            int randomNum = UnityEngine.Random.Range(-1, 2);
            if (randomNum < 0)
            {
                playerAnimator.SetTrigger("HitRight");
            }
            else
            {
                playerAnimator.SetTrigger("HitLeft");
            }
            SoundController.Static.playSoundFromName("Pot");
            print("Barrel Count  " + barrelPotTouche_Count);
            barrelPotTouche_Count++;
            if (barrelPotTouche_Count == playerHealt)
            {
                //PlayerEnemyController.Static.currentEnemyState = PlayerEnemyController.PlayerEnemyStates.attack;
                CurrentState = PlayerStates.PlayerDead;
                playerAnimator.SetTrigger("CrashBack");
                GameController.Static.OnGameEnd();
                GameUIController.Static.ContinueScreen();
            }
            Invoke("ResetBarrelPotCount", 5.0f);
            Destroy(incomingObj);
        }

        else if (incomingTag.Contains("Pots"))
        {
            isPotBroken = true;
            PlayerPrefs.SetInt("MissionDestroyPotsCount", PlayerPrefs.GetInt("MissionDestroyPotsCount", 0) - 1);
            //PlayerEnemyController.Static.currentEnemyState = PlayerEnemyController.PlayerEnemyStates.chasing;// Player enemy state changes here
            potPosition = incomingObj.transform.position;
            //GameController.Static.GenerateBrokenPots();// to generate broken pot here
            int randomNum = UnityEngine.Random.Range(-1, 2);
            if (randomNum < 0)
            {
                playerAnimator.SetTrigger("HitRight");
            }
            else
            {
                playerAnimator.SetTrigger("HitLeft");
            }
            SoundController.Static.playSoundFromName("Pot");
            print("Pot Count  " + barrelPotTouche_Count);
            barrelPotTouche_Count++;
            if (barrelPotTouche_Count == playerHealt)
            {
                //PlayerEnemyController.Static.currentEnemyState = PlayerEnemyController.PlayerEnemyStates.attack;
                CurrentState = PlayerStates.PlayerDead;
                playerAnimator.SetTrigger("CrashBack");
                GameController.Static.OnGameEnd();
                GameUIController.Static.ContinueScreen();
            }
            Invoke("ResetBarrelPotCount", 5.0f);// to reset the pots touche count and player enemy state
            Destroy(incomingObj);
        }
    }
    #endregion

    void ResetBarrelPotCount()
    {
        //PlayerEnemyController.Static.currentEnemyState = PlayerEnemyController.PlayerEnemyStates.Idle;
        barrelPotTouche_Count = 0;
    }

    #region Player Collision with
    public static bool isPlayerDead;
    public BoxCollider boxCollider;

    void OnControllerColliderHit(ControllerColliderHit incoming)
    {
        if (incoming.collider.tag == null || incoming.collider.tag.Contains("Undefined"))
            return;
        string incomingTag = incoming.collider.tag;

        //Playser Collision with Obstacle...................
        if (incoming.collider.tag != null && incoming.collider.tag.Contains("DoubleJump"))
        {

            if (Time.timeSinceLevelLoad - lastTriggerJumpTime > 0.1f)
            {
                Debug.Log("Double jumped fromPlayerController");
                incoming.collider.enabled = false;
                lastTriggerJumpTime = Time.timeSinceLevelLoad;
                doubleJump = true;
                InputController.Static.isJump = true;
                SoundController.Static.playSoundFromName("jumpTrigger");
            }
        }
        else if (incomingTag.Contains("Obstacle") && (CurrentState == PlayerStates.PlayerRunning || CurrentState == PlayerStates.powerJump || CurrentState == PlayerStates.fly))
        {

            //this is to to avoid collision caliculation when he is double jump mode
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == Double_Jump)
                return;

            // aici e o problema de coliziune : playerul moare cand face slide jos de pe platforma
            if (thisTranfrom.position.y < 3)
            {
                thisTranfrom.position = new Vector3(thisTranfrom.position.x, 1.28f, thisTranfrom.position.z);
                playerAnimator.SetTrigger("CrashBack");
            }
            else
            {
                if (thisTranfrom.position.y >= 6)
                {
                    thisTranfrom.position = new Vector3(thisTranfrom.position.x, 4.4f, thisTranfrom.position.z);
                    playerAnimator.SetTrigger("CrashBack");
                }
                else
                {
                    thisTranfrom.position = new Vector3(thisTranfrom.position.x, thisTranfrom.position.y, thisTranfrom.position.z);
                    playerAnimator.SetTrigger("CrashBack");
                }
            }
            //Destroy(incoming.gameObject);
            KillPlayer();
            //PlayerEnemyController.Static.currentEnemyState = PlayerEnemyController.PlayerEnemyStates.attack;
        }
        //............................................................
    }
    #endregion

    public void switchOffMultiplier()
    {
        GameUIController.Static.progressBarScript.multiplierProgressBar.fillAmount = 1;
        isMultiplierIndicator = false;

        GameUIController.Static.multiplierIndicator.SetActive(false);
        GameUIController.Static.multiplierValue = PlayerPrefs.GetInt("MultiplierCount_Ingame", 1);
    }

    public void switchOffMagnet()
    {
        GameUIController.Static.progressBarScript.magnetProgressBar.fillAmount = 1;
        coinControl.isONMagetPower = false;
        isMagnetIndicator = false;
        powerObj_Magnet.SetActive(false);
        //ProgressBarMagnet.Static.magnetProgressBar.fillAmount = 1;
        GameUIController.Static.magnetIndicator.SetActive(false);
        if (switchOFFMagnetPower != null)
            switchOFFMagnetPower(null, null);
    }

    void PlayJumpSound()
    {
        SoundController.Static.playSoundFromName("Jump");
    }

    #region RayDraw to Play Down Anim

    RaycastHit hit;
    float lastTime;
    public string hitObjTagName;
    public float hitDistance;
    bool rayBool = true;
    float lastDownAnimPlayTime;

    void isPlayerOnGround()
    {
        Vector3 Down = thisTranfrom.TransformDirection(-Vector3.up);

        if (Physics.Raycast(transform.position, Down, out hit, 5.0f))
        {
            if (hit.distance > 2.8f && (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == runState) && Time.timeSinceLevelLoad - lastDownAnimPlayTime > 1.0f
                && (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash != Double_Jump))
            {
                playerAnimator.SetTrigger("Down");
                lastDownAnimPlayTime = Time.timeSinceLevelLoad;
            }
            else if (hit.distance < 1.0f && (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == downStateValue3))
            {
                playerAnimator.SetTrigger("Run");
            }
        }
    }
    #endregion

    #region Lanchange Anim

    public void LeftSideMoving()
    {
        //thisTranfrom.eulerAngles = new Vector3 (0, 0, 0);
        if (ObstacleCheck.CheckLeftSide())
        {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == runState)
            {
                playerAnimator.SetTrigger("LeftSideHit");
            }
            speed = originalSpeed;
            playeHurtCount++;
            GameUIController.Static.UpdateHearts(playeHurtCount);
            if (playeHurtCount == playerHealt)
            {
                KillPlayer();
            }
        }
        else if (CurrentState != PlayerStates.PlayerDead && CurrentState != PlayerStates.Idle && CurrentState != PlayerStates.empty)
        {
            handledLaneChange = false;

            if (isTurning)
                return;
            
            switch (currentLane)
            {
                case PlayerLane.one:
                    break;

                case PlayerLane.two:
                    currentLane = PlayerLane.one;
                    ChangeLaneActions(false);
                    break;

                case PlayerLane.three:
                    currentLane = PlayerLane.two;
                    ChangeLaneActions(false);
                    break;

                case PlayerLane.four:
                    currentLane = PlayerLane.three;
                    ChangeLaneActions(false);
                    break;
            }
        }
    }

    //Player Lane changes here to check the right side is any obstacle player hurt count increased
    public void RightSideMoving()
    {

        //thisTranfrom.eulerAngles = new Vector3 (0, 0, 0);
        if (ObstacleCheck.CheckRightSide())
        {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == runState)
            {
                playerAnimator.SetTrigger("RightSideHit");
            }
            speed = originalSpeed;
            playeHurtCount++;
            GameUIController.Static.UpdateHearts(playeHurtCount);
            if (playeHurtCount == playerHealt)
            {
                KillPlayer();
            }

        }
        else if (CurrentState != PlayerStates.PlayerDead && CurrentState != PlayerStates.Idle && CurrentState != PlayerStates.empty)
        {

            handledLaneChange = false;

            if (isTurning)
                return;

            switch (currentLane)
            {
                case PlayerLane.one:
                    currentLane = PlayerLane.two;
                    ChangeLaneActions(true);
                    break;

                case PlayerLane.two:
                    currentLane = PlayerLane.three;
                    ChangeLaneActions(true);
                    break;
                case PlayerLane.three:
                    currentLane = PlayerLane.four;
                    ChangeLaneActions(true);
                    break;

                case PlayerLane.four:
                    break;
            }
        }
    }

    void ChangeLaneActions(bool b)
    {
        if (b)
        {
            PlayerPrefs.SetInt("MissionswipeRightCount", PlayerPrefs.GetInt("MissionswipeRightCount") - 1);
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == runState)
            {
                playerAnimator.SetTrigger("RightTurn");
            }
            SoundController.Static.playSoundFromName("swipe");
        }
        else
        {
            PlayerPrefs.SetInt("MissionswipeLeftCount", PlayerPrefs.GetInt("MissionswipeLeftCount", 0) - 1);
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == runState)
            {
                playerAnimator.SetTrigger("LeftTurn");
            }
            SoundController.Static.playSoundFromName("swipe");
        }
    }

    public Transform nextStreetTarget;
    public Transform IntersectionParent;
    public enum cardinalDir { north, east, south, west }
    public cardinalDir CardinalDir;
    public void GetNewStreetTarget(IntersectionType i)
    {
        moveDir = Vector3.zero;
        moveDir.y = transform.position.y;
        nextStreetTarget = null;
        if (i == IntersectionType.x)
        {
            // get next target for CROSS INTERSECTION pt ca aliniez playerul cu local forward al strazii
            if (_currentTurnCollider == TurnColliders.EAST && InputController.Static.turnSide == InputController.TurnSide.left)
            {
                nextStreetTarget = IntersectionParent.FindChild(TurnColliders.SOUTH.ToString());
                CardinalDir = cardinalDir.south;
            }

            if (_currentTurnCollider == TurnColliders.EAST && InputController.Static.turnSide == InputController.TurnSide.right)
            {
                nextStreetTarget = IntersectionParent.FindChild(TurnColliders.NORTH.ToString());
                CardinalDir = cardinalDir.north;
            }


            if (_currentTurnCollider == TurnColliders.EAST && InputController.Static.turnSide == InputController.TurnSide.none)
            {
                nextStreetTarget = IntersectionParent.FindChild(TurnColliders.WEST.ToString());
                CardinalDir = cardinalDir.west;
            }


            if (_currentTurnCollider == TurnColliders.WEST && InputController.Static.turnSide == InputController.TurnSide.left)
            {
                nextStreetTarget = IntersectionParent.FindChild(TurnColliders.NORTH.ToString());
                CardinalDir = cardinalDir.north;
            }

            if (_currentTurnCollider == TurnColliders.WEST && InputController.Static.turnSide == InputController.TurnSide.right)
            {
               nextStreetTarget = IntersectionParent.FindChild(TurnColliders.SOUTH.ToString());
                CardinalDir = cardinalDir.south;
            }
 
            if (_currentTurnCollider == TurnColliders.WEST && InputController.Static.turnSide == InputController.TurnSide.none)
            {
                nextStreetTarget = IntersectionParent.FindChild(TurnColliders.EAST.ToString());
                CardinalDir = cardinalDir.east;
            }


            if (_currentTurnCollider == TurnColliders.SOUTH && InputController.Static.turnSide == InputController.TurnSide.left)
            {
                nextStreetTarget = IntersectionParent.FindChild(TurnColliders.WEST.ToString());
                CardinalDir = cardinalDir.west;
            }
                
            if (_currentTurnCollider == TurnColliders.SOUTH && InputController.Static.turnSide == InputController.TurnSide.right)
            {
                nextStreetTarget = IntersectionParent.FindChild(TurnColliders.EAST.ToString());
                CardinalDir = cardinalDir.east;
            }

            if (_currentTurnCollider == TurnColliders.SOUTH && InputController.Static.turnSide == InputController.TurnSide.none)
            {
                nextStreetTarget = IntersectionParent.FindChild(TurnColliders.NORTH.ToString());
                CardinalDir = cardinalDir.north;
            }

            if (_currentTurnCollider == TurnColliders.NORTH && InputController.Static.turnSide == InputController.TurnSide.left)
            {
                nextStreetTarget = IntersectionParent.FindChild(TurnColliders.EAST.ToString());
                CardinalDir = cardinalDir.east;
            }

            if (_currentTurnCollider == TurnColliders.NORTH && InputController.Static.turnSide == InputController.TurnSide.right)
            {
                nextStreetTarget = IntersectionParent.FindChild(TurnColliders.WEST.ToString());
                CardinalDir = cardinalDir.west;
            }
            if (_currentTurnCollider == TurnColliders.NORTH && InputController.Static.turnSide == InputController.TurnSide.none)
            {
                nextStreetTarget = IntersectionParent.FindChild(TurnColliders.SOUTH.ToString());
                CardinalDir = cardinalDir.south;
            }

        }        
        IntersectionParent.gameObject.SetActive(false);
        Invoke("ResetTargetTrigger", 3);
        isTurning = true;
    }


    float playerRotDiff;
    public Transform currentTurnLaneBlock;

    public void RotatePlayer()  // called from fixed update
    {

        if (nextStreetTarget == null)
            return;

        //if (canTurn) this fucks up turning for some reason
        //    return;

        if (isTurning)
        {
            moveDir = Vector3.zero;
            playerRotDiff = Quaternion.Angle(transform.rotation, nextStreetTarget.rotation);
            if (playerRotDiff >= 5)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, nextStreetTarget.rotation, 10f * Time.deltaTime);
            }
            else
            {
                switch (CardinalDir)
                {
                    case cardinalDir.north:
                        transform.position = new Vector3(nextStreetTarget.position.x + targetLanePosition, transform.position.y, currentTurnLaneBlock.position.z);

                        break;
                    case cardinalDir.east:
                        transform.position = new Vector3(currentTurnLaneBlock.position.x, transform.position.y, nextStreetTarget.position.z - targetLanePosition);

                        break;
                    case cardinalDir.west:
                        transform.position = new Vector3(currentTurnLaneBlock.position.x, transform.position.y, nextStreetTarget.position.z + targetLanePosition);

                        break;
                    case cardinalDir.south:
                        transform.position = new Vector3(nextStreetTarget.position.x - targetLanePosition, transform.position.y, nextStreetTarget.position.z);

                        break;
                    default:
                        break;
                }

                transform.rotation = nextStreetTarget.rotation;
                moveDir = transform.forward;  
                isTurning = false;
                
            }
        }

    }


    void ResetTargetTrigger()
    {
        IntersectionParent.gameObject.SetActive(true);
    }

    void KillPlayer()
    {
        CurrentState = PlayerStates.PlayerDead;
        playerAnimator.SetTrigger("CrashBack");
        GameController.Static.OnGameEnd();
        GameUIController.Static.ContinueScreen();
    }

    void ResetPlayerHurtCount()
    {
        playeHurtCount = 0;
        GameUIController.Static.UpdateHearts(playeHurtCount);
    }
    #endregion






}

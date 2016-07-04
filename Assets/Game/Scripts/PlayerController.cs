using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

// player States.......... 
public enum PlayerStates
{
	
	PlayerAlive,
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
        jumpSpeed = 8.0F,
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

    int playerHealt;// hitCount changes accoding to player index value THIS IS A STAT SPECIFFIC TO EACH PLAYER
    int runState = Animator.StringToHash("Base Layer.Run");
    int downStateValue2 = Animator.StringToHash("Base Layer.Roll");
    int downStateValue1 = Animator.StringToHash("Base Layer.Slide");
    int downStateValue3 = Animator.StringToHash("Base Layer.Down");
    int JumpStateValue = Animator.StringToHash("Base Layer.jump");
    int hitSide1 = Animator.StringToHash("Base Layer.LookBackSideleft");
    int hitSide2 = Animator.StringToHash("Base Layer.LookBackSideRight");//Double_Jump
    int Double_Jump = Animator.StringToHash("Base Layer.Double_Jump");
    // To check the Animations state

    public int leftTurn = Animator.StringToHash("Base Layer.Left_Turn");
    public int rightTurn = Animator.StringToHash("Base Layer.Right_Turn");
    Transform thisTranfrom;
    public PlayerObstacleCheck ObstacleCheck;

    private float 
        presentSpeed,
        originalSpeed;

    void Awake()
    {
 

    }



    void Start()
    {

        if (PlayerPrefs.GetInt("SelectedPlayer", 0) == 0)
        { // for Player 1
            // instantiate Correct player prefab
            GameObject p = Instantiate(playerPrefabs[0], transform.position, Quaternion.identity) as GameObject;
            p.transform.SetParent(transform);
            p.transform.position = transform.position;
            // set it's stats
        }
        else if (PlayerPrefs.GetInt("SelectedPlayer", 0) == 1)
        {//  for Player 2
            // instantiate Correct player prefab
            GameObject p = Instantiate(playerPrefabs[1], transform.position, Quaternion.identity) as GameObject;
            p.transform.SetParent(transform);
            p.transform.position = transform.position;
            // set it's stats
        }
        else if (PlayerPrefs.GetInt("SelectedPlayer", 0) == 2)
        { // for Player 3
            // instantiate Correct player prefab
            GameObject p = Instantiate(playerPrefabs[2], transform.position, Quaternion.identity) as GameObject;
            p.transform.SetParent(transform);
            p.transform.position = transform.position;
            // set it's stats

        }
        else if (PlayerPrefs.GetInt("SelectedPlayer", 0) == 3)
        { // for Player 4
            // instantiate Correct player prefab
            GameObject p = Instantiate(playerPrefabs[3], transform.position, Quaternion.identity) as GameObject;
            p.transform.SetParent(transform);
            p.transform.position = transform.position;
            // set it's stats
        }
        //.......................................

        playerAnimator = GetComponentInChildren<Animator>();
        //powerObj_JetPack.SetActive(false); dezactivate pana testez platforma
        //powerObj_Magnet.SetActive(false);
        isPlayerDead = false;
        //shoe1.SetActive(false);
        //shoe2.SetActive(false);
        controller = GetComponent<CharacterController>();
        thisTranfrom = transform;
        originalSpeed = speed;

        playerHealt = 3; // lifes? per player?

        CurrentState = PlayerStates.Idle;
    }

    public float tilt;
    float lastTimeColliderChange;
    float PlayerHorizontalMovement;
    public float HorizontalLerpTarget;
    public float horizontalLerpSpeed;
    public float flyHeight, flySpeed;

    void FixedUpdate()
    {

        thisPosition = thisTranfrom.position;//  this variable copy the present Player Position

        if (GameController.Static.isGamePaused)
            return;

        switch (CurrentState)
        {
            // player in normal mode....................................................
            case PlayerStates.PlayerAlive:
                InputController.Static.takeInput = true;
                isPlayerDead = false;
                speed = Mathf.Lerp(speed, TopSpeed, increaseSpeedTime);
                presentSpeed = speed;
                
                isPlayerOnGround();
                // for LaneChanging
                PlayerLaneChanging();
                //..................................

                if (controller.isGrounded)
                {
                    GameController.Static.stopObsticalIns = false;

                    moveDirection = new Vector3(0, 0, Time.deltaTime * 10 * speed);
                    moveDirection = transform.TransformDirection(moveDirection);
                    moveDirection *= speed;

                    if (doubleJump || (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == runState && InputController.Static.isJump))
                    {
                        if (normalJump)
                        {//Normal Jump
                            playerAnimator.SetTrigger("JumpHigh");
                            jumpSpeed = Mathf.Clamp(speed * 1.6f, 15, 18);
                            InputController.Static.isJump = false;
                            doubleJump = false;
                        }
                        else if (doubleJump)
                        {// Double jump trigger	
                            doubleJump = false;
                            jumpSpeed = Mathf.Clamp(speed * 1.6f, 30, 36);
                            InputController.Static.isJump = false;
                            if (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash != Double_Jump)
                                //playerAnimator.SetTrigger ("DoubleJump");
                                playerAnimator.SetTrigger("JumpHigh");
                        }
                        else
                        { // when jump power 

                            playerAnimator.SetTrigger("Jump");
                            PlayJumpSound();
                            PlayerPrefs.SetInt("MissionJumpCount", PlayerPrefs.GetInt("MissionJumpCount", 0) - 1);
                            jumpSpeed = speed;
                            InputController.Static.isJump = false;
                        }
                        moveDirection.y = jumpSpeed;
                    }
                }

                moveDirection.y -= (gravity * Time.deltaTime);
                controller.Move(moveDirection * Time.deltaTime);
                thisTranfrom.position = new Vector3(lanePosition, thisTranfrom.position.y, thisTranfrom.position.z);

                //   player Collider reduces here when Player animation Base Layer name is Slide or Roll

                if (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == downStateValue1 || playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == downStateValue2)
                {
                    controller.center = DownPosistion;
                    controller.height = DownHeight;
                }
                else
                {
                    if (Time.timeSinceLevelLoad - lastTimeColliderChange > 0.2f)
                    {
                        controller.center = StandingPosition;
                        controller.height = StandingHeight;
                        lastTimeColliderChange = Time.timeSinceLevelLoad;
                    }
                }
                break;

            //............................................................

            // player in fly mode........................................

            case PlayerStates.fly:

                PlayerCamera.Static.currentCam = PlayerCamera.Cam.flyModeCam;
                PlayerLaneChanging();
                speed = 18;
                flyHeight = Mathf.Lerp(flyHeight, 19, flySpeed);
                moveDirection = new Vector3(0, 0, Time.deltaTime * 10 * speed);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                controller.Move(moveDirection * Time.deltaTime);
                thisTranfrom.rotation = Quaternion.Euler(0, lanePosition * tilt, 0);
                thisTranfrom.position = new Vector3(lanePosition, flyHeight, thisTranfrom.position.z);
                if (flyHeight > 10)
                {
                    playerAnimator.SetTrigger("JetPackFly");
                }
                //Invoke ("PlayerStateChange", 10.0f);
                break;
            //.......................................................

            case PlayerStates.PlayerDead:
                CurrentState = PlayerStates.empty;
                InputController.Static.takeInput = false;
                isPlayerDead = true;
                break;
            case PlayerStates.Idle:
                //Debug.Log("Playerstate is IDLE");
                playerAnimator.SetTrigger("Slide");
                //play idle anim
                break;
        }
    }


    // Random Animation for slide or role
    public void Slide_Roll()
    {
        playerAnimator.SetTrigger("Roll");

        SoundController.Static.playSoundFromName("roll");
    }
    //......................................

    // to reset the jump power.......

    public void PowerJumpReset()
    {
        normalJump = false;
        GameUIController.Static.playerInJumpIndicator.SetActive(false);
        GameUIController.Static.progressBarScript.jumpModeProgressbar.fillAmount = 1;
        isJumpModeIndicator = false;
        shoe1.SetActive(false);
        shoe2.SetActive(false);
    }
    //......................................

    // to reset the Jetpack power.......

    public void JetPackPowerReset()
    {
        PlayerCamera.Static.currentCam = PlayerCamera.Cam.NormalCam;
        GameUIController.Static.playerInFlyIndicator.SetActive(false);
        GameUIController.Static.progressBarScript.flyModeProgressBar.fillAmount = 1;// to reset fill amount here
        isFlyModeIndicator = false;
        playerAnimator.SetTrigger("JetPackLand");
        powerObj_JetPack.SetActive(false);
        SoundController.Static.jetPackSound.volume = 0f;
        CurrentState = PlayerStates.PlayerAlive;
        speed = presentSpeed;
        ObstacleGenerator.Static.resetObstacles();
        if (DestroyUpCoins != null)
            DestroyUpCoins(null, null);
    }
    //................................



    #region player lanePosition

    //public float[] LaneLimits;
    public float[] LanesPositions;
    public float laneShiftSpeed;
    public float targetLanePosition, lanePosition;

    public enum PlayerLane
    {
        one,
        two,
        three
    };

    public PlayerLane currentLane, lastLane;

    // Player lane changing here this method is called at fixed update Player state alive 

    void PlayerLaneChanging()
    {
        switch (currentLane)
        {
            case PlayerLane.one:
                targetLanePosition = LanesPositions[0];
                break;
            case PlayerLane.two:
                targetLanePosition = LanesPositions[1];
                break;
            case PlayerLane.three:
                targetLanePosition = LanesPositions[2];
                break;
        }
        lastLane = currentLane;
        lanePosition = Mathf.Lerp(lanePosition, targetLanePosition, laneShiftSpeed);
    }



    #endregion

    //for  destroy already crated objects 
    GameObject[] destroy_Obsticals_Respwan;

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
        //PlayerEnemyController.Static.ResetToChase ();
        playerAnimator.SetTrigger("Run");
        CurrentState = PlayerStates.PlayerAlive;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<curverSetter>().enabled = true;
        GameController.Static.OnGameStart();
        GameUIController.isGameEnd = false;
    }
    // used when player trigger with DoubleJump
    public static bool doubleJump = false, normalJump = false;
    public bool isMagnetIndicator = false, isMultiplierIndicator = false, isFlyModeIndicator = false, isJumpModeIndicator = false;
    public static Vector3 barrelPosition, potPosition;
    public static bool isBarrelBroken = false, isPotBroken = false;
    int barrelPotTouche_Count = 0;
    int score = 0;

    #region player Trigger Enter with
    //for some reason ,if player is at great speed ,it misses the collision with DoubleJump trigger,so we checking with ray

    public float magnetPowerTime, multiplierPowerTime, lastTriggerJumpTime;

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
                GameUIController.Static.UpdateHearts(playeHurtCount);
                if (playeHurtCount == playerHealt)
                {
                    Debug.Log("hurt count max!");
                    KillPlayer();

                }
            }
        }


        // Player Trigger with Coin............

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

        //......................................................		

        // player  Trigger with Magnet...........

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
        //.........................................................

        //Player trigger with Multiplier..........

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
        //.........................................................

        // Player trigger with JumpMode the current will jump mode in 10sec
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

        // Player trigger with FlyMode the currentState will  Fly mode 10 sec

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
            for (int i = 0; i <= Obj.Length - 1; i++)
            {
                Destroy(Obj[i], 1.0f);
            }
            GameUIController.Static.playerInFlyIndicator.SetActive(true);
            isFlyModeIndicator = true;
            playerAnimator.SetTrigger("JetPackJump");
            GameController.Static.GenerateCoins_FlyMode();
            GameController.Static.stopObsticalIns = true;
            ObstacleGenerator.Static.index = 0;
            //PlayerEnemyController.Static.QuickHideEnemy ();
            Destroy(incomingObj);
        }
        //...................................................

        // incomingTag.Contains Barrel or Pot................

        else if (incomingTag.Contains("Barrel"))
        {
            PlayerPrefs.SetInt("MissionDestroyBarrelCount", PlayerPrefs.GetInt("MissionDestroyBarrelCount", 0) - 1);
            //PlayerEnemyController.Static.currentEnemyState = PlayerEnemyController.PlayerEnemyStates.chasing;
            isBarrelBroken = true;
            barrelPosition = incomingObj.transform.position;
            GameController.Static.GenerateBrokenBarrel();
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

        // trigger with Pots
        else if (incomingTag.Contains("Pots"))
        {
            isPotBroken = true;
            PlayerPrefs.SetInt("MissionDestroyPotsCount", PlayerPrefs.GetInt("MissionDestroyPotsCount", 0) - 1);
            //PlayerEnemyController.Static.currentEnemyState = PlayerEnemyController.PlayerEnemyStates.chasing;// Player enemy state changes here
            potPosition = incomingObj.transform.position;
            GameController.Static.GenerateBrokenPots();// to generate broken pot here
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
        else if (incomingTag.Contains("Obstacle") && (CurrentState == PlayerStates.PlayerAlive || CurrentState == PlayerStates.powerJump || CurrentState == PlayerStates.fly))
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

    // to reset the Multiplier Power............
    public void switchOffMultiplier()
    {
        GameUIController.Static.progressBarScript.multiplierProgressBar.fillAmount = 1;
        isMultiplierIndicator = false;

        GameUIController.Static.multiplierIndicator.SetActive(false);
        GameUIController.Static.multiplierValue = PlayerPrefs.GetInt("MultiplierCount_Ingame", 1);
    }
    //........................................

    // to reset the magnet Power......................
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
    //................................................

    #region On Game End

    //void GameEnd()
    //{
    //    InGameUIController.Static.HUD.SetActive(false);// to set hud set deactive here
    //    InGameUIController.Static.GameEndMenuParent.SetActive(true);// to set Game end menu active here
    //}

    #endregion

    void PlayJumpSound()
    {
        SoundController.Static.playSoundFromName("Jump");
    }

    // to draw ray down when palyer in slopes to play down animation

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
    //.......................................................


    #region Lanchange Anim

    //Player Lane changes here to check the left side is any obstacle player hurt count increased
    int playeHurtCount = 0;

    public void LeftSideMoving()
    {

        //thisTranfrom.eulerAngles = new Vector3 (0, 0, 0);
        //Invoke("ResetPlayerHurtCount", 5.0f); // to reset player hurt count
        if (ObstacleCheck.CheckLeftSide())
        {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == runState)
            {
                playerAnimator.SetTrigger("LeftSideHit");
            }
            print("Debug PlayerHurtCount : " + playeHurtCount);
            speed = originalSpeed;
            playeHurtCount++;
            GameUIController.Static.UpdateHearts(playeHurtCount);
            if (playeHurtCount == playerHealt)
            {
                KillPlayer();
            }
        }
        else if (CurrentState != PlayerStates.PlayerDead)
        {

            switch (currentLane)
            {
                case PlayerLane.one:
                    break;
                case PlayerLane.two:

                    currentLane = PlayerLane.one;
                    PlayerPrefs.SetInt("MissionswipeLeftCount", PlayerPrefs.GetInt("MissionswipeLeftCount", 0) - 1);
                    if (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == runState)
                    {
                        playerAnimator.SetTrigger("LeftTurn");
                    }
                    SoundController.Static.playSoundFromName("swipe");
                    break;
                case PlayerLane.three:

                    currentLane = PlayerLane.two;
                    PlayerPrefs.SetInt("MissionswipeLeftCount", PlayerPrefs.GetInt("MissionswipeLeftCount", 0) - 1);
                    if (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == runState)
                    {
                        playerAnimator.SetTrigger("LeftTurn");
                    }
                    SoundController.Static.playSoundFromName("swipe");
                    break;
            }
        }
    }

    //Player Lane changes here to check the right side is any obstacle player hurt count increased

    public void RightSideMoving()
    {
        
        //thisTranfrom.eulerAngles = new Vector3 (0, 0, 0);
        //Invoke("ResetPlayerHurtCount", 5.0f);// Player hurt count reset after 5 seconds,so he can hit two more times
        if (ObstacleCheck.CheckRightSide())
        {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == runState)
            {
                playerAnimator.SetTrigger("RightSideHit");
            }
            print("Debug PlayerHurtCount : " + playeHurtCount);
            speed = originalSpeed;
            playeHurtCount++;
            GameUIController.Static.UpdateHearts(playeHurtCount);
            if (playeHurtCount == playerHealt)
            {
                KillPlayer();
            }

        }
        else


        if (CurrentState != PlayerStates.PlayerDead)
        {
            switch (currentLane)
            {
                case PlayerLane.one:

                    PlayerPrefs.SetInt("MissionswipeRightCount", PlayerPrefs.GetInt("MissionswipeRightCount") - 1);
                    currentLane = PlayerLane.two;
                    if (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == runState)
                    {
                        playerAnimator.SetTrigger("RightTurn");
                    }
                    SoundController.Static.playSoundFromName("swipe");
                    break;
                case PlayerLane.two:

                    PlayerPrefs.SetInt("MissionswipeRightCount", PlayerPrefs.GetInt("MissionswipeRightCount") - 1);
                    currentLane = PlayerLane.three;
                    if (playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == runState)
                    {
                        playerAnimator.SetTrigger("RightTurn");
                    }
                    SoundController.Static.playSoundFromName("swipe");
                    break;
                case PlayerLane.three:

                    break;

            }

        }
    }

    void KillPlayer()
    {
        CurrentState = PlayerStates.PlayerDead;
        playerAnimator.SetTrigger("CrashBack");
        GameController.Static.OnGameEnd();
        GameUIController.Static.ContinueScreen();
    }

    // Player hurt count reset here
    void ResetPlayerHurtCount()
    {
        playeHurtCount = 0;
        GameUIController.Static.UpdateHearts(playeHurtCount);
    }
    #endregion






}

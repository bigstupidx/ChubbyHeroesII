using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
	public GameObject 
    ResumeMenuParent, 
    GameEndMenuParent, 
    HUD, 
    continueScreen,
        continueContainer,
        continueBgImg,
        powEffect,
    magnetIndicator, 
    multiplierIndicator, 
    playerInFlyIndicator, 
    playerInJumpIndicator, 
    missionCompletedIndicator, 
    loadingParent, 
    insufficientFunds,
    DeathParticles;

	public int inGameCoinCount = 0;
	public int inGameScoreCount = 0;
    public int multiplierValue = 1;
    int countinueCount = 1;
    public int
    swipeLeftCount,
    swipeRightCount,
    jumpCount,
    coinCount;

    public Image[] hearts;	
     
	public float 
    inGameDistance, 
    continueCoins;
    float indicatorSpeed;

    public Text  
    scoreCountText, 
    coinsCountText, 
    distanceCountText, 
    missionCompletedText,
    cointinueCost, 
    totalCoinsAtContinueScreen,
    multiplierCountText,
    powerUpIndicatorText, 
    highestScoreText;

	public static GameUIController Static;
    curverSetter worldCruveScript;

    public ProgressBar progressBarScript;
	public static event EventHandler 
    showLeaderBoard,
    faceBookShare;

    public Animator
    moveIndicatorAnim,
    countdownToDeath,
    powerIndicatorAnim,
    highestScoreAnim;
	Vector3 missionCompleteIncator_StartPosition;

    public static bool isGameEnd = false;
    GameObject[] coinsScript;

    // hashes
    int finish = Animator.StringToHash("Base Layer.finish");
    


    void OnEnable ()
	{
		coinControl.isONMagetPower = false;
		coinsScript = GameObject.FindGameObjectsWithTag ("Coin");

		for (int i=0; i<coinsScript.Length; i++)
        {
			coinsScript [i].GetComponent<coinControl> ().resetSize ();
		}

		isGameEnd = false;				
	}

    void OnApplicationFocus(bool focusStatus)
    {
        if (!isGameEnd)
        OnButtonClick("Pause");
    }

    void Start ()
	{
        Static = this;
        multiplierValue = PlayerPrefs.GetInt ("MultiplierCount_Ingame", 1);
		Invoke ("ShowHighestIndicatorAnim", 2);
		progressBarScript = GameObject.Find ("GameUI").GetComponent<ProgressBar> ();
		missionCompleteIncator_StartPosition = missionCompletedIndicator.transform.localPosition;
		float v = missionCompletedIndicator.transform.localPosition.x;
		continueCoins = 500;
		worldCruveScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<curverSetter> ();
	}

	void Update ()
	{
        totalCoinsAtContinueScreen.text = "" + PlayerPrefs.GetInt ("TotalCoins", 0);

        if (!isGameEnd)
        {
            Completed_MissionsIndications ();
            Coins_IngameCount ();
            Score_IngameCount ();          	
            Multiplier_IngameCount ();				
            Distance_IngameCount ();
        }

        if (countdownToDeath.GetCurrentAnimatorStateInfo (0).fullPathHash == finish && isGameEnd)
        {
            GameEnd (); // to show game end menu on clock ran out 						
        }

        if (Input.GetKeyUp (KeyCode.Escape))
        {
			if (Time.timeScale == 0)
                OnButtonClick ("Resume");		
            else
                OnButtonClick ("Pause");
        }
	}

    public void UpdateHearts(int hurtCount)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(hurtCount <= i)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }
        
	public void OnButtonClick (string ButtonName)
	{
		switch (ButtonName) {
		    case "Pause":
				Time.timeScale = 0;
				HUD.SetActive (false);
				ResumeMenuParent.SetActive (true);
				worldCruveScript.enabled = false;
				SoundController.Static.playSoundFromName ("Click");
				break;
		    case "Resume":
				Time.timeScale = 1;
				ResumeMenuParent.SetActive (false);
				HUD.SetActive (true);
				worldCruveScript.enabled = true;
				SoundController.Static.playSoundFromName ("Click");
				break;
		    case "Restart":
				Time.timeScale = 1;				
				GameEndMenuParent.transform.localScale = Vector3.zero;
				continueScreen.SetActive (false);
				ResumeMenuParent.SetActive (false);						
				loadingParent.SetActive (true);
                //SoundController.Static.playSoundFromName("Click");
                MenuHelper._Instance.restartFromGameplay = 1;
                SceneManager.LoadSceneAsync(1);
				break;
		    case "Home":
                //SoundController.Static.playSoundFromName ("Click");
                ResumeMenuParent.SetActive (false);
                MenuHelper._Instance.restartFromGameplay = 0;
                SceneManager.LoadSceneAsync(1);
				break;
		    case "FbLike":
				string fbUrl = "https://www.facebook.com/AceGamesHyderabad";
				Application.OpenURL (fbUrl);
				SoundController.Static.playSoundFromName ("Click");
				break;
		    case "FbShare":
				if (faceBookShare != null)
						faceBookShare (null, null);
				Debug.Log ("Best Score PlayerPrefs value " + PlayerPrefs.GetFloat ("BestScore", 0));
				break;
		    case "LeaderBoard":
				if (showLeaderBoard != null)
						showLeaderBoard (null, null);
				break;
		    case "YesBtn":
				if (PlayerPrefs.GetInt ("TotalCoins", 0) >= GameUIController.Static.continueCoins)
                {
					GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().RespwanPlayer ();
					Debug.Log ("Call Respwan method from Player Controller");
					continueScreen.SetActive (false);
					isGameEnd = false;
					HUD.SetActive (true);
				}
                else
                {
					InsufficientFunds ();
				}
	            //GameEnd ();
				break;
		    case "Okay":
				insufficientFunds.SetActive (false);
				GameEnd ();
				break;
		}
	}


	void InsufficientFunds ()
	{
		continueScreen.SetActive (false);
		insufficientFunds.SetActive (true);
	}

    // _________
    public GameObject[] powShapes;
    public Text powText;
    public string[] powStrings;
    int shapeIndex;

    public void ContinueScreen ()
	{
		isGameEnd = true;

        shapeIndex = UnityEngine.Random.Range(0, powShapes.Length);
        powShapes[shapeIndex].SetActive(true);

        int powTextIndex = UnityEngine.Random.Range(0, powStrings.Length);
        powText.text = powStrings[powTextIndex];

        continueScreen.SetActive (true);
        powEffect.SetActive(true);
        Invoke("StartCountTodeath", 1.5f);
        HUD.SetActive (false);			 
		continueCoins = 500 * countinueCount;
		cointinueCost.text = "" + continueCoins;
		countinueCount++;
		worldCruveScript.enabled = false;  // add pause curveing
	}

    void StartCountTodeath()
    {
        continueBgImg.SetActive(true);
        powEffect.SetActive(false);
        powShapes[shapeIndex].SetActive(false);
        continueContainer.SetActive(true);
        countdownToDeath.SetTrigger("count2Death");
    }

	public void GameEnd ()
	{
		GameEndMenuParent.SetActive (true);
		HUD.SetActive (false);
		continueScreen.SetActive (false);
	}

	void Multiplier_IngameCount ()
	{
		multiplierCountText.text = "x " + multiplierValue.ToString ();
	}

	void Coins_IngameCount ()
	{
		coinsCountText.text = "" + inGameCoinCount.ToString ().PadLeft (3, '0');
	}

	void Score_IngameCount ()
	{
		//scoreCountText.text = "" + inGameScoreCount.ToString ().PadLeft (4,'0');
	}

	
	void Distance_IngameCount ()
	{
		inGameDistance += multiplierValue * 0.4f;
		distanceCountText.text = "" + Mathf.RoundToInt (inGameDistance).ToString ().PadLeft (6, '0');
	}

	void Completed_MissionsIndications ()
	{
		//indicatorSpeed = Mathf.Lerp (indicatorSpeed, -250, 0.5f);
		if (PlayerPrefs.GetInt ("MissionCoinsCount", 0) <= 0 && PlayerPrefs.GetInt ("CollectCoins", 0) == 1) {
				missionCompletedText.text = "Collect Coins Completed";
				//moveIndicatorAnim.SetTrigger ("NewPosition");
				Invoke ("MissionIndicatorMove_Start", 4f);
				PlayerPrefs.SetInt ("CollectCoins", 2);
		} else if (PlayerPrefs.GetInt ("MissionMagnetPowerCount", 0) <= 0 && PlayerPrefs.GetInt ("MagnerPower", 0) == 1) {
				missionCompletedText.text = "Collect Magnet Power Completed";
				//moveIndicatorAnim.SetTrigger ("NewPosition");
				Invoke ("MissionIndicatorMove_Start", 4f);
				PlayerPrefs.SetInt ("MagnetPower", 2);
		} else if (PlayerPrefs.GetInt ("MissionFlyPowerCount", 0) <= 0 && PlayerPrefs.GetInt ("FlyPower", 0) == 1) {
				missionCompletedText.text = "Collect JetPack Power Completed";
				//moveIndicatorAnim.SetTrigger ("NewPosition");
				Invoke ("MissionIndicatorMove_Start", 4f);
				PlayerPrefs.SetInt ("FlyPower", 2);
		} else if (PlayerPrefs.GetInt ("Mission2XPowerCount", 0) <= 0 && PlayerPrefs.GetInt ("2XPower", 0) == 1) {
				missionCompletedText.text = "Collect Coin Multiplier Power Completed";
				//moveIndicatorAnim.SetTrigger ("NewPosition");
				Invoke ("MissionIndicatorMove_Start", 4f);
				PlayerPrefs.GetInt ("2XPower", 2);
		} else if (PlayerPrefs.GetInt ("MissionJumpPowerCount", 0) <= 0 && PlayerPrefs.GetInt ("JumpPower", 0) == 1) {
				missionCompletedText.text = "Collect Extra Jump Power Completed";
				//moveIndicatorAnim.SetTrigger ("NewPosition");
				Invoke ("MissionIndicatorMove_Start", 4f);
				PlayerPrefs.SetInt ("JumpPower", 2);
		} else if (PlayerPrefs.GetInt ("MissionRoll/SlideCount", 0) <= 0 && PlayerPrefs.GetInt ("Roll/Slide", 0) == 1) {
				missionCompletedText.text = "Slide and Roll Completed";
				//moveIndicatorAnim.SetTrigger ("NewPosition");
				Invoke ("MissionIndicatorMove_Start", 4f);
				PlayerPrefs.SetInt ("Roll/Slide", 2);
		} else if (PlayerPrefs.GetInt ("MissionDestroyBarrelCount", 0) <= 0 && PlayerPrefs.GetInt ("Barrel", 0) == 1) {
				missionCompletedText.text = "Destroy Barrels Completed";
				//moveIndicatorAnim.SetTrigger ("NewPosition");
				Invoke ("MissionIndicatorMove_Start", 4f);
				PlayerPrefs.SetInt ("Barrel", 2);
		} else if (PlayerPrefs.GetInt ("MissionDestroyPotsCount", 0) <= 0 && PlayerPrefs.GetInt ("Pots", 0) == 1) {
				missionCompletedText.text = "Destroy Pots Completed";
				//moveIndicatorAnim.SetTrigger ("NewPosition");
				Invoke ("MissionIndicatorMove_Start", 4f);
				PlayerPrefs.SetInt ("Pots", 2);
		} else if (PlayerPrefs.GetInt ("MissionJumpCount", 0) <= 0 && PlayerPrefs.GetInt ("JumpCount", 0) == 1) {
				missionCompletedText.text = "Jump Completed";
				//moveIndicatorAnim.SetTrigger ("NewPosition");
				Invoke ("MissionIndicatorMove_Start", 4f);
				PlayerPrefs.SetInt ("JumpCount", 2);
		}
	}

	void MissionIndicatorMove_Start ()
	{
		//moveIndicatorAnim.SetTrigger ("Previous");
	}


	public void ShowPowerIndicatorAnim ()
	{
		powerIndicatorAnim.SetTrigger ("ShowPowerUp");
		Invoke ("HidePowerIndicatorAnim", 3.0f);
	}

	public void HidePowerIndicatorAnim ()
	{
		if (powerIndicatorAnim)
				powerIndicatorAnim.SetTrigger ("HidePowerUps");	
	}

	public void ShowHighestIndicatorAnim ()
	{
		highestScoreAnim.SetTrigger ("ShowHighestScore");
		highestScoreText.text = "" + Mathf.RoundToInt (PlayerPrefs.GetFloat ("BestDistance", 0));
		Invoke ("HideHighestIndicatorAnim", 3.0f);
	}

	public void HideHighestIndicatorAnim ()
	{
		highestScoreAnim.SetTrigger ("HideHighestScore");
	}
}

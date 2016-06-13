using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class InGameUIController : MonoBehaviour
{

		public GameObject ResumeMenuParent, GameEndMenuParent, HUD, continueScreen;
		public int inGameCoinCount = 0;
		public int inGameScoreCount = 0;
	 
		public float inGameDistance, continueCoins;
		public Text  scoreCountText, coinsCountText, distanceCountText, missionCompletedText;
		public static InGameUIController Static;
		public GameObject magnetIndicator, multiplierIndicator, playerInFlyIndicator, playerInJumpIndicator, missionCompletedIndicator, loadingParent, unSufficentCoins;
		public ProgressBar progressBarScript;
		public Text cointinueCost, totalCoinsAtContinueScreen;
		//Events..................
		public static event EventHandler showLeaderBoard,faceBookShare;

		public Animator moveIndicatorAnim, InGameAnimator;
		// these Variables Used in Dialy Mission

		public int swipeLeftCount, swipeRightCount, jumpCount, coinCount;
		Vector3 missionCompleteIncator_StartPosition;
		curverSetter worldCruveScript;

		void OnEnable ()
		{
				coinControl.isONMagetPower = false;
				coinsScript = GameObject.FindGameObjectsWithTag ("Coin");
				for (int i=0; i<coinsScript.Length; i++) {
						coinsScript [i].GetComponent<coinControl> ().resetSize ();
				}
				isGameEnd = false;
				
		}

		void Start ()
		{
				multiplierValue = PlayerPrefs.GetInt ("MultiplierCount_Ingame", 1);
				//to show highest score 
				Invoke ("ShowHighestIndicatorAnim", 2);
				Static = this;
				progressBarScript = GameObject.Find ("IngameUI").GetComponent<ProgressBar> ();
				missionCompleteIncator_StartPosition = missionCompletedIndicator.transform.localPosition;
				float v = missionCompletedIndicator.transform.localPosition.x;
				continueCoins = 500;

				worldCruveScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<curverSetter> ();
		}

		//bool isOnceDistance_1 = true, isOnceDistance_2, isOnceDistance_3, isOnceDistance_4, isOnceDistance_5;
		//bool isMoving = true;

		void Update ()
		{

				totalCoinsAtContinueScreen.text = "" + PlayerPrefs.GetInt ("TotalCoins", 0);
				if (!isGameEnd) {
						Completed_MissionsIndications ();
						//for Distance
			 
						//for Coins
						Coins_IngameCount ();
						// for Score
						Score_IngameCount ();
	          	
						Multiplier_IngameCount ();
				
						Distance_IngameCount ();
				}
				if (InGameAnimator.GetCurrentAnimatorStateInfo (0).fullPathHash == finish && isGameEnd) {
						GameEnd (); // to show game end menu on clock ran out 
						
				}
				if (Input.GetKeyUp (KeyCode.Escape)) {
			
						Debug.Log ("Escaped pressed ");
						if (Time.timeScale == 0) { 
								OnButtonClick ("Resume");
				
						} else
								OnButtonClick ("Pause");
				}

		}

		public static bool isGameEnd = false;
		GameObject[] coinsScript;

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
						Invoke ("PlayAgain2", 0.1f);
						SoundController.Static.playSoundFromName ("Click");
						break;
				case "Home":
						SceneManager.LoadSceneAsync ("MainMenu");
						ResumeMenuParent.SetActive (false);
						SoundController.Static.playSoundFromName ("Click");
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
						if (PlayerPrefs.GetInt ("TotalCoins", 0) >= InGameUIController.Static.continueCoins) {
								GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().RespwanPlayer ();
								Debug.Log ("Call Respwan method from Player Controller");
								continueScreen.SetActive (false);
								isGameEnd = false;
								HUD.SetActive (true);
						} else {
								UnsufficeCoins ();
						}
			//GameEnd ();
						break;

				case "Okay":
						unSufficentCoins.SetActive (false);
						GameEnd ();
						break;
				}
		}

		int finish = Animator.StringToHash ("Base Layer.finish");

		void UnsufficeCoins ()
		{
				continueScreen.SetActive (false);
				unSufficentCoins.SetActive (true);
		}

    void PlayAgain2()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(thisScene.name);
    }

    int countinueCount = 1;

		public void ContinueScreen ()
		{
				isGameEnd = true;

				InGameAnimator.SetTrigger ("CountinueBoXMoving");
				//CountinueBoXMoving
				continueScreen.SetActive (true);
				HUD.SetActive (false);
				 
				continueCoins = 500 * countinueCount;
				cointinueCost.text = "" + continueCoins;
				countinueCount++;
				//Debug.Log("Finish");
				worldCruveScript.enabled = false;
		}

		public void GameEnd ()
		{
				//script.enabled = false;
				GameEndMenuParent.SetActive (true);
				HUD.SetActive (false);
				continueScreen.SetActive (false);
		}

		public Text multiplierCountText;

		void Multiplier_IngameCount ()
		{
				multiplierCountText.text = "x " + multiplierValue.ToString ();
		}

		void Coins_IngameCount ()
		{
				coinsCountText.text = "" + inGameCoinCount.ToString ().PadLeft (3, '0');

		}

		void 	Score_IngameCount ()
		{
				//scoreCountText.text = "" + inGameScoreCount.ToString ().PadLeft (4,'0');

		}

		public int multiplierValue = 1;

		void Distance_IngameCount ()
		{
				inGameDistance += multiplierValue * 0.4f;

				distanceCountText.text = "" + Mathf.RoundToInt (inGameDistance).ToString ().PadLeft (6, '0');
			 

		}

		float indicatorSpeed;
		//public bool isMoving = false;
		void Completed_MissionsIndications ()
		{

				//indicatorSpeed = Mathf.Lerp (indicatorSpeed, -250, 0.5f);



				if (PlayerPrefs.GetInt ("MissionCoinsCount", 0) <= 0 && PlayerPrefs.GetInt ("CollectCoins", 0) == 1) {
						missionCompletedText.text = "Collect Coins Completed";
						moveIndicatorAnim.SetTrigger ("NewPosition");
						Invoke ("MissionIndicatorMove_Start", 4f);
						PlayerPrefs.SetInt ("CollectCoins", 2);
				} else if (PlayerPrefs.GetInt ("MissionMagnetPowerCount", 0) <= 0 && PlayerPrefs.GetInt ("MagnerPower", 0) == 1) {
						missionCompletedText.text = "Collect Magnet Power Completed";
						moveIndicatorAnim.SetTrigger ("NewPosition");
						Invoke ("MissionIndicatorMove_Start", 4f);
						PlayerPrefs.SetInt ("MagnetPower", 2);
				} else if (PlayerPrefs.GetInt ("MissionFlyPowerCount", 0) <= 0 && PlayerPrefs.GetInt ("FlyPower", 0) == 1) {
						missionCompletedText.text = "Collect JetPack Power Completed";
						moveIndicatorAnim.SetTrigger ("NewPosition");
						Invoke ("MissionIndicatorMove_Start", 4f);
						PlayerPrefs.SetInt ("FlyPower", 2);
				} else if (PlayerPrefs.GetInt ("Mission2XPowerCount", 0) <= 0 && PlayerPrefs.GetInt ("2XPower", 0) == 1) {
						missionCompletedText.text = "Collect Coin Multiplier Power Completed";
						moveIndicatorAnim.SetTrigger ("NewPosition");
						Invoke ("MissionIndicatorMove_Start", 4f);
						PlayerPrefs.GetInt ("2XPower", 2);
				} else if (PlayerPrefs.GetInt ("MissionJumpPowerCount", 0) <= 0 && PlayerPrefs.GetInt ("JumpPower", 0) == 1) {
						missionCompletedText.text = "Collect Extra Jump Power Completed";
						moveIndicatorAnim.SetTrigger ("NewPosition");
						Invoke ("MissionIndicatorMove_Start", 4f);
						PlayerPrefs.SetInt ("JumpPower", 2);
				} else if (PlayerPrefs.GetInt ("MissionRoll/SlideCount", 0) <= 0 && PlayerPrefs.GetInt ("Roll/Slide", 0) == 1) {
						missionCompletedText.text = "Slide and Roll Completed";
						moveIndicatorAnim.SetTrigger ("NewPosition");
						Invoke ("MissionIndicatorMove_Start", 4f);
						PlayerPrefs.SetInt ("Roll/Slide", 2);
				} else if (PlayerPrefs.GetInt ("MissionDestroyBarrelCount", 0) <= 0 && PlayerPrefs.GetInt ("Barrel", 0) == 1) {
						missionCompletedText.text = "Destroy Barrels Completed";
						moveIndicatorAnim.SetTrigger ("NewPosition");
						Invoke ("MissionIndicatorMove_Start", 4f);
						PlayerPrefs.SetInt ("Barrel", 2);
				} else if (PlayerPrefs.GetInt ("MissionDestroyPotsCount", 0) <= 0 && PlayerPrefs.GetInt ("Pots", 0) == 1) {
						missionCompletedText.text = "Destroy Pots Completed";
						moveIndicatorAnim.SetTrigger ("NewPosition");
						Invoke ("MissionIndicatorMove_Start", 4f);
						PlayerPrefs.SetInt ("Pots", 2);
				} else if (PlayerPrefs.GetInt ("MissionJumpCount", 0) <= 0 && PlayerPrefs.GetInt ("JumpCount", 0) == 1) {
						missionCompletedText.text = "Jump Completed";
						moveIndicatorAnim.SetTrigger ("NewPosition");
						Invoke ("MissionIndicatorMove_Start", 4f);
						PlayerPrefs.SetInt ("JumpCount", 2);
				}


		}

		void MissionIndicatorMove_Start ()
		{
				moveIndicatorAnim.SetTrigger ("Previous");

		}

		public Animator powerIndicatorAnim, highestScoreAnim;
		public Text powerUpIndicatorText, highestScoreText;

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

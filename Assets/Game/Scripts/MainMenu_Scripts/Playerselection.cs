using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Playerselection : MonoBehaviour
{
	public GameObject
        selectButton,
        buyButton,
        selectedButton,
        LevelSelectionParent, 
        buyPopUp, 
        PlayerSelectionMenu, 
        MainMenuParent, 
        UnsufficentCoinsForPlayerselection, 
        loadingParent,
        previousButton,
        nextButton;

    public GameObject[] PlayerMeshObjs;

    public Text 
        //playOrBuy, 
        playerDescriptionText,
        PlayerNameText,
        PlayerPriceDisplayText;

	public Transform playerCamera;
    
	public Vector3[] targetPlayerCamPositions; 

	public float playerCamSpeed;

    public static int PlayerIndex = 0;

    float lastSwipeTime;

    Vector2 startMousePosition;

    bool touchStarted = false;

    float Swipedistance = 20;



    void Start ()
	{ 
        PlayerIndex = PlayerPrefs.GetInt("SelectedPlayer", 0);
        ShowPlayerInfo();
    }

	void Update ()
	{
		playerCamera.localPosition = Vector3.MoveTowards (playerCamera.localPosition, targetPlayerCamPositions [PlayerIndex], playerCamSpeed * Time.deltaTime);
		 
		if (Input.GetKeyDown (KeyCode.Mouse0) && MainMenuScreens.currentScreen == MainMenuScreens.MenuScreens.playerSelectionMenu) {
			startMousePosition = Input.mousePosition;
			touchStarted = true;
		}
	 			
		if (touchStarted && Vector2.Distance (startMousePosition, Input.mousePosition) > Swipedistance) {
			if (Input.mousePosition.x - startMousePosition.x < 0) {
				showNextPlayer ();
			} else
				showPreviousPlayer ();

			touchStarted = false;
		}

		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			touchStarted = false;
		}
	}

	public void OnButtonClick (string ButtonName)
	{
		switch (ButtonName) {
		case "Select":
				SoundController.Static.playSoundFromName ("Click");
                selectedButton.SetActive(true);
                // save selected player index
                PlayerPrefs.SetInt("SelectedPlayer", PlayerIndex);
				break;
		case "Previous":
				SoundController.Static.playSoundFromName ("Click");
				showPreviousPlayer ();
				break;
		case "Next":
				SoundController.Static.playSoundFromName ("Click");
				showNextPlayer ();
				break;
		case "Buy":
				SoundController.Static.playSoundFromName ("Click");
				PurchasePlayer ();
				MainMenuScreens.currentScreen = MainMenuScreens.MenuScreens.UnSufficentCoinsMenu;
				break;
		case "Back":
				SoundController.Static.playSoundFromName ("Click");
				PlayerSelectionMenu.SetActive (false);
				MainMenuParent.SetActive (true);
				break;
		}
	}


	void showNextPlayer ()
	{	
		if (Time.timeSinceLevelLoad - lastSwipeTime < 0.3f)
				return;
		PlayerIndex++;
		if (PlayerIndex > PlayerMeshObjs.Length - 1)
				PlayerIndex = targetPlayerCamPositions.Length - 1;
				
		lastSwipeTime = Time.timeSinceLevelLoad;
		ShowPlayerInfo ();			
	}

	void showPreviousPlayer ()
	{
		if (Time.timeSinceLevelLoad - lastSwipeTime < 0.3f)
				return;
		PlayerIndex--;
		if (PlayerIndex < 0)
				PlayerIndex = 0;
		lastSwipeTime = Time.timeSinceLevelLoad;
		ShowPlayerInfo ();
	}

	public void ShowPlayerInfo ()
	{
		switch (PlayerIndex) {
			case 0:
				PlayerNameText.text = "Ninja";
				PlayerPriceDisplayText.text = "FREE ";
				playerDescriptionText.text = "WITHSTAND 1 HIT";	
				buyButton.SetActive (false);
                previousButton.SetActive(false);
                if (PlayerPrefs.GetInt("SelectedPlayer") == PlayerIndex)
                {
                    selectButton.SetActive(false);
                    selectedButton.SetActive(true);
                }
                else
                {
                    selectedButton.SetActive(false);
                    selectButton.SetActive(true);
                }
				break;
			case 1:		 
				playerDescriptionText.text = "WITHSTAND 2 HITS ";
				PlayerNameText.text = "Fire Ninja";
				PlayerPriceDisplayText.text = "10000 ";
                previousButton.SetActive(true);
                if (PlayerPrefs.GetInt ("isPlayer1Purchased", 0) == 1) {
					buyButton.SetActive (false);
                    if (PlayerPrefs.GetInt("SelectedPlayer") == PlayerIndex)
                    {
                        selectButton.SetActive(false);
                        selectedButton.SetActive(true);
                    }
                    else
                    {
                        selectedButton.SetActive(false);
                        selectButton.SetActive(true);
                    }
                } else {
                    buyButton.SetActive (true);
                    selectedButton.SetActive(false);
                    selectButton.SetActive(false);
                }
				break;
			case 2:		 
				playerDescriptionText.text = "WITHSTAND 3 HITS ";
				PlayerNameText.text = "Earth Ninja";
				PlayerPriceDisplayText.text = "30000 ";
			
				if (PlayerPrefs.GetInt ("isPlayer2Purchased", 0) == 1) {
					buyButton.SetActive (false);
                    if (PlayerPrefs.GetInt("SelectedPlayer") == PlayerIndex)
                    {
                        selectButton.SetActive(false);
                        selectedButton.SetActive(true);
                    }
                    else
                    {
                        selectedButton.SetActive(false);
                        selectButton.SetActive(true);
                    }
                } else {
					buyButton.SetActive (true);
                    selectedButton.SetActive(false);
                    selectButton.SetActive(false);
                }
				break;
			case 3:					 
				playerDescriptionText.text = "WITHSTAND 4 HITS ";
				PlayerNameText.text = "Water Ninja";
				PlayerPriceDisplayText.text = "40000 ";
			
				if (PlayerPrefs.GetInt ("isPlayer3Purchased", 0) == 1) {
					buyButton.SetActive (false);
                    if (PlayerPrefs.GetInt("SelectedPlayer") == PlayerIndex)
                    {
                        selectButton.SetActive(false);
                        selectedButton.SetActive(true);
                    }
                    else
                    {
                        selectedButton.SetActive(false);
                        selectButton.SetActive(true);
                    }
                } else {
					buyButton.SetActive (true);
                    selectedButton.SetActive(false);
                    selectButton.SetActive(false);
                }
				break;
			case 4:					 
				PlayerNameText.text = "";
				PlayerPriceDisplayText.text = "Price  : 50000 ";
                nextButton.SetActive(true);
                if (PlayerPrefs.GetInt ("isPlayer4Purchased", 0) == 1) {
					buyButton.SetActive (false);
                    if (PlayerPrefs.GetInt("SelectedPlayer") == PlayerIndex)
                    {
                        selectButton.SetActive(false);
                        selectedButton.SetActive(true);
                    }
                    else
                    {
                        selectedButton.SetActive(false);
                        selectButton.SetActive(true);
                    }
                } else {
					buyButton.SetActive (true);
                    selectedButton.SetActive(false);
                    selectButton.SetActive(false);
                }
				break;
			case 5:						 
				PlayerNameText.text = "Name : FFF";
				PlayerPriceDisplayText.text = "PRice  : 7000 ";
                nextButton.SetActive(false);
				if (PlayerPrefs.GetInt ("isPlayer5Purchased", 0) == 1) {
					buyButton.SetActive (false);
                    if (PlayerPrefs.GetInt("SelectedPlayer") == PlayerIndex)
                    {
                        selectButton.SetActive(false);
                        selectedButton.SetActive(true);
                    }
                    else
                    {
                        selectedButton.SetActive(false);
                        selectButton.SetActive(true);
                    }
                } else {
					buyButton.SetActive (true);
                    selectedButton.SetActive(false);
                    selectButton.SetActive(false);
                }
                break;
			}	
	}

	void PurchasePlayer ()
	{
		switch (PlayerIndex) {
		case 1:		
			if (TotalCoins.Static.totalCoins >= 10000) {
					buyPopUP.PlayerCost = 1000;//to set the cost in buyPopUpScript
					buyPopUp.SetActive (true);
					//PlayerSelectionMenu.SetActive (false);
			} else {
					///	InAPPMenu.SetActive(true);
					//	gameObject.SetActive(false);
					UnsufficentCoinsForPlayerselection.SetActive (true);
					//PlayerSelectionMenu.SetActive (false);
			}
			break;
		case 2:
			if (TotalCoins.Static.totalCoins >= 30000) {
					buyPopUP.PlayerCost = 3000;
					buyPopUp.SetActive (true);
					//PlayerSelectionMenu.SetActive (false);
			} else {
					//	InAPPMenu.SetActive(true);
					//PlayerSelectionMenu.SetActive(false);
					UnsufficentCoinsForPlayerselection.SetActive (true);
					//PlayerSelectionMenu.SetActive (false);
			}		
			break;
		case 3:
			if (TotalCoins.Static.totalCoins >= 40000) {
					buyPopUP.PlayerCost = 4000;
					buyPopUp.SetActive (true);
					//PlayerSelectionMenu.SetActive (false);
			} else {
					//InAPPMenu.SetActive(true);
					//PlayerSelectionMenu.SetActive(false);
					UnsufficentCoinsForPlayerselection.SetActive (true);
					//PlayerSelectionMenu.SetActive (false);
			}		
			break;
		case 4:
			if (TotalCoins.Static.totalCoins >= 50000) {
					buyPopUP.PlayerCost = 5000;
					buyPopUp.SetActive (true);
					//PlayerSelectionMenu.SetActive (false);
			} else {
					//	InAPPMenu.SetActive(true);
					//PlayerSelectionMenu.SetActive(false);
					UnsufficentCoinsForPlayerselection.SetActive (true);
					//PlayerSelectionMenu.SetActive (false);
			}			
			break;
		case 5:
			if (TotalCoins.Static.totalCoins >= 6000) {
					buyPopUP.PlayerCost = 6000;
					buyPopUp.SetActive (true);
					//PlayerSelectionMenu.SetActive (false);
			} else {
					//	InAPPMenu.SetActive(true);
					//	PlayerSelectionMenu.SetActive(false);
					UnsufficentCoinsForPlayerselection.SetActive (true);
					//PlayerSelectionMenu.SetActive (false);
			}			
			break;
		}	
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StoreMenu : MonoBehaviour {

	// Use this for initialization

	public GameObject[] MagnetIndicators,multiplierIndicators,flyIndicators,jumpIndicators;
	public GameObject unSufficentCoinsParent,mainMenuParent,StoreMenuParent;
	public Text magnetCostText,multiplierCostText,MagnetFull,MultiplayerFull,flyCostText,jumpCostText,flyFullText,jumpFullText;
 	

	void Start () {

		for(int i=0;i<PlayerPrefs.GetInt("MagnetPower",0);i++){
			MagnetIndicators[i].SetActive(true);
		}

		for(int i=0;i<PlayerPrefs.GetInt("Multiplier",0);i++){
			multiplierIndicators[i].SetActive(true);
		}

		for(int i=0;i<PlayerPrefs.GetInt("Fly",0);i++){
			flyIndicators[i].SetActive(true);
		}

		for(int i=0;i<PlayerPrefs.GetInt("Jump",0);i++){
			jumpIndicators[i].SetActive(true);
		}


	}
	float lastTime;
	void Update () {

	if(Time.timeSinceLevelLoad-lastTime >1.0f){
		magnetCostText.text = ""+PlayerPrefs.GetInt ("MagnetCost",1000);
		multiplierCostText.text = ""+PlayerPrefs.GetInt ("MultiplierCost",2000);
		flyCostText.text = "" + PlayerPrefs.GetInt ("FlyCost", 3000);
		jumpCostText.text = "" + PlayerPrefs.GetInt ("JumpCost", 4000);
			lastTime = Time.timeSinceLevelLoad;
		}

		if (Input.GetKey (KeyCode.D)) {
			PlayerPrefs.DeleteAll();
				}

	}
 
          public	void OnButtonClick(string ButtonName){

		switch (ButtonName) {

		case "BuyMagnet":
			print ("Clicked on buy item1");
			IncraseMagnetPower();
			SoundController.Static.playSoundFromName("Click");
			break;
		case "BuyMultiplier":
			print ("Clicked on buy item2");
			IncraseMultipiler();
			SoundController.Static.playSoundFromName("Click");
			break;
		case "BuyFlyPower":
			print ("Clicked on buy item3");
			IncreaseFlyPower();
			SoundController.Static.playSoundFromName("Click");
			break;
		case "BuyJumpPower":
			print ("Clicked on buy item3");
			IncreaseJumpPower();
			SoundController.Static.playSoundFromName("Click");
			break;
		case "Back":
			StoreMenuParent.SetActive(false);
			mainMenuParent.SetActive(true);
			SoundController.Static.playSoundFromName("Click");
			break;

				}
		}
	
 

	void IncraseMagnetPower()
	{
		if (TotalCoins.Static.totalCoins >= PlayerPrefs.GetInt("MagnetCost",1000) && PlayerPrefs.GetInt ("MagnetPower", 0) <= 3) {

			TotalCoins.Static.SubtractCoins (PlayerPrefs.GetInt("MagnetCost",1000));
			PlayerPrefs.SetInt("MagnetCost",PlayerPrefs.GetInt("MagnetCost",1000)+1000);
			for (int i=0; i<MagnetIndicators.Length; i++) {
				Debug.Log("Magnet Power1 "+PlayerPrefs.GetInt ("MagnetPower", 0));
								if (i == PlayerPrefs.GetInt ("MagnetPower", 0)) {
								MagnetIndicators [i].SetActive(true);
								}
						}
		
			PlayerPrefs.SetInt("MagnetCount_Ingame",PlayerPrefs.GetInt("MagnetCount_Ingame",1)+2);
			PlayerPrefs.SetInt ("MagnetPower", PlayerPrefs.GetInt ("MagnetPower") + 1);
				} else {
						if( PlayerPrefs.GetInt ("MagnetPower", 0)>3){
				MagnetFull.text ="Full";
					}
				else
					{
							unSufficentCoinsParent.SetActive(true);
			             	StoreMenuParent.SetActive(false);
					}
			}

	}

	void IncraseMultipiler()
	{
		if (TotalCoins.Static.totalCoins >= PlayerPrefs.GetInt("MultiplierCost",2000) && PlayerPrefs.GetInt ("Multiplier", 0) <= 3) {
			TotalCoins.Static.SubtractCoins (PlayerPrefs.GetInt("MultiplierCost",2000));
			PlayerPrefs.SetInt("MultiplierCost",PlayerPrefs.GetInt("MultiplierCost",2000)+1000);
			for (int i=0; i<multiplierIndicators.Length; i++) {
				if (i == PlayerPrefs.GetInt ("Multiplier", 0)) {
					multiplierIndicators [i].SetActive(true);
				}
			}
			PlayerPrefs.SetInt("MultiplierCount_Ingame",PlayerPrefs.GetInt("MultiplierCount_Ingame",1)*2);
			PlayerPrefs.SetInt ("Multiplier", PlayerPrefs.GetInt ("Multiplier") + 1);
		} else {
			if( PlayerPrefs.GetInt ("Multiplier", 0)>3){
				MultiplayerFull.text ="Full";
			}
			else
			{
				StoreMenuParent.SetActive(false);
				unSufficentCoinsParent.SetActive(true);
		
			}
		}
		
	}


	void IncreaseFlyPower()
	{
		if (TotalCoins.Static.totalCoins >= PlayerPrefs.GetInt ("FlyCost", 3000) && PlayerPrefs.GetInt ("Fly", 0) <= 3) {
						TotalCoins.Static.SubtractCoins (PlayerPrefs.GetInt ("FlyCost", 3000));
						PlayerPrefs.SetInt ("FlyCost", PlayerPrefs.GetInt ("FlyCost", 3000) + 1000);
						for (int i=0; i<flyIndicators.Length; i++) {
								if (i == PlayerPrefs.GetInt ("Fly", 0)) {
										flyIndicators [i].SetActive (true);
								}
						}
						PlayerPrefs.SetInt ("FlyPower_Ingame", PlayerPrefs.GetInt ("FlyPower_Ingame", 0) + 2);
						PlayerPrefs.SetInt ("Fly", PlayerPrefs.GetInt ("Fly") + 1);
				} else {
			if( PlayerPrefs.GetInt ("Fly", 0)>3){
				flyFullText.text ="Full";
			}
			else
			{
				StoreMenuParent.SetActive(false);
				unSufficentCoinsParent.SetActive(true);
				
			}

				}

	}

	void IncreaseJumpPower()
	{
		if (TotalCoins.Static.totalCoins >= PlayerPrefs.GetInt ("JumpCost", 4000) && PlayerPrefs.GetInt ("Jump", 0) <= 3) {
			TotalCoins.Static.SubtractCoins (PlayerPrefs.GetInt ("JumpCost", 4000));
			PlayerPrefs.SetInt ("JumpCost", PlayerPrefs.GetInt ("JumpCost", 4000) + 1000);
			for (int i=0; i<jumpIndicators.Length; i++) {
				if (i == PlayerPrefs.GetInt ("Jump", 0)) {
					jumpIndicators [i].SetActive (true);
				}
			}
			PlayerPrefs.SetInt ("JumpPower_Ingame", PlayerPrefs.GetInt ("JumpPower_Ingame", 0) + 2);
			PlayerPrefs.SetInt ("Jump", PlayerPrefs.GetInt ("Jump") + 1);
		} else {
			if( PlayerPrefs.GetInt ("Jump", 0)>3){
				jumpFullText.text ="Full";
			}
			else
			{
				StoreMenuParent.SetActive(false);
				unSufficentCoinsParent.SetActive(true);
				
			}
			
		}
		
	}




}

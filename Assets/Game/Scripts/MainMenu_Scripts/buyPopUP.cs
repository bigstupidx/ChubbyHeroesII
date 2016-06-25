﻿using UnityEngine;

public class buyPopUP : MonoBehaviour {

	//public Text costText;
	public GameObject 
        PlayerSelectionMenuParent,
        buyPopUpMenuParent,
        SelectBtn,
        BuyBtn;
		

	public static int PlayerCost;
	
	void  Update ()
    {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			buyPopUpMenuParent.SetActive(false);
			//PlayerSelectionMenuParent.SetActive(true);
		}
	}


    public void OnButtonClick(string ButtonName)
	{
		switch(ButtonName)
			{
			case "YES":
				PlayerPrefs.SetInt("isPlayer" + Playerselection.PlayerIndex+"Purchased", 1); // to save the Player lock status
                PlayerPrefs.SetInt("SelectedPlayer", Playerselection.PlayerIndex);
			  	TotalCoins.Static.SubtractCoins(PlayerCost);
		     	//SelectBtn.SetActive(true);
			    //BuyBtn.SetActive(false);
                GetComponent<Playerselection>().ShowPlayerInfo();
                //PlayerSelectionMenuParent.SetActive(true);
				buyPopUpMenuParent.SetActive(false);
				SoundController.Static.playSoundFromName("Click");
			break;
			case "NO":
				SoundController.Static.playSoundFromName("Click");
				//PlayerSelectionMenuParent.SetActive(true);
				buyPopUpMenuParent.SetActive(false);
			break; 
			}			
		}		
	}
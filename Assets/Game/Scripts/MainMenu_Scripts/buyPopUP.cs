using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class buyPopUP : MonoBehaviour {


	//public Text costText;
	public GameObject PlayerSelectionMenuParent,buyPopUpMenuParent,SelectBtn,BuyBtn;
		

	public static int PlayerCost;
	void OnEnable()
	{
		//costText.text=" "+PlayerCost;
   

	}
	void Start () {
	
	}
	
	void  Update (){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			buyPopUpMenuParent.SetActive(false);
			PlayerSelectionMenuParent.SetActive(true);
		}
		}


public	void OnButtonClick(string ButtonName )
	{


		switch(ButtonName)
			{
			case "YES":
				PlayerPrefs.SetInt("isPlayer"+Playerselection.PlayerIndex+"Purchased",1); // to save the Player lock status
			  	TotalCoins.Static.SubtractCoins(PlayerCost);
		     	SelectBtn.SetActive(true);
			    BuyBtn.SetActive(false);
				PlayerSelectionMenuParent.SetActive(true);
				buyPopUpMenuParent.SetActive(false);
				SoundController.Static.playSoundFromName("Click");
			break;
			case "NO":
				SoundController.Static.playSoundFromName("Click");
				PlayerSelectionMenuParent.SetActive(true);
				buyPopUpMenuParent.SetActive(false);
			break; 
			}
			
		}
		
	}


using UnityEngine;
using System.Collections;

public class UnsufficentCoinsMenu : MonoBehaviour {

	public GameObject InAppMenuParent,UnsufficentMenu;
	void Start () {
	
	}
	void  Update (){

	}



	public void OnButtonClick(string ButtonName){
		switch (ButtonName){
		case "Ok":
			InAppMenuParent.SetActive(true);
			UnsufficentMenu.SetActive(false);
			SoundController.Static.playSoundFromName("Click");
			MainMenuScreens.currentScreen=MainMenuScreens.MenuScreens.InnAppmenu;
			break;
		}
	}
}

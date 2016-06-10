using UnityEngine;
using System.Collections;

public class ExitParent : MonoBehaviour {

	public GameObject exitParent,mainMenuParent,totalCoinsParent;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnButtonClick(string ButtonName){
		switch (ButtonName){
		case "Yes":
			SoundController.Static.playSoundFromName("Click");
		
			Application.Quit();
			break;
		case "No":
			SoundController.Static.playSoundFromName("Click");
			exitParent.SetActive(false);
			mainMenuParent.SetActive(true);
			totalCoinsParent.SetActive(true);
			MainMenuScreens.currentScreen=MainMenuScreens.MenuScreens.InnAppmenu;
			break;
		}
	}
}

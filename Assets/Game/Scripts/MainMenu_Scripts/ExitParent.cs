using UnityEngine;
using System.Collections;

public class ExitParent : MonoBehaviour {

	public GameObject 
        exitParent,
        mainMenuParent;

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
			    MainMenuScreens.currentScreen=MainMenuScreens.MenuScreens.InnAppmenu;
			    break;
		}
	}
}

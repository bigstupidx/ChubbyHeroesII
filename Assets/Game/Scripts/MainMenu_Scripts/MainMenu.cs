using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public static MainMenu Static;

	public GameObject 
        MainMenuParent,
        CreditsMenuParent,
        StoreMunuParent,
        PlayerSelectionmenuParent,
        missionsParent,
        exitParent,
        totalCoinsParent,
        exitButton,
        inGameUi;

	void Start ()
    {
		Time.timeScale = 1;
    }
	
	public void OnButtonClick(string ButtonName){
		switch(ButtonName){
		    case "Play": // start intro animation, enable ingameUI
                GameController.Static.currentGameState = GameController.GameState.gameplay;
                SoundController.Static.playSoundFromName("Click");
                Debug.Log("I am Main MENU");
			    MainMenuParent.SetActive(false);
                inGameUi.SetActive(true);
			    break;
		    case "More":
			    SoundController.Static.playSoundFromName("Click");
			    break;
		    case "PlayerSelect":
                MainMenuParent.SetActive(false);
                PlayerSelectionmenuParent.SetActive(true);           
			    SoundController.Static.playSoundFromName("Click");
			    break;
		    case "Credits":
			    SoundController.Static.playSoundFromName("Click");
			    MainMenuParent.SetActive(false);
			    CreditsMenuParent.SetActive(true);
			    MainMenuScreens.currentScreen=MainMenuScreens.MenuScreens.CredtsMenu;
			    break;
		    case "Exit":
			    SoundController.Static.playSoundFromName("Click");
			    exitParent.SetActive(true);
			    MainMenuParent.SetActive(false);
			    totalCoinsParent.SetActive(false);
			    //Application.Quit();
			    break;
		    case "Store":
			    PlayerSelectionmenuParent.SetActive(false);
			    StoreMunuParent.SetActive(true);
			    MainMenuParent.SetActive(false);
			    SoundController.Static.playSoundFromName("Click");
			    MainMenuScreens.currentScreen=MainMenuScreens.MenuScreens.StoreMenu;
			    break;
		    case "Missions":
			    missionsParent.SetActive(true);
			    MainMenuParent.SetActive(false);
			    SoundController.Static.playSoundFromName("Click");
			    MainMenuScreens.currentScreen=MainMenuScreens.MenuScreens.StoreMenu;
			    break;
		}
	}
}

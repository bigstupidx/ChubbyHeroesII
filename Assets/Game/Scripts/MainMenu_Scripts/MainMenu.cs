using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject MainMenuParent,CreditsMenuParent,StoreMunuParent,PlayerSelectionmenuParent,missionsParent,exitParent,totalCoinsParent,exitButton;
	// Use this for initialization

	void OnEnable()
	{
		#if UNITY_IOS
		
		ExitButton.SetActive (false);
		#endif
		}

	void Start () {
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//is,and,on
	public void OnButtunClick(string ButtonName){
		switch(ButtonName){
		case "Play":
			SoundController.Static.playSoundFromName("Click");
			MainMenuParent.SetActive(false);
			PlayerSelectionmenuParent.SetActive(true);
			MainMenuScreens.currentScreen=MainMenuScreens.MenuScreens.playerSelectionMenu;
			break;
		case "More":
			Application.OpenURL ("https://play.google.com/store/apps/developer?id=Ace+Games");
			#if UNITY_IPHONE
			Application.OpenURL("https://play.google.com/store/apps/developer?id=Ace+Games");
			# elif UNITY_ANDROID
			Application.OpenURL("https://play.google.com/store/apps/developer?id=Ace+Games");
			#elif UNITY_WP8
			Application.OpenURL("https://play.google.com/store/apps/developer?id=Ace+Games");
			#endif
			SoundController.Static.playSoundFromName("Click");
			break;
		case "Review":
			Application.OpenURL ("https://play.google.com/store/apps/developer?id=Ace+Games");
			#if UNITY_IPHONE
			Application.OpenURL("https://play.google.com/store/apps/developer?id=Ace+Games");
			# elif UNITY_ANDROID
			Application.OpenURL("https://play.google.com/store/apps/developer?id=Ace+Games");
			#elif UNITY_WP8
			Application.OpenURL("https://play.google.com/store/apps/developer?id=Ace+Games");
			#endif
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

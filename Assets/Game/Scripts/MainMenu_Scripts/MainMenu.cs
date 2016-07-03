using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public static MainMenu Static;

    public enum MenuScreens
    {
        mainmenu,
        playerSelectionMenu,
        ControlselectionMenu,
        CredtsMenu,
        ByPopupMenu,
        UnSufficentCoinsMenu,
        LevelSelectionMenu,
        StoreMenu,
        InnAppmenu,
        Missions,
        PauseMenu,
        SettingsMenu
    };
    public static MenuScreens currentScreen;

    public GameObject 
        MainMenuParent,
        CreditsMenuParent,
        UpgradesMenuParent,
        PlayerSelectionmenuParent,
        MissionsMenuParent,
        ExitParent,
        TotalCoinsParent,
        SettingsMenuParent,
        InGameUi,
        LoadingMenuParent,
        LevelSelectionMenuParent,
        ByPopupMenuParent,
        InSufficentCoinsMenuParent,
        InsufficentCoinsForPlayerselectionMenu,
        InnAppMenuParent;


    void Start ()
    {
		Time.timeScale = 1;
        currentScreen = MenuScreens.mainmenu;
        DeActive();
        if (MenuHelper._Instance.restartFromGameplay == 0)
            MainMenuParent.SetActive(true);
    }

    void DoPlayStart()
    {
        GameController.Static.currentGameState = GameController.GameState.gameplay;
        MainMenuParent.SetActive(false);
        InGameUi.SetActive(true);
        CameraFade.current.FadeIn(null, 1f, 0f);
    }
	
	public void OnButtonClick(string ButtonName){
		switch(ButtonName){
		    case "Play": // start intro animation, enable ingameUI
                CameraFade.current.FadeOut(CameraFade.current.FadeInTest, 0.2f, 0f);
                GameController.Static.OnGameStart();
                SoundController.Static.playSoundFromName("Click");
                MainMenuParent.SetActive(false);
                InGameUi.SetActive(true);
                break;
		    case "PlayerSelect":
                //CameraFade.current.FadeOut(CameraFade.current.FadeInTest, 0.3f, 0f);
                MainMenuParent.SetActive(false);
                PlayerSelectionmenuParent.SetActive(true);           
			    SoundController.Static.playSoundFromName("Click");
                currentScreen = MenuScreens.playerSelectionMenu;
                break;
		    case "Credits":
			    SoundController.Static.playSoundFromName("Click");
			    MainMenuParent.SetActive(false);
			    CreditsMenuParent.SetActive(true);
			    currentScreen=MenuScreens.CredtsMenu;
			    break;
		    case "Exit":
			    SoundController.Static.playSoundFromName("Click");
			    ExitParent.SetActive(true);
			    MainMenuParent.SetActive(false);
			    //TotalCoinsParent.SetActive(false);
			    Application.Quit();
			    break;
		    case "Store":
                //CameraFade.current.FadeOut(CameraFade.current.FadeInTest, 0.3f, 0f);

                PlayerSelectionmenuParent.SetActive(false);
                UpgradesMenuParent.SetActive(true);
			    MainMenuParent.SetActive(false);
			    SoundController.Static.playSoundFromName("Click");
			    currentScreen=MenuScreens.StoreMenu;
			    break;
		    case "Missions":
                //CameraFade.current.FadeOut(CameraFade.current.FadeInTest, 0.3f, 0f);

                MissionsMenuParent.SetActive(true);
			    MainMenuParent.SetActive(false);
			    SoundController.Static.playSoundFromName("Click");
			    currentScreen=MenuScreens.Missions;
			    break;
            case "Settings":
                //CameraFade.current.FadeOut(CameraFade.current.FadeInTest, 0.3f, 0f);

                SettingsMenuParent.SetActive(true);
                MainMenuParent.SetActive(false);
                SoundController.Static.playSoundFromName("Click");
                currentScreen = MenuScreens.SettingsMenu;
                break;
            case "PauseMenu":
                //CameraFade.current.FadeOut(CameraFade.current.FadeInTest, 0.3f, 0f);

                SettingsMenuParent.SetActive(true);
                MainMenuParent.SetActive(false);
                SoundController.Static.playSoundFromName("Click");
                currentScreen = MenuScreens.PauseMenu;
                break;
        }
	}

    void Update()
    {
        if (GameController.Static.currentGameState == GameController.GameState.gameplay)
            return;

        switch (currentScreen)
        {
            case MenuScreens.mainmenu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    //DeActive();
                    ExitParent.SetActive(true);
                }
                break;

            case MenuScreens.playerSelectionMenu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    DeActive();
                    MainMenuParent.SetActive(true);
                }
                break;

            case MenuScreens.ControlselectionMenu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    DeActive();
                    LevelSelectionMenuParent.SetActive(true);
                    currentScreen = MenuScreens.LevelSelectionMenu;
                    //TotalCoinsParent.SetActive(true);
                }
                break;

            case MenuScreens.CredtsMenu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    DeActive();
                    MainMenuParent.SetActive(true);
                }
                break;

            case MenuScreens.ByPopupMenu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    DeActive();
                    PlayerSelectionmenuParent.SetActive(true);
                }
                break;

            case MenuScreens.UnSufficentCoinsMenu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    DeActive();
                    MainMenuParent.SetActive(true);
                }
                break;

            case MenuScreens.LevelSelectionMenu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    DeActive();
                    PlayerSelectionmenuParent.SetActive(true);
                    currentScreen = MenuScreens.playerSelectionMenu;
                }
                break;

            case MenuScreens.StoreMenu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    DeActive();
                    MainMenuParent.SetActive(true);
                }
                break;

            case MenuScreens.InnAppmenu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    DeActive();
                    MainMenuParent.SetActive(true);
                }
                break;

            case MenuScreens.Missions:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    DeActive();
                    MainMenuParent.SetActive(true);
                }
                break;
        }
    }

    void DeActive()
    {
        MainMenuParent.SetActive(false);
        LoadingMenuParent.SetActive(false);
        PlayerSelectionmenuParent.SetActive(false);
        CreditsMenuParent.SetActive(false);
        ByPopupMenuParent.SetActive(false);
        InSufficentCoinsMenuParent.SetActive(false);
        LevelSelectionMenuParent.SetActive(false);
        UpgradesMenuParent.SetActive(false);
        InnAppMenuParent.SetActive(false);
        InsufficentCoinsForPlayerselectionMenu.SetActive(false);
        MissionsMenuParent.SetActive(false);
    }
}

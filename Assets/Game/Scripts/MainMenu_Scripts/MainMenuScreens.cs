using UnityEngine;
using System.Collections;

public class MainMenuScreens : MonoBehaviour
{

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
				Missions
	}
		;
		public GameObject MainMenuParent, LoadingMenuParent, PlayerSelectionParent, ControlselectionMenuParent, CredtsMenuParent, 
				ByPopupMenuParent, UnSufficentCoinsMenuParent, LevelSelectionMenuParent, StoreMenuParent, InnAppMenuParent, UnsufficentCoinsForPlayerselectionMenu, TotalCoinsParent,missionsParent;
		public static MenuScreens currentScreen ;

		void Start ()
		{
				currentScreen = MenuScreens.mainmenu;
				DeActive ();
				MainMenuParent.SetActive (true);
		}

		void Update ()
		{


				switch (currentScreen) {
				case MenuScreens.mainmenu:
						if (Input.GetKeyDown (KeyCode.Escape)) {
			
								Application.Quit ();
						}
						break;
				case MenuScreens.playerSelectionMenu:
						if (Input.GetKeyDown (KeyCode.Escape)) {
								DeActive ();
								MainMenuParent.SetActive (true);
						}
						break;
				case MenuScreens.ControlselectionMenu:
						if (Input.GetKeyDown (KeyCode.Escape)) {
								DeActive ();
								LevelSelectionMenuParent.SetActive (true);
								currentScreen = MenuScreens.LevelSelectionMenu;
								TotalCoinsParent.SetActive (true);
						}
						break;
				case MenuScreens.CredtsMenu:
						if (Input.GetKeyDown (KeyCode.Escape)) {
								DeActive ();
								MainMenuParent.SetActive (true);
						}
						break;
				case MenuScreens.ByPopupMenu:
						if (Input.GetKeyDown (KeyCode.Escape)) {
								DeActive ();
								PlayerSelectionParent.SetActive (true);
						}
						break;
				case MenuScreens.UnSufficentCoinsMenu:
						if (Input.GetKeyDown (KeyCode.Escape)) {
								DeActive ();
								MainMenuParent.SetActive (true);
						}
						break;
				case MenuScreens.LevelSelectionMenu:
						if (Input.GetKeyDown (KeyCode.Escape)) {
								DeActive ();
								PlayerSelectionParent.SetActive (true);
								currentScreen = MenuScreens.playerSelectionMenu;
						}
						break;
				case MenuScreens.StoreMenu:
						if (Input.GetKeyDown (KeyCode.Escape)) {
								DeActive ();
								MainMenuParent.SetActive (true);

						}
						break;
		case MenuScreens.InnAppmenu:
			if (Input.GetKeyDown (KeyCode.Escape)) {
				DeActive ();
				MainMenuParent.SetActive (true);
				
			}
			break;
		case MenuScreens.Missions:
			if (Input.GetKeyDown (KeyCode.Escape)) {
				DeActive ();
				MainMenuParent.SetActive (true);
				
			}
			break;

		
				}

		}

		void DeActive ()
		{
				MainMenuParent.SetActive (false);
				LoadingMenuParent.SetActive (false);
				PlayerSelectionParent.SetActive (false);
				ControlselectionMenuParent.SetActive (false);
				CredtsMenuParent.SetActive (false);
				ByPopupMenuParent.SetActive (false);
				UnSufficentCoinsMenuParent.SetActive (false);
				LevelSelectionMenuParent.SetActive (false);
				StoreMenuParent.SetActive (false);
				InnAppMenuParent.SetActive (false);
				UnsufficentCoinsForPlayerselectionMenu.SetActive (false);
				missionsParent.SetActive (false);
		}
}

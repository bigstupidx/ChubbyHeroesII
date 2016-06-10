using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TotalCoins : MonoBehaviour
{


		public GameObject MainMenuParent, LoadingMenuParent, PlayerSelectionParent, ControlselectionMenuParent, CredtsMenuParent, 
	ByPopupMenuParent, UnSufficentCoinsMenuParent, LevelSelectionMenuParent, StoreMenuParent, InnAppMenuParent,UnsufficentCoinsForPlayerselectionMenu;
		public static TotalCoins Static;
		public  Text TotalCoinstext;
		public int totalCoins;
		int coinsIn;

		void Start ()
		{
				UpdateCoins ();
				Static = this;
		}
	
		public  void UpdateCoins ()
		{
				totalCoins = PlayerPrefs.GetInt ("TotalCoins", 0)+PlayerPrefs.GetInt("AddMissionCoins",0);
				TotalCoinstext.text = "" + PlayerPrefs.GetInt ("TotalCoins", 0);
		}

		public void AddCoins (int AddingCoins)
		{
				PlayerPrefs.SetInt ("TotalCoins", PlayerPrefs.GetInt ("TotalCoins", 0) + AddingCoins);
				UpdateCoins ();
		}

		public void SubtractCoins (int SubtractingCoins)
		{
				PlayerPrefs.SetInt ("TotalCoins", PlayerPrefs.GetInt ("TotalCoins", 0) - SubtractingCoins);
				UpdateCoins ();
		}

		public int getCoinCount ()
		{
				return PlayerPrefs.GetInt ("TotalCoins", 0);
		}

		public void ClearCoins ()
		{
				PlayerPrefs.DeleteAll ();
		}

		public void OnButtonClic (string ButtonName)
		{
				switch (ButtonName) {
				case "BuyCoins":
						DeActive ();
			InnAppMenuParent.SetActive (true);
			MainMenuScreens.currentScreen=MainMenuScreens.MenuScreens.InnAppmenu;
			SoundController.Static.playSoundFromName("Click");
						break;
				}
		}

		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.P)) {
						AddCoins (10000);
				}
				if (Input.GetKeyDown (KeyCode.Q)) {
						PlayerPrefs.DeleteAll ();
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
		}

}

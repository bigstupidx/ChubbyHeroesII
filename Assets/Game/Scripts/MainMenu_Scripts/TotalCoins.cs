using UnityEngine;
using UnityEngine.UI;

public class TotalCoins : MonoBehaviour
{
	public GameObject 
    MainMenuParent, 
    LoadingMenuParent, 
    PlayerSelectionParent, 
    ControlselectionMenuParent, 
    CredtsMenuParent, 
	ByPopupMenuParent, 
    UnSufficentCoinsMenuParent, 
    LevelSelectionMenuParent, 
    StoreMenuParent, 
    InnAppMenuParent,
    InsufficentCoinsForPlayerselectionMenu;
	public static TotalCoins Static;
	public  Text TotalCoinstext, TotalCoinsEverywhereText;
	public int totalCoins;
	int coinsIn;


    //TEMP HEATS
    public void CheatCoins()
    {
        AddCoins(10000);
        UpdateCoins();
    }

    public void CheatClearCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", 0);
        PlayerPrefs.DeleteAll();
        UpdateCoins();
    }

    public void CheatAll()
    {
        PlayerPrefs.DeleteAll();
        UpdateCoins();
    }
    // -------------------------------

    void Start ()
	{
        //PlayerPrefs.SetInt("TotalCoins", 150000);
        UpdateCoins ();
		Static = this;
	}
	
	public void UpdateCoins ()
	{
		totalCoins = PlayerPrefs.GetInt ("TotalCoins", 0) + PlayerPrefs.GetInt("AddMissionCoins",0);
        UpdateCointTexts();
    }

    public void UpdateCointTexts()
    {
        TotalCoinstext.text = "" + PlayerPrefs.GetInt("TotalCoins", 0);
        TotalCoinsEverywhereText.text = "" + PlayerPrefs.GetInt("TotalCoins", 0);
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



	public void OnButtonClic (string ButtonName)
	{
		switch (ButtonName)
        {
			case "BuyCoins":
				DeActive ();
		        InnAppMenuParent.SetActive (true);
		        MainMenu.currentScreen=MainMenu.MenuScreens.InnAppmenu;
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
		InsufficentCoinsForPlayerselectionMenu.SetActive (false);
	}
}

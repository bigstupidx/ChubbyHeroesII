using UnityEngine;
using System.Collections;

public class UnsufficentCoinsForPlayerselection : MonoBehaviour
{

    public GameObject 
        InsufficentCoinsForPlayerselectionMenu,
        InAppMenuParent;

	
    public void OnButtonClick(string ButtonName)
    {
	    switch (ButtonName)
        {
	        case "ok":
		        SoundController.Static.playSoundFromName("Click");
		        InsufficentCoinsForPlayerselectionMenu.SetActive(false);
		        InAppMenuParent.SetActive(true);
		        MainMenuScreens.currentScreen=MainMenuScreens.MenuScreens.InnAppmenu;
		        break;
	    }
    }
}

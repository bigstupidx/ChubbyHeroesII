using UnityEngine;
using System.Collections;

public class UnsufficentCoinsMenu : MonoBehaviour {

	public GameObject 
        InAppMenuParent,
        InsufficentMenu;

	public void OnButtonClick(string ButtonName)
    {
		switch (ButtonName)
        {
		    case "Ok":
			    //InAppMenuParent.SetActive(true);
			    InsufficentMenu.SetActive(false);
			    SoundController.Static.playSoundFromName("Click");
			    //MainMenu.currentScreen=MainMenu.MenuScreens.InnAppmenu;
			    break;
		}
	}
}

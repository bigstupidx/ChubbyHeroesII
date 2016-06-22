using UnityEngine;
using System.Collections;

public class CreditsMenu : MonoBehaviour {

	public GameObject 
        CreditsMenuParent,
        MainMenuParent;
	
	public void OnButtonClick(string ButtonName)
    {
		switch (ButtonName)
        {
		    case "Back":
			    CreditsMenuParent.SetActive(false);
			    MainMenuParent.SetActive(true);
			    SoundController.Static.playSoundFromName("Click");
			    break;
		}
	}
}

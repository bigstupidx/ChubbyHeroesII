using UnityEngine;
using System.Collections;

public class CreditsMenu : MonoBehaviour {

	public GameObject CreditsMenuParent,MainMenuParent;
	void Start () {
	
	}


	void  Update (){

	}

	
	public void OnButtonClick(string ButtonName){
		switch (ButtonName){
		case "Back":
			CreditsMenuParent.SetActive(false);
			MainMenuParent.SetActive(true);
			SoundController.Static.playSoundFromName("Click");
			break;

		}

	}
}

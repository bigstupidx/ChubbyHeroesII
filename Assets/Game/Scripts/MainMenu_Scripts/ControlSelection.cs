using UnityEngine;
using System.Collections;

public class ControlSelection : MonoBehaviour {

	public GameObject LoadingMenuParent,LevelSelectionParent,ControlSelectionParent,TotalCoinsParent;
	void Start () {
	
	}

	void  Update (){
//		if (Input.GetKeyDown (KeyCode.Escape)) {
//			ControlSelectionParent.SetActive(false);
//			LevelSelectionParent.SetActive(true);
//		}
	}


	
	public void OnButtonClic(string ButtonName){
		switch(ButtonName){
		case "AccelarationMode":
			SoundController.Static.playSoundFromName("Click");
			LoadingMenuParent.SetActive(true);
			ControlSelectionParent.SetActive(false);
			TotalCoinsParent.SetActive(false);
			break;

		case "ButtonMode":
			SoundController.Static.playSoundFromName("Click");
			LoadingMenuParent.SetActive(true);
			ControlSelectionParent.SetActive(false);
			TotalCoinsParent.SetActive(false);
			break;

		case "Back":
			SoundController.Static.playSoundFromName("Click");
			ControlSelectionParent.SetActive(false);
			LevelSelectionParent.SetActive(true);
			break;
		}

	}
}

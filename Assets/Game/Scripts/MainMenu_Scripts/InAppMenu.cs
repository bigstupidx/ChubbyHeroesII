using UnityEngine;
using System.Collections;
using System;

public class InAppMenu : MonoBehaviour {


	public static event EventHandler gamerPackEvent,extremPackEvent,staterPackEvent;
	public GameObject InAppMenuParent,MainMenuParent;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnButtonClicked(string ButtonName){
		switch (ButtonName){
		case "Buy1":
			if(staterPackEvent!=null)
			{
				staterPackEvent(null,null);

			}
			Debug.Log("Statern Pack button clicked");
		
			SoundController.Static.playSoundFromName("Click");
			break;
		case "Buy2":
			if(extremPackEvent!=null)
			{
				extremPackEvent(null,null);

			}
			Debug.Log("extrem Pack button clicked");
			SoundController.Static.playSoundFromName("Click");
			break;
		case "Buy3":
			if(gamerPackEvent!=null)
			{
				gamerPackEvent(null,null);

			}
			Debug.Log("Game Pack button clicked");
			SoundController.Static.playSoundFromName("Click");
			break;
		case "Back":
			InAppMenuParent.SetActive(false);
			MainMenuParent.SetActive(true);
			SoundController.Static.playSoundFromName("Click");
			break;

		}

	}
}

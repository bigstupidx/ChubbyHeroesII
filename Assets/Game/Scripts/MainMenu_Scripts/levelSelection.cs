﻿using UnityEngine;
using System.Collections;

public class levelSelection : MonoBehaviour
{	
	public static string levelName;
	public GameObject 
        ControlSelection,
        PlayerSelectionMenu,
        LevelSelectionParent;

	public void OnButtonClick(string ButtonName)
    {
		switch (ButtonName)
        {
		case "BACK":
			LevelSelectionParent.SetActive(false);
			PlayerSelectionMenu.SetActive(true);
			SoundController.Static.playSoundFromName("Click");
			break;
		case "Level1":
			levelName = "GamePlay";
			ControlSelection.SetActive(true);
			LevelSelectionParent.SetActive(false);
			MainMenu.currentScreen=MainMenu.MenuScreens.ControlselectionMenu;
			SoundController.Static.playSoundFromName("Click");
            break;
		case "Level2":
			levelName = "cityGameplay";
			ControlSelection.SetActive(true);
			LevelSelectionParent.SetActive(false);
			MainMenu.currentScreen=MainMenu.MenuScreens.ControlselectionMenu;
			SoundController.Static.playSoundFromName("Click");
            break;
		case "Level3":
			SoundController.Static.playSoundFromName("Click");
			levelName = "NightGameplay";
			ControlSelection.SetActive(true);
			LevelSelectionParent.SetActive(false);
			MainMenu.currentScreen=MainMenu.MenuScreens.ControlselectionMenu;
			break;
		}
	}
}

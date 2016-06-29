using UnityEngine;
using System.Collections;

public class SettingsMenu : MonoBehaviour
{

    public GameObject
        mainMenuParent,
        settingsMenuParent;

    public void OnButtonClick(string ButtonName)
    {
        switch (ButtonName)
        {
            case "Back":
                settingsMenuParent.SetActive(false);
                mainMenuParent.SetActive(true);
                SoundController.Static.playSoundFromName("Click");
                break;
        }
    }
}

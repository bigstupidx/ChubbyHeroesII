using UnityEngine;

public class buyPopUP : MonoBehaviour {

	//public Text costText;
	public GameObject 
        PlayerSelectionMenuParent,
        buyPopUpMenuParent,
        SelectBtn,
        BuyBtn;
		

	public static int PlayerCost;
	
	void  Update ()
    {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			buyPopUpMenuParent.SetActive(false);
			//PlayerSelectionMenuParent.SetActive(true);
		}
	}


    public void OnButtonClick(string ButtonName)
	{
		switch(ButtonName)
			{
			case "YES":
                Invoke("SelectedYes", 0.2f);
			break;
			case "NO":
                Invoke("SelectedNo", 0.2f);
			break; 
			}			
		}	
    
    void SelectedYes()
    {
        PlayerPrefs.SetInt("isPlayer" + Playerselection.PlayerIndex + "Purchased", 1); // to save the Player lock status
        PlayerPrefs.SetInt("SelectedPlayer", Playerselection.PlayerIndex);
        TotalCoins.Static.SubtractCoins(PlayerCost);
        GetComponent<Playerselection>().ShowPlayerInfo();
        buyPopUpMenuParent.SetActive(false);
        SoundController.Static.playSoundFromName("Click");
    }

    void SelectedNo()
    {
        SoundController.Static.playSoundFromName("Click");
        buyPopUpMenuParent.SetActive(false);
    }

}
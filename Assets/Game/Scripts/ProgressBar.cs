using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
	

	public Image magnetProgressBar,multiplierProgressBar,flyModeProgressBar,jumpModeProgressbar;
	PlayerController playerScript; 

	public int MagnetValue,MultiplerValue,JetpackValue,DoubleJumpValue;
	void Start () {
	
		playerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();

		MagnetValue = PlayerPrefs.GetInt ("MagnetCount_Ingame", 1);
		MultiplerValue = PlayerPrefs.GetInt ("MultiplierCount_Ingame", 1);
		JetpackValue = PlayerPrefs.GetInt ("FlyPower_Ingame", 1);
		DoubleJumpValue = PlayerPrefs.GetInt ("JumpPower_Ingame", 1);
	}

	void Update () {
		if (playerScript.isMagnetIndicator) {
			magnetProgressBar.fillAmount -= (float) 1/( MagnetValue * 500);
			if(magnetProgressBar.fillAmount==0)
			{
				playerScript.switchOffMagnet();
			}
				}

		else if (playerScript.isMultiplierIndicator) {
			multiplierProgressBar.fillAmount -=(float) 1/( MultiplerValue *500);
			if(multiplierProgressBar.fillAmount==0)
			{
				playerScript.switchOffMultiplier ();
			}
		}

		else if(playerScript.isFlyModeIndicator)
		{
			flyModeProgressBar.fillAmount -=(float) 1/(JetpackValue  *500 );
			if(flyModeProgressBar.fillAmount==0)
			{
				playerScript.JetPackPowerReset();
			}
			
		}
		else if(playerScript.isJumpModeIndicator)
		{
			jumpModeProgressbar.fillAmount -= (float) 1/( DoubleJumpValue   *500);
			if(jumpModeProgressbar.fillAmount==0){
				playerScript.PowerJumpReset();
			}
			
		}

	
	}
}

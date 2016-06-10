using UnityEngine;
using System.Collections;

public class RandomPowerUpSelect : MonoBehaviour
{

		// Use this for initialization
		public static bool canGivePowerUps = false;
		public GameObject[] powerUps;
		static float lasTime ;
		static int ObjectCount = 0;
		void Start ()
		{
				//we need to give power up based on random ,and how many time he broken things in past,if its value is less,we need to show atleast this feature exists.
				if (Time.timeSinceLevelLoad - lasTime > 60 || ObjectCount == 0) {
						int randomValue = Random.Range (0, powerUps.Length);
						powerUps [randomValue].SetActive (true);
						powerUps [randomValue].transform.parent = null;

						canGivePowerUps = false;
						PlayerPrefs.SetInt ("brokenCount", PlayerPrefs.GetInt ("brokenCount", 0) + 1);
						lasTime = Time.timeSinceLevelLoad;
						ObjectCount++;
				}
	
		}
	
	 
}

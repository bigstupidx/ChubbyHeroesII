using UnityEngine;
using System.Collections;

public class CoinRandomActivation : MonoBehaviour
{

		public GameObject[] coins;
		void Start ()
		{

				for (int i=0; i<= coins.Length-1; i++) {
						coins [i].SetActive (true);
				}

				#if UNITY_IPHONE
		int randomCoins0 = Random.Range (0, coins.Length);
		coins [randomCoins0].SetActive (true);
				# elif UNITY_ANDROID
				int randomCoins1 = Random.Range (0, coins.Length);
				coins [randomCoins1].SetActive (true);
				#elif UNITY_WP8
		int randomCoins2 = Random.Range (0, coins.Length);
		coins [randomCoins2].SetActive (true);
				#endif
	
		}
	
		
}

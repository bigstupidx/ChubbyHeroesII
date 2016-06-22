using UnityEngine;

public class CoinRandomActivation : MonoBehaviour
{
	public GameObject[] coins;
	void Start ()
	{
		for (int i=0; i<= coins.Length-1; i++)
        {
			coins [i].SetActive (true);
		}

	int randomCoins1 = Random.Range (0, coins.Length);
	coins [randomCoins1].SetActive (true);	
	}	
}

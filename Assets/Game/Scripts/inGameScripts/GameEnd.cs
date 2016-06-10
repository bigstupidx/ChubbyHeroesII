using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class GameEnd : MonoBehaviour {

	public float startCoinsCount ,TargetCoisCount, startScoreCount,startBestScoreCount ,TargetDistanceCount,TargetBestScoreCount;
	private float toreachCoins, toreachDistance,toreachBestScore ;
	public float  valueForDistance,ValueForBestScore,valueForCoins ;
	public Text finalScore,bestScore,finalCoins;
	public GameObject newBestScore_Image,buttonGroup;
	public static event EventHandler showAds,silentScoreUpload;

	public enum endMenuStates{
		coinCount,DistanceCount,BestScoreCount ,showButtons,none

	}

	public endMenuStates currentState ;

	void Start () {
		TotalCoins ();
		FinalScoreCount ();
	    BestScore ();
		//SoundController.Static.coinsCount.enabled = true;
		buttonGroup.SetActive (false);
		currentState = endMenuStates.DistanceCount;

		finalCoins.text = "0" ;
		finalScore.text = "0";
		GameObject[] Obj = GameObject.FindGameObjectsWithTag ("Destroy");
		for (int i=0; i<=Obj.Length-1; i++) {
			Destroy (Obj [i], 1.0f);
		}
	}
	
	public float diff;
	void Update () {

		diff = toreachCoins - valueForCoins;
		switch (currentState) {


		case endMenuStates.coinCount:
			valueForCoins = Mathf.Lerp (valueForCoins,toreachCoins,0.2f);
			finalCoins.text = "" +Mathf.RoundToInt (valueForCoins);

			if ( toreachCoins - valueForCoins < 1) {
				finalCoins.text = "" +Mathf.RoundToInt (toreachCoins);
				currentState = endMenuStates.BestScoreCount;
			} 
			break;

		case endMenuStates.DistanceCount:
			valueForDistance = Mathf.Lerp (valueForDistance, toreachDistance, 0.2f);
			finalScore.text = "" + Mathf.RoundToInt (valueForDistance);
			if (  toreachDistance - valueForDistance < 1 )
			{
				currentState = endMenuStates.coinCount;
			}

			break;

		case endMenuStates.BestScoreCount:

			ValueForBestScore=toreachBestScore;
			bestScore.text= ""+Mathf.RoundToInt(ValueForBestScore);
            currentState = endMenuStates.showButtons;
			 
			break;

		case endMenuStates.showButtons:

			showButtons();
			currentState = endMenuStates.none;
			break;

				}

	}

 
	void FinalScoreCount()
	{
	
		TargetDistanceCount = Ace_IngameUiControl.Static.inGameDistance;
		toreachDistance = TargetDistanceCount;
	 
	}
	void ChangeScoreCount(int newScoreCount)
	{
		finalScore.text = "" + newScoreCount;
		 
	}

	void TotalCoins()
	{
		
		TargetCoisCount = Ace_IngameUiControl.Static.inGameCoinCount;

		toreachCoins = TargetCoisCount;
		//too store in playerprefs
		PlayerPrefs.SetInt ("TotalCoins", PlayerPrefs.GetInt ("TotalCoins", 0) + (int) TargetCoisCount);
	}


	void BestScore()
	{
		toreachBestScore =Mathf.RoundToInt(PlayerPrefs.GetFloat ("BestDistance", 0));
		if (PlayerPrefs.GetFloat ("BestDistance", 0) < Ace_IngameUiControl.Static.inGameDistance) {
				newBestScore_Image.SetActive(true);

			TargetBestScoreCount=Ace_IngameUiControl.Static.inGameDistance;
			toreachBestScore=TargetBestScoreCount;
				
			PlayerPrefs.SetFloat("BestDistance",Ace_IngameUiControl.Static.inGameDistance );
			}
		bestScore.text= ""+Mathf.RoundToInt(PlayerPrefs.GetFloat ("BestDistance", 0));
	 
		if (silentScoreUpload != null)
						silentScoreUpload (null, null);
	}
 
	 

	void showButtons()
	{
		SoundController.Static.coinsCount.enabled = false;
		buttonGroup.SetActive (true);
		Invoke("showadd",1.4f);
	}
	void showadd()
	{
		if(showAds!=null)
		{
			showAds(null,null);
		}
	}
		
 


}

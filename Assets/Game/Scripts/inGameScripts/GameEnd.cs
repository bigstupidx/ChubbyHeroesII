using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class GameEnd : MonoBehaviour {

	public float 
        TargetCoinsCount, 
        TargetDistanceCount,
        TargetScoreCount,
        TargetEnemiesCount,

        valueForDistance,
        ValueForScore,
        valueForCoins,
        valueForEnemies;

	private float 
        toreachCoins, 
        toreachDistance,
        toReachEnemies,
        toreachScore;

	public Text 
        finalDistance,
        finalEnemiesDefeated,
        finalScore,
        finalCoins,

        universalMultiplier;

    public GameObject newHighscore, buttonGroup;
	public static event EventHandler showAds, silentScoreUpload;

	public enum endMenuStates
    {
		coinCount,
        DistanceCount,
        EnemiesCount,
        universalMulti,
        ScoreCount,
        showButtons,
        none
	}

	public endMenuStates currentState ;

	void Start ()
    {
        newHighscore.SetActive(false);

        FinalCoins ();
		FinalDistanceCount ();
        FinalEnemiesCount();
	    FinalScore ();
        
		buttonGroup.SetActive (false);
		currentState = endMenuStates.coinCount;

		finalCoins.text = "0";
		finalDistance.text = "0";
        finalEnemiesDefeated.text = "0";
        finalScore.text = "0";

		GameObject[] Obj = GameObject.FindGameObjectsWithTag ("Destroy");
		for (int i=0; i<=Obj.Length-1; i++) {
			Destroy (Obj [i], 1.0f);
		}
	}
	
	void Update ()
    {
		switch (currentState)
        {
		    case endMenuStates.coinCount:
			    valueForCoins = Mathf.Lerp (valueForCoins, toreachCoins, 0.05f);
			    finalCoins.text = "" + Mathf.RoundToInt (valueForCoins);

			    if (toreachCoins - valueForCoins < 1)
                {
				    finalCoins.text = "" + Mathf.RoundToInt (toreachCoins);
				    currentState = endMenuStates.DistanceCount;
			    } 
			    break;

            case endMenuStates.DistanceCount:
                
                valueForDistance = Mathf.Lerp(valueForDistance, toreachDistance, 0.05f);
                finalDistance.text = "" + Mathf.RoundToInt(valueForDistance);

                if (toreachDistance - valueForDistance < 1)
                {
                    currentState = endMenuStates.EnemiesCount;
                }
                break;

            case endMenuStates.EnemiesCount:
			    valueForEnemies = Mathf.Lerp (valueForEnemies, toReachEnemies, 0.05f);
			    finalEnemiesDefeated.text = "" + Mathf.RoundToInt (valueForEnemies);

			    if (toReachEnemies - valueForEnemies < 1)
			    {
				    currentState = endMenuStates.universalMulti;
			    }
			    break;

            case endMenuStates.universalMulti:
                universalMultiplier.text = PlayerPrefs.GetInt("UniversalMultiplier", 1).ToString();
                currentState = endMenuStates.ScoreCount;
                break;

            case endMenuStates.ScoreCount:
                ValueForScore = Mathf.Lerp(ValueForScore, toreachScore, 0.05f);
                finalScore.text = "" + Mathf.RoundToInt(ValueForScore);

                if (toreachScore - ValueForScore < 1)
                {
                    finalScore.text = "" + Mathf.RoundToInt(ValueForScore);
                    currentState = endMenuStates.showButtons;
                }
			    break;

		    case endMenuStates.showButtons:
                // wiggle facebook button
                CheckNewHighScore();
                showButtons();
			    currentState = endMenuStates.none;
			    break;
			    }
	}

    void FinalCoins()
    {
        TargetCoinsCount = GameUIController.Static.inGameCoinCount;
        toreachCoins = TargetCoinsCount;
        PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins", 0) + (int)TargetCoinsCount);
    }

    void FinalDistanceCount()
	{
        Debug.Log("valueForDistance is " + valueForDistance);
        TargetDistanceCount = GameController.Static.runDistance;
		toreachDistance = TargetDistanceCount; 
	}

    void FinalEnemiesCount()
    {
        TargetEnemiesCount = GameUIController.Static.inGameEnemiesKilled;
        toReachEnemies = TargetEnemiesCount;
    }

    // calculate score
    void FinalScore()
	{
        TargetScoreCount = (((int)TargetCoinsCount* 10 + (int)TargetDistanceCount * 60 + (int)TargetEnemiesCount * 30) /10 )* PlayerPrefs.GetInt("UniversalMultiplier", 1); // add enemies
        toreachScore = TargetScoreCount;
        

		if (silentScoreUpload != null)
			silentScoreUpload (null, null);
	}

    void CheckNewHighScore()
    {
        if((int)TargetScoreCount > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", (int)TargetScoreCount);
            newHighscore.SetActive(true);
        }

    }




    void showButtons()
	{
		SoundController.Static.coinsCount.enabled = false;
		buttonGroup.SetActive (true);
		//Invoke("showadd",1.4f);
	}
}

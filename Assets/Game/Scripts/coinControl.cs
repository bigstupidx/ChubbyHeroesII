using UnityEngine;
using System.Collections;
using System;

public class coinControl : MonoBehaviour
{
	
	// Use this for initialization
	float coinSpeed = 1.0f;
	public Transform coinTrans ;
	public static int  turnCount;
	public BoxCollider box ;
	Vector3 originalScale;
	public static bool isONMagetPower ;
	public bool moveToPlayer = false ;
	public bool moveToCoinTarget = false;
	Transform thisTrans;
	
	void OnEnable ()
	{
		thisTrans = transform;
		box = GetComponent<BoxCollider> () as BoxCollider;
		
		box.size = box.size + new Vector3 (0, 3.3f, 0);
		
		originalScale = box.size;
		PlayerController.switchOnMagnetPower += onMagenetPower;
		PlayerController.switchOFFMagnetPower += offMagenetPower;
		PlayerController.gameEnded += onGameEnd;
		coinTrans = thisTrans.GetChild (0);
		//thisTrans.parent = null;
		
	}
	
	void OnDisable ()
	{
		PlayerController.switchOnMagnetPower -= onMagenetPower;
		PlayerController.switchOFFMagnetPower -= offMagenetPower;
		PlayerController.gameEnded -= onGameEnd;
		//Destroy(gameObject);
	}
	
	void onGameEnd (System.Object obj, EventArgs args)
	{
		Destroy (gameObject);
	}
	
	void onMagenetPower (System.Object obj, EventArgs args)
	{
		isONMagetPower = true;
		
		if (box != null)
			box.size = new Vector3 (7, 7, 7);
	}
	
	public void resetSize ()
	{
		if (box != null)
			box.size = originalScale;
	}
	
	void offMagenetPower (System.Object obj, EventArgs args)
	{
		isONMagetPower = false;
		resetSize ();
	}
	
	void Start ()
	{
		if (coinTrans != null)
			coinTrans.Rotate (0, turnCount, 0);
		turnCount += 4;
		if (isONMagetPower)
			onMagenetPower (null, null);
	}
	
	
	Vector3 newCoinPostionTarget;
	bool isAwaredNitrous = false;
	bool punchCoinUIImage = true;
	
	void FixedUpdate ()
	{
		
		if (coinTrans != null)
			coinTrans.Rotate (0, 2, 0);
		
		if (moveToPlayer) {
			
			thisTrans.position = Vector3.MoveTowards (thisTrans.position, PlayerController.thisPosition + new Vector3 (0, 0, 6), 80.0f * Time.deltaTime);
			if (Vector3.Distance (thisTrans.position, PlayerController.thisPosition) < 4) {
				moveToPlayer = false;
				moveToCoinTarget = true;
				Destroy (gameObject, 1f); 
				
			}
			
		} else if (moveToCoinTarget) {
			
			newCoinPostionTarget = GameController.Static.mainCameraTrans.position + new Vector3 (-320, 500, 860);
			thisTrans.position = Vector3.MoveTowards (thisTrans.position, newCoinPostionTarget, 80 * Time.deltaTime);
			
			if (punchCoinUIImage) {
				Ace_IngameUiControl.Static.InGameAnimator.SetTrigger ("coinPunch");
				punchCoinUIImage = false;
				
			}
			
		}
		
			
		
		
	}
	
	public void MoveToPlayer ()
	{
		moveToPlayer = true;
		box.enabled = false;
		Invoke ("MoveCoinAway", 0.05f);
		Destroy (box);
		
	}
	
	void MoveCoinAway ()
	{
		moveToPlayer = false;
		moveToCoinTarget = true;
	}
	void OnBecameInvisible ()
	{ 
		Destroy (gameObject, 1f); 
	}
}

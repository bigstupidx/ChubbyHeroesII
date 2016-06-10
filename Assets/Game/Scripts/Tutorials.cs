using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorials : MonoBehaviour
{




		public Text swipeInfo;
		public GameObject tutorialParent, hud;

		//for accelerating Controles......... 

		public GameObject swipeUp_Obj, swipeDown_Obj, swiperight_Obj, swipeLeft_Obj;
		bool swipeRight = false, swipeLeft = false, swipeUp = false, swipeDown = false, tiltDevice = false, doubleTap = false; 
		bool showTuto = false;
		void OnEnable ()
		{

				// Tutorials life time 2
				// Tutorials life time 2
				if (PlayerPrefs.GetInt ("tutorials", 0) <= 1) {
						showTuto = true;
						hud.SetActive (false);
						swipeLeft_Obj.SetActive (true);
						swipeInfo.text = "Swipe to Move";
						swipeLeft = true;
						PlayerPrefs.SetInt ("tutorials", PlayerPrefs.GetInt ("tutorials", 0) + 1);
				} else {
						 
						//print ("Destroyed Game object");
						ActivateCountDown ();
						swipeInfo.text = "";		
				}
				
		}
		 

		void Update ()
		{


				if (!showTuto)
						return;
				// swipe Left

				if ((Input.GetKeyDown (KeyCode.LeftArrow) || InputController.Static.swipeLeft) && swipeLeft) {//|| InputController.Static.isDown) && swipeDown
						swipeLeft_Obj.SetActive (false);
						swiperight_Obj.SetActive (true);
						InputController.Static.swipeLeft = false;
						swipeInfo.text = "Swipe to Move";
						swipeLeft = false;
						swipeRight = true;				
				}

						// swipe Right

		else if ((Input.GetKeyDown (KeyCode.RightArrow) || InputController.Static.swipeRight) && swipeRight) {//|| InputController.Static.isDown) && swipeDown
						swipeUp_Obj.SetActive (true);	
						swiperight_Obj.SetActive (false);
						InputController.Static.swipeRight = false;
						swipeInfo.text = "Swipe up to Jump";
						swipeRight = false;
						swipeUp = true;
				}

						// swipe Up

		else if ((Input.GetKeyDown (KeyCode.UpArrow) || InputController.Static.swipeUp) && swipeUp) {// || InputController.Static.isJump) && swipeUp
						swipeDown_Obj.SetActive (true);	
						swipeUp_Obj.SetActive (false);
						InputController.Static.swipeUp = false;
						swipeInfo.text = "Swipe Down to Roll";
						swipeUp = false;
						swipeDown = true;
						ActivateCountDown ();
				}
						
				//swipe Down
		else 	if ((Input.GetKeyDown (KeyCode.DownArrow) || InputController.Static.swipeDown) && swipeDown) {//|| InputController.Static.isDown) && swipeDown
						swipeDown_Obj.SetActive (false);	
						InputController.Static.swipeDown = false;
						swipeInfo.text = "";
						swipeDown = false;
						doubleTap = true;
						ActivateCountDown ();
				} 
//		else 	if (InputController.Static.stopTutorial && doubleTap) {//|| InputController.Static.isDown) && swipeDown
//						swipeInfo.text = "";
//						doubleTap = false;
//						ActivateCountDown ();
//				}


		}
	

		void ActivateCountDown ()
		{

				hud.SetActive (true);
				//Ace_IngameUiControl.Static.ShowHighestIndicatorAnim();
				tutorialParent.SetActive (false);
				GameController.Static.ON_GAME_Start ();
				this.enabled = false;
		}

}

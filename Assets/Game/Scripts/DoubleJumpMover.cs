using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DoubleJumpMover : MonoBehaviour {
	
	public Animator DoubleJumpTriggerAnim;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.L)){
			
		}
		
	}
	float jumpHeight;
	void OnTriggerEnter(Collider incoming)
	{
		if (incoming.GetComponent<Collider>().tag.Contains ("Player") || incoming.GetComponent<Collider>().tag.Contains ("coinTrigger")) {
			DoubleJumpTriggerAnim.SetTrigger("DoubleJumpTrigger");
			PlayerController.doubleJump = true ;
			InputController.Static.isJump = true;
			SoundController.Static.playSoundFromName ("jumpTrigger");
			GetComponent<Collider>().enabled = false;
			
		}
	}
}
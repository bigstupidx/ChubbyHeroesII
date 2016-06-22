using UnityEngine;

public class DoubleJumpMover : MonoBehaviour {
	
	public Animator DoubleJumpTriggerAnim;

	float jumpHeight;

	void OnTriggerEnter(Collider incoming)
	{
		if (incoming.GetComponent<Collider>().tag.Contains ("Player") || incoming.GetComponent<Collider>().tag.Contains ("coinTrigger")) {
			//DoubleJumpTriggerAnim.SetTrigger("DoubleJumpTrigger");
			PlayerController.doubleJump = true ;
			InputController.Static.isJump = true;
			SoundController.Static.playSoundFromName ("jumpTrigger");
			GetComponent<Collider>().enabled = false;	
		}
	}
}
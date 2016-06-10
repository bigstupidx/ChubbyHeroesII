using UnityEngine;
using System.Collections;
using System;
public class PlayerCamera : MonoBehaviour {

	// Use this for initialization

	public Transform targetTrans,thisTransform ;
 
	public Vector3 offset;
	public Vector3 flyModeOffset;
	public static PlayerCamera Static; 
	public float camChangeSpeedX,camPositionX,camPositionY,camChangeSpeedY,flyCamChangeY,camChangeSpeedZ,camPositionZ;
	 
	 
	public Animator camAnimator;

	int Finish = Animator.StringToHash ("Base Layer.Finish");

	public enum Cam
	{
		StartingCam,
		NormalCam,
		flyModeCam,
		none
	}
	public Cam currentCam;

	   void Start()
	   {
		Static = this;
		 
		 
		}
	public int camIndex ; 

 
	void FixedUpdate () {



		switch(currentCam)
		{
		case Cam.StartingCam:

 
		
			break;

		case Cam.NormalCam:
			camPositionX = Mathf.Lerp (camPositionX, targetTrans.position.x, camChangeSpeedX);
			camPositionY = Mathf.Lerp (camPositionY, targetTrans.position.y+offset.y, camChangeSpeedY);
			camPositionZ = Mathf.Lerp(camPositionZ,targetTrans.position.z+PlayerEnemyController.Static.CamZPosition,camChangeSpeedZ);//+PlayerEnemyController.Static.CamZPosition
		
			if (targetTrans != null) {
				thisTransform.position = new Vector3 (camPositionX, camPositionY,  camPositionZ);
			}

			break;
		case Cam.flyModeCam:
			camPositionX = Mathf.Lerp (camPositionX, targetTrans.position.x, camChangeSpeedX);
			camPositionY = Mathf.Lerp (camPositionY, targetTrans.position.y+offset.y, flyCamChangeY);
			camPositionZ = Mathf.Lerp(camPositionZ, targetTrans.position.z-flyModeOffset.z,camChangeSpeedZ);
			if (targetTrans != null) {
				thisTransform.position = new Vector3 (camPositionX, camPositionY+flyModeOffset.y,  targetTrans.position.z-flyModeOffset.z);
			}

			break;
		}
			
	}
 
}

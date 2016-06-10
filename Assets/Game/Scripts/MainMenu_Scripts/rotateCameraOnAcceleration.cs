using UnityEngine;
using System.Collections;

public class rotateCameraOnAcceleration : MonoBehaviour {

	// Use this for initialization
	Transform camTrans;
	void Start () {

		camTrans = transform;
	
	}
	
	// Update is called once per frame
	Quaternion targetRotation;
	float target ;
	void Update () {
		float reciver = Input.acceleration.x * 5;
#if UNITY_EDITOR
		reciver = Input.GetAxis("Vertical");
#endif
		target = Mathf.Lerp (target,reciver,Time.deltaTime*0.1f );
		transform.rotation =  Quaternion.Euler(-target,target*5,0);
	}
}

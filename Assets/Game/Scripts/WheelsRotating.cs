using UnityEngine;
using System.Collections;

public class WheelsRotating : MonoBehaviour {


	bool isVisible = false;
	
	void Start () {
	
	}
	void Update () {
		//if (isVisible) {
			transform.Rotate(new Vector3(0,0,500*Time.deltaTime));
			//	}
	
	
	}
	void OnBecameVisible(){

		isVisible = true;
		}

}

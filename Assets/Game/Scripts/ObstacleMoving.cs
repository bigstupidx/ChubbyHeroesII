using UnityEngine;
using System.Collections;

public class ObstacleMoving : MonoBehaviour {

	
	bool isVisible = false;
	
	void Start () {
		
	}
	void Update () {
				//if (isVisible) {
			transform.Translate (Vector3.forward * -10f * Time.deltaTime);
				
		//}
		
	}
	void OnBecameVisible(){
		isVisible = true;
	}
	 
}

using UnityEngine;
using System.Collections;

public class ObstacleMoving_Cart: MonoBehaviour {

	
	bool isVisible = false;
	
	void Start () {
		
	}
	void Update () {
		if (isVisible) {
		
		transform.Translate (Vector3.forward * -20 * Time.deltaTime);
			}
		
	}
	void OnBecameVisible(){
		isVisible = true;
		
	}
}

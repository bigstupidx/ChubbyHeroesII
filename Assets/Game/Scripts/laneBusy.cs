using UnityEngine;
using System.Collections;

public class laneBusy : MonoBehaviour {


	public bool IsBusy = false;
	void OnEnable()
	{
		GetComponent<Renderer>().enabled = false;
	}
	void Update()
	{

	}


	float lastTime;
	void OnTriggerEnter(Collider inc)
	{
		if (inc.GetComponent<Collider>().tag.Contains ("Obstacle")) {
			IsBusy = true;
		GetComponent<Renderer>().material.color = Color.red;
			lastTime = Time.timeSinceLevelLoad;
			Invoke("lateDeactive",0.8f);
		}
	}

	void OnTriggerExit(Collider inc)
	{
		if (inc.GetComponent<Collider>().tag.Contains ("Obstacle")&& Time.timeSinceLevelLoad-lastTime  > 1.0f) {
			//Debug.Log("here collider in normal white color");
		//	IsBusy = false;
			GetComponent<Renderer>().material.color = Color.white;
		}
		if (inc.GetComponent<Collider>().tag.Contains ("Obstacle")&& Time.timeSinceLevelLoad-lastTime  < 1.0f) {

		}

	}

	void lateDeactive()
	{
		GetComponent<Renderer>().material.color = Color.white;
		IsBusy = false;
	}
}

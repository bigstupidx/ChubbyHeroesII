using UnityEngine;
using System.Collections;

public class camTargetMovement : MonoBehaviour {

	public Vector3 targetPosition;
	Transform thisTrans;
	public float speed ;
	// Use this for initialization
	void Start () {
	

		thisTrans = transform;
	}
	
	// Update is called once per frame
	void Update () {


		thisTrans.localPosition = Vector3.MoveTowards( thisTrans.localPosition,targetPosition,speed*Time.deltaTime);

		if (this.transform.localPosition == targetPosition)
						this.enabled = false;

	
	}
}
